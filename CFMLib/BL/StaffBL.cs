using Persistence;
using DAL;

namespace BL
{
    public class StaffBL
    {
        StaffDAL staffDAL = new StaffDAL();
        public Staff? Login()
        {
            Console.Write("User Name: ");
            string UserName = Console.ReadLine() ?? "";
            Console.Write("Pass Word: ");
            string PassWord = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && PassWord.Length > 0)
                {
                    Console.Write("\b \b");
                    PassWord = PassWord[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    PassWord += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            
            Staff staff = new Staff();
            staff = staffDAL.GetStaffAccount(UserName);
            if (staff.Password == PassWord && staff.StaffStatus == 1)
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