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
            UserName = Console.ReadLine();
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
                if (order != null)
                {
                    Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                    order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                    string[] functionsItem = { "Add product to order", "Remove an unfinished product from the order", "Confirm product in order", "Change Table", "Confirm order", "Exit" };
                    string updateChoice;
                    UI.PrintOrderDetails(order.ProductsList, orderStaff, order, title, staff.StaffName);
                    updateChoice = UI.SellectFunction(functionsItem);
                    switch (updateChoice)
                    {
                        case "Add product to order":
                            List<Persistence.Product> productListAfterAdd = GetListProductsToAddToOrder(order.ProductsList, listAllProducts, order);
                            if (productListAfterAdd == null)
                            {
                                break;
                            }
                            else
                            {
                                order.ProductsList = productListAfterAdd;
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
                            }
                            break;
                        case "Remove an unfinished product from the order":
                            List<Product> listProductafter = GetProductsToRemoveProductsFromOrder(order.ProductsList, order, title, staff);
                            if (listProductafter == null)
                                break;
                            order.ProductsList = listProductafter;
                            UI.PrintSaleReceipt(order, orderStaff, "UPDATE ORDER");
                            string deleteAsk = UI.AskToContinueDelete();
                            switch (deleteAsk)
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
                        case "Confirm product in order":
                            Persistence.Product productConfirm = ChangeProductStatusToComplete(order.ProductsList, order, title, staff);
                            if (productConfirm == null)
                                break;
                            string continueAsk = UI.AskToContinueConfirm();
                            switch (continueAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Update Order: " + (productBL.UpdateProductStatusInOrder(productConfirm, order) ? "completed!" : "not complete!"));
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
                        case "Change Table":
                            break;
                        case "Confirm order":
                            break;
                        case "Exit":
                            break;
                    }
                }
                else
                {
                    break;
                }

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

    public List<Product> GetProductsToRemoveProductsFromOrder(List<Product> listProductInOrder, Persistence.Order order, string title, Staff staff)
    {
        bool active = true;
        List<Product> newList = new List<Product>();
        while (active)
        {

            int productNumber = 0;
            int productId;
            int sizeId;

            do
            {
                productId = 0;
                sizeId = 0;
                UI.PrintOrderDetails(listProductInOrder, orderStaff, order, title, staff.StaffName);
                AnsiConsole.Markup(" Input the order number of the product in the order to delete (Input [green]0[/] to exit): ");
                if (int.TryParse(Console.ReadLine(), out productNumber) && productNumber >= 0 && productNumber <= listProductInOrder.Count())
                {
                    if (productNumber == 0)
                    {
                        return null;
                    }
                    else if (listProductInOrder[productNumber - 1].StatusInOrder == 1)
                    {
                        for (int i = 0; i < listProductInOrder.Count(); i++)
                        {
                            if (listProductInOrder[i].ProductId == listProductInOrder[productNumber - 1].ProductId && listProductInOrder[i].ProductSizeId == listProductInOrder[productNumber - 1].ProductSizeId)
                            {
                                productId = listProductInOrder[i].ProductId;
                                sizeId = listProductInOrder[i].ProductSizeId;
                            }
                        }

                        foreach (Product item in listProductInOrder)
                        {
                            if (item.ProductId == productId && item.ProductSizeId != sizeId)
                            {
                                newList.Add(item);

                            }
                            else if (item.ProductId != productId && item.ProductSizeId == sizeId)
                            {
                                newList.Add(item);

                            }
                            else if (item.ProductId != productId && item.ProductSizeId != sizeId)
                            {
                                newList.Add(item);

                            }
                        }
                        return newList;
                    }
                    else
                    {
                        UI.RedMessage("Product is Complete! Can't remove. Please re-enter");
                    }
                }
                else
                {
                    UI.RedMessage("Product Not Exits! Please re-enter");
                }
            } while (true);
        }
        return newList;
    }

    public Product ChangeProductStatusToComplete(List<Product> listProductInOrder, Persistence.Order order, string title, Staff staff)
    {
        Product product = new Product();
        bool active = true;

        while (active)
        {
            bool checkProductNumber;
            int productNumber;
            int productId;
            int sizeId;
            do
            {
                UI.PrintOrderDetails(listProductInOrder, orderStaff, order, title, staff.StaffName);
                AnsiConsole.Markup(" Input the order number of the product in the order to change status to complete (Input [green]0[/] to exit): ");
                if (int.TryParse(Console.ReadLine(), out productNumber) && productNumber >= 0 && productNumber <= listProductInOrder.Count())
                {
                    if (productNumber == 0)
                    {
                        return null;
                    }
                    else if (listProductInOrder[productNumber - 1].StatusInOrder == 1)
                    {
                        for (int i = 0; i < listProductInOrder.Count(); i++)
                        {
                            if (listProductInOrder[i].ProductId == listProductInOrder[productNumber - 1].ProductId && listProductInOrder[i].ProductSizeId == listProductInOrder[productNumber - 1].ProductSizeId)
                            {
                                return listProductInOrder[i];
                            }
                        }
                    }
                    else
                    {
                        UI.RedMessage("Product is Complete! No need to rework . Please re-enter");
                    }
                }
                else
                {
                    UI.RedMessage("Product Not Exits! Please re-enter");
                }
            }
            while (true);
        }
        return product;
    }

    public List<Product> GetListProductsToAddToOrder(List<Product> productsList, List<Product> listAllProducts, Order order)
    {
        List<Product> listProductsAfterAdd = new List<Product>();
        bool active = true;
        Product product;
        bool continuee = true;
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
                    foreach (Product item in productsList)
                    {
                        if (item.ProductId == product.ProductId && item.ProductSizeId == product.ProductSizeId)
                        {
                            item.ProductQuantity += product.ProductQuantity;
                            item.StatusInOrder = 1;
                            checkDup = false;
                        }
                    }
                    if (checkDup == true)
                    {
                        productsList.Add(product);
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
                            break;
                    }
                }
                else
                {
                    return null;
                }

            }
            while (continuee == false);
            listProductsAfterAdd = productsList;

        }
        return listProductsAfterAdd;
    }
}