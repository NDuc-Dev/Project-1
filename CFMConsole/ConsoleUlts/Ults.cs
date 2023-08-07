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
                    string[] functionsItem = { "Add product to order", "Remove an unfinished product from the order", "Confirm product in order", "Confirm order", "Exit" };
                    string updateChoice;
                    UI.PrintOrderDetails(order.ProductsList, orderStaff, order, title, staff.StaffName);
                    updateChoice = UI.SellectFunction(functionsItem);
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
                                    bool checkDup = true;
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
                                                        Console.WriteLine("Update Order: " + (orderBL.UpdateOrder(order) ? "completed!" : "not complete!"));
                                                        UI.PressAnyKeyToContinue();
                                                        active = false;
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
                                        continuee = false;
                                        active = false;
                                        break;
                                    }
                                }
                                while (continuee == false);
                            }
                            break;
                        case "Remove an unfinished product from the order":
                            break;
                        case "Confirm product in order":
                            break;
                        case "Confirm order":
                            break;
                        case "Exit":
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

    public List<Product> RemoveProductsFromOrder(List<Product> listProductInOrder, Persistence.Order order, string title, Staff staff)
    {
        bool active = true;
        List<Product> newList = new List<Product>();
        while (active)
        {
            bool checkProductId;
            bool checkSize;
            int productId = 0;
            int sizeId = 0;
            bool equal = false;
            do
            {
                do
                {
                    UI.PrintOrderDetails(listProductInOrder, orderStaff, order, title, staff.StaffName);
                    AnsiConsole.Markup("Product ID: ");
                    if (int.TryParse(Console.ReadLine(), out productId) && productId > 0)
                    {
                        checkProductId = true;
                    }
                    else
                    {
                        UI.RedMessage("Invalid ID");
                        checkProductId = false;
                    }
                } while (checkProductId);

                do
                {
                    AnsiConsole.Markup("Input size[Green](S, M or L)[/]: ");
                    string size = Console.ReadLine().ToUpper();
                    if (size == "S")
                    {
                        sizeId = 1;
                        checkSize = true;
                    }
                    else if (size == "M")
                    {
                        sizeId = 2;
                        checkSize = true;
                    }
                    else if (size == "L")
                    {
                        sizeId = 3;
                        checkSize = true;
                    }
                    else
                    {
                        UI.RedMessage("Invalid Size");
                        checkSize = false;
                    }

                }
                while (checkSize);

                foreach (var item in listProductInOrder)
                {
                    if (item.ProductId != productId || item.ProductSizeId != sizeId)
                        newList.Add(item);
                }
                if (newList.Count() == listProductInOrder.Count())
                {
                    equal = true;
                    UI.RedMessage("Product Not Found");
                }
                else 
                {
                    return newList;
                }
            } while (equal);


        }

        return newList;
    }
}