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
    OrderBL orderBL = new OrderBL();
    List<Product>? lstproduct;
    Staff? orderStaff;
    string[] MainMenu = { "Create Order", "Update Order", "Update Product", "Payment", "Check Out", "About" };

    public void Login()
    {
        bool active = true;
        UI.Introduction();
        while (active = true)
        {
            string UserName;
            UI.ApplicationLogoBeforeLogin();
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
                    UI.ApplicationLogoAfterLogin(orderStaff);
                    string MainMenuChoice = UI.Menu("MAIN MENU", MainMenu);

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
        Product? product = null;
        lstproduct = productBL.GetAll();
        Order orders = new Order();
        while (active)
        {
            if (active == false)
            {
                break;
            }
            bool continuee = false;
            do
            {
                if (active == false)
                {
                    break;
                }
                product = new Product();
                product = GetProduct(lstproduct, orderStaff, "Create Order");
                if (product != null)
                {
                    orders.OrderStaffID = orderStaff.StaffId;
                    orders.ProductsList.Add(product);
                    int quantity = UI.InputQuantity(product, orderStaff, "Create Order");
                    orders.ProductsList[orders.ProductsList.Count() - 1].ProductQuantity = quantity;
                    string addAsk = UI.AskToContinueAdd();
                    switch (addAsk)
                    {
                        case "Yes":
                            continuee = true;
                            break;
                        case "No":
                            continuee = false;
                            UI.PrintOrderDetails(orders.ProductsList, orderStaff, "Create Order");
                            string createAsk = UI.AskToContinueCreate();
                            switch (createAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Create Order: " + (orderBL.SaveOrder(orders) ? "completed!" : "not complete!"));
                                    UI.PressAnyKeyToContinue();
                                    break;
                                case "No":
                                    AnsiConsole.Markup("[Green]Canceling order successfully.[/]");
                                    UI.PressAnyKeyToContinue();
                                    active = false;
                                    break;
                            }
                            break;
                    }
                }
                else
                {
                    active = false;
                    break;
                }
            } while (continuee == false);
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

    public Product GetProduct(List<Product> lstproduct, Staff orderStaff, string title)
    {
        bool active = true;
        Product product = new Product();
        do
        {
            UI.PrintProductsTable(lstproduct, orderStaff);
            Console.Write("Product ID: ");
            int productId;
            do
            {


                if (int.TryParse(Console.ReadLine(), out productId))
                {
                    if (productId == 0)
                    {
                        return product = null;
                    }
                    else
                    {
                        if (productBL.GetProductById(productId) != null)
                        {
                            int sizeId = UI.ChooseProductsize(orderStaff, productId, "Create Order");
                            product = productBL.GetProductByIdAndSize(productId, sizeId);
                            return product;
                        }
                        else
                        {
                            active = true;
                            UI.RedMessage("Product not exist !");
                        }
                    }
                }
                else
                {
                    UI.RedMessage("Invalid ID !");
                }
            }
            while (productBL.GetProductById(productId) != null);
        }
        while (active);
        return product;
    }
}