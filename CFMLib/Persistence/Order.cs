using System;
using System.Collections.Generic;

namespace Persistence
{
    public static class OrderStatus
    {
        public const int ORDER_INPROGRESS = 1;
        public const int ORDER_COMPLETE = 2;
    }
    public class Order
    {

        public int OrderId { set; get; }
        public int OrderStaffID { get; set; }
        public DateTime OrderDate { set; get; }
        public decimal Amount { set; get; }
        public int OrderStatus { set; get; }
        public List<Product> ProductsList { set; get; } = new List<Product>();
        
    }
}