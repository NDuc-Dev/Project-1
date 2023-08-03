using Persistence;
using DAL;

namespace BL
{
    public class TableBL
    {
        private TableDAL tableDal = new TableDAL();
        public Table GetTableById(int tableId)
        {
            return tableDal.GetTableById(tableId);
        }
        public List<Table> GetAll()
        {
            return new TableDAL().GetTables();
        }
    }
}