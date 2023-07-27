using Persistence;
using DAL;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();
        public Staff? Login(string UserName)
        {
            Console.Write("Password: ");
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
            
            Staff staff = new Staff();
            staff = staffDAL.GetStaffAccount(UserName);
            if (staff.Password == staffDAL.ChangePasswordMD5(Password) && staff.StaffStatus == 1)
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