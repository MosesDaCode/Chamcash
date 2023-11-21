using _01_Chamcash;
using System.Globalization;
using System.Text;

namespace _01_ChamCash
{
    class Program
    {

        static void Main(string[] args)
        {
            DefaultProducts.CheckForDefaultProducts();
            var campaignPrice = new Campaigns();
            var newCostumer = new NewCostumer();
            var AdminMenuOption = new AdminMenuOption();
            campaignPrice.GetCampaignFromFile();
            campaignPrice.RemoveExpiredCampaign();

            
            bool menuIsRunning = true;
            while (menuIsRunning)
            {
                string menuChoice = Menus.FirstMenu();
                Console.Clear();

                switch (menuChoice)
                {
                    case "1":
                        newCostumer.NewCostumerChoice();
                        break;
                    case "2":
                        AdminMenuOption.AdminMenuChoice();
                        break;
                    case "0":
                        Menus.GoodByeMessage();
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










//--Funktion: När man kägger till en ny kampanj; Lägg till felhanteringskontroll att slutdatum ska vara efter startdatum eller tvärtom. -3[V]
//--Lägga till kampanj 2 för 1 funkar med start och slutdatum tidigare än idag. -2 el 3. Görs samma sak med procentkampanjen[V]
//--Systemet kraschar när du köper en produkt som du redigerat tidigare med tomma värden. Tex du anger produktid men inte anger värdet för resten (trycker enter bara) - 1 [V]
//--Ändra till rätt enhet tex klicha 1 st till rätta enheten som är kg så det blir 1 kg  - 3[V]
//--ta bort texten "kampanj existerar inte" för varje produkt som inte har kampanj efter pay - 4 [V]
//--kontrollera att antalet är integer. tex om du anger "300 e" så blir utfallet 300 0 -4 [V]
//--Efter man lagt till en ny produkt i listan så hittar den inte produkten när man vill köpa.[V]
//--lägg all logik som finns i switchcases i metoder.[V]
//--Fråga om många olika kampanjer eller många av samma kampanj. (spelar ingen roll det kan vara många av samma kampanj)[V]
//--debugga varför den dubblerar kampanjlistan när man lägger till ny procent kampanj [V] Svar: man ska inte röra listan i .txt filen då spökar den.
//--Fixa så att man inte kan skriva 0% i procentkampanj [V]
//--Fixa så man kan gå tillbaka i ta bort kampanjer menyval.[V]
//--Fixa ta bort kampanjer metod så att den tar bort utgången kampanj automatiskt[V]
//--fixa så att kampanjpriset sätts i _price property i Campaigns.[V]
//--lägg till felhantering för att skapa ny produkt.[V]
//--fixa så att _price får ett värde av kampanjpriset.[V]
//--kalla på GetCampaignFromFile() i main för att lägga till kampanj i kvitto[V]
//--kalla på GetCampaignFromFile() i main för att lägga till kampanj i kvitto
//--krashar när man skriver ett produkt id i linearSearch[V]
//--Man ska kunna lägga till flera kampanjer för olika datum[V]
//--fixa input för kampanj filen[V]
//--produkter i kassasystemet ska lagras i fil [V]
//--hamnar i en loop när jag lägger till kampanjer. kan inte gå tillbaka från menyn.[V]
//--Går inte betala med stora bokstäver PAY [V]
//--Lägg till kr i uppvisning. (nykund)[V]
//--Fixa enhetsinmatning utan / i "lägga till produkter" [V]
//--Inmatning av Produkt-Id och antal ska vara samma rad med mellanrum [V]
//--Angivna artiklar ska visas i konsollen medan man fyller på kvittot.[V]
//--lägg till felmedelande för inmatning av fel antal produkter. [V]
//--Kvitto ska sparas i annan fil med tid och datum. [V]
//--lägg till toUpper. [V]
//--styla meny bättre [V]
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
