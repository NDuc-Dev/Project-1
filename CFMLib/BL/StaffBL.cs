using Persistence;
using DAL;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();
        public Staff GetStaffById(int staffId)
        {
            return new StaffDAL().GetStaffById(staffId);
        }

        public Staff CheckAuthorize(string userName, string Password)
        {
            return staffDAL.CheckAuthorize(userName,Password);
        }

        public Staff? GetLastStaffLogOut()
        {
            return staffDAL.GetLastStaffLogOut();
        }

        public bool CheckNullableInLogindetails()
        {
            return staffDAL.CheckNullableInLogindetails();
        }

        public bool InsertNewLoginDetails(Staff staff)
        {
            return staffDAL.InsertNewLoginDetails(staff);
        }

        public bool UpdateLogoutTime(string timeFormat, decimal total)
        {
            return staffDAL.UpdateLogoutTime(timeFormat, total);
        }

        public bool InsertProblemLogin(string problem)
        {
            return staffDAL.InsertProblemLogin(problem);
        }
    }
}