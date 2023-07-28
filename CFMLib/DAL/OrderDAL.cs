using MySqlConnector;
using Persistence;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        
        public bool CreateOrder(Order order)
        {
            if (order == null || order.ProductsList == null || order.ProductsList.Count == 0)
            {
                return false;
            }
            bool result = false;
            try
            {
                using (MySqlTransaction trans = connection.BeginTransaction())
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();

                    MySqlDataReader? reader = null;
                    

                    //insert order
                    cmd.CommandText = "insert into Orders(order_staff_id, order_status) values (@StaffId, @orderStatus);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@staffId", order.OrderStaffID);
                    cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                    cmd.ExecuteNonQuery();
                    //get new Order_ID
                    cmd.CommandText = "select LAST_INSERT_ID() as order_id";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("order_id");
                    }
                    reader.Close();

                    //insert Order Details table
                    foreach (var item in order.ProductsList)
                    {
                        if (item.ProductId == 0 || item.ProductQuantity <= 0)
                        {
                            throw new Exception("Not Exists Product");
                        }
                        //get unit_price
                        cmd.CommandText = "select price from Products where product_id=@ProductId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@productId", item.ProductId);
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        item.ProductPrice = reader.GetDecimal("price");
                        reader.Close();

                        //insert to Order Details
                        cmd.CommandText = @"insert into OrderDetails(order_id, item_id, unit_price, quantity) values 
                            (" + order.OrderId + ", " + item.ProductId + ", " + item.ProductPrice + ", " + item.ProductQuantity + ");";
                        cmd.ExecuteNonQuery();

                        //update quantity in Items
                        cmd.CommandText = "update Product set quantity=quantity-@quantity where item_id=" + item.ProductId + ";";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@quantity", item.ProductQuantity);
                        cmd.ExecuteNonQuery();
                    }
                    //commit transaction
                    trans.Commit();
                    result = true;
                    trans.Rollback();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }
    }
}
