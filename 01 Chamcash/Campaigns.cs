using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    class Campaigns : Products
    {
        public List<Campaigns> _campaignPrices { get; set; } = new List<Campaigns>();
        private List<string[]> _products;
        public static string _campaignFilePath = "../../../Campaigns/CampaignList.txt";


        public string _productId { get; set; }
        public DateOnly _startDate { get; set; }
        public DateOnly _endDate { get; set; }
        public float _price { get; set; }



        public Campaigns(string productId, DateOnly startDate, DateOnly endDate, float price, string productFilePath) : base(productFilePath)
        {
            _productId = productId;
            _startDate = startDate;
            _endDate = endDate;
            _price = price;
        }
        public Campaigns() : base("../../../Products/ProductList.txt")
        {
            _products = new List<string[]>();
        }

        public void GetCampaignFromFile()
        {
            string[] campaignlines = File.ReadAllLines(_campaignFilePath);

            foreach (string line in campaignlines)
            {
                string[] campaignInfo = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (campaignInfo.Length == 5)
                {
                    string productId = campaignInfo[1];

                    if (DateOnly.TryParse(campaignInfo[2].Trim(), out DateOnly startDate) &&
                        (DateOnly.TryParse(campaignInfo[3].Trim(), out DateOnly endDate) &&
                        (float.TryParse(campaignInfo[4].Trim(), out float price))))
                    {
                        Campaigns campaign = new Campaigns(productId, startDate, endDate, price, _productFilePath);
                        _campaignPrices.Add(campaign);
                    }
                    else
                    {
                        Console.WriteLine($"Felaktig information i filen: " + line);
                    }
                }
                else
                {
                    Console.WriteLine($"Felaktig information i filen: " + line);

                }
            }
        }
        public void AddCampaignToFile(List<Campaigns> newCampaign)
        {
                string line = String.Join(", ", newCampaign);

                File.AppendAllText(_campaignFilePath, line);

            newCampaign.Clear();
            
        }
        public void AddActiveCampaign(Campaigns campaign)
        {
            _campaignPrices.Add(campaign);
            AddCampaignToFile(_campaignPrices);
        }
        public void RemoveActiveCampaign(string productToRemove)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            _campaignPrices.RemoveAll(campaign => campaign._endDate < currentDate && campaign._productId == productToRemove);
        }
        public void RemoveCampaign(string removeCampaignstring)
        {
            Campaigns campaignToRemove = _campaignPrices.Find(campaign => campaign._productId == removeCampaignstring);
            if (campaignToRemove != null)
            {
                _campaignPrices.Remove(campaignToRemove);
                Console.WriteLine("Kampanjen har tagits bort!");
            }
            else
            {
                Console.WriteLine("En kampanj på det angivna product-ID existerar inte!");
            }
        }
        public override string ToString()
        {
            return $"Kampanj: {_productId}, {_startDate}, {_endDate}, {_price}";
        }
        public void CreateCampaign()
        {
            var campaignChoice = Menus.CampaignMenu();
            var products = new Products("../../../Products/ProductList.txt");


            bool campaignManagmentRunning = true;
            while (campaignManagmentRunning)
            {
                switch (campaignChoice)
                {
                    case "1":
                        Console.Write("Skriv Produkt-ID för produkten: ");
                        string productToAddCampaign = Console.ReadLine();
                        int searchresult = Products.LinearSearch(products.GetProductsFromFile(), productToAddCampaign);

                        if (searchresult != -1)
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
                                        Campaigns newCampaignPrice = new Campaigns(productToAddCampaign, startDate, endDate, campaignPrice, "../../../Products/ProductList.txt");
                                        if (newCampaignPrice._endDate >= DateOnly.FromDateTime(DateTime.Now))
                                        {
                                            AddActiveCampaign(newCampaignPrice);
                                        }
                                        Console.WriteLine("wohoo kampanjpriser har uppdaterats!!");
                                        campaignManagmentRunning = false;
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
                            campaignChoice = Menus.CampaignMenu();

                        }
                        else
                        {
                            Console.WriteLine("Producten finns inte, försök igen!");
                        }
                        break;
                    case "2":
                        Console.Write("Ange product-ID på vars kampanj du vill ta bort: ");
                        string productToRemove = Console.ReadLine();

                        RemoveCampaign(productToRemove);
                        RemoveActiveCampaign(productToRemove);

                        if (productToRemove == "0")
                        {
                            Console.Clear();
                            campaignChoice = Menus.CampaignMenu();
                        }
                        break;
                    case "0":
                        campaignManagmentRunning = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Du har angett fel meny val, tryck på enter för att fortsätta!");
                        Console.ReadKey();
                        Console.Clear();
                        campaignChoice = Menus.CampaignMenu();

                        break;
                }
            }
        }
    }
}
