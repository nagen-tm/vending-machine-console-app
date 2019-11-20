using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capstone
{
    public class Menu
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void MainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                // Display menu to user
                Console.Clear();
                Console.WriteLine("Vending Machine\n");
                Console.WriteLine("1. Display Items");
                Console.WriteLine("2. Purchase Items");
                Console.WriteLine("Q. Exit");

                // Get user input
                var selection = Console.ReadKey();
                Console.Clear();

                // Run code based on input
                if (selection.Key == ConsoleKey.D1)
                {
                    Console.WriteLine(vendingMachine.GetInventoryDisplay());
                    Console.WriteLine("\nPress Any Key To Return To Menu.");
                    Console.ReadKey();
                }
                else if (selection.Key == ConsoleKey.D2)
                {
                    SubMenu();
                    Console.ReadKey();
                }
                else if (selection.Key == ConsoleKey.Q)
                {
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection, Press Any Key To Continue.");
                    Console.ReadKey();
                }
            }
        }
        public void SubMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Purchase Menu\n");
                Console.WriteLine("1. Feed Money");
                Console.WriteLine("2. Select Product");
                Console.WriteLine("3. Finish Transaction");

                var selection = Console.ReadKey();
                Console.Clear();

                if (selection.Key == ConsoleKey.D1)
                {
                    FeedMoney();
                    Console.ReadKey();
                }
                else if (selection.Key == ConsoleKey.D2)
                {
                    Console.WriteLine(vendingMachine.GetInventoryDisplay());
                    SelectProduct();
                    Console.ReadKey();
                }
                else if (selection.Key == ConsoleKey.D3)
                {
                    FinishTransaction();
                    Console.ReadKey();
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection, Press Any Key To Continue.");
                    Console.ReadKey();
                }
            }
        }
        public void FeedMoney()
        {
            bool success = false;
            while (success == false)
            {
                Console.Write("Please Enter Amount To Deposit In Whole Dollars: ");

                try
                {
                    int depositAmount = int.Parse(Console.ReadLine());
                    if (depositAmount >= 1)
                    { 
                    success = true;
                    Console.WriteLine($"\nYou've Deposited: {depositAmount.ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
                    Console.WriteLine($"Your current balance is {vendingMachine.UpdateBalance(depositAmount).ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
                    Console.WriteLine("\nPlease Hit Any Key To Return To Menu.");
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Positive Amount.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nPlease Enter Whole Dollar Amount. ");
                }
            }
        }
        public void SelectProduct()
        {
            Console.WriteLine($"\nYour Balance is: {vendingMachine.Balance().ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
            Console.Write("\nPlease Select Item By Slot Number: ");
            string SlotSelection = Console.ReadLine().ToUpper();
            try
            {
                Console.WriteLine(vendingMachine.SelectedItem(SlotSelection));
            }
            catch(Exception)
            {
                Console.WriteLine( "\nThe slot does not exist.");
            }       
        }
        public void FinishTransaction()
        {
            decimal change = vendingMachine.Balance();
            Console.WriteLine(vendingMachine.ReceivingChange(change));
            vendingMachine.SalesReport();
            vendingMachine.LogReport();
        }
    }
}
