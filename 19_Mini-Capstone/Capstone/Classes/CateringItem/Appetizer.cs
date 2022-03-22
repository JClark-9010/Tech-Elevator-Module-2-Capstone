using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Appetizer : CateringItem
    {
        public Appetizer(string code, string description, decimal price) : base(code, description, price)
        {
            Message = "You might need extra plates.";
            Type = "Appetizer";
        }
    }
}
