using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ChamCash
{
    public class ProductEditor : Products
    {
        public ProductEditor(string productFilePatch) : base(productFilePatch)
        {

        }

        public void EditProduct(List<string[]> productToEdit)
        {
            Console.WriteLine("\t---------------------------");
            Console.WriteLine("\t||Redigering av produkter||");
            Console.WriteLine("\t---------------------------\n");

            Console.Write("\tAnge Produkt-ID för produkten som ska redigeras: ");
            string oldProductId = Console.ReadLine();
            int searchResult = LinearSearch(productToEdit, oldProductId);
            if (searchResult != -1)
            {
                Console.Write("\tAnge ett nytt namn: ");
                string newName = Console.ReadLine();

                Console.Write("\tAnge ett nytt pris: ");
                string newPrice = Console.ReadLine();

                Console.Write("\tAnge en ny enhet emellan st eller kg: ");
                string newUnit = Console.ReadLine();

                string[] editedProduct = { newName, oldProductId, newUnit, newPrice };

                int index = _productList.FindIndex(product => product[1] == oldProductId);

                if (searchResult != -1)
                {
                    _productList[index][0] = editedProduct[0];
                    _productList[index][3] = editedProduct[3];
                    _productList[index][2] = editedProduct[2];

                    string updatedLine = string.Join(", ", _productList[index]);

                    List<string> lines = File.ReadAllLines(_productFilePath).ToList();

                    lines[index] = updatedLine;

                    File.WriteAllLines(_productFilePath, lines);
                }

                Console.WriteLine("Produkten har updaterats!");
                Console.WriteLine("\tTryck på enter för att fortsätta... ");
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Produkt-ID hittades ej, försökt igen!");
            }

        }
        
        public void CreateNewProduct(List<string[]> product)
        {
            Console.WriteLine("\t-------------------------");
            Console.WriteLine("\t||Lägg till en produkt ||");
            Console.WriteLine("\t-----------------------\n");
            Console.Write("\n\n\tAnge produkt-ID: ");
            string productId = Console.ReadLine();
            int searchResult = LinearSearch(product, productId);
            if (searchResult == -1)
            {

                Console.Write("\tAnge produktens namn: ");
                string productName = Console.ReadLine();



                Console.Write("\tAnge ett pris: ");
                string productprice = Console.ReadLine();

                Console.Write("\tAnge en enhet emellan st eller kg: ");
                string productUnit = Console.ReadLine();

                string[] newProduct = { productName, productId, productUnit, productprice };
                SaveProductsToFile(newProduct);

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
