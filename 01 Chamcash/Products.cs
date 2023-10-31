using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ChamCash
{
    public class Products
    {
        public List<string[]> _productList;
        public static string _productFilePath = "../../../Products/ProductList.txt";

        public Products(string productFilePath)
        {
            _productFilePath = productFilePath;
            _productList = GetProductsFromFile();

        }
        public List<string[]> GetProductsFromFile()
        {
            string[] productlines = File.ReadAllLines(_productFilePath);

            List<string[]> products = new List<string[]>();
            foreach (string line in productlines)
            {
                string[] productInfo = line.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                products.Add(productInfo);
            }
            return products;
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

        public void SaveProductsToFile(string[] newproduct)
        {
            string line = String.Join(", ", newproduct);

            File.AppendAllText(_productFilePath, line + Environment.NewLine);
        }
        
    }
}
