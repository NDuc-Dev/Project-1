using System;

namespace menu
{
    class MainClass
    {
        public static void printMenu(String[] options)
        {
            foreach (String option in options)
            {
                Console.WriteLine(option);
            }
            Console.Write("Choose your option : ");
        }

        
        public static void Main(string[] args)
        {
            Console.WriteLine("Console menu application example in C#");
            String[] options = {"1- Option 1",
                            "2- Option 2",
                            "3- Option 3",
                            "4- Exit",
                                };
           
            int option=0;
            while (true)
            {
                printMenu(options); 
            }
        }
    }
}