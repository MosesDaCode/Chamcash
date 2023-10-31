using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ChamCash
{
    public class SerialNumber
    {
        public int ReceiptSerialNumber()
        {

            int serialNumber = 0;
            string serialNumberFilePath = "../../../SerialNumber/serialNumber.txt";
            if (File.Exists(serialNumberFilePath))
            {
                string serialNumberContent = File.ReadAllText(serialNumberFilePath);
                if (int.TryParse(serialNumberContent, out serialNumber))
                {
                    serialNumber++;
                }
            }
            return serialNumber;
           
        }

    }
}
