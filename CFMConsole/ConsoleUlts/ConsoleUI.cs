using System.IO.Compression;
using BL;
using DAL;
using Persistence;
using Spectre.Console;
namespace UI
{
    public class ConsoleUI
    {
        SizeBL sizeBL = new SizeBL();
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
            Console.Clear();
            if (title != null)
            {
                Title(title);
            }
            CurrentStaff(staff);
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Move [green]UP/DOWN button and ENTER[/] to select function")
               .PageSize(10)
               .AddChoices(item));
            return choice;
        }



        //Title
        public void Title(string title)
        {
            Console.Clear();
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
        public void PrintProducts(List<Product> prlst)
        {
            var table = new Table();
            table.AddColumn(new TableColumn("Product ID").Centered());
            table.AddColumn(new TableColumn("Product Name").LeftAligned());
            table.AddColumn(new TableColumn("Size S").Centered());
            table.AddColumn(new TableColumn("Size M").Centered());
            table.AddColumn(new TableColumn("Size L").Centered());
            foreach (var item in prlst)
            {
                decimal priceSize1 = sizeBL.GetSizeSByProductID(item.ProductId).SizePrice;
                decimal priceSize2 = sizeBL.GetSizeMByProductID(item.ProductId).SizePrice;
                decimal priceSize3 = sizeBL.GetSizeLByProductID(item.ProductId).SizePrice;
                string pricesize1 = string.Format("{00:##'.'### VND}", priceSize1);
                string pricesize2 = string.Format("{00:##'.'### VND}", priceSize2);
                string pricesize3 = string.Format("{00:##'.'### VND}", priceSize3);
                table.AddRow($"{item.ProductId}", $"{item.ProductName}", $"{pricesize1}", $"{pricesize2}", $"{pricesize3}");
            }
            AnsiConsole.Write(table.Centered());
        }

        public void PrintProductInfo(Product product)
        {
            List<Persistence.Size>? sizes = new List<Persistence.Size>();
            // Console.Clear();
            Title(@"
██████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗ ██████╗████████╗    ██████╗ ███████╗████████╗ █████╗ ██╗██╗     ███████╗
██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██║   ██║██╔════╝╚══██╔══╝    ██╔══██╗██╔════╝╚══██╔══╝██╔══██╗██║██║     ██╔════╝
██████╔╝██████╔╝██║   ██║██║  ██║██║   ██║██║        ██║       ██║  ██║█████╗     ██║   ███████║██║██║     ███████╗
██╔═══╝ ██╔══██╗██║   ██║██║  ██║██║   ██║██║        ██║       ██║  ██║██╔══╝     ██║   ██╔══██║██║██║     ╚════██║
██║     ██║  ██║╚██████╔╝██████╔╝╚██████╔╝╚██████╗   ██║       ██████╔╝███████╗   ██║   ██║  ██║██║███████╗███████║
╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝  ╚═════╝   ╚═╝       ╚═════╝ ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝╚══════╝╚══════╝
");
            Console.WriteLine("Product ID: " + product.ProductId);
            Console.WriteLine("Product Name: " + product.ProductName);
            sizes = sizeBL.GetListProductSizeByProductID(product.ProductId);
            PrintSizes(sizes);
            Console.WriteLine("Choose Product Size:");
            Console.ReadKey();
        }

        // Message Color
        public void RedMessage(string message)
        {
            AnsiConsole.Markup($"[underline yellow]{message}[/]");
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
            Console.WriteLine("Size : " + size.SizeProduct + "  price :" + size.SizePrice + " VND" + "  Quantity Instock :" + size.Quantity);
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
            table.AddColumn(new TableColumn($"{staff.StaffName}" + " - " + "ID:" + $"{staff.StaffId}")).Centered();
            AnsiConsole.Write(table);
        }
        public void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TitleNoBorder("Welcome " + staff.StaffName);
            Thread.Sleep(1000);
        }
    }
}