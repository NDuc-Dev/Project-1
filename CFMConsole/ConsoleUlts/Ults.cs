using Persistence;
using Spectre.Console;
using UI;
using BL;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
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
    string[] MainMenu = { "Create Order", "Update Order", "Update Product Status Instock", "Payment", "Check Out", "About" };

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
                bool login = true;
                UI.WelcomeStaff(currentStaff);
                while (login)
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
                        case "Update Product Status Instock":
                            UpdateProductStatusInstock();
                            break;
                        case "Check Out":
                            List<Order> listOrderUnComplete = orderBL.GetOrdersInprogress();
                            if (listOrderUnComplete.Count() != 0)
                            {
                                UI.PrintListOrder(listOrderUnComplete, currentStaff, "CHECK OUT");
                            }
                            else
                            {
                                UI.ApplicationLogoAfterLogin(currentStaff);
                                UI.Title("CHECK OUT");
                            }
                            AnsiConsole.Markup($"You have [green]{listOrderUnComplete.Count}[/] unpaid orders\n");
                            List<Order> listOrderCompleted = orderBL.GetOrdersCompleted();
                            decimal amountInShop = 0;
                            foreach (Order order in listOrderCompleted)
                            {
                                order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                                foreach (Product product in order.ProductsList)
                                {
                                    amountInShop += product.ProductQuantity * productBL.GetProductByIdAndSize(product.ProductId, product.ProductSizeId).ProductPrice;
                                }
                            }
                            string formattedTotal = amountInShop.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                            AnsiConsole.Markup($"Total amount in shop: [green]{formattedTotal} VND[/]\n");
                            string checkOut = UI.Ask("Do you want to check out ?");
                            switch (checkOut)
                            {
                                case "Yes":
                                    login = false;
                                    break;
                                case "No":
                                    break;
                            }
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
                    string addAsk = UI.Ask("[Green]Add product to order complete[/], do you want to [Green]CONTINUE[/] to add product ?");
                    switch (addAsk)
                    {
                        case "Yes":
                            continuee = true;
                            break;
                        case "No":
                            UI.PrintSaleReceipt(orders, currentStaff, currentStaff, "CREATE ORDER");
                            continuee = false;
                            string createAsk = UI.Ask("Do you want to [Green]CREATE[/] this order ?");
                            switch (createAsk)
                            {
                                case "Yes":
                                    Console.WriteLine("Create Order: " + (orderBL.SaveOrder(orders) ? "completed!" : "not complete!"));
                                    Console.WriteLine("Your Order Id is : " + orders.OrderId);
                                    UI.PressAnyKeyToContinue();
                                    active = false;
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
        List<Product> listAllProducts = productBL.GetAll();
        Product product;
        Order order;
        bool active = true;
        bool continuee = false;
        while (active)
        {
            List<Order> listOrderInprogress = orderBL.GetOrdersInprogress();
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
                    bool view = true;
                    while (view)
                    {
                        Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                        order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                        string[] functionsItem = { "Add product to order", "Remove an unfinished product from the order", "Change product in order", "Remove Order", "Confirm product in order", "Change Table", "Confirm order", "Exit" };
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
                                            string addAsk = UI.Ask("[Green]Add product to order complete[/], do you want to [Green]CONTINUE[/] to add product ?");
                                            switch (addAsk)
                                            {
                                                case "Yes":
                                                    continuee = true;
                                                    break;
                                                case "No":
                                                    UI.PrintOrderDetails(order.ProductsList, currentStaff, order, "ADD PRODUCT TO ORDER", staff.StaffName, 4);
                                                    continuee = false;
                                                    string updateAsk = UI.Ask("Do you want to [Green]UPDATE[/] this order ?");
                                                    switch (updateAsk)
                                                    {
                                                        case "Yes":
                                                            order.OrderStatus = 1;
                                                            Console.WriteLine("Update Order: " + (orderBL.UpdateOrder(order) ? "completed!" : "not complete!"));
                                                            UI.PressAnyKeyToContinue();
                                                            continuee = false;
                                                            subActive = false;
                                                            break;
                                                        case "No":
                                                            AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                                            UI.PressAnyKeyToContinue();
                                                            continuee = false;
                                                            subActive = false;
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
                                if (order.ProductsList.Count() == 1)
                                {
                                    AnsiConsole.Markup("[Red]The order has only 1 product left[/]\n");
                                    string deleteOrderAsk = UI.Ask("Do you want to [Green]DELETE[/] this order?");
                                    switch (deleteOrderAsk)
                                    {
                                        case "Yes":
                                            Console.WriteLine("Delete Order: " + (orderBL.DeleteOrder(order) ? "completed!" : "not complete!"));
                                            UI.PressAnyKeyToContinue();
                                            break;
                                        case "No":
                                            AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                            UI.PressAnyKeyToContinue();
                                            break;
                                    }

                                }
                                RemoveProductsInOrder(order.ProductsList, "REMOVE PRODUCT IN ORDER", order, staff);
                                break;
                            case "Change product in order":
                                List<Product> listProductChange = GetProductToChange(order.ProductsList, order, "CHANGE PRODUCT IN ORDER", staff);
                                if (listProductChange == null)
                                    break;
                                order.ProductsList = listProductChange;
                                UI.PrintOrderDetails(listProductChange, currentStaff, order, "CHANGE PRODUCT IN ORDER", staff.StaffName, 5);
                                string changeAsk = UI.Ask("This is your order after update, do you want to [Green]CONINUE[/] compltete ?");
                                switch (changeAsk)
                                {
                                    case "Yes":
                                        Console.WriteLine("Update Order: " + (orderBL.DeleteOrder(order) ? "completed!" : "not complete!"));
                                        UI.PressAnyKeyToContinue();
                                        break;
                                    case "No":
                                        AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                        UI.PressAnyKeyToContinue();
                                        break;
                                }
                                break;
                            case "Remove Order":
                                int checkProductComplete = 0;
                                for (int i = 0; i < order.ProductsList.Count(); i++)
                                {
                                    if (order.ProductsList[i].StatusInOrder == 2)
                                    {
                                        checkProductComplete++;
                                    }
                                }
                                if (checkProductComplete == 0)
                                {
                                    string deleteOrderAsk = UI.Ask("Do you want to [Green]DELETE[/] this order?");
                                    switch (deleteOrderAsk)
                                    {
                                        case "Yes":
                                            Console.WriteLine("Delete Order: " + (orderBL.DeleteOrder(order) ? "completed!" : "not complete!"));
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
                                    UI.RedMessage("The order already has a finished product, cannot be deleted");
                                }
                                break;
                            case "Confirm product in order":
                                Persistence.Product productConfirm = ChangeProductStatusToComplete(order.ProductsList, order, "CONFIRM PRODUCT IN ORDER", staff);
                                if (productConfirm == null)
                                {
                                    break;
                                }
                                string continueAsk = UI.Ask("Do you want to [Green]CONINUE[/] confirm this product ?");
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
                                    string continueComplete = UI.Ask("Do you want to [Green]CONFIRM[/] this order ?");
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
                                view = false;
                                break;
                        }

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
                        orderChoose.ProductsList = productBL.GetListProductsInOrder(orderChoose.OrderId);
                        orderStaff = staffBL.GetStaffById(orderChoose.OrderStaffID);
                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                        string complete = UI.Ask("Do you want to [Green]COMPLETE[/] this order ?");
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
                                    AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].\n");
                                    AnsiConsole.Markup("Enter the amount received input [green]0[/] to exit: ");
                                    decimal amountReceived;
                                    if (decimal.TryParse(Console.ReadLine(), out amountReceived) && amountReceived >= 0)
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
                                                AnsiConsole.Markup($"Return amount: [green]{formattedreturn} VND[/]\n");
                                                UI.PressAnyKeyToContinue();
                                                orderChoose.OrderStatus = 3;
                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                Console.WriteLine("Payment " + (orderBL.CompleteOrder(orderChoose) ? "completed!" : "not complete!"));
                                                UI.PressAnyKeyToContinue();
                                                break;
                                            }
                                            else
                                            {
                                                AnsiConsole.Markup("[red]the amount received cannot be less than the total amount in the sale recipt.[/]\n");
                                                UI.PressAnyKeyToContinue();
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
            do
            {
                UI.PrintNewProductsTable(lstproduct, orderStaff, title);
                AnsiConsole.Markup("Product Number: ");
                int productNumber;
                if (int.TryParse(Console.ReadLine(), out productNumber) && productNumber >= 0)
                {
                    if (productNumber == 0)
                    {
                        return null;
                    }
                    else
                    {
                        product = productBL.GetProductById(lstproduct[productNumber - 1].ProductId);
                        int productId = product.ProductId;
                        if (product.ProductId != 0)
                        {
                            int sizeId = UI.ChooseProductsize(orderStaff, product.ProductId, title);
                            if (sizeId == 0)
                            {
                                break;
                            }
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
                UI.PrintListOrder(listOrder, currentStaff, title);
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
                        if (title == "PAYMENT")
                        {
                            if (orderBL.GetOrderById(orderId).OrderId != 0 && orderBL.GetOrderById(orderId).OrderStatus != 3 && orderBL.GetOrderById(orderId).OrderStatus != 1)
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
                        else if (title == "UPDATE ORDER")
                        {
                            if (orderBL.GetOrderById(orderId).OrderId != 0 && orderBL.GetOrderById(orderId).OrderStatus != 3)
                            {
                                order = orderBL.GetOrderById(orderId);
                                return order;
                            }
                            else
                            {
                                active = true;
                                UI.RedMessage("Order not exist!");
                            }
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

    public List<Product> GetProductToChange(List<Product> listProductInOrder, Persistence.Order order, string title, Staff staff)
    {
        bool active = true;
        List<Product> newList = new List<Product>();
        while (active)
        {
            int productNumber = 0;
            int productId;
            int sizeId;
            Product newProduct = new Product();

            do
            {
                productId = 0;
                sizeId = 0;
                UI.PrintOrderDetails(listProductInOrder, currentStaff, order, title, staff.StaffName, 1);
                AnsiConsole.Markup(" Input the order number of the product in the order to change (Input [green]0[/] to exit): ");
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
                        newProduct = GetProductToAddToOrder(productBL.GetAll(), currentStaff, title);
                        if (newProduct != null)
                        {
                            bool checkDup = true;
                            foreach (Product item in newList)
                            {
                                if (item.ProductId == newProduct.ProductId && item.ProductSizeId == newProduct.ProductSizeId)
                                {
                                    item.ProductQuantity += newProduct.ProductQuantity;
                                    item.StatusInOrder = 1;
                                    checkDup = false;
                                }
                            }
                            if (checkDup == true)
                            {
                                newList.Add(newProduct);
                            }
                            return newList;

                        }
                        else
                        {
                            UI.GreenMessage("Cancel Change Complete !");
                            break;
                        }
                    }
                    else
                    {
                        UI.RedMessage("Product is Complete! Can't change. Please re-enter");
                    }
                }
                else
                {
                    UI.RedMessage("Product Not Exits! Please re-enter");
                }
            }
            while (true);
        }
        return newList;
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

    public void UpdateProductStatusInstock()
    {
        bool active = true;
        while (active)
        {
            if (active == false)
                break;
            bool err = false;
            do
            {
                List<Product> listProductInstock = productBL.GetListAllProductInStock();
                UI.PrintAllProductsInstock(listProductInstock, currentStaff, "UPDATE PRODUCT STATUS IN STOCK");
                AnsiConsole.Markup("Product ID: ");
                int productId;
                if (int.TryParse(Console.ReadLine(), out productId) && productId >= 0)
                {
                    if (productId == 0)
                    {
                        active = false;
                        break;
                    }
                    else
                    {
                        Product product = productBL.GetProductInstockById(productId);
                        if (product.ProductId != 0)
                        {
                            UI.PrintProductTable(productBL.GetProductInstockById(productId));
                            string change = UI.AskChangeStatus();
                            switch (change)
                            {
                                case "Change Product Status to Out Of Stock":
                                    if (product.ProductStatus == 0)
                                    {
                                        UI.RedMessage("The product is already in this state, no need to change");
                                    }
                                    else
                                    {
                                        int newStatus = 0;
                                        UI.GreenMessage("Change status " + (productBL.ChangeProductStatus(newStatus, product.ProductId) ? "completed!" : "not complete!"));
                                        UI.PressAnyKeyToContinue();
                                    }
                                    break;
                                case "Change Product Status to In Stock":
                                    if (product.ProductStatus == 1)
                                    {
                                        UI.RedMessage("The product is already in this state, no need to change");
                                    }
                                    else
                                    {
                                        int newStatus = 1;
                                        UI.GreenMessage("Change status " + (productBL.ChangeProductStatus(newStatus, product.ProductId) ? "completed!" : "not complete!"));
                                        UI.PressAnyKeyToContinue();
                                    }
                                    break;
                                case "Exit":
                                    break;
                            }
                        }
                        else
                        {
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

    }

    public void RemoveProductsInOrder(List<Product> listProductsInOrder, string title, Order order, Staff staff)
    {
        bool active = true;
        while (active)
        {

            string input;
            UI.PrintOrderDetails(listProductsInOrder, currentStaff, order, title, staff.StaffName, 1);
            Dictionary<int, Product> productMap = new Dictionary<int, Product>();
            for (int i = 0; i < listProductsInOrder.Count; i++)
            {
                productMap.Add(i + 1, listProductsInOrder[i]);
            }
            do
            {
                AnsiConsole.Markup("Enter the serial number of the product you want to [red]REMOVE[/], separated by the character '[green],[/]' or enter [red]0[/] to EXIT:");
                input = Console.ReadLine();
                if (input == "0")
                {
                    active = false;
                    break;
                }
            } while (!IsValidNumberString(input, listProductsInOrder));

            if (active == false)
                break;
            string[] targetNumberStrings = input.Split(',');

            List<int> targetNumbers = new List<int>();
            foreach (var targetNumberString in targetNumberStrings)
            {
                if (int.TryParse(targetNumberString.Trim(), out int targetNumber) && targetNumber > 0 && targetNumber <= listProductsInOrder.Count)
                {
                    targetNumbers.Add(targetNumber);
                }
                else
                {
                    Console.WriteLine($"Invalid serial number: {targetNumberString}");
                }
            }
            List<int> productNumbersComplete = new List<int>();
            List<Product> productsToRemove = new List<Product>();
            foreach (var targetNumber in targetNumbers)
            {
                if (productMap.TryGetValue(targetNumber, out Product product) && product.StatusInOrder == 1)
                {
                    productsToRemove.Add(product);
                }
                else
                {
                    productNumbersComplete.Add(targetNumber);
                }
            }
            if (productsToRemove.Count() == listProductsInOrder.Count())
            {
                string deleteOrderAsk = UI.Ask("You have selected all the products in your order,Do you want to [Green]DELETE[/] this order?");
                switch (deleteOrderAsk)
                {
                    case "Yes":
                        Console.WriteLine("Delete Order: " + (orderBL.DeleteOrder(order) ? "completed!" : "not complete!"));
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
            if (active == false)
            {
                break;
            }
            foreach (var productToRemove in productsToRemove)
            {
                listProductsInOrder.Remove(productToRemove);
            }

            UI.PrintOrderDetails(listProductsInOrder, currentStaff, order, "REMOVE PRODUCT IN ORDER", staff.StaffName, 2);
            string deleteAsk = UI.Ask("This is your order after update, do you want to [Green]CONINUE[/] compltete ?");
            switch (deleteAsk)
            {
                case "Yes":
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
                        orderBL.ConfirmOrder(order);
                    }
                    foreach (var productToRemove in productsToRemove)
                    {
                        Console.WriteLine($"Product removed: {productBL.GetProductById(productToRemove.ProductId).ProductName}");
                    }
                    foreach (var targetnumber in productNumbersComplete)
                    {
                        Console.WriteLine($"Can't delete product {targetnumber} because it's finished ");
                    }
                    Console.WriteLine("Update Order: " + (orderBL.UpdateOrder(order) ? "completed!" : "not complete!"));
                    UI.PressAnyKeyToContinue();
                    active = false;
                    break;
                case "No":
                    AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                    active = false;
                    UI.PressAnyKeyToContinue();
                    break;
            }
            if (active == false)
            {
                break;
            }
        }
    }


    public bool IsValidNumberString(string input, List<Product> productList)
    {

        // Sử dụng biểu thức chính quy để kiểm tra chuỗi có chứa chỉ số và dấu phẩy hay không
        if (!Regex.IsMatch(input, @$"^(\d+,)*\d+$"))
        {
            Console.WriteLine("Invalid string. Please re-enter.");
            return false;
        }

        string[] numbers = input.Split(',');
        foreach (string number in numbers)
        {
            if (!int.TryParse(number.Trim(), out _))
            {
                Console.WriteLine($"Invalid Number: {number}");
                return false;
            }
        }

        return true;
    }




}