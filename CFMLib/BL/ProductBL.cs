using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class ProductBL
    {
        private ProductDAL productDAL = new ProductDAL();
        public Product GetProductById(int productId)
        {
            return productDAL.GetProductById(productId);
        }

        public Product GetProductInstockById(int productId)
        {
            return productDAL.GetProductInstockById(productId);
        }
        public List<Product> GetAllProductActive()
        {
            return productDAL.GetAllProductActive();
        }
        public Product GetProductByIdAndSize(int productId, int sizeId)
        {
            return productDAL.GetProductByIdAndSize(productId, sizeId);
        }

        public List<Product> GetListProductsInOrder(int orderId)
        {
            return productDAL.GetListProductsInOrder(orderId);
        }

        public List<Product> GetListAllProductInStock()
        {
            return productDAL.GetListAllProductInstock();
        }

        public bool ChangeProductStatus(int newStatus, int productId)
        {
            return productDAL.ChangeProductStatus(newStatus, productId);
        }
    }

}