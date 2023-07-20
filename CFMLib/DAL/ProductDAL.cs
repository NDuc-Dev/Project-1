using MySqlConnector;
using Model;

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
                query = @"select product_id, product_name, unit_price, quantity, product_status,
                        ifnull(product_description, '') as product_description
                        from Product where product_id=@productId;";
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
            product.ProductPrice = reader.GetDecimal("unit_price");
            product.ProductQuantity= reader.GetInt32("quantity");
            product.ProductDescription = reader.GetString("product_description");
            return product;
        }

         public List<Product> GetProducts(int productFilter, Product product)
        {
            List<Product> lst = new List<Product>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                switch (productFilter)
                {
                    case ProductFilter.GET_ALL:
                        query = @"select product_id, product_name, unit_price, quantity, product_status, ifnull(product_description, '') as product_description from Products";
                        break;
                    case ProductFilter.FILTER_BY_PRODUCT_NAME:
                        query = @"select product_id, product_name, unit_price, quantity, product_status, ifnull(product_description, '') as product_description from Products
                                where product_name like concat('%',@productName,'%');";
                        command.Parameters.AddWithValue("@productName", product.ProductName);
                        break;
                }
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