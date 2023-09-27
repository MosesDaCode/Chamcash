using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    internal class Menus
    {
        public static string FirstMenu()
        {
                Console.WriteLine("\t-----------------");
                Console.WriteLine("\t||KASSA        ||");
                Console.WriteLine("\t-----------------");
                Console.WriteLine("\t||1. Ny kund   ||");
                Console.WriteLine("\t||2. Admin     ||");
                Console.WriteLine("\t||0. Avsluta   ||");
                Console.WriteLine("\t||             ||");
                Console.WriteLine("\t-----------------");
                Console.Write("\tAnge menyval: ");
                string menuChoice = Console.ReadLine();
            return menuChoice;

        }
        public static int NewCostumerMenu()
        {
            Console.WriteLine("\t-----------------------------");
            Console.WriteLine("\t||Ny kund                  ||");
            Console.WriteLine("\t||Ange produktid och antal.||");
            Console.WriteLine("\t-----------------------------");
            Console.Write("\tAnge in antalet produkter som ska skannas: ");
            int.TryParse(Console.ReadLine(), out int totalAmount);    // krashar vid mellanslag FIXA!! Kan vara För att det är en int inte en sträng
            
            return totalAmount ;
        }
    }
}
