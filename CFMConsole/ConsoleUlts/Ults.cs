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
    string[] MainMenu = { "Create Order", "Update Order", "Update Product", "Payment", "Check Out", "About" };

    public void Login()
    {
        bool active = true;
        UI.Introduction();
        while (active = true)
        {
            string UserName;
            UI.ApplicationLogo();
            UI.Title("LOGIN");
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
                Console.Write("Password: ");
                orderStaff = staffBL.GetPasswordAndCheckAuthorize(UserName);
            }

            if (orderStaff != null)
            {
                UI.WelcomeStaff(orderStaff);
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    UI.ApplicationLogo();
                    string MainMenuChoice = UI.Menu("MAIN MENU", MainMenu, orderStaff);

                    switch (MainMenuChoice)
                    {
                        case "Create Order":
                            CreateOrder();
                            break;
                        case "Update Order":
                            break;
                        case "Payment":
                            break;
                        case "Update Product":
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
                UI.RedMessage("Invalid Username or Password ! Please re-enter.");
            }
        }
    }

    public void CreateOrder()
    {
        int productId;
        bool active = true;
        Product product = new Product();
        lstproduct = productBL.GetAll();
        while (active)
        {
            do
            {
                
                UI.PrintProductsTable(lstproduct, orderStaff);
                int.TryParse(Console.ReadLine(), out productId);
                if (productId < 0 || productId > lstproduct.Count())
                {
                    showAlert = true;
                }
                else if (productId == 0)
                {
                    active = false;
                    break;
                }
                else
                {
                    showAlert = false;
                    product = productBL.GetProductById(productId);
                    int sizeChoice = UI.ChooseProductsize(product);
                    Console.WriteLine(sizeChoice);
                    Console.WriteLine(product.ProductId);
                    Console.ReadKey();
                }

                if (showAlert)
                {
                    UI.RedMessage("Invalid input, please re-enter");
                }
            } while (productId < 0 || productId > lstproduct.Count());
        }

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