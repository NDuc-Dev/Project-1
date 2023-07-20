using MySqlConnector;
using Persistence;
namespace DAL
{
    public class SizeDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();
        public Size GetProductSizeByProductId(int productId)
        {
            Size size = new Size();
            try
            {
                query = @"select p.product_id, s.Product_Size, ps.price, ps.quantity from products p 
                        inner join product_sizes ps on p.Product_ID = ps.Product_ID
                        inner join sizes s on ps.size_id = s.size_id where product_id=@productId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    size = GetSize(reader);
                }
                reader.Close();
            }
            catch { }
            return size;
        }
        internal Size GetSize(MySqlDataReader reader)
        {
            Size size = new Size();
            size.SizeID = reader.GetInt32("size_id");
            size.SizeProduct = reader.GetChar("product_size");
            return size;
        }

        public List<Size> GetSizes(Size size)
        {
            List<Size> lst = new List<Size>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select p.product_id, p.product_name, s.Product_Size, ps.price, ps.quantity, p.descriptions from products p 
                        inner join product_sizes ps on p.Product_ID = ps.Product_ID
                        inner join sizes s on ps.size_id = s.size_id;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                lst = new List<Size>();
                reader.Close();
            }
            catch { }
            return lst;
        }
    }
}