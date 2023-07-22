using BL;
using UI;
using DAL;
using System.Text;
using Persistence;
using Spectre.Console;
public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Ultilities UI = new Ultilities();
        OrderBL orderBL = new OrderBL();
        staffBL staffBL = new staffBL();
        ProductBL productBL = new ProductBL();
        List<Product> lstproduct;
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
                        OrderStaff = staffBL.Login();
                        if (OrderStaff != null)
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Welcome " + OrderStaff.StaffName);
                            Thread.Sleep(900);
                            UI.ProgressAsync();
                            Thread.Sleep(1000);
                            Console.ForegroundColor = ConsoleColor.White;
                            int MainMenuChoice = UI.Menu(@"
                 ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗ 
                ██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗
                ██║     █████╗  ██╔████╔██║    ███████║██████╔╝██████╔╝
                ██║     ██╔══╝  ██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝ 
                ╚██████╗██║     ██║ ╚═╝ ██║    ██║  ██║██║     ██║     
                 ╚═════╝╚═╝     ╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝                          
", MainMenu);

                            switch (MainMenuChoice)
                            {
                                case 1:
                                    UI.Title(@"
                 ██████╗██████╗ ███████╗ █████╗ ████████╗     ██████╗ ██████╗ ██████╗ ███████╗██████╗ 
                ██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝    ██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗
                ██║     ██████╔╝█████╗  ███████║   ██║       ██║   ██║██████╔╝██║  ██║█████╗  ██████╔╝
                ██║     ██╔══██╗██╔══╝  ██╔══██║   ██║       ██║   ██║██╔══██╗██║  ██║██╔══╝  ██╔══██╗
                ╚██████╗██║  ██║███████╗██║  ██║   ██║       ╚██████╔╝██║  ██║██████╔╝███████╗██║  ██║
                 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝        ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝                                                                              
");
                                    
                                    lstproduct = productBL.GetAll();
                                    UI.PrintProducts(lstproduct);
                                    break;
                                case 2:
                                break;
                            }
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
