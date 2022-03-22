using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Dessert : CateringItem
    {
        public Dessert(string code, string description, decimal price) : base(code, description, price)
        {
            Message = "Coffee goes with dessert.";
            Type = "Dessert";
        }
    }
}
