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
            products.Add(new string[] { "Bananer", "300", "/kg", "12.50" });
            products.Add(new string[] { "Kaffe", "301", "/st", "35.50" });
            products.Add(new string[] { "Choklad", "302", "/st", "15.00" });
            products.Add(new string[] { "Mjölk", "303", "/st", "19.50" });
            products.Add(new string[] { "Smör", "304", "/st", "34.50" });
            products.Add(new string[] { "Läsk", "305", "/kg", "94.50" });
        }
    }
}
