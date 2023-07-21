using Persistence;
using Spectre.Console;
namespace UI
{
    public class Ultilities
    {
        public void Line()
        {
            Console.WriteLine("█████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ █████ ");
        }

        public void SmallLine()
        {
            Console.WriteLine("--------------------------------------------------");
        }

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
        public int Menu(string? title, string[] item)
        {
            Console.Clear();
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

        public void Title(string title)
        {
            Console.Clear();
            Line();
            Console.WriteLine(" " + title);
            Line();
        }

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
            Console.Clear();
            Title("Product Details");
            Console.WriteLine("Product ID: " + product.ProductId);
            Console.WriteLine("Product Name: " + product.ProductName);
            Console.WriteLine("Product Description: " + product.ProductDescription);
        }

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
        }
    }
}