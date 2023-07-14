namespace Model
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
        public int Quantity { set; get; }
        public string? Description { set; get; }

        public Product ()
        {
            ProductName = "no name";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Product)
            {
                return ((Product)obj).ProductId.Equals(ProductId);
            }
            return false;
        }

    }
}