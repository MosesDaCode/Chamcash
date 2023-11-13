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

        }

        public void GetCampaignFromFile()
        {
            string[] campaignlines = File.ReadAllLines(_campaignFilePath);

            foreach (string line in campaignlines)
            {
                string[] campaignInfo = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (campaignInfo.Length == 4)
                {
                    string productId = campaignInfo[0];

                    if (DateOnly.TryParse(campaignInfo[1].Trim(), out DateOnly startDate) &&
                        (DateOnly.TryParse(campaignInfo[2].Trim(), out DateOnly endDate) &&
                        (float.TryParse(campaignInfo[3].Trim(), out float price))))
                    {
                        Campaigns campaign = new Campaigns(productId, startDate, endDate, price, _productFilePath);
                        //Fixa Bo ent gånt

                        _price = price;

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


            string[] line = newCampaign.Select(campaign => $"{campaign._productId}, {campaign._startDate}, {campaign._endDate}, {campaign._price}").ToArray();
            //string line = String.Join(", ", newCampaign);

            File.AppendAllLines(_campaignFilePath, line);

            newCampaign.Clear();

        }
        public void AddActiveCampaign(Campaigns campaign)
        {
            _campaignPrices.Add(campaign);
            AddCampaignToFile(_campaignPrices);
        }
        public void RemoveExpiredCampaign()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            //string[] lines = File.ReadAllLines(_campaignFilePath);

            //var remainingLines = lines.Where(lines => lines.)
            _campaignPrices.RemoveAll(campaign => campaign._endDate < currentDate);
            UpdateCampaignFile();
        }
        public void UpdateCampaignFile()
        {
            File.WriteAllText(_campaignFilePath, string.Empty);

            foreach (var campaign in _campaignPrices)
            {
                string line = $"{campaign._productId}, {campaign._startDate}, {campaign._endDate}, {campaign._price}";
                File.AppendAllText(_campaignFilePath, line + Environment.NewLine);
            }
        }
        public void RemoveCampaign(string removeCampaignstring)
        {
            Campaigns campaignToRemove = _campaignPrices.Find(campaign => campaign._productId == removeCampaignstring);
            if (campaignToRemove != null)
            {
                _campaignPrices.Remove(campaignToRemove);
                UpdateCampaignFile();
                Console.WriteLine("Kampanjen har tagits bort!, tryck på enter för att fortsätta.");
            }
            else
            {
                Console.WriteLine("En kampanj på det angivna product-ID existerar inte!, Tryck på enter för att fortsätta.");
            }
        }
        public override string ToString()
        {
            return $"{_productId}, {_startDate}, {_endDate}, {_price}";
        }
        public void CampaignManagment()
        {
            var products = new Products("../../../Products/ProductList.txt");


            bool campaignManagmentRunning = true;
            while (campaignManagmentRunning)
            {
                var campaignChoice = Menus.CampaignMenu();
                switch (campaignChoice)
                {
                    case "1":
                        Console.Clear();
                        CreateCampaign();

                        break;
                    case "2":
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
                        campaignChoice = Menus.CampaignMenu();
                        }
                        else if (productToRemove == "0")
                        {
                            Console.Clear();
                            //campaignChoice = Menus.CampaignMenu();
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
        public void CreateCampaign()
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
                                        Campaigns newCampaignPrice = new Campaigns(productToAddCampaign, startDate, endDate, campaignPrice, "../../../Products/ProductList.txt");
                                        if (newCampaignPrice._endDate >= DateOnly.FromDateTime(DateTime.Now))
                                        {
                                            AddActiveCampaign(newCampaignPrice);
                                        }
                                        Console.WriteLine("wohoo kampanjpriser har uppdaterats!!");
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
