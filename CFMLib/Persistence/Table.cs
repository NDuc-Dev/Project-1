namespace Persistence
{
    public static class TableStatus
    {
        public const int EMPTY_TABLE = 0;
        public const int TABLE_IS_USING = 1;
    }

    public class Table
    {
        public int TableId {set; get;}
        public int TableStatus {set; get;}
    }
}