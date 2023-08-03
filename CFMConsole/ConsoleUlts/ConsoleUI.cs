using System.IO.Compression;
using BL;
using Persistence;
using Spectre.Console;
using System.Globalization;


namespace UI
{
    public class ConsoleUI
    {
        SizeBL sizeBL = new SizeBL();
        ProductBL productBL = new ProductBL();
        TableBL tableBL = new TableBL();
        // Line
        public void Line()
        {
            var rule = new Rule();
            AnsiConsole.Write(rule.DoubleBorder());
        }

        //Time Line
        public string TimeLineCreateOrderContent(int step)
        {
            string content = "";
            switch (step)
            {
                case 1:
                    content = "[green]CHOOSE PRODUCT[/] ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> CHOOSE TABLE ==> COMPLETE ORDER";
                    break;
                case 2:
                    content = "CHOOSE PRODUCT ==> [green]CHOOSE PRODUCT SIZE[/] ==> INPUT QUANTITY ==> CHOOSE TABLE ==> COMPLETE ORDER";
                    break;
                case 3:
                    content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> [green]INPUT QUANTITY[/] ==> CHOOSE TABLE ==> COMPLETE ORDER";
                    break;
                case 4:
                    content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> [green]CHOOSE TABLE[/] ==> COMPLETE ORDER";
                    break;
                case 5:
                    content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> CHOOSE TABLE ==> [green]COMPLETE ORDER[/]";
                    break;
            }
            return content;
        }

        public void TimeLine(string content)
        {
            Line();
            var table = new Spectre.Console.Table();
            table.AddColumn($"{content}").Centered();
            AnsiConsole.Write(table.NoBorder());
            Line();
        }

        //PressAnyKeyToContinue
        public void PressAnyKeyToContinue()
        {
            AnsiConsole.Markup("[Yellow]Press any key to continue[/]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        //Logo
        public void ApplicationLogoAfterLogin(Staff staff)
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn(@"
             ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗             
            ██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗            
            ██║     ███████╗██╔████╔██║    ███████║██████╔╝██████╔╝            
            ██║     ╚════██║██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝             
            ╚██████╗███████║██║ ╚═╝ ██║    ██║  ██║██║     ██║                 
             ╚═════╝╚══════╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝                 
            ----------------------------+--------------------------            ").Centered()).RoundedBorder().Centered();
            table.AddRow($"[Green]{staff.StaffName + " - ID: " + staff.StaffId}[/]");

            AnsiConsole.Write(table);
        }


        public void ApplicationLogoBeforeLogin()
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn(@"
             ██████╗███████╗███╗   ███╗     █████╗ ██████╗ ██████╗             
            ██╔════╝██╔════╝████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗            
            ██║     ███████╗██╔████╔██║    ███████║██████╔╝██████╔╝            
            ██║     ╚════██║██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝             
            ╚██████╗███████║██║ ╚═╝ ██║    ██║  ██║██║     ██║                 
             ╚═════╝╚══════╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝                 
            ----------------------------+--------------------------            ").Centered()).RoundedBorder().Centered();
            AnsiConsole.Write(table);
        }

        public void Introduction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            TitleNoBorder(@"
                ██████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗ ██████╗████████╗    ██████╗ ██╗   ██╗                
                ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██║   ██║██╔════╝╚══██╔══╝    ██╔══██╗╚██╗ ██╔╝                
                ██████╔╝██████╔╝██║   ██║██║  ██║██║   ██║██║        ██║       ██████╔╝ ╚████╔╝                 
                ██╔═══╝ ██╔══██╗██║   ██║██║  ██║██║   ██║██║        ██║       ██╔══██╗  ╚██╔╝                  
                ██║     ██║  ██║╚██████╔╝██████╔╝╚██████╔╝╚██████╗   ██║       ██████╔╝   ██║                   
                ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝  ╚═════╝   ╚═╝       ╚═════╝    ╚═╝                   
");
            Thread.Sleep(1000);
            Console.Clear();
            TitleNoBorder(@"
            ██╗   ██╗████████╗ ██████╗     █████╗  ██████╗ █████╗ ██████╗ ███████╗███╗   ███╗██╗   ██╗            
            ██║   ██║╚══██╔══╝██╔════╝    ██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝████╗ ████║╚██╗ ██╔╝            
            ██║   ██║   ██║   ██║         ███████║██║     ███████║██║  ██║█████╗  ██╔████╔██║ ╚████╔╝             
            ╚██╗ ██╔╝   ██║   ██║         ██╔══██║██║     ██╔══██║██║  ██║██╔══╝  ██║╚██╔╝██║  ╚██╔╝              
             ╚████╔╝    ██║   ╚██████╗    ██║  ██║╚██████╗██║  ██║██████╔╝███████╗██║ ╚═╝ ██║   ██║               
              ╚═══╝     ╚═╝    ╚═════╝    ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝     ╚═╝   ╚═╝               

");
            Thread.Sleep(1000);
            Console.Clear();
            TitleNoBorder(@"
                                 ██████╗███████╗███╗   ███╗                             
                                ██╔════╝██╔════╝████╗ ████║                             
                                ██║     ███████╗██╔████╔██║                             
                                ██║     ╚════██║██║╚██╔╝██║                             
                                ╚██████╗███████║██║ ╚═╝ ██║                             
                                 ╚═════╝╚══════╝╚═╝     ╚═╝                             
                                                                                        
 ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗     █████╗ ██████╗ ██████╗ 
██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ██╔══██╗██╔══██╗██╔══██╗
██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ███████║██████╔╝██████╔╝
██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██╔══██║██╔═══╝ ██╔═══╝ 
╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║  ██║██║     ██║     
 ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝     
");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Menu Handle

        public string Menu(string? title, string[] item)
        {
            if (title != null)
            {
                Title(title);
            }
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Move [green]UP/DOWN[/] button and [Green] ENTER[/] to select function")
               .PageSize(10)
               .AddChoices(item));
            return choice;
        }

        //Title
        public void Title(string title)
        {
            // Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn($"{title}").Centered()).RoundedBorder().Centered();
            AnsiConsole.Write(table);
        }

        public void TitleNoBorder(string title)
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn($"{title}").Centered()).NoBorder().Centered();
            AnsiConsole.Write(table);
        }

        //Product Handle

        public void PrintProductsTable(List<Product> prlst, Staff staff)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title("CREATE ORDER");
                TimeLine(TimeLineCreateOrderContent(1));
                var table = new Spectre.Console.Table();
                table.AddColumn(new TableColumn("Product ID").Centered());
                table.AddColumn(new TableColumn("Product Name").LeftAligned());
                table.AddColumn(new TableColumn("Size S").Centered());
                table.AddColumn(new TableColumn("Size M").Centered());
                table.AddColumn(new TableColumn("Size L").Centered());
                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, prlst.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    decimal priceSize1 = sizeBL.GetSizeSByProductID(prlst[i].ProductId).SizePrice;
                    decimal priceSize2 = sizeBL.GetSizeMByProductID(prlst[i].ProductId).SizePrice;
                    decimal priceSize3 = sizeBL.GetSizeLByProductID(prlst[i].ProductId).SizePrice;
                    string formattepricesize1 = priceSize1.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string formattepricesize2 = priceSize2.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string formattepricesize3 = priceSize3.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    table.AddRow($"{prlst[i].ProductId}", $"{prlst[i].ProductName}", $"{formattepricesize1 + " VND"}", $"{formattepricesize2 + " VND"}", $"{formattepricesize3 + " VND"}");
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)prlst.Count / pageSize)}" + ">");
                AnsiConsole.Write(Pagination.Centered().NoBorder());
                AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to choose product by PRODUCT ID or input [Green]PRODUCT ID = 0[/] to exit.\n");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentPage > 1)
                    {
                        currentPage--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentPage < Math.Ceiling((double)prlst.Count / pageSize))
                    {
                        currentPage++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }

        public int InputQuantity(Product product, Staff staff, string title)
        {
            int quantity = 0;
            bool err = false;
            bool active = true;
            while (active)
            {

                do
                {
                    Console.Clear();
                    ApplicationLogoAfterLogin(staff);
                    Title(title);
                    TimeLine(TimeLineCreateOrderContent(3));
                    Console.WriteLine("Product ID : " + product.ProductId);
                    Console.WriteLine("Product Name : " + product.ProductName);
                    Console.WriteLine("Product Size : " + product.ProductSize);
                    string formattepricesize = product.ProductPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    Console.WriteLine("Unit Price : " + formattepricesize + " VND");
                    Console.Write("Input Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        return quantity;
                    }
                    else
                    {
                        err = true;
                        RedMessage("Invalid quantity ! Please re-enter.");
                    }
                } while (err == false);
            }
            return quantity;
        }

        // Message Color
        public void RedMessage(string message)
        {
            AnsiConsole.Markup($"[underline red]{message}[/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            PressAnyKeyToContinue();
            Console.Clear();
        }

        public void GreenMessage(string message)
        {
            Console.WriteLine();
            AnsiConsole.Markup($"[underline green]{message}[/] ");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintProductSizeInfo(Persistence.Size size)
        {
            string Size = string.Format("{00:##'.'### VND}", size.SizePrice);
            Console.WriteLine("Size : " + size.SizeProduct + "  price :" + Size);
        }


        //Staff
        public void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TitleNoBorder("Welcome " + staff.StaffName);
            Thread.Sleep(1000);
        }

        public void PrintTables(List<Persistence.Table> listTable)
        {
            Console.Write("Id of current empty tables:");
            foreach (Persistence.Table table in listTable)
            {
                Console.Write(table.TableId + "  ");
            }
        }
        public int ChooseTable(Staff staff, string title)
        {
            int tableId = 0;
            bool active = true;
            List<Persistence.Table> listTable = tableBL.GetAll();
            bool checkTableId;
            while (active)
            {

                if (listTable.Count() == 0)
                {
                    ApplicationLogoAfterLogin(staff);
                    Title(title);
                    TimeLine(TimeLineCreateOrderContent(4));
                    AnsiConsole.Markup("There are currently no tables available, press [Green]ENTER[/] button to continue creating takeout order. ");
                    return 0;
                }
                else
                {
                    do
                    {
                        ApplicationLogoAfterLogin(staff);
                        Title(title);
                        TimeLine(TimeLineCreateOrderContent(4));
                        PrintTables(listTable);
                        AnsiConsole.Markup("\nInput [Green]TABLE ID[/] to choose table or input [green]0[/] to creating takeout order: ");
                        if (int.TryParse(Console.ReadLine(), out tableId) && tableId >= 0)
                        {
                            if (tableId == 0)
                            {
                                return 0;
                            }
                            else
                            {
                                if (tableBL.GetTableById(tableId).TableId == 0)
                                {
                                    RedMessage("Invalid Id, please re-enter Table ID");
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
                            RedMessage("Invalid Id, please re-enter Table ID");
                        }
                    }
                    while (checkTableId);
                }
            }
            return tableId;
        }
        public int ChooseProductsize(Staff staff, int productId, string title)
        {
            string size;
            int sizeId = 0;
            bool showAlert = false;
            bool active = true;
            bool err = false;
            while (active)
            {

                do
                {
                    Console.Clear();
                    ApplicationLogoAfterLogin(staff);
                    Title(title);
                    TimeLine(TimeLineCreateOrderContent(2));
                    Product currentProduct = productBL.GetProductById(productId);
                    Console.WriteLine("Product ID: " + currentProduct.ProductId);
                    Console.WriteLine("Product Name: " + currentProduct.ProductName);
                    AnsiConsole.Markup("Choose Product Size(Input [Green]S[/], [Green]M[/] or [Green]L[/] to choose [Green]PRODUCT SIZE[/]):");
                    size = Console.ReadLine().ToUpper();
                    if (size == "S" || size == "M" || size == "L")
                    {
                        switch (size)
                        {
                            case "S":
                                return 1;
                            case "M":
                                return 2;
                            case "L":
                                return 3;
                        }
                    }
                    else
                    {
                        showAlert = true;
                        err = true;
                    }
                    if (showAlert)
                    {
                        RedMessage("Invalid choice. Please Re-enter");
                    }
                } while (err == false);
            }
            return sizeId;
        }

        public void PrintSaleReceipt(Order order, Staff staff, string title)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(staff);
            Title(title);
            TimeLine(TimeLineCreateOrderContent(5));
            var warp = new Spectre.Console.Table();
            warp.AddColumn(new TableColumn("[Bold]SALE RECEIPT[/]").Centered());
            var outerTable = new Spectre.Console.Table();
            outerTable.AddColumn(new TableColumn("[Bold]VTCA Coffee[/]").Centered().NoWrap());
            outerTable.AddRow("4th floor, VTC Building, 18 Tam Trinh, HBT, HN").Centered();
            outerTable.AddRow("Email: VTCACoffee@gmail.com").Centered();
            outerTable.AddRow("Order Staff: " + $"{staff.StaffName}").Centered();
            outerTable.AddRow("Table: " + $" {order.TableID}").Centered();
            outerTable.AddRow("");
            var innertable = new Spectre.Console.Table();
            innertable.AddColumn(new TableColumn("Product Name").LeftAligned()).NoBorder();
            innertable.AddColumn(new TableColumn("Size").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Price").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Quantity").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Amount").Centered()).NoBorder();
            decimal totalAmount = 0;
            foreach (var item in order.ProductsList)
            {
                decimal amount = item.ProductPrice * item.ProductQuantity;
                string formattedAmount = amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                string formattedPrice = item.ProductPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                innertable.AddRow($"{item.ProductName}", $"({item.ProductSize})", $"{formattedPrice + " VND"}", $"x{item.ProductQuantity}", $"{formattedAmount + " VND"}").NoBorder();
                totalAmount += item.ProductQuantity * item.ProductPrice;
            }
            string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            innertable.AddRow("");
            innertable.AddRow("[Bold]TOTAL AMOUNT[/]", "", "", "", $"{formattedTotal + " VND"}");
            warp.AddRow(outerTable.NoBorder());
            warp.AddRow(innertable.Centered());
            AnsiConsole.Write(warp.Centered());
        }

        public void PrintSaleReceiptTakeAway(Order order, Staff staff, string title)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(staff);
            Title(title);
            TimeLine(TimeLineCreateOrderContent(5));
            var warp = new Spectre.Console.Table();
            warp.AddColumn(new TableColumn("[Bold]SALE RECEIPT[/]").Centered());
            var outerTable = new Spectre.Console.Table();
            outerTable.AddColumn(new TableColumn("[Bold]VTCA Coffee[/]").Centered().NoWrap());
            outerTable.AddRow("4th floor, VTC Building, 18 Tam Trinh, HBT, HN").Centered();
            outerTable.AddRow("Email: VTCACoffee@gmail.com").Centered();
            outerTable.AddRow("Order Staff: " + $"{staff.StaffName}").Centered();
            outerTable.AddRow("TakeAway").Centered();
            outerTable.AddRow("");
            var innertable = new Spectre.Console.Table();
            innertable.AddColumn(new TableColumn("Product Name").LeftAligned()).NoBorder();
            innertable.AddColumn(new TableColumn("Size").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Price").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Quantity").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("Amount").Centered()).NoBorder();
            decimal totalAmount = 0;
            foreach (var item in order.ProductsList)
            {
                decimal amount = item.ProductPrice * item.ProductQuantity;
                string formattedAmount = amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                string formattedPrice = item.ProductPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                innertable.AddRow($"{item.ProductName}", $"({item.ProductSize})", $"{formattedPrice + " VND"}", $"x{item.ProductQuantity}", $"{formattedAmount + " VND"}").NoBorder();
                totalAmount += item.ProductQuantity * item.ProductPrice;
            }
            string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            innertable.AddRow("");
            innertable.AddRow("[Bold]TOTAL AMOUNT[/]", "", "", "", $"{formattedTotal + " VND"}");
            warp.AddRow(outerTable.NoBorder());
            warp.AddRow(innertable.Centered());
            AnsiConsole.Write(warp.Centered());
        }

        //ask
        public string AskToContinueAdd()
        {
            string[] item = { "Yes", "No" };
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("[Green]Add product to order complete[/], do you want to [Green]CONTINUE[/] to add product ?")
               .PageSize(3)
               .AddChoices(item));
            return choice;
        }

        public string AskToContinueCreate()
        {
            string[] item = { "Yes", "No" };
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Do you want to [Green]CREATE[/] this order ?")
               .PageSize(3)
               .AddChoices(item));
            return choice;
        }

        public void About(Staff orderStaff)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(orderStaff);
            Title("ABOUT");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Coffee Shop Management Application");
            Console.WriteLine("Version: Beta_0.2.2");
            Console.WriteLine("Made By : Nguyen Ngoc Duc, Nguyen Thi Khanh Ly");
            Console.WriteLine("Instructor: Nguyen Xuan Sinh");
            PressAnyKeyToContinue();
        }

        public void PrintListOrder(List<Order> listOrder, Staff staff, string title)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại
            while (true)
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                var table = new Spectre.Console.Table();
                table.AddColumn(new TableColumn("Order ID").Centered());
                table.AddColumn(new TableColumn("Order Staff ID").LeftAligned());
                table.AddColumn(new TableColumn("Order Date").Centered());
                table.AddColumn(new TableColumn("Order Table").Centered());
                table.AddColumn(new TableColumn("Order Status").Centered());

                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, listOrder.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (listOrder[i].TableID == 0)
                    {
                        table.AddRow($"{listOrder[i].OrderId}", $"{listOrder[i].OrderStaffID}", $"{listOrder[i].OrderDate}", "Take Away", $"{listOrder[i].OrderStatus}");
                    }
                    else
                    {
                        table.AddRow($"{listOrder[i].OrderId}", $"{listOrder[i].OrderStaffID}", $"{listOrder[i].OrderDate}", $"{listOrder[i].TableID}", $"{listOrder[i].OrderStatus}");
                    }
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)listOrder.Count / pageSize)}" + ">");
                AnsiConsole.Write(Pagination.Centered().NoBorder());
                AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to choose order by ORDER ID or input [Green]ORDEER ID = 0[/] to exit.\n");
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentPage > 1)
                    {
                        currentPage--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentPage < Math.Ceiling((double)listOrder.Count / pageSize))
                    {
                        currentPage++;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        }
    }
}
