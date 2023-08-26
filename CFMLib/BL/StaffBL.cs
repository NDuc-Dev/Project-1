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
        public string GetPassword()
        {
            string Password = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && Password.Length > 0)
                {
                    Console.Write("\b \b");
                    Password = Password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    Password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return Password;
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

        public bool UpdateLogoutTime(string time, decimal total)
        {
            return staffDAL.UpdateLogoutTime(time, total);
        }

        public bool InsertProblemLogin(string problem)
        {
            return staffDAL.InsertProblemLogin(problem);
        }
    }
}