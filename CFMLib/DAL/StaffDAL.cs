using MySqlConnector;
using Model;

namespace DAL
{
    public class StaffDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";
        public Staff GetStaffAccount(string UserName)
        {
            Staff staff = new Staff();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                
            }
                string query = @"select * from Staff where UserName = @UserName";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserName", UserName);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    staff = GetStaff(reader);
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return staff;
        }

        public Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.StaffId = reader.GetInt32("StaffID");
            staff.StaffName = reader.GetString("StaffName");
            staff.UserName = reader.GetString("UserName");
            staff.Password = reader.GetString("Password");
            return staff;
        }
    }
}