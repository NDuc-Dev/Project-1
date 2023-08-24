using MySqlConnector;
using Persistence;

namespace DAL
{
    public static class ProductFilter
    {
        public const int GET_ALL = 0;
    }
    public class ProductDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();

        public Product GetProductById(int productId)
        {
            Product product = new Product();
            try
            {
                query = @"select * from products where product_id=@productId and status = 1;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = GetProduct(reader);
                }
                reader.Close();
            }
            catch { }
            return product;
        }

        public Product GetProductInstockById(int productId)
        {
            Product product = new Product();
            try
            {
                query = @"select * from products where product_id=@productId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = GetProduct(reader);
                }
                reader.Close();
            }
            catch { }
            return product;
        }

        public Product GetProductByIdAndSize(int productId, int sizeId)
        {
            Product product = new Product();
            try
            {
                query = @"SELECT * FROM product_sizes ps
                inner JOIN products p on ps.product_id = p.product_id
                inner JOIN sizes s on ps.size_id = s.size_id
                where ps.product_id=@productId and ps.size_id=@sizeId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.Parameters.AddWithValue("@sizeId", sizeId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    product = GetAllProductInfo(reader);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return product;
        }

        internal Product GetAllProductInfo(MySqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32("product_id");
            product.ProductName = reader.GetString("product_name");
            product.ProductPrice = reader.GetDecimal("price");
            product.ProductSize = reader.GetChar("Product_Size");
            product.ProductSizeId = reader.GetInt32("size_id");
            return product;
        }

        internal Product GetProduct(MySqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32("product_id");
            product.ProductName = reader.GetString("product_name");
            product.ProductStatus = reader.GetInt32("status");
            return product;
        }

        internal Product GetProductInOrder(MySqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32("product_id");
            product.ProductSizeId = reader.GetInt32("size_id");
            product.ProductQuantity = reader.GetInt32("quantity");
            product.ProductPrice = reader.GetDecimal("amount");
            product.StatusInOrder = reader.GetInt32("status");
            return product;
        }

        public List<Product> GetAllProductActive()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select * from products where status = 1 ;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listProduct.Add(GetProduct(reader));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return listProduct;
        }

        public bool UpdateProductStatusInOrder(Product product, Order order)
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
                        cmd.CommandText = "update order_details set status = 2 where order_id =@orderId and product_id =@productId and size_id =@sizeId ;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@productId", product.ProductId);
                        cmd.Parameters.AddWithValue("@orderId", order.OrderId);
                        cmd.Parameters.AddWithValue("@sizeId", product.ProductSizeId);
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
                        catch (Exception ex)
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        public List<Product> GetListProductsInOrder(int orderId)
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                query = "select * from order_details where order_id = @orderId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listProduct.Add(GetProductInOrder(reader));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return listProduct;
        }

        public List<Product> GetListAllProductInstock()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                query = "select * from products;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listProduct.Add(GetProduct(reader));
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return listProduct;
        }

        public bool ChangeProductStatus(int newStatus, int productId)
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
                        cmd.CommandText = "update products set status = @newStatus where product_id =@productId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@newStatus", newStatus);
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
                        catch (Exception ex)
                        {
                            Console.WriteLine($"ERROR: {ex.Message}");
                        }
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