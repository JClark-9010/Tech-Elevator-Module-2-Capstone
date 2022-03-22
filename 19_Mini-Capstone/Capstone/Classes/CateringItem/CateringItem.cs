using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class should contain the definition for one catering item

        public string Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; }

        public CateringItem(string code, string description, decimal price)
        {
            
            Code = code;
            Description = description;
            Quantity = 25;
            Price = price;
        }

        public override string ToString()
        {
            string quantity = Quantity.ToString();
            if(Quantity <= 0)
            {
                quantity = "SOLD OUT";
            }
            return $" {Code.PadRight(16)}{Description.PadRight(24)}" +
                    $"{quantity.PadRight(10)}{Price.ToString("C")}";
        }
    }
}
