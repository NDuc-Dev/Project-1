using MySqlConnector;
using Persistence;

namespace DAL
{
    public class TableDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";

        public List<Table> GetTables()
        {
            List<Table> listTable = new List<Table>();
            try
            {
                MySqlCommand command = new MySqlCommand("", connection);
                query = @"select * from tables where table_Id != 0 ;";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listTable.Add(GetTable(reader));
                }
                reader.Close();
            }
            catch { }
            return listTable;
        }
        public Table GetTableById(int tableId)
        {
            Table table = new Table();
            try
            {
                query = @"select * from tables where table_id=@tableId and table_status = 0;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@tableId", tableId);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    table = GetTable(reader);
                }
                reader.Close();
            }
            catch { }
            return table;
        }

        internal Table GetTable(MySqlDataReader reader)
        {
            Table table = new Table();
            table.TableId = reader.GetInt32("table_id");
            table.TableStatus = reader.GetInt32("table_status");
            return table;
        }

        public bool ChangeTableOrder(int newtableId, Order order)
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
                        cmd.CommandText = "lock tables tables write;";
                        cmd.ExecuteNonQuery();

                        //change table id in current order to new table id
                        cmd.CommandText = "UPDATE coffeeshop.orders SET order_table = @newTableId WHERE order_id = @orderId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@newTableId", newtableId);
                        cmd.ExecuteNonQuery();

                        //change status old table to 0
                        cmd.CommandText = "UPDATE coffeeshop.tables SET table_status = 0 WHERE table_id = @tableId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tableId", order.TableID);
                        cmd.ExecuteNonQuery();

                        //change status new table to 1
                        cmd.CommandText = "UPDATE coffeeshop.tables SET table_status = 1 WHERE table_id = @tableId;";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@tableId",newtableId);
                        cmd.ExecuteNonQuery();

                        //commit transaction
                        trans.Commit();
                        result = true;
                        // trans.Rollback();
                    }
                    catch
                    {
                        try
                        {
                            trans.Rollback();
                        }
                        catch { }
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