﻿using Ultilities;
using Persistence;
using Spectre.Console;
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
                currentStaff = staffBL.GetPasswordAndCheckAuthorize(UserName);
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
                                List<Order> listOrderInBarUnComplete = orderBL.GetOrdersInBarInprogress();
                                List<Order> listOrderTakeAwayUnComplete = orderBL.GetTakeAwayOrdersInprogress();
                                uI.PrintListOrderInProgress(listOrderInBarUnComplete, listOrderTakeAwayUnComplete, currentStaff, "LOGIN");
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
                                        login = false;
                                        uI.RedMessage($"Please contact the staff of the previous shift ({staffBL.GetStaffById(lastStaff.StaffId).StaffName}) to confirm the information.");
                                        break;
                                }
                            }
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
                            List<Order> listOrderInBarUnComplete = orderBL.GetOrdersInBarInprogress();
                            List<Order> listOrderTakeAwayUnComplete = orderBL.GetTakeAwayOrdersInprogress();
                            uI.PrintListOrderInProgress(listOrderInBarUnComplete, listOrderTakeAwayUnComplete, currentStaff, "CHECK OUT");
                            string checkOut = uI.Ask("Do you want to check out ?");
                            switch (checkOut)
                            {
                                case "Yes":
                                    currentStaff.LogoutTime = DateTime.Now;
                                    AnsiConsole.Markup("Check out: " + (staffBL.UpdateLogoutTimeForStaff(currentStaff) ? "[Green]SUCCESS[/] !\n" : "[Red]WRONG[/] !\n"));
                                    login = false;
                                    break;
                                case "No":
                                    break;
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