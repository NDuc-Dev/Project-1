using System;

namespace Persistence
{
    public static class ProductStatus
    {
        public const int NOT_ACTIVE = 0;
        public const int ACTIVE = 1;
    }

    public class Product
    {
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public decimal ProductPrice { set; get; }
        public int ProductQuantity { set; get; }
        public string? ProductDescription { set; get; }
        public int ProductSizeId {set; get;}
        public char ProductSize { set; get; }
        public int ProductStatus { set; get; }
        public int StatusInOrder { set; get; }
        public Staff? CreateBy { set; get; }
        public Staff? UpdateBy { set; get; }
        public DateTime? CreateTime { set; get; }
        public DateTime? UpdateTime { set; get; }

        public Product()
        {
            ProductName = "";
        }

    }
}