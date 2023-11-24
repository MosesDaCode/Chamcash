using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class NewCostumer
    {
        public void NewCostumerChoice()
        {
            var products = new Products("../../../Products/ProductList.txt");
            var productList = products.GetProductsFromFile();
            var campaignPrice = new Campaigns();
            bool newCostumer = true;
            while (newCostumer)
            {
                double totalSum = 0;
                DateTime dateTime = System.DateTime.Now;




                Menus.NewCostumerDisplay();



                int serialNumber = 1;
                string serialNumberFilePath = "../../../SerialNumber/serialNumber.txt";
                if (File.Exists(serialNumberFilePath))
                {
                    string serialNumberContent = File.ReadAllText(serialNumberFilePath);
                    if (int.TryParse(serialNumberContent, out serialNumber))
                    {
                        serialNumber++;
                    }
                }

                string filePath = $"../../../Receipts/RECIEPT_{dateTime.ToString("yyyy-MM-dd")}.txt";
                string receiptText = $"\n\n\tKVITTO\n ";
                receiptText += $"{dateTime}\n" +
         $"Kvitto:{serialNumber}\n" +
         $"---------------------------\n";



                List<string[]> receipt = new List<string[]>();


                float totalPriceOfProduct = 0.0f;
                bool productExist = true;

                while (productExist)
                {
                    int searchResult = -1;
                    int inputAmount = 0;

                    Console.Write("\n\tAnge Produkt-ID och antal med mellanslag: ");
                    string inputIdAndAmountString = Console.ReadLine();


                    string[] inputIdAndAmount = inputIdAndAmountString.Split(' ');

                    if (inputIdAndAmount.Length == 2)
                    {
                        string inputId = inputIdAndAmount[0];

                        if (int.TryParse(inputIdAndAmount[1], out inputAmount))
                        {
                            searchResult = Products.LinearSearch(productList, inputId);

                            if (searchResult == -1)
                            {
                                Console.WriteLine("\n\tArtikeln finns ej med, tryck på enter för att fortsätta.");
                                Console.ReadKey();
                            }
                            else
                            {
                                receipt.Add(new string[] { inputId, inputAmount.ToString(), productList[searchResult][0], productList[searchResult][3], productList[searchResult][2] });
                                receiptText += $"Produkt-ID: {inputId}, Antal: {inputAmount}\n";

                                Console.WriteLine($"\t{productList[searchResult][0]} {productList[searchResult][3]}kr * {inputAmount} {productList[searchResult][2]}  ");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n\tOgiltig inmatning av antal, försök igen.");
                        }


                    }
                    else if (inputIdAndAmountString.ToUpper() == "PAY")
                    {
                        receiptText += "---------------------------\n";
                        foreach (var items in receipt)
                        {
                            if (float.TryParse(items[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float inputAmountFloat) &&
                                float.TryParse(items[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float pricePerUnitFloat))
                            {
                                totalPriceOfProduct = inputAmountFloat * pricePerUnitFloat;
                                receiptText += $"{items[2]}\t{items[1]}{items[4]}\t{items[3]}kr /{items[4]} = {totalPriceOfProduct}kr\n";
                                totalSum += totalPriceOfProduct;
                            }

                            else
                            {
                                Console.WriteLine("\tOgiltigt kvitto!!");
                            }

                            campaignPrice.GetCampaignFromFile();
                            var validCampaign = campaignPrice._campaignPrices.Find(campaign => campaign._productId == items[0] &&
                            DateOnly.FromDateTime(DateTime.Now) >= campaign._startDate && DateOnly.FromDateTime(DateTime.Now) <= campaign._endDate);

                            if (validCampaign != null)
                            {
                                float validCampaignPrice = validCampaign._price;

                                float discount = (float)totalPriceOfProduct - ((float)totalPriceOfProduct * Convert.ToSingle(validCampaignPrice / 100));
                                float discountSum = totalPriceOfProduct -= discount;
                                receiptText += $"Kampanjpris för {items[2]}: {validCampaignPrice}% rabatt = {discount}\n\n";
                                totalSum -= discountSum;
                            }

                        }

                        receiptText += $"\n\tTotalt: {totalSum:0.00}kr\n-------------------------- ";
                        if (inputIdAndAmountString.ToUpper() == "PAY")
                        {

                            File.AppendAllText(filePath, receiptText);
                            Console.WriteLine(receiptText);
                            Console.WriteLine("Kvittot har sparats, tryck på enter för att komma vidare.");

                            receipt.Clear();
                        }
                        File.WriteAllText(serialNumberFilePath, serialNumber.ToString());

                        Console.ReadKey();
                        Console.Clear();
                        productExist = false;
                        newCostumer = false;
                    }
                    else if (inputIdAndAmountString == "0")
                    {
                        Console.Clear();
                        productExist = false;
                        newCostumer = false;
                    }
                    else
                    {
                        Console.WriteLine("\tFel format. Var god ange både Produkt-ID och Antal med ett mellanslag innan antalet!");

                        if (inputIdAndAmountString.ToLower() == "bajskorv" || inputIdAndAmountString.ToLower() == "bajs")
                            Console.WriteLine("\tBajs/Bajskorv funkar inte heller");
                    }


                }

            }
        }
    }
}
