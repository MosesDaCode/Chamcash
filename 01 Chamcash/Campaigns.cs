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
    public class Campaigns : Products
    {
        public List<Campaigns> _campaignPrices { get; set; } = new List<Campaigns>();
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

            File.AppendAllLines(_campaignFilePath, line);

            ClearCampaignList();

        }
        public void ClearCampaignList()
        {
            _campaignPrices.Clear();
        }
        public void AddActiveCampaign(Campaigns campaign)
        {
            _campaignPrices.Add(campaign);
            AddCampaignToFile(new List<Campaigns> { campaign});
        }
        public void RemoveExpiredCampaign()
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
           
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
        public void RemoveCampaign(Campaigns removeCampaignstring)
        {
            //GetCampaignFromFile();
            Campaigns campaignToRemove = _campaignPrices.Find(campaign => campaign._productId == removeCampaignstring._productId);
            if (removeCampaignstring != null)
            {
                _campaignPrices.Remove(removeCampaignstring);
                UpdateCampaignFile();
                Console.WriteLine("Kampanjen har tagits bort!, tryck på enter för att fortsätta.");
                Console.ReadKey();
                Console.Clear();
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
            CampaignCreation campaignCreation = new CampaignCreation();
            CampaignRemoval removeCampaign = new CampaignRemoval();

            bool campaignManagmentRunning = true;
            while (campaignManagmentRunning)
            {
                string campaignChoice = Menus.CampaignMenu();
                switch (campaignChoice)
                {
                    case "1":
                        Console.Clear();
                        campaignCreation.CreateCampaign();

                        break;
                    case "2":
                        Console.Clear();
                        removeCampaign.RemoveExistingCampaign();
                        break;
                    case "0":
                        campaignManagmentRunning = false;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Du har angett fel meny val, tryck på enter för att fortsätta!");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
        
    }
}
