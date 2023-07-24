using Persistence;
using Spectre.Console;
using UI;
using BL;
namespace Ultilities;

public class Ults
{
    ConsoleUI UI = new ConsoleUI();
    StaffBL staffBL = new StaffBL();
    List<Product> lstproduct;
    string[] LoginMenu = { "Login", "Exit" };
    string[] MainMenu = { " Create Order", " Update Order", " Payment", " Check out" };

    public void Welcome()
    {
        
        do
        {
            int Loginchoice = UI.Menu(@"
                ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
                ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
                ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
                ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
                ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
                 ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
", LoginMenu);

            switch (Loginchoice)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

        }
        while (LoginChoice != LoginMenu.Length);
    }

    public void Login()
    {
        
        Staff orderStaff;
        UI.Title(@"
                ██╗      ██████╗  ██████╗ ██╗███╗   ██╗
                ██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
                ██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
                ██║     ██║   ██║██║   ██║██║██║╚██╗██║
                ███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
                ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝                                                       
");
        orderStaff = staffBL.Login();
        if (orderStaff != null)
        {
            UI.WelcomeStaff(orderStaff);
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
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
        else
        {
            Console.WriteLine("");
            UI.RedMessage("Invalid Username or Password");
            UI.PressAnyKeyToContinue();
        }

    }

    public void CreateOrder()
    {
        UI.Title(@"
                 ██████╗██████╗ ███████╗ █████╗ ████████╗███████╗     ██████╗ ██████╗ ██████╗ ███████╗██████╗ 
                ██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██╔════╝    ██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗
                ██║     ██████╔╝█████╗  ███████║   ██║   █████╗      ██║   ██║██████╔╝██║  ██║█████╗  ██████╔╝
                ██║     ██╔══██╗██╔══╝  ██╔══██║   ██║   ██╔══╝      ██║   ██║██╔══██╗██║  ██║██╔══╝  ██╔══██╗
                ╚██████╗██║  ██║███████╗██║  ██║   ██║   ███████╗    ╚██████╔╝██║  ██║██████╔╝███████╗██║  ██║
                 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝
                                                                                                              
");
        lstproduct = productBL.GetAll();
        UI.PrintProducts(lstproduct);
    }
}