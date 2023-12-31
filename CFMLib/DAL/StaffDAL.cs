using MySqlConnector;
using Persistence;


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

        public Staff GetStaffById(int staffId)
        {
            Staff staff = new Staff();
            try
            {
                string query = @"select * from Staffs where Staff_Id = @staffId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@staffId", staffId);
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

        public Staff CheckAuthorize(string UserName, string Password)
        {
            Staff staff = new Staff();
            staff = GetStaffAccount(UserName);
            if (staff.Password ==Password && staff.StaffStatus == 1)
            {
                return staff;
            }
            else
            {
                return null;
            }
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

        public bool CheckNullableInLogindetails()
        {
            bool result;
            string query = "SELECT COUNT(*) FROM login_details";
            long rowCount = 0;
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                rowCount = (long)command.ExecuteScalar();
            }
            if (rowCount > 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public Staff GetLastStaffLogOut()
        {
            Staff staff = new Staff();
            try
            {
                string query = "SELECT *FROM login_details ORDER BY login_time DESC LIMIT 1;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    staff.StaffId = reader.GetInt32("Staff_Id");
                    staff.LogoutTime = reader.GetDateTime("Logout_Time");
                }
                else
                {
                    return null;
                }
                reader.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return staff;
        }

        public bool UpdateLogoutTime(string timeFormat, decimal total)
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
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write, login_details write;";
                        cmd.ExecuteNonQuery();
                        MySqlDataReader? reader = null;

                        cmd.CommandText = "select LAST_INSERT_ID() as login_Id";
                        int loginId = 0;
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            loginId = reader.GetInt32("login_Id");
                        }
                        reader.Close();
                        
                        cmd.CommandText = $"update login_details set logout_time = '{timeFormat}', total_amount_in_shop = @total where login_id = @loginId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@total",total);
                        cmd.Parameters.AddWithValue("@loginId", loginId);
                        // cmd.Parameters.AddWithValue("@logoutTime", time);
                        cmd.ExecuteNonQuery();

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
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }

        public bool InsertProblemLogin(string problem)
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
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write, login_details write;";
                        cmd.ExecuteNonQuery();

                        MySqlDataReader? reader = null;

                        cmd.CommandText = "select LAST_INSERT_ID() as login_Id";
                        int loginId = 0;
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            loginId = reader.GetInt32("login_Id");
                        }
                        reader.Close();

                        //insert order
                        cmd.CommandText = "update login_details set Descriptions = @Descriptions where login_id = @loginId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Descriptions", problem);
                        cmd.Parameters.AddWithValue("@loginId", loginId);
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
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return result;
        }
        
        public bool InsertNewLoginDetails(Staff staff)
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
                        cmd.CommandText = "lock tables Orders write, staffs write, product_sizes write, tables write, Order_Details write, login_details write;";
                        cmd.ExecuteNonQuery();

                        //insert order
                        cmd.CommandText = "insert into Login_details(staff_id, login_time) values (@staffId, @loginTime);";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@staffId", staff.StaffId);
                        cmd.Parameters.AddWithValue("@loginTime", staff.LoginTime);
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
                    finally
                    {
                        //unlock all tables;
                        cmd.CommandText = "unlock tables;";
                        cmd.ExecuteNonQuery();
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