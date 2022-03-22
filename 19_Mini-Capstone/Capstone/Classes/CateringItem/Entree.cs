using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Entree : CateringItem
    {
        public Entree(string code, string description, decimal price) : base(code, description, price)
        {
            Message = "Did you remember Dessert?";
            Type = "Entree";
        }
    }
}
