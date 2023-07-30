using System.IO.Compression;
using BL;
using DAL;
using Persistence;
using Spectre.Console;
using Spectre.Console.Extensions;
using System.Collections.Generic;
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
            AnsiConsole.Write(rule);
        }
        //PressAnyKeyToContinue
        public void PressAnyKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        //Logo
        public void ApplicationLogo()
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

        public string Menu(string? title, string[] item, Staff staff)
        {
            if (title != null)
            {
                Title(title);
            }
            CurrentStaff(staff);
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

        public void PrintProductsTable(List<Product> prlst)
        {
            int pageSize = 5; // Số sản phẩm mỗi trang
            int currentPage = 1; // Trang hiện tại

            while (true)
            {
                Console.Clear();
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
                    string pricesize1 = string.Format("{00:##'.'### VND}", priceSize1);
                    string pricesize2 = string.Format("{00:##'.'### VND}", priceSize2);
                    string pricesize3 = string.Format("{00:##'.'### VND}", priceSize3);
                    table.AddRow($"{prlst[i].ProductId}", $"{prlst[i].ProductName}", $"{pricesize1}", $"{pricesize2}", $"{pricesize3}");
                }
                AnsiConsole.Write(table.Centered());
                AnsiConsole.Markup("Press the [Green]LEFT ARROW KEY (←)[/] to go back to the previous page, the [Green]RIGHT ARROW KEY (→)[/] to go to the next page. Press Esc to exit.");
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
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break; // Thoát khỏi vòng lặp nếu nhấn Esc
                }
            }


        }
        public void PrintProductInfo(Product product)
        {

            ApplicationLogo();
            Title("PRODUCT DETAILS");
            Console.WriteLine("Product ID: " + product.ProductId);
            Console.WriteLine("Product Name: " + product.ProductName);
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
        public void CurrentStaff(Staff staff)
        {
            var table = new Table();
            Console.ForegroundColor = ConsoleColor.Green;
            table.AddColumn(new TableColumn($"{staff.StaffName}" + " - " + "ID:" + $"{staff.StaffId}").Centered()).RightAligned();
            AnsiConsole.Write(table);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TitleNoBorder("Welcome " + staff.StaffName);
            Thread.Sleep(1000);
        }
        public int ChooseProductsize(Product product)
        {
            int sizeId = 0;
            bool active = true;
            bool showAlert = false;
            int sizeChoose;
            List<Persistence.Size> listsize = new List<Persistence.Size>();

            while (active)
            {
                do
                {
                    PrintProductInfo(product);
                    listsize = sizeBL.GetListProductSizeByProductID(product.ProductId);
                    PrintSizes(listsize);
                    AnsiConsole.Markup("Choose Product Size([Green]1 to choose S[/], [Green]2 to choose M[/], [Green]3 to choose L[/]):");
                    int.TryParse(Console.ReadLine(), out sizeId);

                    if (sizeId < 0 || sizeId > listsize.Count())
                    {
                        showAlert = true;
                    }
                    else if (sizeId == 0)
                    {
                        active = false;
                        break;
                    }
                    else
                    {
                        return sizeId;
                    }
                    if (showAlert)
                    {
                        RedMessage("Invalid choice. Please Re-enter");
                    }
                }
                while (sizeId < 0 || sizeId > listsize.Count());

            }
            return sizeId;
        }
    }
}
