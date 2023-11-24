using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ChamCash
{
    public class ProductEditor : Products
    {
        public ProductEditor(string productFilePath) : base(productFilePath)
        {

        }

        public void EditProduct(List<string[]> productToEdit)
        {
            Console.WriteLine("\t-------------------------------");
            Console.WriteLine("\t||Redigering av produkter    ||");
            Console.WriteLine("\t||Ange 0 för att gå tillbaka ||");
            Console.WriteLine("\t-------------------------------\n");

            Console.Write("\tAnge Produkt-ID för produkten som ska redigeras: ");
            string oldProductId = Console.ReadLine();
            int searchResult = LinearSearch(productToEdit, oldProductId);
            if (searchResult != -1)
            {
                Console.Write("\tAnge ett nytt namn: ");
                string newName = Console.ReadLine();
                if (newName != "0" && newName != "")
                {
                    Console.Write("\tAnge ett nytt pris: ");
                    string newPrice = Console.ReadLine();
                    if (newPrice != "0" && newPrice != "")
                    {
                        Console.Write("\tAnge antingen st eller kg: ");
                        string newUnit = Console.ReadLine();
                        if (newUnit != "0" && newUnit != "" && (newUnit == "kg".ToLower() || newUnit == "st".ToLower()))
                        {
                            string[] editedProduct = { newName, oldProductId, newUnit, newPrice };

                            int index = _productList.FindIndex(product => product[1] == oldProductId);

                            if (searchResult != -1)
                            {
                                _productList[index][0] = editedProduct[0];
                                _productList[index][3] = editedProduct[3];
                                _productList[index][2] = editedProduct[2];

                                string updatedLine = string.Join(",", _productList[index]);

                                List<string> lines = File.ReadAllLines(_productFilePath).ToList();

                                lines[index] = updatedLine;

                                File.WriteAllLines(_productFilePath, lines);
                            }

                            Console.WriteLine("\tProdukten har updaterats!");
                            Console.WriteLine("\tTryck på enter för att fortsätta... ");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Redigering avbruten, Tryck på enter för att fortsätta.");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Redigering avbruten, Tryck på enter för att fortsätta.");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                else
                {
                    Console.WriteLine("Redigering avbruten, Tryck på enter för att fortsätta.");
                    Console.ReadKey();
                    Console.Clear();
                }


            }
            else if (oldProductId == "0")
            {
                Console.WriteLine("Redigering avbruten, Tryck på enter för att fortsätta.");
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Produkt-ID hittades ej, tryck på enter och försökt igen!");
                Console.ReadKey();
                Console.Clear();
                EditProduct(_productList);
            }

        }

        public void CreateNewProduct(List<string[]> product)
        {
            Console.WriteLine("\t-------------------------------");
            Console.WriteLine("\t||Lägg till en produkt       ||");
            Console.WriteLine("\t||Ange 0 för att gå tillbaka ||");
            Console.WriteLine("\t-------------------------------\n");
            Console.Write("\n\n\tAnge produkt-ID för produkten som ska läggas till: ");
            string productId = Console.ReadLine();
            string productName = null;
            string productPrice = null;
            string productUnit = null;
            int searchResult = LinearSearch(product, productId);
            if (searchResult == -1 && productId != "0")
            {
                Console.Write("\tAnge produktens namn: ");
                productName = Console.ReadLine();

                if (productName != "0" && productName != "")
                {
                    Console.Write("\tAnge ett pris: ");
                    productPrice = Console.ReadLine();
                    if (productPrice != "0" && productPrice != "")
                    {
                        Console.Write("\tAnge antingen st eller kg: ");
                        productUnit = Console.ReadLine();
                        if (productUnit != "0" && productUnit != "" && (productUnit == "kg".ToLower() || productUnit == "st".ToLower()))
                        {
                            string[] newProduct = { productName, productId, productUnit, productPrice };
                            SaveProductsToFile(newProduct);

                            Console.WriteLine("\tProdukten har sparats! Tryck på enter för att fortsätta.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Åtgärd avbruten, Tryck på enter för att fortsätta.");
                            Console.ReadKey();
                            Console.Clear();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Åtgärd avbruten, Tryck på enter för att fortsätta.");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                else
                {
                    Console.WriteLine("Åtgärd avbruten, Tryck på enter för att fortsätta.");
                    Console.ReadKey();
                    Console.Clear();
                }


            }
            else if (productId == "0")
            {
                Console.WriteLine("Åtgärd avbruten, Tryck på enter för att fortsätta.");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\tProdukt-ID existerar redan! Tryck enter för att fortsätta");
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
