using _01_ChamCash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Chamcash
{
    public class DefaultProducts
    {
        public static void CheckForDefaultProducts()
        {
            string defaultProducts = "Bananer,300,kg,12.50\n" +
                                     "Kaffe,301,st,35.50\n" +
                                     "Godis,302,kg,98.90\n" +
                                     "Mjölk,303,st,19.50\n" +
                                     "Snus,304,st,49";
            if (File.Exists("../../../Products/ProductList.txt"))
            {
                return;
            }
            else
            {
                File.WriteAllText("../../../Products/ProductList.txt", defaultProducts);
            }
        }
    }
}
