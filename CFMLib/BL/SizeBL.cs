using DAL;
using Persistence;

namespace BL
{
    public class SizeBL
    {
        private SizeDAL sizeDAL = new SizeDAL();
        public List<Size> GetListProductSizeByProductID(int productId)
        {
            return new SizeDAL().GetListProductSizeByProductId(productId);
        }

    }
}