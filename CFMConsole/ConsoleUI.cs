using Model;

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

        public void PrintMenu(Product product)
        {
            Console.WriteLine("| {0, 5} | {1, 45} |", product.ProductId,product.ProductName);
            //                      Id   ProductName
            SmallLine();
        }

        public void PrintProductDetailsInfo(Product product)
        {
            Console.Clear();
            Title("Product Details");
            Console.WriteLine("Product ID: " + product.ProductId);
            Console.WriteLine("Product Name: " + product.ProductName);
            Console.WriteLine("Product Description: " + product.ProductDescription);
            foreach (Size item in product.ProductSize)
            {
            Console.WriteLine("Size:" + item.ProductSize, "Price:" + item.SizePrice, "Quantity instock:" + item.Quantity);
            }
        }
    }
}