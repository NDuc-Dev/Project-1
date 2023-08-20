using BL;
using Persistence;
using Spectre.Console;
using System.Globalization;
using System.Xml;

namespace UI
{
    public class ConsoleUI
    {
        SizeBL sizeBL = new SizeBL();
        ProductBL productBL = new ProductBL();
        TableBL tableBL = new TableBL();
        StaffBL staffBL = new StaffBL();
        public void Line()
        {
            var rule = new Rule();
            AnsiConsole.Write(rule.DoubleBorder());
        }

        public string TimeLineContent(int step, string title)
        {
            string content = "";
            switch (title)
            {
                case "CREATE ORDER":
                    switch (step)
                    {
                        case 1:
                            content = "[green]CHOOSE TABLE[/] ==> CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE ORDER";
                            break;
                        case 2:
                            content = "CHOOSE TABLE ==> [green]CHOOSE PRODUCT[/] ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE ORDER";
                            break;
                        case 3:
                            content = "CHOOSE TABLE ==> CHOOSE PRODUCT ==> [green]CHOOSE PRODUCT SIZE[/] ==> INPUT QUANTITY ==> COMPLETE ORDER";
                            break;
                        case 4:
                            content = "CHOOSE TABLE ==> CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> [green]INPUT QUANTITY[/] ==> COMPLETE ORDER";
                            break;
                        case 5:
                            content = "CHOOSE TABLE ==> CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> [green]COMPLETE ORDER[/]";
                            break;
                    }
                    break;
                case "CHANGE PRODUCT IN ORDER":
                    switch (step)
                    {
                        case 1:
                            content = "[green]CHOOSE PRODUCT REMOVE[/]  ==> CHOOSE NEW PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE";
                            break;
                        case 2:
                            content = "CHOOSE PRODUCT REMOVE ==> [green]CHOOSE NEW PRODUCT[/] ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE";
                            break;
                        case 3:
                            content = "CHOOSE PRODUCT REMOVE ==> CHOOSE NEW PRODUCT ==> [green]CHOOSE PRODUCT SIZE[/] ==> INPUT QUANTITY ==> COMPLETE";
                            break;
                        case 4:
                            content = "CHOOSE PRODUCT REMOVE ==> CHOOSE NEW PRODUCT ==> CHOOSE PRODUCT SIZE ==> [green]INPUT QUANTITY[/] ==> COMPLETE";
                            break;
                        case 5:
                            content = "CHOOSE PRODUCT REMOVE ==> CHOOSE NEW PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> [green]COMPLETE[/]";
                            break;
                    }
                    break;
                case "REMOVE PRODUCT IN ORDER":
                    switch (step)
                    {
                        case 1:
                            content = "[green]CHOOSE PRODUCT[/] ==> COMPLETE ORDER";
                            break;
                        case 2:
                            content = "CHOOSE PRODUCT ==> [green]COMPLETE ORDER[/]";
                            break;
                    }
                    break;
                case "ADD PRODUCT TO ORDER":
                    switch (step)
                    {
                        case 1:
                            content = "[green]CHOOSE PRODUCT[/]  ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE ADD";
                            break;
                        case 2:
                            content = "CHOOSE PRODUCT ==> [green]CHOOSE PRODUCT SIZE[/] ==> INPUT QUANTITY ==> COMPLETE ADD";
                            break;
                        case 3:
                            content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> [green]INPUT QUANTITY[/] ==> COMPLETE ADD";
                            break;
                        case 4:
                            content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> [green]COMPLETE ADD[/]";
                            break;
                    }
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
        public void PrintProductTable(Product product)
        {
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn("Product ID").Centered());
            table.AddColumn(new TableColumn("Product Name").LeftAligned());
            table.AddColumn(new TableColumn("Size S").Centered());
            table.AddColumn(new TableColumn("Size M").Centered());
            table.AddColumn(new TableColumn("Size L").Centered());
            decimal priceSize1 = sizeBL.GetSizeSByProductID(product.ProductId).SizePrice;
            decimal priceSize2 = sizeBL.GetSizeMByProductID(product.ProductId).SizePrice;
            decimal priceSize3 = sizeBL.GetSizeLByProductID(product.ProductId).SizePrice;
            string formattepricesize1 = priceSize1.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            string formattepricesize2 = priceSize2.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            string formattepricesize3 = priceSize3.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            table.AddRow($"{product.ProductId}", $"{product.ProductName}", $"{formattepricesize1 + " VND"}", $"{formattepricesize2 + " VND"}", $"{formattepricesize3 + " VND"}");
            AnsiConsole.Write(table.Centered());
        }

        public void PrintNewProductsTable(List<Product> prlst, Staff staff, string title)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                if (title == "CREATE ORDER")
                {
                    TimeLine(TimeLineContent(2, "CREATE ORDER"));
                }
                else if (title == "ADD PRODUCT TO ORDER")
                {
                    TimeLine(TimeLineContent(1, "ADD PRODUCT TO ORDER"));
                }
                else if (title == "CHANGE PRODUCT IN ORDER")
                {
                    TimeLine(TimeLineContent(2, "CHANGE PRODUCT IN ORDER"));
                }
                var table = new Spectre.Console.Table();
                table.AddColumn(new TableColumn("No").Centered());
                table.AddColumn(new TableColumn("Product Name").LeftAligned());
                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, prlst.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    table.AddRow($"{i + 1}", $"{prlst[i].ProductName}");
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)prlst.Count / pageSize)}" + ">");
                AnsiConsole.Write(Pagination.Centered().NoBorder());
                AnsiConsole.Markup(@"Press the [Green]BUTTON (←)[/] to go back to the previous page, the [Green]BUTTON (→)[/] to go to the next page
[Green]ENTER[/] to choose product by [green]PRODUCT NUMBER[/] or input [Green]PRODUCT NUMBER = 0[/] to [red]EXIT[/].");
                Console.WriteLine();
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

        public void PrintAllProductsInstock(List<Product> productList, Staff staff, string title)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                var table = new Spectre.Console.Table();
                table.AddColumn(new TableColumn("Product ID").Centered());
                table.AddColumn(new TableColumn("Product Name").LeftAligned());
                table.AddColumn(new TableColumn("Size S").Centered());
                table.AddColumn(new TableColumn("Size M").Centered());
                table.AddColumn(new TableColumn("Size L").Centered());
                table.AddColumn(new TableColumn("Status Instock").Centered());
                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, productList.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    decimal priceSize1 = sizeBL.GetSizeSByProductID(productList[i].ProductId).SizePrice;
                    decimal priceSize2 = sizeBL.GetSizeMByProductID(productList[i].ProductId).SizePrice;
                    decimal priceSize3 = sizeBL.GetSizeLByProductID(productList[i].ProductId).SizePrice;
                    string formattepricesize1 = priceSize1.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string formattepricesize2 = priceSize2.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string formattepricesize3 = priceSize3.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    if (productList[i].ProductStatus == 0)
                    {
                        table.AddRow($"{productList[i].ProductId}", $"{productList[i].ProductName}", $"{formattepricesize1 + " VND"}", $"{formattepricesize2 + " VND"}", $"{formattepricesize3 + " VND"}", "[Red]OUT OF STOCK[/]");
                    }
                    else
                    {
                        table.AddRow($"{productList[i].ProductId}", $"{productList[i].ProductName}", $"{formattepricesize1 + " VND"}", $"{formattepricesize2 + " VND"}", $"{formattepricesize3 + " VND"}", "[Green]INSTOCK[/]");
                    }
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)productList.Count / pageSize)}" + ">");
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
                    if (currentPage < Math.Ceiling((double)productList.Count / pageSize))
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
            bool active = true;
            while (active)
            {

                bool err = false;
                bool continuee = false;
                do
                {
                    Console.Clear();
                    ApplicationLogoAfterLogin(staff);
                    Title(title);
                    if (title == "CREATE ORDER")
                    {
                        TimeLine(TimeLineContent(4, "CREATE ORDER"));
                    }
                    else if (title == "ADD PRODUCT TO ORDER")
                    {
                        TimeLine(TimeLineContent(3, "ADD PRODUCT TO ORDER"));
                    }
                    else if (title == "CHANGE PRODUCT IN ORDER")
                    {
                        TimeLine(TimeLineContent(4, "CHANGE PRODUCT IN ORDER"));
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
                            string askAceptQuantity = Ask("[green]Quantity > 10[/], Are you sure ?");
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
                        // return quantity;
                    }
                    else
                    {
                        err = true;
                        RedMessage("Invalid quantity ! Please re-enter.");
                    }
                } while (err == false && continuee);
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

        //Staff
        public void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            TitleNoBorder($"[green]Welcome {staff.StaffName}[/]");
            Thread.Sleep(1000);
        }

        public void PrintAllTables(List<Persistence.Table> listTable)
        {
            var tableprint = new Spectre.Console.Table();
            tableprint.AddColumn(new TableColumn("Table ID").Centered());
            tableprint.AddColumn(new TableColumn("Table Status").Centered());
            foreach (Persistence.Table table in listTable)
            {
                string status;
                if (table.TableStatus == 1)
                {
                    status = "[Red]Using[/]";
                }
                else
                {
                    status = "[green]Empty Table[/]";
                }
                tableprint.AddRow($"{table.TableId}", $"{status}");
            }
            AnsiConsole.Write(tableprint.Centered());
        }

        public int ChooseTable(Staff staff, string title, int tableOrder)
        {
            int tableId = 0;
            List<Persistence.Table> listTable = tableBL.GetAll();
            bool checkTableId;

            ApplicationLogoAfterLogin(staff);
            Title(title);
            if (title == "CREATE ORDER")
            {
                TimeLine(TimeLineContent(1, "CREATE ORDER"));
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
                            do
                            {
                                ApplicationLogoAfterLogin(staff);
                                Title(title);
                                TimeLine(TimeLineContent(1, "CREATE ORDER"));
                                PrintAllTables(listTable);
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
                                            RedMessage("Table is using, please re-enter Table ID.");
                                            checkTableId = false;
                                        }
                                        else if (tableBL.GetTableById(tableId).TableId == 0)
                                        {
                                            RedMessage("Invalid Id, please re-enter Table ID.");
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
                    PrintAllTables(listTable);
                    AnsiConsole.Markup($"\nCurrent [Green]TABLE ID[/] of this order is [green]{tableOrder}[/].");
                    AnsiConsole.Markup("\nInput [Green]TABLE ID[/] to choose table to change or input [green]0[/] to exit: ");
                    if (int.TryParse(Console.ReadLine(), out tableId) && tableId > 0)
                    {
                        if (tableId == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            if (tableBL.GetTableById(tableId).TableStatus == 1)
                            {
                                RedMessage("Table is using, please re-enter Table ID.");
                                checkTableId = false;
                            }
                            else if (tableBL.GetTableById(tableId).TableId == 0)
                            {
                                RedMessage("Invalid Id, please re-enter Table ID.");
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

            return tableId;
        }
        public int ChooseProductsize(Staff staff, int productId, string title)
        {
            string[] size = { "Size S", "Size M", "Size L", "Choose another product" };
            int sizeId = 0;
            bool active = true;
            while (active)
            {
                ApplicationLogoAfterLogin(staff);
                Title(title);
                if (title == "CREATE ORDER")
                {
                    TimeLine(TimeLineContent(3, "CREATE ORDER"));
                }
                else if (title == "ADD PRODUCT TO ORDER")
                {
                    TimeLine(TimeLineContent(2, "ADD PRODUCT TO ORDER"));
                }
                else if (title == "CHANGE PRODUCT IN ORDER")
                {
                    TimeLine(TimeLineContent(3, "CHANGE PRODUCT IN ORDER"));
                }
                PrintProductTable(productBL.GetProductById(productId));
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

        public void PrintSaleReceipt(Order order, Staff currentstaff, Staff staff, string title)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(currentstaff);
            Title(title);
            if (title == "CREATE ORDER")
            {
                TimeLine(TimeLineContent(5, "CREATE ORDER"));
            }
            var warp = new Spectre.Console.Table();
            warp.AddColumn(new TableColumn("[Bold]SALE RECEIPT[/]").Centered());
            var outerTable = new Spectre.Console.Table();
            outerTable.AddColumn(new TableColumn("[Bold][Blue]VTCA Coffee[/][/]").Centered().NoWrap());
            outerTable.AddRow("4th floor, VTC Building, 18 Tam Trinh, HBT, HN").Centered();
            outerTable.AddRow("Email: VTCACoffee@gmail.com").Centered();
            outerTable.AddRow("Order Staff: " + $"{staff.StaffName}").Centered();
            if (order.TableID == 0)
            {
                outerTable.AddRow("TakeAway").Centered();
            }
            else
            {
                outerTable.AddRow("Table: " + $"{order.TableID}").Centered();
            }
            outerTable.AddRow("");
            var innertable = new Spectre.Console.Table();
            innertable.AddColumn(new TableColumn("[bold][Cyan]Product Name[/][/]").LeftAligned()).NoBorder();
            innertable.AddColumn(new TableColumn("[bold][Cyan]Size[/][/]").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("[bold][Cyan]Price[/][/]").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("[bold][Cyan]Quantity[/][/]").Centered()).NoBorder();
            innertable.AddColumn(new TableColumn("[bold][Cyan]Amount[/][/]").RightAligned()).NoBorder();
            decimal totalAmount = 0;
            foreach (var item in order.ProductsList)
            {
                decimal amount = productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice * item.ProductQuantity;
                string formattedAmount = amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                string formattedPrice = productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                innertable.AddRow($"{productBL.GetProductById(item.ProductId).ProductName}", $"({sizeBL.GetSizeByID(item.ProductSizeId).SizeProduct})", $"{formattedPrice + " VND"}", $"x{item.ProductQuantity}", $"{formattedAmount + " VND"}").NoBorder();
                totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
            }
            string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            innertable.AddRow("");
            innertable.AddRow("[Bold][cyan]TOTAL AMOUNT[/][/]", "", "", "", $"[Bold][cyan]{formattedTotal + " VND"}[/][/]");
            warp.AddRow(outerTable.NoBorder());
            warp.AddRow(innertable.Centered());
            if (title == "PAYMENT")
                warp.AddRow("[Green]THANK YOU AND SEE YOU AGAIN[/]");
            AnsiConsole.Write(warp.Centered());
        }

        //ask

        public string Ask(string ask)
        {
            string[] item = { "Yes", "No" };
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title($"{ask}")
               .PageSize(3)
               .AddChoices(item));
            return choice;
        }

        public string AskChangeStatus()
        {
            string[] item = { "Change Product Status to Out Of Stock", "Change Product Status to In Stock", "Exit" };
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .PageSize(3)
               .AddChoices(item));
            return choice;
        }

        public void About(Staff orderStaff)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(orderStaff);
            Title("ABOUT");
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn ("ABOUT").Centered());
            table.AddRow("Coffee Shop Management Application");
            table.AddRow("Version: V1.0.0");
            table.AddRow("Made By : Nguyen Ngoc Duc, Nguyen Thi Khanh Ly");
            table.AddRow("Instructor: Nguyen Xuan Sinh");
            AnsiConsole.Write(table.Centered());
            PressAnyKeyToContinue();
        }

        public void PrintListOrderTakeAway(List<Order> listOrder, Staff staff, string title)
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
                table.AddColumn(new TableColumn("Order Staff").Centered());
                table.AddColumn(new TableColumn("Order Date").Centered());
                table.AddColumn(new TableColumn("Order Status").Centered());

                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, listOrder.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    string status;
                    if (listOrder[i].OrderStatus == 1)
                    {
                        status = "[yellow]INPROGRESS[/]";
                    }
                    else if (listOrder[i].OrderStatus == 2)
                    {
                        status = "[Green]CONFIRMED[/]";
                    }
                    else
                    {
                        status = "[red]COMPLETE[/]";
                    }
                    table.AddRow($"{listOrder[i].OrderId}", $"{staffBL.GetStaffById(listOrder[i].OrderStaffID).StaffName}", $"{listOrder[i].OrderDate}", status);
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)listOrder.Count / pageSize)}" + ">");
                AnsiConsole.Write(Pagination.Centered().NoBorder());
                if (title == "CHECK OUT")
                {
                    AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to continue.\n");
                }
                else
                {
                    AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to choose order.\n");
                }
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

        public void PrintListOrderInBar(List<Order> listOrder, Staff staff, string title)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại
            while (true)
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                var table = new Spectre.Console.Table();
                table.AddColumn(new TableColumn("Table").Centered());
                table.AddColumn(new TableColumn("Order Staff").Centered());
                table.AddColumn(new TableColumn("Order Date").Centered());
                table.AddColumn(new TableColumn("Order Status").Centered());

                // Hiển thị sản phẩm trên trang hiện tại
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, listOrder.Count - 1);
                for (int i = startIndex; i <= endIndex; i++)
                {
                    string status;
                    if (listOrder[i].OrderStatus == 1)
                    {
                        status = "[yellow]INPROGRESS[/]";
                    }
                    else if (listOrder[i].OrderStatus == 2)
                    {
                        status = "[Green]CONFIRMED[/]";
                    }
                    else
                    {
                        status = "[red]COMPLETE[/]";
                    }
                    table.AddRow($"{listOrder[i].TableID}", $"{staffBL.GetStaffById(listOrder[i].OrderStaffID).StaffName}", $"{listOrder[i].OrderDate}", status);
                }
                AnsiConsole.Write(table.Centered());
                var Pagination = new Spectre.Console.Table();
                Pagination.AddColumn("<" + $"{currentPage}" + "/" + $"{Math.Ceiling((double)listOrder.Count / pageSize)}" + ">");
                AnsiConsole.Write(Pagination.Centered().NoBorder());
                if (title == "CHECK OUT")
                {
                    AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to continue.\n");
                }
                else
                {
                    AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page, [Green]ENTER[/] to choose order.\n");
                }
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

        public void PrintOrderDetails(List<Product> listProducts, Staff currentstaff, Persistence.Order order, string title, string staffName, int step)
        {

            Console.Clear();
            ApplicationLogoAfterLogin(currentstaff);
            Title(title);
            if (title == "REMOVE PRODUCT IN ORDER")
            {
                TimeLine(TimeLineContent(step, "REMOVE PRODUCT IN ORDER"));
            }
            else if (title == "ADD PRODUCT TO ORDER")
            {
                TimeLine(TimeLineContent(step, "ADD PRODUCT TO ORDER"));
            }
            else if (title == "CHANGE PRODUCT IN ORDER")
            {
                TimeLine(TimeLineContent(step, "CHANGE PRODUCT IN ORDER"));
            }
            var warp = new Spectre.Console.Table();
            warp.AddColumn(new TableColumn($"[Bold]{"Order Id: " + order.OrderId}[/]").Centered());
            if (order.TableID != 0)
            {
                warp.AddRow($"[Cyan]TABLE: {order.TableID}[/]").Centered();
            }
            else
            {
                warp.AddRow($"[Cyan]Take Away Order[/]").Centered();
            }
            var orderInfoTable = new Spectre.Console.Table();
            orderInfoTable.AddColumn(new TableColumn($"{"Order Staff: " + staffName}"));
            var datetimeTable = new Spectre.Console.Table();
            datetimeTable.AddColumn(new TableColumn($"Order Date: {order.OrderDate}").Centered()).Centered();
            if (order.OrderStatus == 1)
            {
                datetimeTable.AddRow($"[Bold][Yellow]Order Status: INPROGRESS[/][/]").Centered();
                datetimeTable.AddRow("");
            }
            else if (order.OrderStatus == 2)
            {
                datetimeTable.AddRow($"[Bold][Green]Order Status: CONFIRMED[/][/]").Centered();
                datetimeTable.AddRow("");
            }
            var productTable = new Spectre.Console.Table();
            productTable.AddColumn(new TableColumn("NO").LeftAligned());
            productTable.AddColumn(new TableColumn("Product Name").LeftAligned());
            productTable.AddColumn(new TableColumn("Product Size").Centered());
            productTable.AddColumn(new TableColumn("Quantity").Centered());
            productTable.AddColumn(new TableColumn("Status").Centered());
            for (int i = 0; i < listProducts.Count(); i++)
            {
                if (listProducts[i].StatusInOrder == 1)
                {
                    productTable.AddRow($"{i + 1}", $"{productBL.GetProductById(listProducts[i].ProductId).ProductName}", $"{sizeBL.GetSizeByID(listProducts[i].ProductSizeId).SizeProduct}", $"{listProducts[i].ProductQuantity}", "[bold][yellow]INPROGRESS[/][/]");
                }
                else
                {
                    productTable.AddRow($"{i + 1}", $"{productBL.GetProductById(listProducts[i].ProductId).ProductName}", $"{sizeBL.GetSizeByID(listProducts[i].ProductSizeId).SizeProduct}", $"{listProducts[i].ProductQuantity}", "[bold][green]COMPLTETE[/][/]");
                }
            }
            warp.AddRow(orderInfoTable.Centered().NoBorder());
            warp.AddRow(datetimeTable.Centered().NoBorder());
            warp.AddRow(productTable.Centered());

            AnsiConsole.Write(warp.Centered());
        }

        public string SellectFunction(string[] item)
        {
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Move [green]UP/DOWN[/] button and [Green] ENTER[/] to select function")
               .PageSize(10)
               .AddChoices(item));
            return choice;
        }

        public void PrintListOrderInProgress(List<Order> ordersInBar, List<Order> ordersTakeAway, Staff currentStaff, string title)
        {
            ApplicationLogoAfterLogin(currentStaff);
            Title(title);
            decimal totalInBar = 0;
            if (ordersInBar.Count > 0)
            {
                var tableOrdersInBar = new Spectre.Console.Table();
                tableOrdersInBar.AddColumn(new TableColumn("Table").Centered());
                tableOrdersInBar.AddColumn(new TableColumn("Order Staff").Centered());
                tableOrdersInBar.AddColumn(new TableColumn("Order Date").Centered());
                tableOrdersInBar.AddColumn(new TableColumn("Order Status").Centered());
                tableOrdersInBar.AddColumn(new TableColumn("Amount").RightAligned());
                foreach (var order in ordersInBar)
                {
                    order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                    decimal totalAmount = 0;
                    foreach (var item in order.ProductsList)
                    {
                        totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                    }
                    string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string status = "";
                    if (order.OrderStatus == 1)
                    {
                        status = "[yellow]INPROGRESS[/]";
                    }
                    else if (order.OrderStatus == 2)
                    {
                        status = "[Green]CONFIRMED[/]";
                    }
                    tableOrdersInBar.AddRow($"{order.TableID}", $"{staffBL.GetStaffById(order.OrderStaffID).StaffName}", $"{order.OrderDate}", status, $"[Green]{formattedTotal} VND[/]");
                    totalInBar += totalAmount;
                }
                AnsiConsole.Write(tableOrdersInBar.Centered());
            }
            decimal totalTakeAway = 0;
            if (ordersTakeAway.Count > 0)
            {
                var tableOrdersTakeAway = new Spectre.Console.Table();
                tableOrdersTakeAway.AddColumn(new TableColumn("Order ID").Centered());
                tableOrdersTakeAway.AddColumn(new TableColumn("Order Staff").Centered());
                tableOrdersTakeAway.AddColumn(new TableColumn("Order Date").Centered());
                tableOrdersTakeAway.AddColumn(new TableColumn("Order Status").Centered());
                tableOrdersTakeAway.AddColumn(new TableColumn("Amount").RightAligned());
                foreach (var order in ordersTakeAway)
                {
                    order.ProductsList = productBL.GetListProductsInOrder(order.OrderId);
                    decimal totalAmount = 0;
                    foreach (var item in order.ProductsList)
                    {
                        totalAmount += item.ProductQuantity * productBL.GetProductByIdAndSize(item.ProductId, item.ProductSizeId).ProductPrice;
                    }
                    string formattedTotal = totalAmount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                    string status = "";
                    if (order.OrderStatus == 1)
                    {
                        status = "[yellow]INPROGRESS[/]";
                    }
                    else if (order.OrderStatus == 2)
                    {
                        status = "[Green]CONFIRMED[/]";
                    }
                    tableOrdersTakeAway.AddRow($"{order.OrderId}", $"{staffBL.GetStaffById(order.OrderStaffID).StaffName}", $"{order.OrderDate}", status, $"[Green]{formattedTotal} VND[/]");
                    totalTakeAway += totalAmount;
                }
                AnsiConsole.Write(tableOrdersTakeAway.Centered());
            }
            decimal total = totalInBar + totalTakeAway;
            string totalFormat = total.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            AnsiConsole.Markup($"You have [green]{ordersInBar.Count}[/] unfinished in bar orders and [green]{ordersTakeAway.Count}[/] unpaid takeout orders.\n");
            AnsiConsole.Markup($"The total amount to be collected for unpaid orders is [green]{totalFormat} VND[/]\n");
        }
    }
}
