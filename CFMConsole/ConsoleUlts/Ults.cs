using Persistence;
using Spectre.Console;
using UI;
using BL;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ultilities;

public class Ults
{
    ConsoleUI UI = new ConsoleUI();
    StaffBL staffBL = new StaffBL();
    ProductBL productBL = new ProductBL();
    OrderBL orderBL = new OrderBL();
    TableBL tableBL = new TableBL();
    List<Product>? lstproduct;

    public void CreateOrder(Staff currentStaff)
    {
        bool active = true;
        Product product;
        lstproduct = productBL.GetAllProductActive();
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

                bool checkDup = true;
                if (orders.ProductsList.Count() == 0)
                {
                    orders.TableID = ChooseTable(currentStaff, "CREATE ORDER", 0);
                    if (orders.TableID == -1)
                    {
                        active = false;
                        break;
                    }
                }
                if (active == false)
                {
                    break;
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
                    string addAsk = UI.Ask("[Green]Add product to order complete[/], do you want to [Green]CREATE ORDER[/] (Choose [red]No[/] to CONTINUE add product) ?");
                    switch (addAsk)
                    {
                        case "No":
                            continuee = true;
                            break;
                        case "Yes":
                            UI.PrintSaleReceipt(orders, currentStaff, currentStaff, "CREATE ORDER");
                            continuee = false;
                            string createAsk = UI.Ask("Do you want to [Green]CREATE[/] this order ?");
                            switch (createAsk)
                            {
                                case "Yes":
                                    AnsiConsole.Markup("Create Order: " + (orderBL.CreateOrder(orders) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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
            if (active == false)
            {
                break;
            }
        }

    }

    public void UpdateOrder(Staff currentStaff)
    {
        string title = "UPDATE ORDER";
        List<Product> listAllProducts = productBL.GetAllProductActive();
        Product product;
        Order order;
        bool active = true;
        bool continuee = false;
        while (active)
        {
            UI.ApplicationLogoAfterLogin(currentStaff);
            UI.Title(title);
            var choice = AnsiConsole.Prompt(
       new SelectionPrompt<string>()
       .Title("Move [green]UP/DOWN[/] button and [Green]ENTER[/] to select.")
       .PageSize(3)
       .AddChoices("Drink at the Coffee Shop", "Take Away orders", "Exit"));
            switch (choice)
            {
                case "Drink at the Coffee Shop":
                    List<Order> listOrderInBarInprogress = orderBL.GetOrdersInBar();
                    if (listOrderInBarInprogress.Count() == 0)
                    {
                        UI.RedMessage("No orders have been created yet !");
                        break;
                    }
                    order = GetOrderToViewDetails(listOrderInBarInprogress, currentStaff, title, "IN BAR");
                    if (order != null)
                    {
                        bool view = true;
                        while (view)
                        {
                            Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                            order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                            string[] functionsItem = { "Add product to order", "Remove unfinished products from the order", "Change product in order", "Confirm product in order", "Change Table", "Exit" };
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
                                                                AnsiConsole.Markup("Update Order: " + (orderBL.UpdateOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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
                                case "Remove unfinished products from the order":
                                    int checkComplete = 0;
                                    foreach (var item in order.ProductsList)
                                    {
                                        if (item.StatusInOrder == 2)
                                        {
                                            checkComplete++;
                                        }
                                    }
                                    if (order.ProductsList.Count == checkComplete)
                                    {
                                        AnsiConsole.Markup("[Red]No unfinished product in the order.[/]\n");
                                        UI.PressAnyKeyToContinue();
                                        break;
                                    }
                                    else
                                    {
                                        if (order.ProductsList.Count() == 1 && order.ProductsList[0].StatusInOrder == 1)
                                        {
                                            AnsiConsole.Markup("[Red]The order has only 1 product left[/]\n");
                                            string deleteOrderAsk = UI.Ask("Do you want to [Green]DELETE[/] this order?");
                                            switch (deleteOrderAsk)
                                            {
                                                case "Yes":
                                                    AnsiConsole.Markup("Delete Order: " + (orderBL.DeleteOrder(order) ? "[green]COMPLETED[/] !\n" : "[red]NOT COMPLETED[/] !\n"));
                                                    UI.PressAnyKeyToContinue();
                                                    view = false;
                                                    break;
                                                case "No":
                                                    AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                                    UI.PressAnyKeyToContinue();
                                                    view = false;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            RemoveProductsInOrder(order.ProductsList, "REMOVE PRODUCT IN ORDER", order, staff, currentStaff);
                                        }
                                    }
                                    if (order.ProductsList.Count() == 0)
                                        view = false;
                                    break;
                                case "Change product in order":
                                    List<Product> listProductChange = GetProductToChange(order.ProductsList, order, "CHANGE PRODUCT IN ORDER", staff, currentStaff);
                                    if (listProductChange == null)
                                        break;
                                    order.ProductsList = listProductChange;
                                    UI.PrintOrderDetails(listProductChange, currentStaff, order, "CHANGE PRODUCT IN ORDER", staff.StaffName, 5);
                                    string changeAsk = UI.Ask("This is your order after update, do you want to [Green]CONINUE[/] confirm ?");
                                    switch (changeAsk)
                                    {
                                        case "Yes":
                                            AnsiConsole.Markup("Update Order: " + (orderBL.UpdateOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                            UI.PressAnyKeyToContinue();
                                            break;
                                        case "No":
                                            AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                            UI.PressAnyKeyToContinue();
                                            break;
                                    }
                                    break;
                                case "Confirm product in order":
                                    ChangeProductStatusToComplete(order.ProductsList, order, "CONFIRM PRODUCT IN ORDER", staff, currentStaff);
                                    break;
                                case "Change Table":
                                    bool checkExit = false;
                                    while (checkExit == false)
                                    {
                                        int newTable = ChooseTable(currentStaff, "CHANGE ORDER TABLE", order.TableID);
                                        if (newTable == -1)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            string continueChange = UI.Ask($"Do you want to [Green]CONTINUE[/] change table {order.TableID} to table {newTable} ?");
                                            switch (continueChange)
                                            {
                                                case "Yes":
                                                    AnsiConsole.Markup("Update Order: " + (tableBL.ChangeTableOrder(newTable, order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                    checkExit = true;
                                                    order.TableID = newTable;
                                                    UI.PressAnyKeyToContinue();
                                                    break;
                                                case "No":
                                                    AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                                    checkExit = true;
                                                    UI.PressAnyKeyToContinue();
                                                    break;
                                            }
                                        }
                                        if (checkExit == true)
                                        {
                                            break;
                                        }

                                    }
                                    break;
                                case "Exit":
                                    view = false;
                                    break;
                            }
                            if (view == false)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case "Take Away orders":
                    List<Order> listTakeAwayOrderInprogress = orderBL.GetTakeAwayOrders();
                    if (listTakeAwayOrderInprogress.Count() == 0)
                    {
                        UI.RedMessage("No orders have been created yet !");
                        break;
                    }
                    order = GetOrderToViewDetails(listTakeAwayOrderInprogress, currentStaff, title, "TAKE AWAY");
                    if (order != null)
                    {
                        bool view = true;
                        while (view)
                        {
                            Staff staff = staffBL.GetStaffById(order.OrderStaffID);
                            order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                            string[] functionsItem = { "Remove an unfinished product from the order", "Confirm product in order", "Exit" };
                            string updateChoice;
                            UI.PrintOrderDetails(order.ProductsList, currentStaff, order, title, staff.StaffName, 0);
                            updateChoice = UI.SellectFunction(functionsItem);
                            switch (updateChoice)
                            {
                                case "Remove an unfinished product from the order":
                                    if (order.ProductsList.Count() == 1)
                                    {
                                        AnsiConsole.Markup("[Red]The order has only 1 product left[/]\n");
                                        string deleteOrderAsk = UI.Ask("Do you want to [Green]DELETE[/] this order?");
                                        switch (deleteOrderAsk)
                                        {
                                            case "Yes":
                                                AnsiConsole.Markup("Delete Order: " + (orderBL.DeleteOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                UI.PressAnyKeyToContinue();
                                                view = false;
                                                break;
                                            case "No":
                                                AnsiConsole.Markup("[Green]Canceling update successfully.[/]\n");
                                                UI.PressAnyKeyToContinue();
                                                view = false;
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        RemoveProductsInOrder(order.ProductsList, "REMOVE PRODUCT IN ORDER", order, staff, currentStaff);
                                    }
                                    if (order.ProductsList.Count() == 0)
                                        view = false;
                                    break;
                                case "Confirm product in order":
                                    ChangeProductStatusToComplete(order.ProductsList, order, "CONFIRM PRODUCT IN ORDER", staff, currentStaff);
                                    break;
                                case "Exit":
                                    view = false;
                                    break;
                            }
                            if (view == false)
                            {
                                break;
                            }
                        }
                    }
                    break;
                case "Exit":
                    active = false;
                    break;
            }
            if (active == false)
            {
                break;
            }
        }
    }

    public void Payment(Staff currentStaff)
    {
        string title = "PAYMENT";
        bool active = true;
        Persistence.Order orderChoose;
        Staff orderStaff;
        while (active)
        {
            UI.ApplicationLogoAfterLogin(currentStaff);
            UI.Title(title);
            var choice = AnsiConsole.Prompt(
       new SelectionPrompt<string>()
       .Title("Move [green]UP/DOWN[/] button and [Green]ENTER[/] to select.")
       .PageSize(3)
       .AddChoices("Drink at the Coffee Shop", "Take Away orders", "Exit"));
            switch (choice)
            {
                case "Drink at the Coffee Shop":
                    while (true)
                    {

                        List<Order> listOrderInBarInprogress = orderBL.GetOrdersInBar();
                        if (listOrderInBarInprogress.Count() == 0)
                        {
                            UI.ApplicationLogoAfterLogin(currentStaff);
                            UI.Title(title);
                            UI.RedMessage("No orders have been created yet !");
                            break;
                        }
                        else
                        {
                            orderChoose = GetOrderToViewDetails(listOrderInBarInprogress, currentStaff, title, "IN BAR");
                            if (orderChoose == null)
                            {
                                break;
                            }
                            else
                            {
                                orderChoose.ProductsList = productBL.GetListProductsInOrder(orderChoose.OrderId);
                                orderStaff = staffBL.GetStaffById(orderChoose.OrderStaffID);
                                int checkComplete = 0;
                                List<Product> productsToRemove = new List<Product>();
                                foreach (Product productInOrder in orderChoose.ProductsList)
                                {
                                    if (productInOrder.StatusInOrder == 2)
                                    {
                                        checkComplete++;
                                    }
                                    else
                                    {
                                        productsToRemove.Add(productInOrder);
                                    }
                                }
                                if (checkComplete == orderChoose.ProductsList.Count())
                                {
                                    UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                    string complete = UI.Ask("Do you want to [Green]COMPLETE[/] this order ?");
                                    switch (complete)
                                    {
                                        case "Yes":
                                            bool paid = true;
                                            while (paid)
                                            {
                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                decimal totalAmount = 0;
                                                foreach (var item in orderChoose.ProductsList)
                                                {
                                                    totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                                                }
                                                string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].\n");
                                                decimal received = InputTotal();
                                                // string receivedFormat = received.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                if (received == -1)
                                                {
                                                    break;
                                                }
                                                if (received % 1000 == 0)
                                                {
                                                    decimal totalAmountInShop = 0;
                                                    DateOnly date = DateOnly.FromDateTime(DateTime.Now);
                                                    string selectedDateFormatted = date.ToString("yyyy/MM/dd");
                                                    List<Order> listOrderComplete = orderBL.GetOrdersCompleteInDay(selectedDateFormatted);
                                                    foreach (Order order in listOrderComplete)
                                                    {
                                                        List<Product> productsInOrder = productBL.GetListProductsInOrder(order.OrderId);
                                                        foreach (Product product in productsInOrder)
                                                        {
                                                            totalAmountInShop += product.ProductPrice;
                                                        }
                                                    }
                                                    // string totalFormat = totalAmountInShop.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                    string receivedFormat = received.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                    string accept = UI.Ask($"Amount received is [green]{receivedFormat} VND[/], do you want to continue ?");
                                                    switch (accept)
                                                    {
                                                        case "Yes":
                                                            if (received - totalAmount < 0)
                                                            {
                                                                UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                            }
                                                            else if (totalAmountInShop < received - totalAmount)
                                                            {
                                                                UI.RedMessage("Return Amount is bigger than total amount in shop.");
                                                            }
                                                            else if (received - totalAmount >= 0)
                                                            {
                                                                string returnFormat = (received - totalAmount).ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                                Console.WriteLine($"Amount received: {receivedFormat} VND");
                                                                Console.WriteLine($"Return amount: {returnFormat} VND");
                                                                AnsiConsole.Markup("Payment " + (orderBL.CompleteOrder(orderChoose) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                                paid = false;
                                                                UI.PressAnyKeyToContinue();
                                                                break;
                                                            }
                                                            break;
                                                        case "No":
                                                            break;
                                                    }

                                                    if (paid == false)
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                }

                                            }
                                            break;
                                        case "No":
                                            break;
                                    }
                                }
                                else if (checkComplete > 0 && checkComplete < orderChoose.ProductsList.Count)
                                {
                                    UI.PrintOrderDetails(orderChoose.ProductsList, currentStaff, orderChoose, title, orderStaff.StaffName, 0);
                                    string delete = UI.Ask($"The order has [green]{orderChoose.ProductsList.Count - checkComplete}[/] unfinished product, do you want to delete the unfinished products and pay now?");
                                    switch (delete)
                                    {
                                        case "Yes":
                                            foreach (var productToRemove in productsToRemove)
                                            {
                                                orderChoose.ProductsList.Remove(productToRemove);
                                            }
                                            UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                            string complete = UI.Ask("Do you want to [Green]COMPLETE[/] this order ?");
                                            switch (complete)
                                            {
                                                case "Yes":
                                                    bool paid = true;
                                                    while (paid)
                                                    {
                                                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                        decimal totalAmount = 0;
                                                        foreach (var item in orderChoose.ProductsList)
                                                        {
                                                            totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                                                        }
                                                        string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                        AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].\n");
                                                        decimal received = InputTotal();
                                                        if (received == -1)
                                                        {
                                                            break;
                                                        }
                                                        if (received % 1000 == 0)
                                                        {
                                                            string accept = UI.Ask("The amount is too big, are you sure?");
                                                            switch (accept)
                                                            {
                                                                case "Yes":
                                                                    if (received - totalAmount < 0)
                                                                    {
                                                                        UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                                    }
                                                                    else if (received - totalAmount >= 0)
                                                                    {
                                                                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                                        Console.WriteLine($"Amount received: {received} VND");
                                                                        Console.WriteLine($"Return amount: {received - totalAmount} VND");
                                                                        orderBL.UpdateOrder(orderChoose);
                                                                        AnsiConsole.Markup("Payment " + (orderBL.CompleteOrder(orderChoose) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                                        paid = false;
                                                                        UI.PressAnyKeyToContinue();
                                                                        break;
                                                                    }
                                                                    break;
                                                                case "No":
                                                                    break;
                                                            }
                                                            if (paid == false)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                        }

                                                    }
                                                    break;
                                                case "No":
                                                    break;
                                            }
                                            break;
                                        case "No":
                                            break;
                                    }
                                }
                                else
                                {
                                    UI.RedMessage("Unable to pay the order. No finished products yet !");
                                }
                            }
                        }
                    }
                    break;
                case "Take Away orders":
                    while (true)
                    {
                        List<Order> listTakeAwayOrderInprogress = orderBL.GetTakeAwayOrders();
                        if (listTakeAwayOrderInprogress.Count() == 0)
                        {
                            UI.ApplicationLogoAfterLogin(currentStaff);
                            UI.Title(title);
                            UI.RedMessage("No orders have been created yet !");
                            break;
                        }
                        else
                        {
                            orderChoose = GetOrderToViewDetails(listTakeAwayOrderInprogress, currentStaff, title, "TAKE AWAY");
                            if (orderChoose == null)
                            {
                                break;
                            }
                            else
                            {
                                orderChoose.ProductsList = productBL.GetListProductsInOrder(orderChoose.OrderId);
                                orderStaff = staffBL.GetStaffById(orderChoose.OrderStaffID);
                                int checkComplete = 0;
                                List<Product> productsToRemove = new List<Product>();
                                foreach (Product productInOrder in orderChoose.ProductsList)
                                {
                                    if (productInOrder.StatusInOrder == 2)
                                    {
                                        checkComplete++;
                                    }
                                    else
                                    {
                                        productsToRemove.Add(productInOrder);
                                    }
                                }
                                if (checkComplete == orderChoose.ProductsList.Count())
                                {
                                    UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                    string complete = UI.Ask("Do you want to [Green]COMPLETE[/] this order ?");
                                    switch (complete)
                                    {
                                        case "Yes":
                                            bool paid = true;
                                            while (paid)
                                            {
                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                decimal totalAmount = 0;
                                                foreach (var item in orderChoose.ProductsList)
                                                {
                                                    totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                                                }
                                                string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].\n");
                                                decimal received = InputTotal();
                                                if (received == -1)
                                                {
                                                    break;
                                                }
                                                if (received % 1000 == 0)
                                                {
                                                    string accept = UI.Ask("The amount is too big, are you sure?");
                                                    switch (accept)
                                                    {
                                                        case "Yes":
                                                            if (received - totalAmount < 0)
                                                            {
                                                                UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                            }
                                                            else if (received - totalAmount >= 0)
                                                            {
                                                                UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                                Console.WriteLine($"Amount received: {received} VND");
                                                                Console.WriteLine($"Return amount: {received - totalAmount} VND");
                                                                AnsiConsole.Markup("Payment " + (orderBL.CompleteOrder(orderChoose) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                                paid = false;
                                                                UI.PressAnyKeyToContinue();
                                                                break;
                                                            }
                                                            break;
                                                        case "No":
                                                            break;
                                                    }
                                                    if (paid == false)
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                }

                                            }
                                            break;
                                        case "No":
                                            break;
                                    }
                                }
                                else if (checkComplete > 0 && checkComplete < orderChoose.ProductsList.Count)
                                {
                                    UI.PrintOrderDetails(orderChoose.ProductsList, currentStaff, orderChoose, title, orderStaff.StaffName, 0);
                                    string delete = UI.Ask($"The order has [green]{orderChoose.ProductsList.Count - checkComplete}[/] unfinished product, do you want to delete the unfinished products and pay now?");
                                    switch (delete)
                                    {
                                        case "Yes":
                                            foreach (var productToRemove in productsToRemove)
                                            {
                                                orderChoose.ProductsList.Remove(productToRemove);
                                            }
                                            UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                            string complete = UI.Ask("Do you want to [Green]COMPLETE[/] this order ?");
                                            switch (complete)
                                            {
                                                case "Yes":
                                                    bool paid = true;
                                                    while (paid)
                                                    {
                                                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                        decimal totalAmount = 0;
                                                        foreach (var item in orderChoose.ProductsList)
                                                        {
                                                            totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                                                        }
                                                        string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                                                        AnsiConsole.Markup($"The total amount you have to pay is [green]{formattedTotal} VND[/].\n");
                                                        decimal received = InputTotal();
                                                        if (received == -1)
                                                        {
                                                            break;
                                                        }
                                                        if (received % 1000 == 0)
                                                        {
                                                            string accept = UI.Ask("The amount is too big, are you sure?");
                                                            switch (accept)
                                                            {
                                                                case "Yes":
                                                                    if (received - totalAmount < 0)
                                                                    {
                                                                        UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                                    }
                                                                    else if (received - totalAmount >= 0)
                                                                    {
                                                                        UI.PrintSaleReceipt(orderChoose, currentStaff, orderStaff, title);
                                                                        Console.WriteLine($"Amount received: {received} VND");
                                                                        Console.WriteLine($"Return amount: {received - totalAmount} VND");
                                                                        orderBL.UpdateOrder(orderChoose);
                                                                        AnsiConsole.Markup("Payment " + (orderBL.CompleteOrder(orderChoose) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                                        paid = false;
                                                                        UI.PressAnyKeyToContinue();
                                                                        break;
                                                                    }
                                                                    break;
                                                                case "No":
                                                                    break;
                                                            }
                                                            if (paid == false)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            UI.RedMessage("Enter the number divisible by 1000 and must be greater than the total amount ! Please re-enter.");
                                                        }

                                                    }
                                                    break;
                                                case "No":
                                                    break;
                                            }
                                            break;
                                        case "No":
                                            break;
                                    }
                                }
                                else
                                {
                                    UI.RedMessage("Unable to pay the order. No finished products yet !");
                                }
                            }
                        }
                    }
                    break;
                case "Exit":
                    active = false;
                    break;
            }
            if (active == false)
            {
                break;
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
                if (int.TryParse(Console.ReadLine(), out productNumber) && productNumber >= 0 && productNumber <= lstproduct.Count)
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
                            int sizeId = ChooseProductsize(orderStaff, product.ProductId, title);
                            if (sizeId == 0)
                            {
                                break;
                            }
                            product = productBL.GetProductByIdAndSize(productId, sizeId);
                            product.ProductQuantity = InputQuantity(product, orderStaff, title);
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

    public Order GetOrderToViewDetails(List<Order> listOrder, Staff currentStaff, string title, string subtitle)
    {
        bool active = true;
        Order order = null;
        bool err = false;
        while (active)
        {
            do
            {
                if (subtitle == "IN BAR")
                {
                    UI.PrintListOrderInBar(listOrder, currentStaff, title);
                    AnsiConsole.Markup("Input [Green]TABLE ID[/] to view order or input [Red] TABLE ID = 0[/] to [Red]EXIT[/].\n");
                    AnsiConsole.Markup("Table ID: ");
                    int tableId;
                    if (int.TryParse(Console.ReadLine(), out tableId) && tableId >= 0)
                    {
                        if (tableId == 0)
                        {
                            return null;
                        }
                        else
                        {
                            if (title == "PAYMENT")
                            {
                                if (orderBL.GetOrderByTable(tableId).OrderId != 0 && orderBL.GetOrderByTable(tableId).OrderStatus != 3)
                                {
                                    order = orderBL.GetOrderByTable(tableId);
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
                                if (orderBL.GetOrderByTable(tableId).OrderId != 0 && orderBL.GetOrderByTable(tableId).OrderStatus != 3 && orderBL.GetOrderByTable(tableId).TableID != 0)
                                {
                                    order = orderBL.GetOrderByTable(tableId);
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
                else if (subtitle == "TAKE AWAY")
                {
                    UI.PrintListOrderTakeAway(listOrder, currentStaff, title);
                    AnsiConsole.Markup("Input [Green]ORDER ID[/] to view order or input [Red]ORDER ID = 0[/] to [Red]EXIT[/].\n");
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
                                if (orderBL.GetOrderById(orderId).TableID == 0 && orderBL.GetOrderById(orderId).OrderStatus != 3)
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
                                if (orderBL.GetOrderById(orderId).TableID == 0 && orderBL.GetOrderById(orderId).OrderStatus != 3)
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

            }
            while (err == false);
        }
        return order;
    }

    public List<Product> GetProductToChange(List<Product> listProductInOrder, Persistence.Order order, string title, Staff staff, Staff currentStaff)
    {
        bool active = true;
        List<Product> newList = new List<Product>();
        while (active)
        {
            int productNumber;
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
                        newProduct = GetProductToAddToOrder(productBL.GetAllProductActive(), currentStaff, title);
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

    public void ChangeProductStatusToComplete(List<Product> listProductsInOrder, Order order, string title, Staff staff, Staff currentStaff)
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
                AnsiConsole.Markup("Enter the serial number of the product you want to [red]CONFIRM[/], separated by the character '[green],[/]' or enter [red]0[/] to EXIT:");
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
            foreach (var targetNumberString in targetNumberStrings)
            {
                if (int.TryParse(targetNumberString.Trim(), out int targetNumber) && targetNumber > 0 && targetNumber <= listProductsInOrder.Count)
                {
                    if (productMap.TryGetValue(targetNumber, out Product productToUpdate))
                    {
                        productToUpdate.StatusInOrder = 2;
                    }
                    else
                    {
                        Console.WriteLine($"No product found with sequence number{targetNumber}");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid serial number: {targetNumberString}");
                }
            }

            List<Product> updatedProductList = productMap.Values.ToList();
            UI.PrintOrderDetails(listProductsInOrder, currentStaff, order, "CONFIRM PRODUCT IN ORDER", staff.StaffName, 0);
            string confirmAsk = UI.Ask("This is your order after update, do you want to [Green]CONINUE[/] compltete ?");
            switch (confirmAsk)
            {
                case "Yes":
                    int checkComplete = 0;
                    for (int i = 0; i < updatedProductList.Count(); i++)
                    {
                        if (updatedProductList[i].StatusInOrder == 2)
                        {
                            checkComplete++;
                        }
                    }
                    if (checkComplete == updatedProductList.Count())
                    {
                        order.OrderStatus = 2;
                    }
                    AnsiConsole.Markup("Update Order: " + (orderBL.UpdateOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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

    public void UpdateProductStatusInstock(Staff currentStaff)
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
                            string[] item = { "Change Product Status to Out Of Stock", "Change Product Status to In Stock", "Exit" };
                            var change = AnsiConsole.Prompt(
                               new SelectionPrompt<string>()
                               .PageSize(3)
                               .AddChoices(item));
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
                                        UI.GreenMessage("Change status " + (productBL.ChangeProductStatus(newStatus, product.ProductId) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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
                                        UI.GreenMessage("Change status " + (productBL.ChangeProductStatus(newStatus, product.ProductId) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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

    public void RemoveProductsInOrder(List<Product> listProductsInOrder, string title, Order order, Staff staff, Staff currentStaff)
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
                        AnsiConsole.Markup("Delete Order: " + (orderBL.DeleteOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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
                        order.OrderStatus = 2;
                    }
                    foreach (var productToRemove in productsToRemove)
                    {
                        Console.WriteLine($"Product removed: {productBL.GetProductById(productToRemove.ProductId).ProductName}");
                    }
                    foreach (var targetnumber in productNumbersComplete)
                    {
                        Console.WriteLine($"Can't delete product {targetnumber} because it's finished ");
                    }
                    AnsiConsole.Markup("Update Order: " + (orderBL.UpdateOrder(order) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
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

    public string FormatCurrency(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "0 VND";
        }
        else if (decimal.TryParse(input, out decimal numericValue))
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            string formattedAmount = string.Format(culture, "{0:N0} VND", numericValue);
            return formattedAmount;
        }
        else
        {
            return "Không hợp lệ";
        }
    }

    public decimal InputTotal()
    {
        AnsiConsole.Markup("Enter the amount received or input [green]0[/] to exit: ");
        string input = "";
        int cursorLeft = Console.CursorLeft;

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
            }
            else if (char.IsDigit(keyInfo.KeyChar))
            {
                input += keyInfo.KeyChar;
            }

            string formattedAmount = FormatCurrency(input);
            Console.SetCursorPosition(cursorLeft, Console.CursorTop);
            Console.Write(formattedAmount.PadRight(Console.WindowWidth - cursorLeft));
        }
        if (input == "0")
        {
            return -1;
        }
        decimal.TryParse(input, out decimal result);
        return result;
    }

    public int ChooseProductsize(Staff staff, int productId, string title)
    {
        string[] size = { "Size S", "Size M", "Size L", "Choose another product" };
        int sizeId = 0;
        bool active = true;
        while (active)
        {
            UI.ApplicationLogoAfterLogin(staff);
            UI.Title(title);
            if (title == "CREATE ORDER")
            {
                UI.TimeLine(UI.TimeLineContent(3, "CREATE ORDER"));
            }
            else if (title == "ADD PRODUCT TO ORDER")
            {
                UI.TimeLine(UI.TimeLineContent(2, "ADD PRODUCT TO ORDER"));
            }
            else if (title == "CHANGE PRODUCT IN ORDER")
            {
                UI.TimeLine(UI.TimeLineContent(3, "CHANGE PRODUCT IN ORDER"));
            }
            UI.PrintProductTable(productBL.GetProductById(productId));
            Product currentProduct = productBL.GetProductById(productId);
            Console.WriteLine("Product Name: " + currentProduct.ProductName);
            var choice = AnsiConsole.Prompt(
       new SelectionPrompt<string>()
       .Title("Move [green]UP/DOWN[/] button and [Green] ENTER[/] to select function")
       .PageSize(10)
       .AddChoices(size));

            switch (choice)
            {
                case "Size S":
                    return 1;
                case "Size M":
                    return 2;
                case "Size L":
                    return 3;
                case "Choose another product":
                    return 0;
            }
        }
        return sizeId;
    }

    public int InputQuantity(Product product, Staff staff, string title)
    {
        int quantity = 0;
        bool active = true;
        while (active)
        {

            bool err = false;
            bool continuee = false;
            do
            {
                Console.Clear();
                UI.ApplicationLogoAfterLogin(staff);
                UI.Title(title);
                if (title == "CREATE ORDER")
                {
                    UI.TimeLine(UI.TimeLineContent(4, "CREATE ORDER"));
                }
                else if (title == "ADD PRODUCT TO ORDER")
                {
                    UI.TimeLine(UI.TimeLineContent(3, "ADD PRODUCT TO ORDER"));
                }
                else if (title == "CHANGE PRODUCT IN ORDER")
                {
                    UI.TimeLine(UI.TimeLineContent(4, "CHANGE PRODUCT IN ORDER"));
                }
                Console.WriteLine("Product Name : " + product.ProductName);
                Console.WriteLine("Product Size : " + product.ProductSize);
                string formattepricesize = product.ProductPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                Console.WriteLine("Unit Price : " + formattepricesize + " VND");
                Console.Write("Input Quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                {
                    continuee = true;
                    if (quantity > 10)
                    {
                        string askAceptQuantity = UI.Ask("[green]Quantity > 10[/], Are you sure ?");
                        switch (askAceptQuantity)
                        {
                            case "Yes":
                                return quantity;
                            case "No":
                                continuee = false;
                                break;
                        }
                    }
                    else
                    {
                        return quantity;
                    }
                }
                else
                {
                    err = true;
                    UI.RedMessage("Invalid quantity ! Please re-enter.");
                }
            } while (err == false && continuee);
        }
        return quantity;
    }

    public int ChooseTable(Staff staff, string title, int tableOrder)
    {
        while (true)
        {

            int tableId = -1;
            List<Persistence.Table> listTable = tableBL.GetAllTables();
            bool checkTableId;

            UI.ApplicationLogoAfterLogin(staff);
            UI.Title(title);
            if (title == "CREATE ORDER")
            {
                UI.TimeLine(UI.TimeLineContent(1, "CREATE ORDER"));
                var choice = AnsiConsole.Prompt(
           new SelectionPrompt<string>()
           .Title("Move [green]UP/DOWN[/] button and [Green]ENTER[/] to select.")
           .PageSize(3)
           .AddChoices("Drink at the Coffee Shop", "Takeout orders", "Exit"));
                switch (choice)
                {
                    case "Drink at the Coffee Shop":
                        while (true)
                        {
                            UI.ApplicationLogoAfterLogin(staff);
                            UI.Title(title);
                            UI.TimeLine(UI.TimeLineContent(1, "CREATE ORDER"));
                            UI.PrintAllTables(listTable);
                            do
                            {
                                AnsiConsole.Markup("\nInput [Green]TABLE ID[/] to choose table or input [Red]TABLE ID = 0[/] to [red]EXIT[/]: ");
                                if (int.TryParse(Console.ReadLine(), out tableId) && tableId >= 0)
                                {
                                    if (tableId == 0)
                                    {
                                        return -1;
                                    }
                                    else
                                    {
                                        if (tableBL.GetTableById(tableId).TableStatus == 1)
                                        {
                                            UI.RedMessage("Table is using, please re-enter Table ID.");
                                            checkTableId = false;
                                        }
                                        else if (tableBL.GetTableById(tableId).TableId == 0)
                                        {
                                            UI.RedMessage("Invalid Id, please re-enter Table ID.");
                                            checkTableId = false;
                                        }
                                        else
                                        {
                                            return tableBL.GetTableById(tableId).TableId;
                                        }
                                    }
                                }
                                else
                                {
                                    checkTableId = false;
                                    UI.RedMessage("Invalid Id, please re-enter Table ID");
                                }
                            }
                            while (checkTableId);
                        }
                    case "Takeout orders":
                        return 0;
                    case "Exit":
                        return -1;
                }
            }
            else if (title == "CHANGE ORDER TABLE")
            {
                do
                {
                    UI.PrintAllTables(listTable);
                    AnsiConsole.Markup($"\nCurrent [Green]TABLE ID[/] of this order is [green]{tableOrder}[/].");
                    AnsiConsole.Markup("\nInput [Green]TABLE ID[/] to choose table to change or input [green]0[/] to exit: ");
                    if (int.TryParse(Console.ReadLine(), out tableId) && tableId >= 0)
                    {
                        if (tableId == 0)
                        {
                            return -1;
                        }
                        else
                        {
                            if (tableBL.GetTableById(tableId).TableStatus == 1)
                            {
                                UI.RedMessage("Table is using, please re-enter Table ID.");
                                checkTableId = false;
                            }
                            else if (tableBL.GetTableById(tableId).TableId == 0)
                            {
                                UI.RedMessage("Invalid Id, please re-enter Table ID.");
                                checkTableId = false;
                            }
                            else
                            {
                                return tableBL.GetTableById(tableId).TableId;
                            }
                        }
                    }
                    else
                    {
                        checkTableId = false;
                        UI.RedMessage("Invalid Id, please re-enter Table ID");
                    }
                }
                while (checkTableId);
            }
        }
    }

    public bool CheckOut(Staff currentStaff)
    {
        bool checkOutbool = false;
        List<Order> listOrderInBarUnComplete = orderBL.GetOrdersInBar();
        List<Order> listOrderTakeAwayUnComplete = orderBL.GetTakeAwayOrders();
        UI.PrintListOrderInProgress(listOrderInBarUnComplete, listOrderTakeAwayUnComplete, currentStaff, "CHECK OUT");
        decimal totalAmountInShop = 0;
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        string selectedDateFormatted = date.ToString("yyyy/MM/dd");
        List<Order> listOrderComplete = orderBL.GetOrdersCompleteInDay(selectedDateFormatted);
        foreach (Order order in listOrderComplete)
        {
            List<Product> productsInOrder = productBL.GetListProductsInOrder(order.OrderId);
            foreach (Product product in productsInOrder)
            {
                totalAmountInShop += product.ProductPrice;
            }
        }
        string totalFormat = totalAmountInShop.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
        AnsiConsole.Markup($"Total amount available in shop : [green]{totalFormat} VND[/]\n");
        string checkOut = UI.Ask("Do you want to check out ?");
        switch (checkOut)
        {
            case "Yes":
                currentStaff.LogoutTime = DateTime.Now;
                string formattedDateTime = currentStaff.LogoutTime.ToString("yyyy-MM-dd HH:mm:ss");
                AnsiConsole.Markup("Check out: " + (staffBL.UpdateLogoutTime(formattedDateTime, totalAmountInShop) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                UI.PressAnyKeyToContinue();
                return true;
            case "No":
                return false;
        }
        return checkOutbool;
    }
}