using MySqlConnector;
using Persistence;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public bool CreateOrder(Order order)
        {
            if (order == null || order.ProductsList == null || order.ProductsList.Count() == 0)
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
                    cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, Order_Details write;";
                    cmd.ExecuteNonQuery();

                    MySqlDataReader? reader = null;
                    

                    //insert order
                    cmd.CommandText = "insert into Orders(order_staff_id, order_status) values (@staffId, @orderStatus);";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@staffId", order.OrderStaffID);
                    cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.ORDER_INPROGRESS);
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
                        cmd.CommandText = "select price from product_sizes where product_id=@productId and size_id=@sizeId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@productId", item.ProductId);
                        cmd.Parameters.AddWithValue("@sizeId", item.ProductSizeId);
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        item.ProductPrice = reader.GetDecimal("price");
                        reader.Close();

                        //insert to Order Details
                        cmd.CommandText = @"insert into Order_Details(order_id, product_id, size_id, quantity, amount) values 
                            (" + order.OrderId + ", " + item.ProductId + ", "+ item.ProductSizeId+ ", " + item.ProductQuantity + ","+ (item.ProductQuantity*item.ProductPrice)+ ");";
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
                // Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }
    }
}
