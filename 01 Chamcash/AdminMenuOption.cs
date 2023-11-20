using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class AdminMenuOption
    {
        public void AdminMenuChoice()
        {
            var products = new Products("../../../Products/ProductList.txt");
            var productEdit = new ProductEditor("../../../Products/ProductList.txt");
            var productList = products.GetProductsFromFile();
            var campaignPrice = new Campaigns();

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
                        campaignPrice.CampaignManagment();
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
        }
    }
}
