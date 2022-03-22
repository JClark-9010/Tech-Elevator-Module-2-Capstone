using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Beverage : CateringItem
    {
        public Beverage(string code, string description, decimal price) : base(code, description, price)
        {
            Message = "Don't Forget Ice.";
            Type = "Beverage";
        }
    }
}
