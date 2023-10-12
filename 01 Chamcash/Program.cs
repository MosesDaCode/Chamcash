using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _01_Chamcash
{
    class Program
    {
        static void Main(string[] args)
        {

            ProductSearch productSearch = new ProductSearch();
            List<string[]> products = productSearch.GetProducts(); // Initierar listan med produkter.
           
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
                            


                            Menus.NewCostumerMenu();



                            string filePath = $"../../../Receipts/RECIEPT_{dateTime.ToString("yyyy-MM-dd")}.txt"; // filePath skapar en .txt fil. filePath är sökvägen där kvittot ska sparas. 
                            string receiptText = $"\n\nKVITTO  {dateTime}\n";


                            List<string[]> receipt = new List<string[]>();


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

                                    searchResult = ProductSearch.LinearSearch(products, inputId);

                                    if (searchResult == -1)
                                    {
                                        Console.WriteLine("\tArtikeln finns ej med");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        receipt.Add(new string[] { inputAmount.ToString(), products[searchResult][0], products[searchResult][3], products[searchResult][2] });
                                        receiptText += $"\tProdukt-ID: {inputId}, Antal: {inputAmount}\n";

                                        Console.WriteLine($"\t{products[searchResult][0]} {products[searchResult][3]} * {inputAmount}  ");
                                    }
                                    

                                }
                                else if (inputIdAndAmountString == "pay")
                                {
                                    receiptText += "---------------------------\n";
                                    foreach (var items in receipt)
                                    {
                                        if (float.TryParse(items[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float inputAmountFloat) &&
                                            float.TryParse(items[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float pricePerUnitFloat))
                                        {
                                            float totalPriceOfProduct = inputAmountFloat * pricePerUnitFloat;
                                            receiptText += $"{items[1]}\t{items[0]}st\t{items[2]}kr{items[3]} = {totalPriceOfProduct}kr\n";
                                            totalSum += totalPriceOfProduct;
                                        }

                                        else
                                        {
                                            Console.WriteLine("\tOgiltigt kvitto!!");
                                        }
                                    }

                                    receiptText += $"\n\tTotalt: {totalSum}kr\n-------------------------";
                                    if (inputIdAndAmountString.ToUpper() == "PAY")
                                    {

                                        File.AppendAllText(filePath, receiptText);
                                        Console.WriteLine(receiptText);
                                        Console.WriteLine("\tKvittot har sparats, tryck på enter för att komma vidare.");

                                        receipt.Clear();
                                    }
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
                                    productSearch.CreateNewProduct();
                                    Console.Clear();
                                    break;
                                case "2":
                                    Console.WriteLine("\t---------------------------");
                                    Console.WriteLine("\t||Redigering av produkter||");
                                    Console.WriteLine("\t---------------------------\n");

                                    Console.Write("\tAnge Produkt-ID för produkten som ska redigeras: ");
                                    string productToEdit = Console.ReadLine();

                                    Console.Write("\tAnge ett nytt namn: ");
                                    string newName = Console.ReadLine();

                                    Console.Write("\tAnge ett nytt pris: ");
                                    string newPrice = Console.ReadLine();

                                    Console.Write("\tAnge en ny enhet enligt följande (/st, /kg): ");
                                    string newUnit = Console.ReadLine();

                                    productSearch.EditProducts(products, productToEdit, newName, newPrice, newUnit);

                                    Console.WriteLine("\tTryck på enter för att fortsätta... ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "3":
                                    Console.WriteLine("Meny valet existerar inte för tillfället, vänligen tryck enter för att försöka igen ");
                                    Console.ReadKey();
                                    Console.Clear();
                                    break;
                                case "0":
                                    Console.Clear ();
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









//Inmatning av Produkt-Id och antal ska vara samma rad med mellanrum [V]
// Angivna artiklar ska visas i konsollen medan man fyller på kvittot.[V]
//lägg till felmedelande för inmatning av fel antal produkter. [V]
// Kvitto ska sparas i annan fil med tid och datum. [V]
//lägg till toUpper. [V]
//styla meny bättre
// Lägg till DateOnly istället för datetime för kvittofilen. [V]

// KassaSystemet
// 0. Data Seeding
// 1. write Menu 
// 2. Userinput (switch, console.readline, if-Satser, Loopar, Variabler.
// 3. stringmanipulation
// 4. File IO
// 5. PAY
// 6. File IO
// 7. User Message.
