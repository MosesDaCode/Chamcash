using System.Globalization;

namespace _01_Chamcash
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initierar listan med produkter.
            List<string[]> products = new List<string[]>();
            products.Add(new string[] { "Bananer", "300", "/kg", "12.50" });
            products.Add(new string[] { "Kaffe", "301", "/st", "35.50" });
            products.Add(new string[] { "Choklad", "302", "/st", "15.00" });
            products.Add(new string[] { "Mjölk", "303", "/st", "19.50" });
            products.Add(new string[] { "Smör", "304", "/st", "34.50" });
            products.Add(new string[] { "Ost", "305", "/kg", "94.50" });

            // Initierar lista för kvitto som drar ifrån listan för produkter.
            List<string[]> receipt = new List<string[]>();

            string pay = null;
            bool menuIsRunning = true; // Håller menyn aktiv

            while (menuIsRunning)
            {
                Console.WriteLine("\t-----------------");
                Console.WriteLine("\t||KASSA        ||");
                Console.WriteLine("\t-----------------");
                Console.WriteLine("\t||1. Ny kund   ||");
                Console.WriteLine("\t||2. Admin     ||");
                Console.WriteLine("\t||0. Avsluta   ||");
                Console.WriteLine("\t||             ||");
                Console.WriteLine("\t-----------------");
                Console.Write("\tAnge menyval: ");
                Int32.TryParse(Console.ReadLine(), out int menuChoice);
                Console.Clear();

                switch (menuChoice)
                {
                    case 1:
                        bool nyKundIsRunning = true; // Talar om för while-loopen att loopen är igång (true = 1 = på)
                        while (nyKundIsRunning)
                        {
                            Console.WriteLine("\t-----------------------------");
                            Console.WriteLine("\t||Ny kund                  ||");
                            Console.WriteLine("\t||Ange produktid och antal.||");
                            Console.WriteLine("\t-----------------------------");
                            Console.Write("\tAnge in antalet produkter som ska skannas: ");
                            int.TryParse(Console.ReadLine(), out int totalAmount);
                            float totalSum = 0.0f;
                            DateTime dateTime = System.DateTime.Now; // initierar dagens datum och tid ifrån datorsystemet till programmet
                            string date = dateTime.ToString("yyyyMMdd"); // initierar dagens datum utan tid ifrån datorsystemet till programmet

                            string filePath = $"../../../Receipts/RECIEPT_{date}.txt"; // filePath är sökvägen där .txt filen ska sparas.
                            string receiptText = $"-------------------------\nKVITTO  {dateTime}\n";
                            string inputIdAndAmountString = null;

                            for (int i = 0; i < totalAmount; i++)
                            {
                                //Fixa if-satsen
                                if (totalAmount <= 5)
                                {
                                    Console.Write("\n\tAnge Produkt-ID och antal med mellanslag: ");
                                     inputIdAndAmountString = Console.ReadLine();
                                }
                                else
                                {
                                    Console.WriteLine("Du har bara 5 produkter bete dig");
                                }
                                    string[] inputIdAndAmount = inputIdAndAmountString.Split(' ');
                                    int searchResult = -1;
                                    int inputAmount = 0;
                                

                                if (inputIdAndAmount.Length == 2)
                                {
                                    string inputId = inputIdAndAmount[0];
                                    int.TryParse(inputIdAndAmount[1], out inputAmount);

                                    searchResult = LinearSearch(products, inputId);

                                    if (searchResult == -1)
                                    {
                                        Console.WriteLine("\tArktikeln finns ej med");
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        receipt.Add(new string[] { inputAmount.ToString(), products[searchResult][0], products[searchResult][3], products[searchResult][2] });
                                        receiptText += $"\tProdukt-ID: {inputId}, Antal: {inputAmount}\n";
                                        nyKundIsRunning = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\tFel format. Var god ange både Produkt-ID och Antal med ett mellanslag innan antalet!");
                                    Console.ReadKey();
                                }
                            }

                            receiptText += "---------------------------\n";
                            foreach (var items in receipt)
                            {
                                if (float.TryParse(items[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float inputAmountFloat) &&
                                    float.TryParse(items[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float pricePerUnitFloat))
                                {
                                    float totalPrisAvProdukt = inputAmountFloat * pricePerUnitFloat;
                                    receiptText += $"{items[1]}\t{items[0]}st\t{items[2]}kr{items[3]} = {totalPrisAvProdukt}kr\n";
                                    totalSum += totalPrisAvProdukt;
                                }
                                else
                                {
                                    Console.WriteLine("\tOgiltigt kvitto!!");
                                }
                            }

                            receiptText += $"\n\tTotalt: {totalSum}kr\n";

                            Console.Write("\tSkriv PAY för att betala:  ");
                            pay = Console.ReadLine();
                            if (pay.ToUpper() == "PAY")
                            {
                                //File.WriteAllText(filePath, receiptText);  / Skriver kvittot receiptText över till filepath som skapar en .txt fil.
                                File.AppendAllText(filePath, receiptText);  // Lägger till nästa kvitto under det föregående.
                                Console.WriteLine("\tKvittot har sparats");
                            }
                            Console.ReadKey();
                            Console.Clear();
                        }
                        break;

                    case 0:
                        Console.WriteLine("\t---------");
                        Console.WriteLine("\t||Hejdå||");
                        Console.WriteLine("\t---------");
                        Console.ReadKey();
                        menuIsRunning = false;
                        break;
                }
            }
        }

        private static int LinearSearch(List<string[]> searchProducts, string stringInput)
        {
            for (int i = 0; i < searchProducts.Count; i++)
            {
                if (searchProducts[i][1] == stringInput)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}




//Inmatning av Produkt-Id och antal ska vara samma rad med mellanrum [V]
// Angivna artiklar ska visas i konsollen medan man fyller på kvittot. [V]
//lägg till felmedelande för inmatning av fel antal produkter.
// Kvitto ska sparas i annan fil med tid och datum. 
//lägg till toUpper.
//styla meny bättre

// KassaSystemet
// 0. Data Seeding
// 1. write Menu 
// 2. Üserinput (switch, console.readline, if-Satser, Loopar, Variabler.
// 3. stringmanipulation
// 4. File IO
// 5. PAY
// 6. File IO
// 7. User Message.
