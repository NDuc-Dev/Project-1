using System.Data;
using MySqlConnector;
using Persistence;
namespace DAL
{
    public class SizeDAL
    {
        private string query = "";
        private MySqlConnection connection = DbConfig.GetConnection();
        // public Size GetSizes (int productId)
        // {
        //     Size size = new Size();
        //     try
        //     {
        //         query = @"select p.product_id, s.Product_Size, ps.price, ps.quantity from products p 
        //                 inner join product_sizes ps on p.Product_ID = ps.Product_ID
        //                 inner join sizes s on ps.size_id = s.size_id where product_id=@productId;";
        //         MySqlCommand command = new MySqlCommand(query, connection);
        //         command.Parameters.AddWithValue("@productId", productId);
        //         MySqlDataReader reader = command.ExecuteReader();
        //         if (reader.Read())
        //         {
        //             size = GetSize(reader);
        //         }
        //         reader.Close();
        //     }
        //     catch { }
        //     return size;
        // }
        internal Size GetSize(MySqlDataReader reader)
        {
            Size size = new Size();
            size.SizeID = reader.GetInt32("size_id");
            size.SizeProduct = reader.GetChar("product_size");
            size.SizePrice = reader.GetDecimal("price");
            size.Quantity = reader.GetInt32("quantity");
            return size;
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
                    lst.Add(GetSize(reader));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lst;
        }
    }
}