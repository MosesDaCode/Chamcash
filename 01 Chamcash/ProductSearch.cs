using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class ProductSearch
    {
        private List<string[]> products;
        private string productFilePatch = "../../../Products/Productlist.txt";
        public ProductSearch()
        {
            products = new List<string[]>();
            InitializeProducts();
        }
        public static int LinearSearch(List<string[]> searchProducts, string stringInput)
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
        public List<string[]> GetProducts()
        {
            return products;
        }
        private void InitializeProducts()
        {
            //products.Add(new string[] { File.ReadAllLines(productFilePatch) });
            products.Add(new string[] { "Bananer", "300", "/kg", "12.50" });
            products.Add(new string[] { "Kaffe", "301", "/st", "35.50" });
            products.Add(new string[] { "Choklad", "302", "/st", "15.00" });
            products.Add(new string[] { "Mjölk", "303", "/st", "19.50" });
            products.Add(new string[] { "Smör", "304", "/st", "34.50" });
            products.Add(new string[] { "Läsk", "305", "/kg", "94.50" });

        }
        public void EditProducts(List<string[]> product, string productId, string newName, string newPrice, string newUnit)
        {
            int searchResult = LinearSearch(product, productId);

            if (searchResult != -1)
            {
                products[searchResult][0] = newName;
                products[searchResult][3] = newPrice;
                products[searchResult][2] = newUnit;

                Console.WriteLine("Produkten har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Produkt-ID hittades ej, Försök igen");
            }
        }
        public void CreateNewProduct()
        {
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||Lägg till en produkt ||");
            Console.WriteLine("\t-----------------------\n");
            Console.Write("\t\n\nAnge produkt-ID: ");
            string productId = Console.ReadLine();
            int searchResult = LinearSearch(products, productId);
            if (searchResult == -1)
            {

                Console.Write("\tAnge produktens namn: ");
                string productName = Console.ReadLine();



                Console.Write("\tAnge ett pris: ");
                string productprice = Console.ReadLine();

                Console.Write("\tAnge en enhet (/st eller /kg: ");
                string productUnit = Console.ReadLine();

                products.Add(new string[] { productName, productId, productUnit, productprice });

                Console.WriteLine("\tProdukten har sparats! Tryck på enter för att fortsätta.");
                Console.ReadKey();
            }
            else 
            {
                Console.WriteLine("\tProdukt-ID existerar redan! tryck enter för att fortsätta");
                Console.ReadKey();
                Console.Clear();
            }
            
        }
    }
}
