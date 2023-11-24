using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class CampaignCreation : Campaigns
    {
        public void CreateCampaign()
        {


            var products = new Products("../../../Products/ProductList.txt");

            Console.Clear();
            Console.WriteLine("\t-------------------");
            Console.WriteLine("\t||Procent kampanj||");
            Console.WriteLine("\t-------------------\n");
            Console.Write("Skriv Produkt-ID för produkten: ");
            string productToAddCampaign = Console.ReadLine();
            int searchResult = Products.LinearSearch(products.GetProductsFromFile(), productToAddCampaign);

            if (searchResult != -1)
            {
                Console.Write("Ange ett startdatum för kampanjen (åååå-MM-dd): ");
                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly startDate))
                {
                    Console.Write("Ange slutdatum för kampanjen (åååå-MM-dd): ");
                    if (DateOnly.TryParse(Console.ReadLine(), out DateOnly endDate))
                    {
                        if (startDate < endDate && endDate >= DateOnly.FromDateTime(DateTime.Now))
                        {
                            Console.Write("Lägg till en rabatt i procent (%): ");
                            if (float.TryParse(Console.ReadLine(), out float campaignPrice))
                            {
                                if (campaignPrice > 0 && campaignPrice < 100)
                                {
                                    Campaigns newCampaignPrice = new Campaigns(productToAddCampaign, startDate, endDate, campaignPrice, "../../../Products/ProductList.txt");
                                    if (OverLappingCampaign(newCampaignPrice))
                                    {
                                        AddActiveCampaign(newCampaignPrice);
                                        Console.WriteLine("Wohoo kampanjpriser har skapats!! tryck på enter för att fortsätta...");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Det finns redan en kampanj inom det angivna tidsspannet. Tryck på enter och försök igen!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ogiltig rabatt, tryck på enter och försök igen!");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Du har anget ett ogiltigt kampanjpris!, Tryck på enter och försök igen");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Du har anget ett ogiltigt slutdatum!, Tryck på enter och försök igen");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Du har anget ett ogiltigt slutdatum!, Tryck på enter och försök igen ");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Du har anget ett ogiltigt startdatum!, Tryck på enter och försök igen");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else if (productToAddCampaign == "0")
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Producten finns inte, försök igen!");
                Console.ReadKey();
                Console.Clear();
            }
        }
        private bool OverLappingCampaign(Campaigns newCampaign)
        {
            GetCampaignFromFile();
            foreach (var existingCampaign in _campaignPrices)
            {
                if (existingCampaign._productId == newCampaign._productId &&
                    newCampaign._startDate <= existingCampaign._endDate &&
                    newCampaign._endDate >= existingCampaign._startDate)
                {
                    return false;
                }
            }
            return true;
        }
    }
}