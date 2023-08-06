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
    Staff orderStaff;
    string[] MainMenu = { "Create Order", "Update Order", "Update Product", "Payment", "Check Out", "About" };

    public void Login()
    {
        bool active = true;
        UI.Introduction();
        while (active)
        {
            string UserName;
            UI.ApplicationLogoBeforeLogin();
            UI.Title("LOGIN");
            UI.GreenMessage("Input User name and password to LOGIN or input User Name = 0 to EXIT.");
            Console.Write("User Name: ");
            UserName = Console.ReadLine() ?? "";
            if (UserName == "0")
            {
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
                            UpdateOrder();
                            break;
                        case "Payment":
                            break;
                        case "Update Product":
                            break;
                        case "Check Out":
                            break;
                        case "About":
                            UI.About(orderStaff);
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
        bool active = true;
        Product product;
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
                bool checkDup = true;
                if (orders.ProductsList.Count() == 0)
                {
                    orders.TableID = UI.ChooseTable(orderStaff, "CREATE ORDER");
                }
                product = GetProductToAddToOrder(lstproduct, orderStaff, "CREATE ORDER");
                if (product != null)
                {
                    orders.OrderStaffID = orderStaff.StaffId;

                    foreach (Product item in orders.ProductsList)
                    {
                        if (item.ProductId == product.ProductId && item.ProductSizeId == product.ProductSizeId)
                        {
                            item.ProductQuantity += product.ProductQuantity;
                            checkDup = false;
                        }
                    }
                    if (checkDup == true)
                    {
                        orders.ProductsList.Add(product);
                    }
                    string addAsk = UI.AskToContinueAdd();
                    switch (addAsk)
                    {
                        case "Yes":
                            continuee = true;
                            break;
                        case "No":
                            UI.PrintSaleReceipt(orders, orderStaff, "Create Order");
                            continuee = false;
                            string createAsk = UI.AskToContinueCreate();
                            switch (createAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Create Order: " + (orderBL.SaveOrder(orders) ? "completed!" : "not complete!"));
                                    Console.WriteLine("Your Order Id is : " + orders.OrderId);
                                    UI.PressAnyKeyToContinue();
                                    break;
                                case "No":
                                    AnsiConsole.Markup("[Green]Canceling order successfully.[/]\n");
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



    public void UpdateOrder()
    {
        string title = "UPDATE ORDER";
        List<Persistence.Order> listOrder = orderBL.GetOrdersInprogress();
        // List<Persistence.Product> listProducts;
        List<Persistence.Product> listAllProducts = productBL.GetAll();
        Persistence.Product product;
        Persistence.Order order;
        bool checkDup = true;
        bool active = true;
        bool continuee = false;
        while (active)
        {
            if (active == false)
            {
                break;
            }
            if (listOrder.Count() == 0)
            {
                UI.ApplicationLogoAfterLogin(orderStaff);
                UI.Title(title);
                UI.RedMessage("No orders have been created yet !");
                break;
            }
            else
            {
                order = GetOrderToViewDetails(listOrder, orderStaff, title);
                Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                if (order != null)
                {
                    string updateChoice;
                    updateChoice = UI.PrintOrderDetails(order.ProductsList, orderStaff, order, title, staff.StaffName);
                    switch (updateChoice)
                    {
                        case "Add product to order":
                            while (active)
                            {
                                if (active == false)
                                {
                                    break;
                                }
                                do
                                {
                                    if (active == false)
                                    {
                                        break;
                                    }
                                    product = GetProductToAddToOrder(listAllProducts, orderStaff, "UPDATE ORDER");
                                    if (product != null)
                                    {
                                        foreach (Product item in order.ProductsList)
                                        {
                                            if (item.ProductId == product.ProductId && item.ProductSizeId == product.ProductSizeId)
                                            {
                                                item.ProductQuantity += product.ProductQuantity;
                                                checkDup = false;
                                            }
                                        }
                                        if (checkDup == true)
                                        {
                                            order.ProductsList.Add(product);
                                        }
                                        string addAsk = UI.AskToContinueAdd();
                                        switch (addAsk)
                                        {
                                            case "Yes":
                                                continuee = true;
                                                break;
                                            case "No":
                                                UI.PrintSaleReceipt(order, orderStaff, "UPDATE ORDER");
                                                continuee = false;
                                                string updateAsk = UI.AskToContinueUpdate();
                                                switch (updateAsk)
                                                {
                                                    case "Yes":
                                                        // Console.WriteLine("Create Order: " + (orderBL.SaveOrder(orders) ? "completed!" : "not complete!"));
                                                        // Console.WriteLine("Your Order Id is : " + orders.OrderId);
                                                        UI.PressAnyKeyToContinue();
                                                        break;
                                                    case "No":
                                                        AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
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
                                }
                                while (continuee == false);
                            }
                            while (continuee == false) ;
                            break;
                        case "Remove an unfinished product from the order":
                            break;
                        case "Confirm product in order":
                            break;
                        case "Confirm order":
                            break;
                    }
                }
                Console.ReadKey();
            }
        }
    }

    public Product GetProductToAddToOrder(List<Product> lstproduct, Staff orderStaff, string title)
    {
        bool active = true;
        Product product = new Product();
        bool err = false;
        while (active)
        {
            UI.PrintProductsTable(lstproduct, orderStaff, title);
            do
            {
                AnsiConsole.Markup("Product ID: ");
                int productId;
                if (int.TryParse(Console.ReadLine(), out productId) && productId >= 0)
                {
                    if (productId == 0)
                    {
                        return null;
                    }
                    else
                    {
                        if (productBL.GetProductById(productId).ProductId != 0)
                        {
                            int sizeId = UI.ChooseProductsize(orderStaff, productId, "Create Order");
                            product = productBL.GetProductByIdAndSize(productId, sizeId);
                            product.ProductQuantity = UI.InputQuantity(product, orderStaff, "Create Order");
                            product.StatusInOrder = 0;
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
                    err = true;
                }
            }
            while (err == false);
        }
        return product;
    }

    public Order GetOrderToViewDetails(List<Order> listOrder, Staff orderStaff, string title)
    {
        bool active = true;
        Order order = null;
        bool err = false;
        while (active)
        {
            do
            {
                UI.PrintListOrder(listOrder, orderStaff, title);
                AnsiConsole.Markup("Order ID: ");
                int orderId;
                if (int.TryParse(Console.ReadLine(), out orderId) && orderId >= 0)
                {
                    if (orderId == 0)
                    {
                        return null;
                    }
                    else
                    {
                        if (orderBL.GetOrderById(orderId).OrderId != 0)
                        {
                            order = orderBL.GetOrderById(orderId);
                            return order;
                        }
                        else
                        {
                            active = true;
                            UI.RedMessage("Order not exist !");
                        }
                    }
                }
                else
                {
                    UI.RedMessage("Invalid ID !");
                    err = true;
                }
            }
            while (err == false);
        }
        return order;
    }
}