using System.Collections.Generic;
using Model;
using DAL;

namespace BL
{
    public class ProductBL
    {
        private ProductDAL pdal = new ProductDAL();
        public Product GetProductById(int productId)
        {
            return pdal.GetProductById(productId);
        }
        public List<Product> GetAll()
        {
            return pdal.GetProducts(ProductFilter.GET_ALL, new Product());
        }
        
    }
}