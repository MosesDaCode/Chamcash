using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class CampaignRemoval : Campaigns
    {
       
        public void RemoveCampaign()
        {
            var menu = new Menus();   
            Console.Clear();
            Console.WriteLine("\t---------------------------");
            Console.WriteLine("\t||Ta bort kampanj        ||");
            Console.WriteLine("\t---------------------------\n");
            Console.Write("Ange product-ID på vars kampanj du vill ta bort: ");
            string productToRemove = Console.ReadLine();
            if (productToRemove != "0")
            {

                RemoveCampaign(productToRemove);
                Console.ReadKey();
                Console.Clear();
            }
            else if (productToRemove == "0")
            {
                Console.Clear();
            }
        }

    }
}
