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
            return new TableDAL().GetAllTables();
        }

        public bool ChangeTableOrder(int newTableId, Order order)
        {
            bool result;
            result = tableDAL.ChangeTableOrder(newTableId, order);
            return result;
        }
    }
}