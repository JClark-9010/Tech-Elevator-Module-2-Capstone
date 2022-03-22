using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class ScreenReport
    {
        public int Quantity { get; set; }
        public CateringItem Item { get; set; }
        public string Code { get; set; }

        public ScreenReport(int quantity, CateringItem item)
        {
            Quantity = quantity;
            Item = item;
            Code = item.Code;
        }

        public override string ToString()
        {
            return $"{Quantity.ToString().PadRight(6)}{Item.Type.PadRight(15)}{Item.Description.PadRight(25)}" +
                $"{Item.Price.ToString("C").PadRight(10)}{(Item.Price*Quantity).ToString("C").PadRight(10)}{Item.Message}";
        }
    }


}
