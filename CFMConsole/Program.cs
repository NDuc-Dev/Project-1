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
        string[] LoginMenu = { " Login", " Exit" };
        string[] MainMenu = { " Create Order", " Update Order", " Payment", " Check out" };
        UI.LogoVTCA();
        do
        {
            int LoginChoice = UI.Menu(@"
██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
 ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
                                                              
", LoginMenu);
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
                        OrderStaff = sBL.Login();
                        if (OrderStaff != null)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Welcome " + OrderStaff.StaffName);
                            Thread.Sleep(900);
                            Console.ForegroundColor = ConsoleColor.White;
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Username or Password");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Press Any Key to continue...");
                            Console.ReadKey();
                        }
                    }
                case 2:
                    return;
            }

        } while (true);
    }
}
