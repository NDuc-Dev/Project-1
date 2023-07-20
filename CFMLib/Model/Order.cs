using System;
using System.Collections.Generic;

namespace Model
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
        public Staff OrderStaffID { get; set; }
        public DateTime OrderDate { set; get; }
        public int Status { set; get; }
        public decimal Amount { set; get; }
        public int OrderStatus { set; get; }
        public List<Product> ProductsList { set; get; }

        public Product? this[int index]
        {
            get
            {
                if (ProductsList == null || ProductsList.Count == 0 || index < 0 || ProductsList.Count < index) return null;
                return ProductsList[index];
            }
            set
            {
                if (ProductsList == null) ProductsList = new List<Product>();
                if (value == null) return;
                ProductsList.Add(value);
            }
        }

        public Order()
        {
            ProductsList = new List<Product>();
            OrderId = 0;
            Status = 0;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Order)
            {
                return ((Order)obj).OrderId.Equals(OrderId);
            }
            return false;
        }
    }
}