using BL;
using UI;
using System.Text;
using Persistence;
using Spectre.Console;
using System.ComponentModel.Design;
using System.Diagnostics;
using Ultilities;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Ults Ultilities = new Ults();
        Ultilities.Login();
    }
}