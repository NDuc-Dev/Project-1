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
                    content = "[green]CHOOSE PRODUCT[/] ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> COMPLETE ORDER";
                    break;
                case 2:
                    content = "CHOOSE PRODUCT ==> [green]CHOOSE PRODUCT SIZE[/] ==> INPUT QUANTITY ==> COMPLETE ORDER";
                    break;
                case 3:
                    content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> [green]INPUT QUANTITY[/] ==> COMPLETE ORDER";
                    break;
                case 4:
                    content = "CHOOSE PRODUCT ==> CHOOSE PRODUCT SIZE ==> INPUT QUANTITY ==> [green]COMPLETE ORDER[/]";
                    break;
            }
            return content;
        }

        public void TimeLine(string content)
        {
            Line();
            var table = new Table();
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
            var table = new Table();
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
            var table = new Table();
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
            // CurrentStaff(staff);
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
            var table = new Table();
            table.AddColumn(new TableColumn($"{title}").Centered()).RoundedBorder().Centered();
            AnsiConsole.Write(table);
        }

        public void TitleNoBorder(string title)
        {
            Console.Clear();
            var table = new Table();
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
                // CurrentStaff(orderStaff);
                TimeLine(TimeLineCreateOrderContent(1));
                var table = new Table();
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
                var Pagination = new Table();
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
            int quantity;
            do
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                TimeLine(TimeLineCreateOrderContent(3));
                Console.WriteLine("Product ID : " + product.ProductId);
                Console.WriteLine("Product Name : " + product.ProductName);
                Console.WriteLine("Product Size : " + product.ProductSize);
                Console.WriteLine("Unit Price : " + product.ProductPrice);
                Console.Write("Input Quantity: ");
                if (int.TryParse(Console.ReadLine(), out quantity))
                {
                    return quantity;
                }
                else
                {
                    RedMessage("Invalid quantity ! Please re-enter.");
                }
            } while (int.TryParse(Console.ReadLine(), out quantity));
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

        public void PrintSizes(List<Persistence.Size> lst)
        {
            foreach (var item in lst)
            {
                PrintProductSizeInfo(item);
            }
        }

        //Staff
        public void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TitleNoBorder("Welcome " + staff.StaffName);
            Thread.Sleep(1000);
        }

        public int ChooseProductsize(Staff staff, int productId, string title)
        {
            int sizeId = 0;
            bool showAlert = false;
            do
            {
                Console.Clear();
                ApplicationLogoAfterLogin(staff);
                Title(title);
                TimeLine(TimeLineCreateOrderContent(2));
                Product currentProduct = productBL.GetProductById(productId);
                Console.WriteLine("Product ID: " + currentProduct.ProductId);
                Console.WriteLine("Product Name: " + currentProduct.ProductName);
                AnsiConsole.Markup("Choose Product Size([Green]1 to choose S[/], [Green]2 to choose M[/], [Green]3 to choose L[/]):");
                int.TryParse(Console.ReadLine(), out sizeId);
                if (sizeId <= 0 || sizeId > 3)
                {
                    showAlert = true;
                }
                else
                {
                    return sizeId;
                }
                if (showAlert)
                {
                    RedMessage("Invalid choice. Please Re-enter");
                }
            } while (sizeId <= 0 || sizeId > 3);
            return sizeId;
        }

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

        public void PrintOrderDetails(List<Product> productList, Staff staff, string title)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(staff);
            Title(title);
            TimeLine(TimeLineCreateOrderContent(4));
            var table = new Table();
            table.Caption("Order Details");
            table.AddColumn(new TableColumn("Product ID").Centered());
            table.AddColumn(new TableColumn("Product Name").LeftAligned());
            table.AddColumn(new TableColumn("Size").Centered());
            table.AddColumn(new TableColumn("Unit Price").Centered());
            table.AddColumn(new TableColumn("Quantity").Centered());
            table.AddColumn(new TableColumn("Amount").Centered());
            foreach (var item in productList)
            {
                decimal amount = item.ProductPrice * item.ProductQuantity;
                string formattedAmount = amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                table.AddRow($"{item.ProductId}", $"{item.ProductName}", $"{item.ProductSize}", $"{item.ProductPrice}", $"{item.ProductQuantity}", $"{formattedAmount + " VND"}");
            }
            AnsiConsole.Write(table.Centered());
        }
    }
}
