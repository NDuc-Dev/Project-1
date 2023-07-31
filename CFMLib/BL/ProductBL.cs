using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class ProductBL
    {
        private ProductDAL productDal = new ProductDAL();
        public Product GetProductById(int productId)
        {
            return productDal.GetProductById(productId);
        }
        public List<Product> GetAll()
        {
            return new ProductDAL().GetProducts();
        } 
        public Product GetProductByIdAndSize (int productId, int sizeId)
        {
            return productDal.GetProductByIdAndSize(productId, sizeId);
        }
    }

}