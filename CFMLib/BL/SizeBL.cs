using DAL;
using Persistence;

namespace BL
{
    public class SizeBL
    {
        private SizeDAL sizeDAL = new SizeDAL();

        public Size GetSizeSByProductID(int productId)
        {
            return new SizeDAL().GetSizeSByProductID(productId);
        }

        public Size GetSizeMByProductID(int productId)
        {
            return new SizeDAL().GetSizeMByProductID(productId);
        }

        public Size GetSizeLByProductID(int productId)
        {
            return new SizeDAL().GetSizeLByProductID(productId);
        }

        public Size GetSizeByID(int sizeId)
        {
            return new SizeDAL().GetSizeBySizeId(sizeId);
        }
        
    }
}