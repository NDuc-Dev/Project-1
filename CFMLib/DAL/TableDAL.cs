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
                query = @"select * from tables where table_status = 0 and table_Id != 0 ;";
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

    }
}