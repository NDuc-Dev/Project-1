using System.Data;
using MySqlConnector;
using Persistence;
namespace DAL
{
    public class SizeDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();

        internal Size GetSizes(MySqlDataReader reader)
        {
            Size size = new Size();
            size.ProductId = reader.GetInt32("product_id");
            size.SizeID = reader.GetInt32("size_id");
            size.SizeProduct = reader.GetChar("product_size");
            size.SizePrice = reader.GetDecimal("price");
            return size;
        }

        internal Size GetProductSize(MySqlDataReader reader)
        {
            Size size = new Size();
            size.ProductId = reader.GetInt32("product_id");
            size.SizeID = reader.GetInt32("size_id");
            // size.SizeProduct = reader.GetChar("product_size");
            size.SizePrice = reader.GetDecimal("price");
            return size;
        }

        internal Size GetSize(MySqlDataReader reader)
        {
            Size size = new Size();
            size.SizeID = reader.GetInt32("size_id");
            size.SizeProduct = reader.GetChar("product_size");
            return size;
        }

        public int GetProductSizeIDBySizeID(int sizeId, int productId)
        {
            int ProductSizeID = 0;
            try
            {
                query = @"select product_size_id from sizes product_sizes where product_id=@productId and size_id=@sizeId;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.Parameters.AddWithValue("@sizeId", sizeId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductSizeID = reader.GetInt32("Product_size_id");
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ProductSizeID;
        }

        public List<Size> GetListProductSizeByProductId(int productId)
        {
            List<Size> lst = new List<Size>();
            try
            {
                query = @"select s.*, ps.* from sizes s INNER JOIN product_sizes ps on s.size_id = ps.size_id where ps.product_id=@productId;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(GetSizes(reader));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lst;
        }

        public Size GetSizeSByProductID(int productId)
        {
            Size size = new Size();
            try
            {
                query = @"select * from product_sizes where product_id=@productId and size_id=1;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    size = GetProductSize(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }

        public Size GetSizeMByProductID(int productId)
        {
            Size size = new Size();
            try
            {
                query = @"select * from product_sizes where product_id=@productId and size_id=2;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    size = GetProductSize(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }

        public Size GetSizeLByProductID(int productId)
        {
            Size size = new Size();
            try
            {
                query = @"select * from product_sizes where product_id=@productId and size_id=3;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@productId", productId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    size = GetProductSize(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }

        public Size GetSizeBySizeId(int sizeId)
        {
            Size size = new Size();
            try
            {
                query = @"select * from sizes where size_id=@sizeId;";
                MySqlCommand command = new MySqlCommand("", connection);
                command.Parameters.AddWithValue("@sizeId", sizeId);
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    size = GetSize(reader);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return size;
        }
    }
}