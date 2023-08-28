using Persistence;
using DAL;

namespace BL
{
    public class TableBL
    {
        private TableDAL tableDAL = new TableDAL();
        public Table GetTableById(int tableId)
        {
            return tableDAL.GetTableById(tableId);
        }
        
        public List<Table> GetAllTables()
        {
            return tableDAL.GetAllTables();
        }
    }
}