﻿using Ultilities;
using Persistence;
using Spectre.Console;
using System.Globalization;
using UI;
using BL;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Ults Ultilities = new Ults();
        ConsoleUI uI = new ConsoleUI();
        StaffBL staffBL = new StaffBL();
        OrderBL orderBL = new OrderBL();
        ProductBL productBL = new ProductBL();
        List<Product>? lstproduct;
        Staff currentStaff;
        string[] MainMenu = { "Create Order", "Update Order", "Update Product Status Instock", "Payment", "Check Out", "About" };

        bool active = true;
        uI.Introduction();
        while (active)
        {
            string UserName;
            uI.ApplicationLogoBeforeLogin();
            uI.Title("LOGIN");
            uI.GreenMessage("Input User name and password to LOGIN or input User Name = 0 to EXIT.");
            Console.Write("User Name: ");
            UserName = Console.ReadLine();
            if (UserName == "0")
            {
                break;
            }
            else
            {
                Console.Write("Password: ");
                string password = Ultilities.GetPassword();
                string passwordMD5 = Ultilities.ChangePasswordMD5(password);
                currentStaff = staffBL.CheckAuthorize(UserName, passwordMD5);
            }

            if (currentStaff != null)
            {
                currentStaff.LoginTime = DateTime.Now;
                bool login = true;
                uI.WelcomeStaff(currentStaff);
                bool checkNull = staffBL.CheckNullableInLogindetails();
                if (checkNull == false)
                {
                    Staff lastStaff = staffBL.GetLastStaffLogOut();
                    if (lastStaff != null)
                    {
                        if (currentStaff.StaffId != lastStaff.StaffId)
                        {
                            if (currentStaff.LoginTime.Day == lastStaff.LogoutTime.Day)
                            {
                                List<Order> listOrderInBarUnComplete = orderBL.GetOrdersInBar();
                                List<Order> listOrderTakeAwayUnComplete = orderBL.GetTakeAwayOrders();
                                uI.PrintListOrderInProgress(listOrderInBarUnComplete, listOrderTakeAwayUnComplete, currentStaff, "LOGIN");
                                List<Order> listOrderComplete = orderBL.GetOrdersCompleteInDay();
                                decimal totalAmountInShop = 0;
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
                                string ask = uI.Ask($"This is the information of staff {staffBL.GetStaffById(lastStaff.StaffId).StaffName} for the previous shift, are you sure it is correct?");
                                switch (ask)
                                {
                                    case "Yes":
                                        login = true;
                                        uI.GreenMessage("Thank you for your confirmation, have a good day !");
                                        staffBL.InsertNewLoginDetails(currentStaff);
                                        uI.PressAnyKeyToContinue();
                                        break;
                                    case "No":
                                        login = true;
                                        while (true)
                                        {
                                            uI.PrintListOrderInProgress(listOrderInBarUnComplete, listOrderTakeAwayUnComplete, currentStaff, "LOGIN");
                                            AnsiConsole.Markup("[green]Enter the problem you encountered and press [red]ENTER[/] to continue[/]: ");
                                            string problem = Console.ReadLine();
                                            string problemTrimed = problem.Trim();
                                            if (problemTrimed.Length > 0)
                                            {
                                                staffBL.InsertNewLoginDetails(currentStaff);
                                                AnsiConsole.Markup("Update Problem: " + (staffBL.InsertProblemLogin(problem) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                                uI.PressAnyKeyToContinue();
                                                break;
                                            }
                                            else
                                            {
                                                uI.RedMessage("Invalid String !");
                                            }
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                staffBL.InsertNewLoginDetails(currentStaff);
                            }
                        }
                        else
                        {
                            staffBL.InsertNewLoginDetails(currentStaff);
                        }
                    }
                }
                else
                {
                    staffBL.InsertNewLoginDetails(currentStaff);
                }
                while (login)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    uI.ApplicationLogoAfterLogin(currentStaff);
                    string MainMenuChoice = uI.Menu("MAIN MENU", MainMenu);

                    switch (MainMenuChoice)
                    {
                        case "Create Order":
                            Ultilities.CreateOrder(currentStaff);
                            break;
                        case "Update Order":
                            Ultilities.UpdateOrder(currentStaff);
                            break;
                        case "Payment":
                            Ultilities.Payment(currentStaff);
                            break;
                        case "Update Product Status Instock":
                            Ultilities.UpdateProductStatusInstock(currentStaff);
                            break;
                        case "Check Out":
                            bool checkOut = Ultilities.CheckOut(currentStaff);
                            if (checkOut)
                            {
                                login = false;
                                break;
                            }
                            else
                            {
                                login = true;
                            }
                            break;
                        case "About":
                            uI.About(currentStaff);
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("");
                uI.RedMessage("Invalid Username or Password ! Please re-enter.");
            }
        }

    }
}