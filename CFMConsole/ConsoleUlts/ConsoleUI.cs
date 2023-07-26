using Persistence;
using Spectre.Console;
namespace UI
{
    public class ConsoleUI
    {
        // Line
        public void Line()
        {
            var rule = new Rule();
            AnsiConsole.Write(rule);
        }

        //PressAnyKeyToContinue
        public void PressAnyKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        //Logo
        public void LogoVTCA()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
                ██████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗ ██████╗████████╗    ██████╗ ██╗   ██╗
                ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██║   ██║██╔════╝╚══██╔══╝    ██╔══██╗╚██╗ ██╔╝
                ██████╔╝██████╔╝██║   ██║██║  ██║██║   ██║██║        ██║       ██████╔╝ ╚████╔╝ 
                ██╔═══╝ ██╔══██╗██║   ██║██║  ██║██║   ██║██║        ██║       ██╔══██╗  ╚██╔╝  
                ██║     ██║  ██║╚██████╔╝██████╔╝╚██████╔╝╚██████╗   ██║       ██████╔╝   ██║   
                ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝  ╚═════╝   ╚═╝       ╚═════╝    ╚═╝                                                                                
");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine(@"
                ██╗   ██╗████████╗ ██████╗     █████╗  ██████╗ █████╗ ██████╗ ███████╗███╗   ███╗██╗   ██╗
                ██║   ██║╚══██╔══╝██╔════╝    ██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝████╗ ████║╚██╗ ██╔╝
                ██║   ██║   ██║   ██║         ███████║██║     ███████║██║  ██║█████╗  ██╔████╔██║ ╚████╔╝ 
                ╚██╗ ██╔╝   ██║   ██║         ██╔══██║██║     ██╔══██║██║  ██║██╔══╝  ██║╚██╔╝██║  ╚██╔╝  
                 ╚████╔╝    ██║   ╚██████╗    ██║  ██║╚██████╗██║  ██║██████╔╝███████╗██║ ╚═╝ ██║   ██║   
                  ╚═══╝     ╚═╝    ╚═════╝    ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝     ╚═╝   ╚═╝   
                                                                                                          
");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Menu Handle
        public int Menu(string? title, string[] item)
        {
            // Console.Clear();
            int i = 0;
            int choice;
            if (title != null)
            {
                Title(title);
            }
            for (i = 0; i < item.Count(); i++)
            {
                Console.WriteLine(" " + (i + 1) + "." + item[i]);
            }
            do
            {
                Console.Write("#YOUR CHOICE: ");
                int.TryParse(Console.ReadLine(), out choice);
            }
            while (choice <= 0 || choice > item.Count());
            return choice;
        }

        //Title
        public void Title(string title)
        {
            Console.Clear();
            Line();
            // var panel = new Panel(title);
            Console.WriteLine(" " + title);
            Line();
        }

        
        //Product Handle
        public void PrintProducts(List<Product> lst)
        {

            var table = new Table();
            table.AddColumn(new TableColumn("Product ID").Centered());
            table.AddColumn(new TableColumn("Product Name").LeftAligned());
            foreach (var item in lst)
            {
                table.AddRow($"{item.ProductId}", $"{item.ProductName}");
            }
            AnsiConsole.Write(table);
        }

        public void PrintProductInfo(Product product)
        {
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
            // foreach (var item in product.ProductSize)
            // {
            //     Console.WriteLine("Size: " + item.SizeProduct + "price: " + item.SizePrice + "VND" + "Quantity instok: "+ item.Quantity);
            // }
            // Console.WriteLine("Product Description: " + product.ProductDescription);
        }

        //Progress Async
        public async void ProgressAsync()
        {
            await AnsiConsole.Progress().StartAsync(async ctx =>
                {
                    // Define tasks
                    var task1 = ctx.AddTask("[green]Progress[/]");
                    // var task2 = ctx.AddTask("Done!!!");

                    while (!ctx.IsFinished)
                    {
                        // Simulate some work
                        await Task.Delay(20);

                        // Increment
                        task1.Increment(4.5);
                        // task2.Increment(2);
                        // Console.Clear();
                    }
                });
            Thread.Sleep(300);
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
            AnsiConsole.Markup($"[underline green]{message}[/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public async void WelcomeStaff(Staff staff)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome " + staff.StaffName);
            Thread.Sleep(900);
            ProgressAsync();
            Thread.Sleep(1000);
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

    }
}