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
                    string MainMenuChoice = UI.NewMenu(@"
                 ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗                     
                ██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗                    
                ██║     █████╗  ██╔████╔██║    ███████║██████╔╝██████╔╝                    
                ██║     ██╔══╝  ██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝                     
                ╚██████╗██║     ██║ ╚═╝ ██║    ██║  ██║██║     ██║                         
                 ╚═════╝╚═╝     ╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝                         
", MainMenu);

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
                            UI.About();
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
        UI.Title(@"
 ██████╗██████╗ ███████╗ █████╗ ████████╗███████╗     ██████╗ ██████╗ ██████╗ ███████╗██████╗ 
██╔════╝██╔══██╗██╔════╝██╔══██╗╚══██╔══╝██╔════╝    ██╔═══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗
██║     ██████╔╝█████╗  ███████║   ██║   █████╗      ██║   ██║██████╔╝██║  ██║█████╗  ██████╔╝
██║     ██╔══██╗██╔══╝  ██╔══██║   ██║   ██╔══╝      ██║   ██║██╔══██╗██║  ██║██╔══╝  ██╔══██╗
╚██████╗██║  ██║███████╗██║  ██║   ██║   ███████╗    ╚██████╔╝██║  ██║██████╔╝███████╗██║  ██║
 ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝   ╚═╝   ╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝
");
        do
        {
            Product product = new Product();
            lstproduct = productBL.GetAll();
            foreach (var item in lstproduct)
            {
                
            }
            do
            {
                UI.PrintProducts(lstproduct);
                Console.Write("Choose id to add product to order: ");
                int.TryParse(Console.ReadLine(), out productId);
                if (productId <= 0 || productId > lstproduct.Count()) showAlert = true;
                else showAlert = false;
                if (showAlert)
                    UI.RedMessage("Invalid input, please re-enter");
            } while (productId <= 0 || productId > lstproduct.Count());
            product = productBL.GetProductById(productId);
            UI.PrintProductInfo(product);
            size = sizeBL.GetListProductSizeByProductID(productId);
            UI.PrintSizes(size);
            Console.WriteLine("Choose Product Size:");
            Console.ReadKey();

        } while (productId < 0 || productId > lstproduct.Count());

        // size = sizeBL.GetListProductSizeByProductID(productId);
        // Console.WriteLine(size.Count());
        // // UI.PrintSizes(size);


    }
}