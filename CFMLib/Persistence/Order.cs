namespace Persistence
{
    public class Order
    {

        public int OrderId { set; get; }
        public int OrderStaffID { get; set; }
        public DateTime OrderDate { set; get; }
        public decimal Amount { set; get; }
        public int OrderStatus { set; get; }
        public int TableID {set; get; }
        public List<Product> ProductsList { set; get; } = new List<Product>();
        
    }
}