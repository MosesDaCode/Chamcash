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
       
        public void RemoveExistingCampaign()
        {
            Console.WriteLine("\t---------------------------");
            Console.WriteLine("\t||Ta bort kampanj        ||");
            Console.WriteLine("\t---------------------------\n");
            Console.Write("Ange product-ID på vars kampanj du vill ta bort: ");
            string productToRemove = Console.ReadLine();
            if (productToRemove != "0")
            {
                GetCampaignFromFile();
                List<Campaigns> campaignsToRemove = _campaignPrices
                    .Where(campaign => campaign._productId == productToRemove).ToList();
                if (campaignsToRemove.Count > 0)
                {
                    for (int i = 0; i < campaignsToRemove.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}, {campaignsToRemove[i]._startDate} - {campaignsToRemove[i]._endDate} ({campaignsToRemove[i]._price}% Rabatt)");
                    }
                    Console.Write("Ange siffran för kampanjen som ska tas bort: ");
                    int.TryParse(Console.ReadLine(), out int selectedIndex);
                    if (selectedIndex > 0 && selectedIndex <= campaignsToRemove.Count)
                    {

                        RemoveCampaign(campaignsToRemove[selectedIndex - 1]);

                    }
                    else if (selectedIndex == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Avbruten åtgärd...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt val! Tryck på enter för att fortsätta.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Ingen kampanj med angivet produkt-ID hittades! Tryck på enter för att fortsätta.");
                    Console.ReadKey();
                    Console.Clear();
                }
                //Console.ReadKey();
                //Console.Clear();
            }
            else if (productToRemove == "0")
            {
                Console.Clear();
            }
        }

    }
}
