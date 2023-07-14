using Model;
using DAL;

namespace BL
{
    public class staffBL
    {
        StaffDAL uDAL = new StaffDAL();
        public Staff? Login(string userName, string password)
        {
            Staff staff = new Staff();
            staff = uDAL.GetStaffAccount(userName);
            if (staff.Password == password)
            {
                return staff;
            }
            else
            {
                return null;
            }
        }
    }
}