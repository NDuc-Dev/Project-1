using System.Data;
using MySqlConnector;
using Persistence;
namespace DAL
{
    public class SizeDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();

        internal Size GetProductSize(MySqlDataReader reader)
        {
            Size size = new Size();
            size.ProductId = reader.GetInt32("product_id");
            size.SizeID = reader.GetInt32("size_id");
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