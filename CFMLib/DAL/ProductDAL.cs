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
                query = @"select product_id, product_name from products where product_id=@productId;";
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
            return product;
        }

        internal Product GetProductInOrder(MySqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32("product_id");
            product.ProductSizeId = reader.GetInt32("size_id");
            product.ProductQuantity = reader.GetInt32("quantity");
            product.ProductPrice =  reader.GetDecimal("amount");
            product.StatusInOrder = reader.GetInt32("status");
            return product;
        }

        public List<Product> GetProducts()
        {
            List<Product> listProduct = new List<Product>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select product_id, product_name from products ;";
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
    }
}