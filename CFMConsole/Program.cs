using BL;
using UI;
using Model;

public class Program
{
    static void Main()
    {
        Ultilities UI = new Ultilities();
        OrderBL oBL = new OrderBL();
        staffBL sBL = new staffBL();
        Staff? OrderStaff;
        string UserName;
        string[] LoginMenu = { " Login", " Exit" };
        string[] MainMenu = { " Create Order", " Update Order", " Payment", " Check out" };
        int LoginChoice = UI.Menu(@"
██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
 ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
                                                              
", LoginMenu);
        do
        {
            switch (LoginChoice)
            {
                case 1:
                    while (true)
                    {

                        UI.Title(@"
██╗      ██████╗  ██████╗ ██╗███╗   ██╗
██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
██║     ██║   ██║██║   ██║██║██║╚██╗██║
███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝
                                       
");
                        Console.Write("User Name: ");
                        UserName = Console.ReadLine();
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



                        OrderStaff = sBL.Login(UserName, PassWord);
                        if (OrderStaff != null)
                        {

                            int MainMenuChoice = UI.Menu(@"
 ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗ 
██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗
██║     █████╗  ██╔████╔██║    ███████║██████╔╝██████╔╝
██║     ██╔══╝  ██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝ 
╚██████╗██║     ██║ ╚═╝ ██║    ██║  ██║██║     ██║     
 ╚═════╝╚═╝     ╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝     
                                                       
", MainMenu);


                        }
                        else
                        {
                            Console.Write("\n");
                            Console.WriteLine("Invalid Username or Password");
                            Console.Write("Press Any Key to continue...");
                            Console.ReadKey();
                        }
                    }
                    break;
                case 2:
                    return;
            }

        } while (true);
    }
}
