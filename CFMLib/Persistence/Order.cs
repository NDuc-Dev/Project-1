using System;
using System.Collections.Generic;

namespace Persistence
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
        public const int ORDER_INPROGRESS = 2;
        public const int ORDER_COMPLETE = 3;
    }
    public class Order
    {

        public int OrderId { set; get; }
        public Staff? OrderStaffID { get; set; }
        public DateTime OrderDate { set; get; }
        public int Status { set; get; }
        public decimal Amount { set; get; }
        public int OrderStatus { set; get; }
        public List<Product> ProductsList { set; get; }


        
    }
}