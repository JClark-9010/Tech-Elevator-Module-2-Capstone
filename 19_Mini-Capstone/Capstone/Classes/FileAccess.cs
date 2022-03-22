using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileAccess
    {

        private string filePath;
        private string inputFile;
        private string outputFile;

        public FileAccess()
        {
            filePath = @"C:\Catering";
            inputFile = Path.Combine(filePath, "cateringsystem.csv");
            outputFile = Path.Combine(filePath, "Log.txt");
        }

        public List<CateringItem> GetInventory()
        {

            List<CateringItem> items = new List<CateringItem>();
            using (StreamReader sr = new StreamReader(inputFile))
            {

                while (!sr.EndOfStream)
                {
                    string item = sr.ReadLine();
                    string[] itemArray = item.Split("|");
                    string itemType = itemArray[0];
                    string itemCode = itemArray[1];
                    string itemDescription = itemArray[2];
                    decimal itemPrice = decimal.Parse(itemArray[3]);
                    CateringItem cateringItem = new CateringItem(itemCode, itemDescription, itemPrice);
                    switch (itemType)
                    {
                        case "A":
                            cateringItem = new Appetizer(itemCode, itemDescription, itemPrice);
                            break;
                        case "B":
                            cateringItem = new Beverage(itemCode, itemDescription, itemPrice);
                            break;
                        case "E":
                            cateringItem = new Entree(itemCode, itemDescription, itemPrice);
                            break;
                        case "D":
                            cateringItem = new Dessert(itemCode, itemDescription, itemPrice);
                            break;
                    }
                    
                    items.Add(cateringItem);
                }
            }
            return items;
        }

        public void OutputLogAddMoney(int money, decimal balance)
        {

            using (StreamWriter sw = new StreamWriter(outputFile, true))
            {
                sw.WriteLine($"{DateTime.Now}  ADD MONEY: {money:C}  {balance:C}");
            }
        }

        public void OutputLogPurchase(int quantity, string item, string itemCode, decimal cost, decimal balance)
        {

            using (StreamWriter sw = new StreamWriter(outputFile, true))
            {
                sw.WriteLine($"{DateTime.Now}  {quantity}  {item}  {itemCode}   {cost*quantity:C}  {balance:C}");
            }
        }

        public void OutputLogGiveChange(decimal change, decimal balance)
        {

            using (StreamWriter sw = new StreamWriter(outputFile, true))
            {
                sw.WriteLine($"{DateTime.Now}  GIVE CHANGE: {change:C}  {balance:C}");
            }
        }
    }
}




