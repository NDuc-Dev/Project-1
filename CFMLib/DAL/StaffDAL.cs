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
                string query = @"select * from Staffs where User_Name = @User_Name";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@User_Name", UserName);
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
            staff.StaffId = reader.GetInt32("Staff_ID");
            staff.StaffName = reader.GetString("Staff_Name");
            staff.UserName = reader.GetString("User_Name");
            staff.Password = reader.GetString("Password");
            staff.StaffStatus = reader.GetInt32("Staff_Status");
            return staff;
        }
    }
}