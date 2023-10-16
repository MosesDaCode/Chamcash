using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    class Campaign
    {
        private ProductSearch _productSearch;
        public List<Campaign> campaignPrices { get; } = new List<Campaign>();
        private List<string[]> products;

        public string _productId { get; set; }
        public DateTime _startDate { get; set; }
        public DateTime _endDate { get; set; }
        public float _price { get; set; }


        public Campaign(string productId, DateTime startDate, DateTime endDate, float price)
        {
            _productId = productId;
            _startDate = startDate;
            _endDate = endDate;
            _price = price;
        }
        

        public Campaign(ProductSearch products) 
        {
            _productSearch = products;
        }
        public void AddActiveCampaign(Campaign campaign)
        {
            campaignPrices.Add(campaign);
        }
        public void RemoveActiveCampaign(string productToRemove)
        {
            DateTime currentDate = DateTime.Now;
            campaignPrices.RemoveAll(campaign => campaign._endDate < currentDate && campaign._productId == productToRemove);  
        }
        public void RemoveCampaign(string removeCampaignstring)
        {
            Campaign campaignToRemove = campaignPrices.Find(campaign => campaign._productId == removeCampaignstring);
            if (campaignToRemove != null)
            {
                campaignPrices.Remove(campaignToRemove);
                Console.WriteLine("Kampanjen har tagits bort!");
            }
            else
            {
                Console.WriteLine("En kampanj på det angivna product-ID existerar inte!");
            }
        }
        public void CampainManagment()
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

            bool campaignManagmentRunning = true;
            while (campaignManagmentRunning)
            {
                switch (campaignChoice)
                {
                    case "1":
                        Console.Write("Skriv Produkt-ID för produkten: ");
                        string productToAddCampaign = Console.ReadLine();
                        int searchresult = ProductSearch.LinearSearch(_productSearch.GetProducts(), productToAddCampaign);

                        if (searchresult != -1)
                        {
                            Console.Write("Ange ett start datum för kampanjen (åååå-MM-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                            {
                                Console.Write("Ange slut datum för kampanjen (åååå-MM-dd): ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                                {
                                    Console.Write("Ange ett kampanjpris: ");
                                    if (float.TryParse(Console.ReadLine(), out float campaignPrice))
                                    {
                                        Campaign newCampaignPrice = new Campaign(productToAddCampaign, startDate, endDate, campaignPrice);
                                        campaignPrices.Add(newCampaignPrice);
                                        if (newCampaignPrice._endDate >= DateTime.Now)
                                        {
                                            AddActiveCampaign(newCampaignPrice);
                                        }
                                        Console.WriteLine("wohoo kampanjpriser har uppdaterats!!");
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
                            campaignManagmentRunning = false;
                            Console.Clear();
                        }
                        else
                        { 
                            Console.WriteLine("Producten finns inte!"); 
                        }
                    break;
                        case "2":
                        Console.Write("Ange product-ID på vars kampanj du vill ta bort: ");
                        string productToRemove = Console.ReadLine();

                        RemoveCampaign(productToRemove);
                        RemoveActiveCampaign(productToRemove);

                        if (productToRemove == "0")
                        {
                            campaignManagmentRunning = false;
                            Console.Clear();
                        }
                        break;
                    case "0":
                        campaignManagmentRunning = false;
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
