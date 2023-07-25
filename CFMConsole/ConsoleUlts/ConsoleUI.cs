using Persistence;
using Spectre.Console;
namespace UI
{
    public class ConsoleUI
    {
        // Line
        public void Line()
        {
            Console.WriteLine("█████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ ");
        }

        public void SmallLine()
        {
            Console.WriteLine("--------------------------------------------------");
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
            Console.WriteLine(" " + title);
            Line();
        }

        //Product Handle
        public void PrintProducts(List<Product> lst)
        {
            foreach (var item in lst)
            {
                PrintProduct(item);
            }
        }

        public void PrintProduct(Product product)
        {
            Console.WriteLine("| {0, 5} | {1, 45} |", product.ProductId, product.ProductName);
            //                      Id   ProductName
            SmallLine();
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
            // Console.WriteLine("Product Description: " + product.ProductDescription);
        }

        //Progress Async
        public static async void ProgressAsync()
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
        }

        // Message Color
        public void RedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("" + message);
        }

        public void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("" + message);
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
            Console.WriteLine("Size : " + size.SizeProduct + "  price :" + size.SizePrice + " VND" +"  Quantity Instock :" + size.Quantity);
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