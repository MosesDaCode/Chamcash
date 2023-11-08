using _01_Chamcash;
using System.Globalization;
using System.Text;

namespace _01_ChamCash
{
    class Program
    {

        static void Main(string[] args)
        {

            ProductEditor productEdit = new ProductEditor("../../../Products/ProductList.txt");
            Products products = new Products("../../../Products/ProductList.txt");
            List<string[]> productList = products.GetProductsFromFile(); // Initierar listan med produkter ifrån GetProducts metoden som är i ProductSearch klassen.
            Campaigns campaignPrice = new Campaigns();

            string removeCampaignString = null;
            string pay = null;

            bool menuIsRunning = true; // Håller menyn aktiv
            while (menuIsRunning)
            {
                string menuChoice = Menus.FirstMenu();
                Console.Clear();

                switch (menuChoice)
                {
                    case "1":
                        bool newCostumer = true; // Talar om för while-loopen att loopen är igång (true = 1 = på)
                        while (newCostumer)
                        {
                            float totalSum = 0.0f;
                            DateTime dateTime = System.DateTime.Now; // initierar dagens datum och tid ifrån datorsystemet till programmet




                            Menus.NewCostumerDisplay();


                            //Plussar på löpnummer efter varje kvitto
                            int serialNumber = 0;
                            string serialNumberFilePath = "../../../SerialNumber/serialNumber.txt";
                            if (File.Exists(serialNumberFilePath))
                            {
                                string serialNumberContent = File.ReadAllText(serialNumberFilePath);
                                if (int.TryParse(serialNumberContent, out serialNumber))
                                {
                                    serialNumber++;
                                }
                            }

                            string filePath = $"../../../Receipts/RECIEPT_{dateTime.ToString("yyyy-MM-dd")}.txt"; // filePath skapar en .txt fil. filePath är sökvägen där kvittot ska sparas. 
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
                                    int.TryParse(inputIdAndAmount[1], out inputAmount);


                                    searchResult = Products.LinearSearch(productList, inputId);

                                    if (searchResult == -1)
                                    {
                                        Console.WriteLine("\n\tArtikeln finns ej med, tryck på enter för att fortsätta.");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        receipt.Add(new string[] { inputId,  inputAmount.ToString(), productList[searchResult][0], productList[searchResult][3], productList[searchResult][2] });
                                        receiptText += $"Produkt-ID: {inputId}, Antal: {inputAmount}\n";

                                        Console.WriteLine($"\t{productList[searchResult][0]} {productList[searchResult][3]}kr * {inputAmount}  ");
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
                                            receiptText += $"{items[2]}\t{items[1]}st\t{items[3]}kr /{items[4]} = {totalPriceOfProduct}kr\n";
                                            totalSum += totalPriceOfProduct;
                                        }

                                        else
                                        {
                                            Console.WriteLine("\tOgiltigt kvitto!!");
                                        }

                                        campaignPrice.GetCampaignFromFile();
                                        var validCampaign = campaignPrice._campaignPrices.Find(campaign => campaign._productId == items[0] && DateOnly.FromDateTime(DateTime.Now) >= campaign._startDate && DateOnly.FromDateTime(DateTime.Now) <= campaign._endDate);

                                        if (validCampaign != null)
                                        {
                                            float discount = totalPriceOfProduct / campaignPrice._price;
                                            float discountSum = totalSum -= discount;
                                            receiptText += $"Kampanjpris för {items[2]}: {campaignPrice._price}% rabatt = {discountSum}";
                                        }
                                        else
                                        {
                                            Console.WriteLine("Inga kampanjer existerar!");
                                        }

                                    }

                                    receiptText += $"\n\tTotalt: {totalSum:0.00}kr\n-------------------------- ";
                                    if (inputIdAndAmountString.ToUpper() == "PAY")
                                    {

                                        File.AppendAllText(filePath, receiptText);
                                        Console.WriteLine(receiptText);
                                        Console.WriteLine("\tKvittot har sparats, tryck på enter för att komma vidare.");

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
                        break;


                    case "2":
                        bool adminRunning = true;

                        while (adminRunning)
                        {
                            string adminChoice = Menus.AdminMenu();
                            Console.Clear();

                            switch (adminChoice)
                            {
                                case "1":
                                    productEdit.CreateNewProduct(productList);
                                    Console.Clear();
                                    break;
                                case "2":
                                    productEdit.EditProduct(productList);
                                    Console.Clear();
                                    break;
                                case "3":
                                        campaignPrice.CreateCampaign();
                                    break;
                                case "0":
                                    Console.Clear();
                                    adminRunning = false;
                                    break;
                                default:
                                    Console.WriteLine("Du har angett fel meny val, tryck på enter för att fortsätta!");

                                    if (adminChoice == "bajskorv" || adminChoice == "bajs")
                                        Console.WriteLine("Bajs/Bajskorv funkar inte!");

                                    Console.ReadKey();
                                    Console.Clear();
                                    break;

                            }
                        }
                        break;



                    case "0":
                        Console.WriteLine("\t---------");
                        Console.WriteLine("\t||Hejdå||");
                        Console.WriteLine("\t---------");
                        menuIsRunning = false;
                        Thread.Sleep(2000);
                        break;

                    default:
                        Console.WriteLine("Du har angett fel meny val, tryck på enter för att fortsätta!");

                        if (menuChoice == "bajskorv" || menuChoice == "bajs")
                            Console.WriteLine("Bajs/Bajskorv funkar inte! Hur gammal är du egentligen!");

                        Console.ReadKey();
                        Console.Clear();
                        break;

                }
            }
        }
    }
}









//kalla på GetCampaignFromFile() i main för att lägga till kampanj i kvitto
//krashar när man skriver ett produkt id i linearSearch
//ändra procent till helpris i kampanj.
//--fixa input för kampanj filen
//--produkter i kassasystemet ska lagras i fil [V]
//hamnar i en loop när jag lägger till kampanjer. kan inte gå tillbaka från menyn. Lägg till Vill du fortsätta eller gå tillbaka
//--Går inte betala med stora bokstäver PAY [V]
//--Lägg till kr i uppvisning. (nykund)[V]
//--Fixa enhetsinmatning utan / i "lägga till produkter"
//--Inmatning av Produkt-Id och antal ska vara samma rad med mellanrum [V]
//--Angivna artiklar ska visas i konsollen medan man fyller på kvittot.[V]
//--lägg till felmedelande för inmatning av fel antal produkter. [V]
//--Kvitto ska sparas i annan fil med tid och datum. [V]
//--lägg till toUpper. [V]
//styla meny bättre
//--Lägg till DateOnly istället för datetime för kvittofilen. [V]

// KassaSystemet
// 0. Data Seeding
// 1. write Menu 
// 2. Userinput (switch, console.readline, if-Satser, Loopar, Variabler.
// 3. stringmanipulation
// 4. File IO
// 5. PAY
// 6. File IO
// 7. User Message.
