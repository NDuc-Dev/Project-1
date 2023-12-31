using MySqlConnector;
using Persistence;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";

        public bool SaveOrder(Order order)
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
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write;";
                        cmd.ExecuteNonQuery();

                        MySqlDataReader? reader = null;


                        //insert order
                        cmd.CommandText = "insert into Orders(order_staff_id, order_status, order_table) values (@staffId, @orderStatus, @orderTable);";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@staffId", order.OrderStaffID);
                        cmd.Parameters.AddWithValue("@orderStatus", order.OrderStatus);
                        cmd.Parameters.AddWithValue("@orderTable", order.TableID);
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
                            (" + order.OrderId + ", " + item.ProductId + ", " + item.ProductSizeId + ", " + item.ProductQuantity + "," + (item.ProductQuantity * item.ProductPrice) + ");";
                            cmd.ExecuteNonQuery();

                            // update table status
                            if (order.TableID != 0)
                            {
                                cmd.CommandText = @"update Tables set table_status = 1 where table_id =" + order.TableID + ";";
                                cmd.Parameters.Clear();
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                trans.Commit();
                                result = true;
                            }

                        }
                        //commit transaction
                        trans.Commit();
                        result = true;
                        // trans.Rollback();
                    }
                    catch
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        public bool UpdateOrder(Order order)
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
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write;";
                        cmd.ExecuteNonQuery();

                        MySqlDataReader? reader = null;

                        //delete old order
                        cmd.CommandText = "Delete from Order_Details where order_Id = @orderId ;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update orders set order_status = @orderStatus where order_id=@orderId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@orderStatus", order.OrderStatus);
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();

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
                            cmd.CommandText = @"insert into Order_Details(order_id, product_id, size_id, quantity, amount, status) values 
                            (" + order.OrderId + ", " + item.ProductId + ", " + item.ProductSizeId + ", " + item.ProductQuantity + "," + (item.ProductQuantity * item.ProductPrice) + "," + item.StatusInOrder + ");";
                            cmd.ExecuteNonQuery();
                        }
                        //commit transaction
                        trans.Commit();
                        result = true;
                        // trans.Rollback();
                    }
                    catch (Exception ex)
                    {

                        try
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                            trans.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        public bool DeleteOrder(Order order)
        {
            bool result = false;
            try
            {
                using (MySqlTransaction trans = connection.BeginTransaction())
                using (MySqlCommand cmd = connection.CreateCommand())
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write;";
                        cmd.ExecuteNonQuery();

                        //delete order
                        cmd.CommandText = "Delete from Order_Details where order_Id = @orderId ;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Delete from Orders where order_Id = @orderId ;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();

                        if (order.TableID != 0)
                        {
                            cmd.CommandText = "update tables set table_status = 0 where table_id = @tableId ;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@tableId", order.TableID);
                            cmd.ExecuteNonQuery();

                            trans.Commit();
                            result = true;
                        }
                        else
                        {
                            trans.Commit();
                            result = true;
                        }


                        //commit transaction
                        // trans.Rollback();
                    }
                    catch (Exception ex)
                    {

                        try
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                            trans.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        internal Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderId = reader.GetInt32("order_id");
            order.OrderStaffID = reader.GetInt32("Order_Staff_ID");
            order.OrderDate = reader.GetDateTime("Order_Date");
            order.OrderStatus = reader.GetInt32("Order_Status");
            order.TableID = reader.GetInt32("Order_Table");
            return order;
        }

        public List<Order> GetOrdersInBar()
        {
            List<Order> listOrder = new List<Order>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select * from orders where order_status = 1 and order_table != 0 or order_status = 2 and order_table != 0;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOrder.Add(GetOrder(reader));
                }
                reader.Close();
            }
            catch { }
            return listOrder;
        }

        public List<Order> GetTakeAwayOrders()
        {
            List<Order> listOrder = new List<Order>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select * from orders where order_status = 1 and order_table = 0 or order_status = 2 and order_table = 0;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOrder.Add(GetOrder(reader));
                }
                reader.Close();
            }
            catch { }
            return listOrder;
        }

        public bool CompleteOrder(Order order)
        {
            bool result = false;
            try
            {
                using (MySqlTransaction trans = connection.BeginTransaction())
                using (MySqlCommand cmd = connection.CreateCommand())
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write;";
                        cmd.ExecuteNonQuery();

                        //set order status = 3
                        cmd.CommandText = "update Orders set order_status = 3 where order_id = @orderId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update tables set table_status = 0 where table_id = @tableId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tableId", order.TableID);
                        cmd.ExecuteNonQuery();

                        //commit transaction
                        trans.Commit();
                        result = true;
                    }
                    catch
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = new Order();
            try
            {
                query = @"select * from orders where order_id=@orderId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order = GetOrder(reader);
                }
                reader.Close();
            }
            catch { }
            return order;
        }

        public Order GetOrderByTable(int tableId)
        {
            Order order = new Order();
            try
            {
                query = @"select * from orders where order_table = @tableId and order_status != 3;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@tableId", tableId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order = GetOrder(reader);
                }
                reader.Close();
            }
            catch { }
            return order;
        }


        public List<Order> GetOrdersCompleteInDay()
        {
            List<Order> listOrder = new List<Order>();
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            string DateFormatted = date.ToString("yyyy/MM/dd");
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @$"SELECT * FROM orders WHERE DATE(order_date) = '{DateFormatted}' and order_status = '3';";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOrder.Add(GetOrder(reader));
                }
                reader.Close();
            }
            catch { }
            return listOrder;
        }
    
            public bool ChangeTableOrder(int newTableId, Order order)
        {
            bool result = false;
            try
            {
                using (MySqlTransaction trans = connection.BeginTransaction())
                using (MySqlCommand cmd = connection.CreateCommand())
                    try
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = trans;
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, sizes write, Order_Details write;";
                        cmd.ExecuteNonQuery();

                        //change status old table to 0
                        cmd.CommandText = "UPDATE tables SET table_status = 0 WHERE table_id = @tableId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tableId", order.TableID);
                        cmd.ExecuteNonQuery();

                        //change table id in current order to new table id
                        cmd.CommandText = "UPDATE orders SET order_table = @newTableId WHERE order_id = @orderId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@newTableId", newTableId);
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.ExecuteNonQuery();


                        //change status new table to 1
                        cmd.CommandText = "UPDATE tables SET table_status = 1 WHERE table_id = @tableId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tableId", newTableId);
                        cmd.ExecuteNonQuery();

                        //commit transaction
                        trans.Commit();
                        result = true;
                    }
                    catch
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                        }
                    }
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"ERR: {ex.Message}");
            }
            return result;
        }
    }
}
