using System;
using System.Threading;

namespace ParkingConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\nLet's create a parking");
            Thread.Sleep(1200);

            Menu.ParkingCustomization();

            Menu.MenuMap();            
        }
    }
}
