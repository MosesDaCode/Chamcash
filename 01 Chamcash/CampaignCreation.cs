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
        public  void CreateCampaign()
        {


            var products = new Products("../../../Products/ProductList.txt");
            var AddCampaignChoice = Menus.AddCampaignChoice();
            bool createCampaign = true;
            while (createCampaign)
            {
                switch (AddCampaignChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("\t-------------------");
                        Console.WriteLine("\t||Procent kampanj||");
                        Console.WriteLine("\t-------------------\n");
                        Console.Write("Skriv Produkt-ID för produkten: ");
                        string productToAddCampaign = Console.ReadLine();
                        int searchResult = Products.LinearSearch(products.GetProductsFromFile(), productToAddCampaign);

                        if (searchResult != -1)
                        {
                            Console.Write("Ange ett start datum för kampanjen (åååå-MM-dd): ");
                            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly startDate))
                            {
                                Console.Write("Ange slut datum för kampanjen (åååå-MM-dd): ");
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly endDate))
                                {
                                    Console.Write("Lägg till en rabatt i procent (%): ");
                                    if (float.TryParse(Console.ReadLine(), out float campaignPrice))
                                    {
                                        if (campaignPrice != 0)
                                        {
                                            Campaigns newCampaignPrice = new Campaigns(productToAddCampaign, startDate, endDate, campaignPrice, "../../../Products/ProductList.txt");
                                            if (newCampaignPrice._endDate >= DateOnly.FromDateTime(DateTime.Now))
                                            {
                                                AddActiveCampaign(newCampaignPrice);
                                                
                                            }
                                            else
                                            {
                                                Console.WriteLine("kampanjen kan inte läggas till! Slutdatum har redan gått ut. Tryck på enter och försök igen!");
                                            }
                                        }

                                        else
                                        {
                                            Console.WriteLine("Du kan inte ha en rabatt på 0%, tryck på enter och försök igen!");
                                        }

                                        Console.WriteLine("Wohoo kampanjpriser har skapats!! tryck på enter för att fortsätta...");

                                        createCampaign = false;
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Du har anget ett ogiltigt kampanjpris!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Du har anget ett ogiltigt slut datum!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Du har anget ett ogiltigt start datum!");
                            }
                        }
                        else if (productToAddCampaign == "0")
                        {
                            Console.Clear();
                            AddCampaignChoice = Menus.AddCampaignChoice();

                        }
                        else
                        {
                            Console.WriteLine("Producten finns inte, försök igen!");
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("\t-------------------");
                        Console.WriteLine("\t||2 för 1 kampanj||");
                        Console.WriteLine("\t-------------------\n");
                        Console.Write("Skriv Produkt-ID för produkten: ");
                        string twoForOneCampaign = Console.ReadLine();
                        searchResult = Products.LinearSearch(products.GetProductsFromFile(), twoForOneCampaign);

                        if (searchResult != -1)
                        {
                            Console.Write("Ange ett start datum för kampanjen (åååå-MM-dd): ");
                            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly startDate))
                            {
                                Console.Write("Ange slut datum för kampanjen (åååå-MM-dd): ");
                                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly endDate))
                                {
                                    float campaignPrice = 0.0f;

                                    Campaigns newCampaignPrice = new Campaigns(twoForOneCampaign, startDate, endDate, campaignPrice, "../../../Products/ProductList.txt");
                                    if (newCampaignPrice._endDate >= DateOnly.FromDateTime(DateTime.Now))
                                    {
                                        AddActiveCampaign(newCampaignPrice);
                                    }
                                    Console.WriteLine("wohoo din 2 för 1 kampanj har skapats!! Tryck på enter för att fortsätta");
                                    createCampaign = false;
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("Du har anget ett ogiltigt slut datum!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Du har anget ett ogiltigt start datum!");
                            }
                        }
                        else if (twoForOneCampaign == "0")
                        {
                            Console.Clear();
                            AddCampaignChoice = Menus.AddCampaignChoice();

                        }
                        else
                        {
                            Console.WriteLine("Producten finns inte, försök igen!");
                        }

                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("\t----------------------");
                        Console.WriteLine("\t||Sänkt pris kampanj||");
                        Console.WriteLine("\t----------------------\n");
                        break;
                    case "0":
                        Console.Clear();
                        createCampaign = false;
                        break;
                    default:
                        Console.WriteLine("Du har angett fel meny val, tryck på enter för att fortsätta!");
                        Console.ReadKey();
                        Console.Clear();
                        AddCampaignChoice = Menus.AddCampaignChoice();
                        break;
                }

            }
        }
    }
}
