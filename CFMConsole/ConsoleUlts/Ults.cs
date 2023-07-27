using Persistence;
using Spectre.Console;
using UI;
using BL;

namespace Ultilities;

public class Ults
{
    ConsoleUI UI = new ConsoleUI();
    StaffBL staffBL = new StaffBL();
    ProductBL productBL = new ProductBL();
    SizeBL sizeBL = new SizeBL();
    List<Product>? lstproduct;
    Staff? orderStaff;
    List<Persistence.Size> size;
    bool showAlert = false;
    string[] MainMenu = { "Create Order", "Update Order", "Payment", "Check Out", "About" };

    public void Login()
    {
        bool active = true;
        UI.Introduction();
        while (active = true)
        {
            string UserName;
            UI.Title(@"
                ██╗      ██████╗  ██████╗ ██╗███╗   ██╗               
                ██║     ██╔═══██╗██╔════╝ ██║████╗  ██║               
                ██║     ██║   ██║██║  ███╗██║██╔██╗ ██║               
                ██║     ██║   ██║██║   ██║██║██║╚██╗██║               
                ███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║               
                ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝               
                                                                 ");

            UI.GreenMessage("Input User name and password to LOGIN or input User Name = 0 to EXIT.");
            Console.Write("User Name: ");
            UserName = Console.ReadLine();
            if (UserName == "0")
            {
                active = false;
                break;
            }
            else
            {
                orderStaff = staffBL.Login(UserName);
            }

            if (orderStaff != null)
            {
                UI.WelcomeStaff(orderStaff);
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string MainMenuChoice = UI.Menu(@"
             ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗             
            ██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗            
            ██║     ███████╗██╔████╔██║    ███████║██████╔╝██████╔╝            
            ██║     ╚════██║██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝             
            ╚██████╗███████║██║ ╚═╝ ██║    ██║  ██║██║     ██║                 
             ╚═════╝╚══════╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝                 
            ----------------------------+--------------------------            
", MainMenu, orderStaff);

                    switch (MainMenuChoice)
                    {
                        case "Create Order":
                            CreateOrder();
                            break;
                        case "Update Order":
                            break;
                        case "Payment":
                            break;
                        case "Check Out":
                            break;
                        case "About":
                            About();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("");
                UI.RedMessage("Invalid Username or Password !");
            }
        }
    }

    public void CreateOrder()
    {
        int productId;
        do
        {
            Product product = new Product();
            lstproduct = productBL.GetAll();
            do
            {
                UI.Title(@"
 ██████╗██████╗ ███████╗ █████╗ ████████╗███████╗     ██████╗ ██████╗ ██████╗ ███████╗██████╗ 
██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██╔════╝    ██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗
██║     ██████╔╝█████╗  ███████║   ██║   █████╗      ██║   ██║██████╔╝██║  ██║█████╗  ██████╔╝
██║     ██╔══██╗██╔══╝  ██╔══██║   ██║   ██╔══╝      ██║   ██║██╔══██╗██║  ██║██╔══╝  ██╔══██╗
╚██████╗██║  ██║███████╗██║  ██║   ██║   ███████╗    ╚██████╔╝██║  ██║██████╔╝███████╗██║  ██║
 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝
");
                UI.CurrentStaff(orderStaff);
                UI.PrintProducts(lstproduct);
                Console.Write("Choose id to add product to order: ");
                int.TryParse(Console.ReadLine(), out productId);
                if (productId < 0 || productId > lstproduct.Count()) showAlert = true;
                else showAlert = false;
                if (showAlert)
                    UI.RedMessage("Invalid input, please re-enter");
            } while (productId < 0 || productId > lstproduct.Count());
            product = productBL.GetProductById(productId);
            UI.PrintProductInfo(product);
        } while (productId < 0 || productId > lstproduct.Count());

        // size = sizeBL.GetListProductSizeByProductID(productId);
        // Console.WriteLine(size.Count());
        // // UI.PrintSizes(size);


    }

    public void About()
    {
        Console.Clear();
        UI.Title(@"
         █████╗ ██████╗  ██████╗ ██╗   ██╗████████╗        
        ██╔══██╗██╔══██╗██╔═══██╗██║   ██║╚══██╔══╝        
        ███████║██████╔╝██║   ██║██║   ██║   ██║           
        ██╔══██║██╔══██╗██║   ██║██║   ██║   ██║           
        ██║  ██║██████╔╝╚██████╔╝╚██████╔╝   ██║           
        ╚═╝  ╚═╝╚═════╝  ╚═════╝  ╚═════╝    ╚═╝           
");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Coffee Shop Management Application");
        Console.WriteLine("Version: Beta_0.0.1");
        Console.WriteLine("Made By : Nguyen Ngoc Duc, Nguyen Thi Khanh Ly");
        Console.WriteLine("Instructor: Nguyen Xuan Sinh");
        UI.PressAnyKeyToContinue();
    }
}