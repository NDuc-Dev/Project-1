using Persistence;
using Spectre.Console;
using UI;
using BL;
using System.Globalization;

namespace Ultilities;

public class Ults
{
    ConsoleUI UI = new ConsoleUI();
    StaffBL staffBL = new StaffBL();
    ProductBL productBL = new ProductBL();
    OrderBL orderBL = new OrderBL();
    TableBL tableBL = new TableBL();
    List<Product>? lstproduct;
    Staff currentStaff;
    string[] MainMenu = { "Create Order", "Update Order", "Update Product Status Instok", "Payment", "Check Out", "About" };

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
                currentStaff = staffBL.GetPasswordAndCheckAuthorize(UserName);
            }

            if (currentStaff != null)
            {
                UI.WelcomeStaff(currentStaff);
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    UI.ApplicationLogoAfterLogin(currentStaff);
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
                            Payment();
                            break;
                        case "Update Product Status Instok":
                            break;
                        case "Check Out":
                            break;
                        case "About":
                            UI.About(currentStaff);
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
                    orders.TableID = UI.ChooseTable(currentStaff, "CREATE ORDER", 0);
                }
                product = GetProductToAddToOrder(lstproduct, currentStaff, "CREATE ORDER");
                if (product != null)
                {
                    orders.OrderStaffID = currentStaff.StaffId;

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
                            UI.PrintSaleReceipt(orders, currentStaff, currentStaff, "CREATE ORDER");
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
        List<Persistence.Product> listAllProducts = productBL.GetAll();
        Persistence.Product product;
        Persistence.Order order;
        bool active = true;
        bool continuee = false;
        while (active)
        {
            List<Persistence.Order> listOrderInprogress = orderBL.GetOrdersInprogress();
            if (active == false)
            {
                break;
            }
            if (listOrderInprogress.Count() == 0)
            {
                UI.ApplicationLogoAfterLogin(currentStaff);
                UI.Title(title);
                UI.RedMessage("No orders have been created yet !");
                break;
            }
            else
            {
                order = GetOrderToViewDetails(listOrderInprogress, currentStaff, title);
                if (order != null)
                {
                    Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                    order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                    string[] functionsItem = { "Add product to order", "Remove an unfinished product from the order", "Confirm product in order", "Change Table", "Confirm order", "Exit" };
                    string updateChoice;
                    UI.PrintOrderDetails(order.ProductsList, currentStaff, order, title, staff.StaffName, 0);
                    updateChoice = UI.SellectFunction(functionsItem);
                    switch (updateChoice)
                    {
                        case "Add product to order":
                            bool subActive = true;
                            while (subActive)
                            {
                                if (subActive == false)
                                {
                                    break;
                                }
                                do
                                {
                                    bool checkDup = true;
                                    if (subActive == false)
                                    {
                                        break;
                                    }
                                    product = GetProductToAddToOrder(listAllProducts, currentStaff, "ADD PRODUCT TO ORDER");
                                    if (product != null)
                                    {
                                        foreach (Product item in order.ProductsList)
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
                                            order.ProductsList.Add(product);
                                        }
                                        string addAsk = UI.AskToContinueAdd();
                                        switch (addAsk)
                                        {
                                            case "Yes":
                                                continuee = true;
                                                break;
                                            case "No":
                                                UI.PrintOrderDetails(order.ProductsList, currentStaff, order, "ADD PRODUCT TO ORDER", staff.StaffName, 4);
                                                continuee = false;
                                                string updateAsk = UI.AskToContinueUpdate();
                                                switch (updateAsk)
                                                {
                                                    case "Yes":
                                                        order.OrderStatus = 1;
                                                        Console.WriteLine("Update Order: " + (orderBL.UpdateOrder(order) ? "completed!" : "not complete!"));
                                                        UI.PressAnyKeyToContinue();
                                                        break;
                                                    case "No":
                                                        AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                                        UI.PressAnyKeyToContinue();
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        continuee = false;
                                        subActive = false;
                                        break;
                                    }
                                }
                                while (continuee == false);
                            }
                            break;
                        case "Remove an unfinished product from the order":
                            List<Product> listProductafter = GetProductsToRemoveProductsFromOrder(order.ProductsList, order, "REMOVE PRODUCT IN ORDER", staff);
                            if (listProductafter == null)
                                break;
                            order.ProductsList = listProductafter;
                            UI.PrintOrderDetails(listProductafter, currentStaff, order, "REMOVE PRODUCT IN ORDER", staff.StaffName, 2);
                            string deleteAsk = UI.AskToContinueDelete();
                            switch (deleteAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Update Order: " + (orderBL.UpdateOrder(order) ? "completed!" : "not complete!"));
                                    UI.PressAnyKeyToContinue();
                                    break;
                                case "No":
                                    AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                    UI.PressAnyKeyToContinue();
                                    break;
                            }
                            break;
                        case "Confirm product in order":
                            Persistence.Product productConfirm = ChangeProductStatusToComplete(order.ProductsList, order, "CONFIRM PRODUCT IN ORDER", staff);
                            if (productConfirm == null)
                            {
                                break;
                            }
                            string continueAsk = UI.AskToContinueConfirm();
                            switch (continueAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Update Order: " + (productBL.UpdateProductStatusInOrder(productConfirm, order) ? "completed!" : "not complete!"));
                                    UI.PressAnyKeyToContinue();
                                    break;
                                case "No":
                                    AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                    UI.PressAnyKeyToContinue();
                                    break;
                            }
                            break;
                        case "Change Table":
                            int newTable = UI.ChooseTable(currentStaff, "CHANGE ORDER TABLE", order.TableID);
                            if (newTable == 0)
                            {
                                break;
                            }
                            else
                            {
                                string continueChange = UI.AskToContinueUpdateTable(newTable, order.TableID);
                                switch (continueChange)
                                {
                                    case "Yes":
                                        Console.WriteLine("Update Order: " + (tableBL.ChangeTableOrder(newTable, order) ? "completed!" : "not complete!"));
                                        UI.PressAnyKeyToContinue();
                                        break;
                                    case "No":
                                        AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                        UI.PressAnyKeyToContinue();
                                        break;
                                }
                            }
                            break;
                        case "Confirm order":
                            int checkComplete = 0;
                            for (int i = 0; i < order.ProductsList.Count(); i++)
                            {
                                if (order.ProductsList[i].StatusInOrder == 2)
                                {
                                    checkComplete++;
                                }
                            }
                            if (checkComplete == order.ProductsList.Count())
                            {
                                string continueComplete = UI.AskToContinueConfirmOrder();
                                switch (continueComplete)
                                {
                                    case "Yes":
                                        Console.WriteLine("Update Order: " + (orderBL.ConfirmOrder(order) ? "completed!" : "not complete!"));
                                        UI.PressAnyKeyToContinue();
                                        break;
                                    case "No":
                                        AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                        UI.PressAnyKeyToContinue();
                                        break;
                                }
                            }
                            else
                            {
                                AnsiConsole.Markup("[red]Have unfinished products that cannot be confirmed.[/]\n");
                                UI.PressAnyKeyToContinue();
                                break;
                            }
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

    public void Payment()
    {
        string title = "PAYMENT";
        bool active = true;
        Persistence.Order orderChoose;
        Staff orderStaff;
        while (active)
        {
            List<Persistence.Order> listOrdersConfirmed = orderBL.GetOrdersConfirmed();
            if (active == false)
            {
                break;
            }
            if (listOrdersConfirmed.Count() == 0)
            {
                UI.ApplicationLogoAfterLogin(currentStaff);
                UI.Title(title);
                UI.RedMessage("No orders have been confirmed yet !");
                break;
            }
            else
            {
                orderChoose = GetOrderToViewDetails(listOrdersConfirmed, currentStaff, title);
                orderChoose.ProductsList = productBL.GetListProductsInOrder(orderChoose.OrderId);
                orderStaff = staffBL.GetStaffById(orderChoose.OrderStaffID);
                if (orderChoose == null)
                {
                    break;
                }
                else
                {
                    if (orderChoose.OrderStatus != 2)
                    {
                        UI.RedMessage("The order does not exist or is not completed. Please re-enter !");
                        break;
                    }
                    else
                    {
                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                        string complete = UI.AskToContinueComplete();
                        switch (complete)
                        {
                            case "Yes":
                                while (true)
                                {
                                    decimal totalAmount = 0;
                                    foreach (var item in orderChoose.ProductsList)
                                    {
                                        totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                                    }
                                    string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                    UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                    AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].");
                                    Console.WriteLine("Enter the amount receivedvinput [green]0[/] to exit: ");
                                    decimal amountReceived;
                                    if (decimal.TryParse(Console.ReadLine(), out amountReceived) && amountReceived > 0)
                                    {
                                        if (amountReceived == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if ((amountReceived - totalAmount) >= 0)
                                            {
                                                string formattedreturn = (amountReceived - totalAmount).ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                AnsiConsole.Markup($"Return amount: [green]{formattedreturn} VND[/]");
                                                UI.PressAnyKeyToContinue();
                                                orderChoose.OrderStatus = 3;
                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                Console.WriteLine("Payment "+ (orderBL.CompleteOrder(orderChoose) ? "completed!" : "not complete!"));
                                                UI.PressAnyKeyToContinue();
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        UI.RedMessage("Invalid input, please re-enter !");
                                    }

                                }
                                break;
                            case "No":
                                break;
                        }
                    }
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
                            int sizeId = UI.ChooseProductsize(orderStaff, productId, title);
                            product = productBL.GetProductByIdAndSize(productId, sizeId);
                            product.ProductQuantity = UI.InputQuantity(product, orderStaff, title);
                            product.StatusInOrder = 1;
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
                UI.PrintOrderDetails(listProductInOrder, currentStaff, order, title, staff.StaffName, 1);
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
                UI.PrintOrderDetails(listProductInOrder, currentStaff, order, title, staff.StaffName, 0);
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
}