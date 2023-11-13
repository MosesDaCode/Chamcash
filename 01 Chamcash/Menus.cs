using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ChamCash
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
                Console.WriteLine("\t-----------------");
                Console.Write("\tAnge menyval: ");
                string menuChoice = Console.ReadLine();
            return menuChoice;

        }
        public static void NewCostumerDisplay()
        {
            Console.WriteLine("\t-----------------------------------");
            Console.WriteLine("\t||Ny kund                        ||");
            Console.WriteLine("\t-----------------------------------");
            Console.WriteLine("\t||Skriv ett produktid och antal. ||");
            Console.WriteLine("\t||Skriv 'PAY' för att betala.    ||");
            Console.WriteLine("\t||Skriv '0' för att gå tillbaka  ||");
            Console.WriteLine("\t-----------------------------------");
        }
        public static string AdminMenu()
        {
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||1. Lägg till produkt ||");
            Console.WriteLine("\t||2. Redigera produkter||");
            Console.WriteLine("\t||3. Kampanjer         ||");
            Console.WriteLine("\t||0. Gå tillbaka       ||");
            Console.WriteLine("\t-------------------------");
            Console.Write("\tAnge menyval: ");
            string adminChoice = Console.ReadLine();
            return adminChoice;
        }
        public static string CampaignMenu()
        {
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||Kampanjhantering     ||");
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||1. Lägg till kampanj ||");
            Console.WriteLine("\t||2. Ta bort kampanj   ||");
            Console.WriteLine("\t||0. Gå tillbaka       ||");
            Console.WriteLine("\t-----------------------\n");
            Console.Write("Ange menyval: ");
            string campaignChoice = Console.ReadLine();
            return campaignChoice;
        }
        public static string AddCampaignChoice()
        {
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||Kampanjval           ||");
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||1. Procent kampanj   ||");
            Console.WriteLine("\t||2. 2 för 1 kampanj   ||");
            Console.WriteLine("\t||3. Sänkt pris kampanj||");
            Console.WriteLine("\t||0. Gå tillbaka       ||");
            Console.WriteLine("\t-----------------------\n");
            Console.Write("Ange menyval: ");
            string addCampaignChoice = Console.ReadLine();
            return addCampaignChoice;
        }

    }
}