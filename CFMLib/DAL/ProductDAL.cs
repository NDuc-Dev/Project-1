using MySqlConnector;
using Persistence;

namespace DAL
{
    public static class ProductFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_PRODUCT_NAME = 1;
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
                query = @"select p.product_id, p.product_name, s.Product_Size, ps.price, ps.quantity, p.descriptions from products p 
                        inner join product_sizes ps on p.Product_ID = ps.Product_ID
                        inner join sizes s on ps.size_id = s.size_id where product_id=@productId;";
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

        internal Product GetProduct(MySqlDataReader reader)
        {
            Product product = new Product();
            product.ProductId = reader.GetInt32("product_id");
            product.ProductName = reader.GetString("product_name");
            // product.ProductSize.SizeProduct = reader.GetChar("Product_Size");
            // product.ProductPrice = reader.GetDecimal("price");
            // product.ProductQuantity = reader.GetInt32("quantity");
            product.ProductDescription = reader.GetString("descriptions");
            return product;
        }

        public List<Product> GetProducts(Product product)
        {
            List<Product> lst = new List<Product>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select product_id, product_name, descriptions from products ;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                lst = new List<Product>();
                while (reader.Read())
                {
                    lst.Add(GetProduct(reader));
                }
                reader.Close();
            }
            catch { }
            return lst;
        }
    }
}