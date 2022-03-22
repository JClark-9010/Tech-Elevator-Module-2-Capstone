using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering;
        private List<CateringItem> items;
        private int menuChoice;
        private int error;
        private bool showMenuAgain;

        public UserInterface()
        {
            catering = new Catering();
            items = catering.GetItems();
            menuChoice = 0;
            showMenuAgain = false;
        }
        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                switch (menuChoice)
                {
                    case 0:
                        Console.Clear();
                        MainMenu();
                        break;
                    case 1:
                        DisplayItems();
                        Console.WriteLine();
                        MainMenu();
                        break;
                    case 2:
                        OrderMenu();
                        break;
                    case 3:
                        done = true;
                        break;
                    case 11:
                        AddMoneyMenu();
                        break;
                    case 12:
                        DisplayItems();
                        Console.WriteLine();
                        SelectProductsMenu();
                        break;
                    case 13:
                        CheckoutMenu();
                        break;
                }
            }
        }

        public void MainMenu()
        {
            int menuOption = 0;
            
            Console.WriteLine("(1) Display Catering Items");
            Console.WriteLine("(2) Order");
            Console.WriteLine("(3) Quit");
            Console.WriteLine();
             
            if (showMenuAgain)
            {
                ErrorMessage(error);
                error = 0;
            }
            Console.Write("Enter a menu option: ");
            try
            {
                menuOption = int.Parse(Console.ReadLine());
                if(menuOption > 3 || menuOption <1)
                {
                    showMenuAgain = true;
                    error = 1;
                    return;
                }
            }
            catch (FormatException)
            {
                showMenuAgain = true;
                error = 1;
                return;
            }

            showMenuAgain = false;
            menuChoice = menuOption;
        }


        public void OrderMenu()
        {
            int menuOption = 0;
            
            Console.Clear();
            Console.WriteLine("(1) Add Money");
            Console.WriteLine("(2) Select Products");
            Console.WriteLine("(3) Complete Transaction");
            Console.WriteLine($"Current Accout Balance: {catering.Balance.ToString("C")}");
            Console.WriteLine();
            if (showMenuAgain)
            {
                ErrorMessage(error);
                error = 0;
            }
            Console.Write("Enter a menu option: ");
            try
            {
                menuOption = int.Parse(Console.ReadLine());
                if (menuOption > 3 || menuOption < 1)
                {
                    showMenuAgain = true;
                    error = 1;
                    return;
                }
            }
            catch (FormatException)
            {
                showMenuAgain = true;
                error = 1;
                return;
            }
            showMenuAgain = false;
            menuChoice = menuOption + 10;
        }


        public void DisplayItems()
        {
            Console.Clear();
            Console.WriteLine($"{"Product Code", -17}{"Description", -24}{"Quantity", -8}Price");
            foreach (CateringItem item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void AddMoneyMenu()
        {
            if(showMenuAgain)
            {
                ErrorMessage(error);
                showMenuAgain = false;
                error = 0;
            }
                
            Console.Write("Enter amount of money to add: ");
            try
            {
                int money = int.Parse(Console.ReadLine());
                if (!catering.AddMoney(money))
                {
                    showMenuAgain = true;
                    error = 2;
                    return;
                }
            }
            catch (FormatException)
            {
                showMenuAgain = true;
                error = 2;
                return;
            }
            showMenuAgain = false;
            menuChoice = 2;
        }

        public void SelectProductsMenu()
        {
            if (showMenuAgain)
            {
                ErrorMessage(error);
                showMenuAgain = false;
                error = 0;
            }


            //need to deal with checking if code is valid
            Console.Write("Enter your selection: ");
            string selection = Console.ReadLine();
            //int quantity = 0;
            try
            {
                selection = $"{selection.Substring(0, 1).ToUpper()}{selection.Substring(1)}";
                //bool validCode = false;
                //foreach(CateringItem item in items)
                //{
                //    if(selection == item.Code)
                //    {
                //        validCode = true;
                //    }
                //}
                if (!catering.ValidCode(selection))
                {
                    showMenuAgain = true;
                    error = 4;
                    return;
                }
            }
            catch (FormatException)
            {
                showMenuAgain = true;
                error = 4;
                return;
            }

            try
            {
                Console.Write("Enter the order quanitity: ");
                int quantity = int.Parse(Console.ReadLine());
                //if (!catering.SelectProduct(selection, quantity))
                //{
                //    error = 3;
                //    showMenuAgain = true;
                //    return;
                //}
                if (!catering.InStock(selection, quantity))
                {
                    showMenuAgain = true;
                    error = 3;
                    return;
                }
                else if(!catering.CanAfford(selection, quantity))
                {
                    showMenuAgain = true;
                    error = 6;
                    return;
                }
                else
                {
                    catering.BuyProduct(selection, quantity);
                }
            }
            catch (FormatException)
            {
                showMenuAgain = true;
                error = 5;
                return;
            }
            catch (ArgumentOutOfRangeException)
            {
                showMenuAgain = true;
                error = 5;
                return;
            }
            showMenuAgain = false;
            menuChoice = 2;
        }

        public void CheckoutMenu()
        {
            decimal total = 0;
            List<ScreenReport> screenReports = catering.GetScreenReports();
            Console.Clear();
            foreach(ScreenReport sr in screenReports)
            {
                Console.WriteLine(sr.ToString());
                total += sr.Item.Price * sr.Quantity;
            }
            Console.WriteLine();
            Console.WriteLine($"Total: {total.ToString("C")}");
            Console.WriteLine();

            //=======
            List<string> change = catering.GetChange();
            Console.Write("You received ");
            for(int i = 0; i < change.Count-1; i++)
            {
                Console.Write(change[i]);
                Console.Write(", ");
            }
            Console.Write(change[change.Count - 1]);
            Console.WriteLine(" in change");
            //=======
            Console.ReadLine();
            menuChoice = 0;
        }

        public void ErrorMessage(int num)
        {
            switch (num)
            {
                case 1:
                    Console.WriteLine("Invalid menu option.");
                    break;

                case 2:
                    Console.WriteLine("Invalid amount of money.");
                    break;

                case 3:
                    Console.WriteLine("Insufficient stock.");
                    break;

                case 4:
                    Console.WriteLine("Invalid product code.");
                    break;
                case 5:
                    Console.WriteLine("Invalid quantity.");
                    break;
                case 6:
                    Console.WriteLine("Insufficient funds.");
                    break;
            }
        }
    }
}

                          