using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    internal class ProductSearch
    {
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
