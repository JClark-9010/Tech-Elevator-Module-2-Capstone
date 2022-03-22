using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();
        public List<ScreenReport> screenReports = new List<ScreenReport>();
        FileAccess fileAccess;
        public decimal Balance { get; set; }

        public Catering()
        {
            fileAccess = new FileAccess();
            items = fileAccess.GetInventory();
        }

        public List<CateringItem> GetItems()
        {
            return SortItems(items);
        }

        public int[] CodeToInt(CateringItem item)
        {
            string itemCode = item.Code;
            string stringCode = itemCode.Substring(0, 1);
            string stringCodeNum = itemCode.Substring(1);
            int code = 0;
            int codeNum = int.Parse(stringCodeNum);
            switch (stringCode)
            {
                case "A":
                    code = 1;
                    break;
                case "B":
                    code = 2;
                    break;
                case "E":
                    code = 3;
                    break;
                case "D":
                    code = 4;
                    break;
            }
            int[] result = new int[] { code, codeNum };
            return result;
        }
        public List<CateringItem> SortItems(List<CateringItem> items)
        {
            bool changedPositions;
            do
            {
                changedPositions = false;
                for (int i = 0; i < items.Count - 1; i++)
                {
                    CateringItem holder;
                    int[] intCode = CodeToInt(items[i]);
                    int[] nextIntCode = CodeToInt(items[i + 1]);
                    if (intCode[0] > nextIntCode[0])
                    {
                        holder = items[i];
                        items[i] = items[i + 1];
                        items[i + 1] = holder;
                        changedPositions = true;
                    }
                    else if (intCode[1] > nextIntCode[1] && intCode[0] == nextIntCode[0])
                    {
                        holder = items[i];
                        items[i] = items[i + 1];
                        items[i + 1] = holder;
                        changedPositions = true;
                    }

                }
            } while (changedPositions);
            return items;
        }

        public bool AddMoney(int money)
        {
            bool moneyAdded = false;
            if (money >= 0 && money <= 500 && money+Balance <= 1500)
            {
                Balance += (decimal)money;
                moneyAdded = true;
                fileAccess.OutputLogAddMoney(money, Balance);
            }
            return moneyAdded;
        }

        public bool SelectProduct(string selection, int quantity)
        {
            bool productPurchased = false;
            foreach (CateringItem item in items)
            {
                if (selection == item.Code && item.Quantity >= quantity && Balance - (item.Price * quantity) >= 0)
                {
                    Balance -= (item.Price * quantity);
                    item.Quantity -= quantity;
                    ScreenReport screenReport = new ScreenReport(quantity, item);
                    screenReports.Add(screenReport);
                    productPurchased = true;
                }
            }
            return productPurchased;
        }



        //=======
        public bool ValidCode(string selection)
        {
            foreach (CateringItem item in items)
            {
                if (selection == item.Code)
                {
                    return true;
                }
            }
            return false;
        }
        public bool InStock(string selection, int quantity)
        {
            foreach (CateringItem item in items)
            {
                if (item.Quantity >= quantity && selection == item.Code)
                {
                    return true;
                }
            }
            return false;
        }
        public bool CanAfford(string selection, int quantity)
        {
            foreach (CateringItem item in items)
            {
                if (Balance - (item.Price * quantity) >= 0 && selection == item.Code)
                {
                    return true;
                }
            }
            return false;
        }
        public void BuyProduct(string selection, int quantity)
        {
            foreach (CateringItem item in items)
            {
                if (selection == item.Code)
                {
                    Balance -= (item.Price * quantity);
                    item.Quantity -= quantity;
                    ScreenReport screenReport = new ScreenReport(quantity, item);
                    screenReports.Add(screenReport);
                    fileAccess.OutputLogPurchase(quantity, item.Description, item.Code, item.Price, Balance);
                }
            }
        }
        //======




        public List<ScreenReport> GetScreenReports()
        {
            return screenReports;
        }

        public List<string> GetChange()
        {
            List<string> result = new List<string>();
            decimal changeForLog = Balance;
            string[] splitter = Balance.ToString().Split(".");
            int dollars = int.Parse(splitter[0]);
            int change = int.Parse(splitter[1]);
            int[] dollarAmounts = new int[] { 1, 5, 10, 20, 50, 100 };
            string[] dollarNames = new string[] {"One", "Five", "Ten", "Twent", "Fift", "Hundred" };
            int[] changeAmounts = new int[] { 5, 10, 25};
            string[] changeNames = new string[] { "Nickel", "Dime", "Quarter" };

            for(int i = dollarAmounts.Length-1; i >= 0; i--)
            {
                int billAmount = dollars / dollarAmounts[i];
                string plural = "";
                if(billAmount > 0)
                {
                    dollars -= billAmount * dollarAmounts[i];
                    if (billAmount > 1)
                    {
                        if (i == 3 || i == 4)
                        {
                            plural = "ies";
                        }
                        else
                        {
                            plural = "s";
                        }
                    }
                    else if (i == 3 || i == 4)
                    {
                        plural = "y";
                    }
                    result.Add($"({billAmount}) {dollarNames[i]}{plural}");
                }
            }
            for (int i = changeAmounts.Length - 1; i >= 0; i--)
            {
                int changeAmount = change / changeAmounts[i];
                string plural = "";
                if (changeAmount > 0)
                {
                    change -= changeAmount * changeAmounts[i];
                    if (changeAmount > 1)
                    {
                        plural = "s";
                    }
                    result.Add($"({changeAmount}) {changeNames[i]}{plural}");
                }
            }
            Balance = 0;
            fileAccess.OutputLogGiveChange(changeForLog, Balance);
            return result;
        }
    }
}
