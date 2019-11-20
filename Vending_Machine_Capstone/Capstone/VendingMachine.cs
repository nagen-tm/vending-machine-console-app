using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        private string directory = Environment.CurrentDirectory;
        private const string filePath = @"..\..\..\..\VendingMachine.txt";
        private const string salesFileName = @"..\..\..\..\...\..\SalesReport.txt";
        private string logFileName = @"..\..\..\..\...\..\LogReport.txt";
        private MoneyHandling moneyHandling = new MoneyHandling();
        private Dictionary<string, Item> Inventory = new Dictionary<string, Item>();

        public VendingMachine()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine();
                        string[] itemProperties = line.Split("|");
                        Item createdItem = new Item(itemProperties[1], decimal.Parse(itemProperties[2]), itemProperties[3]);
                        Inventory.Add(itemProperties[0], createdItem);
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error Reading The File.");
                Console.ReadKey();
            }
        }

        public string GetInventoryDisplay()
        {
            string result = "";
            foreach (string key in Inventory.Keys)
            {
                if (Inventory[key].Quantity > 0)
                {
                    result += $"\n {key} | {Inventory[key].Name.PadRight(20,' ')} "+ $"| {Inventory[key].Price} ";
                }
                else
                {
                    result += $" {key} | SOLD OUT\n";
                }
            }
            return result;
        }
        //writing out sales report and log files
        public void SalesReport()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(salesFileName, false))
                {
                    foreach (string key in Inventory.Keys)
                    {       
                        sw.WriteLine($"{Inventory[key].Name} | {5 - Inventory[key].Quantity}");
                    }
                    sw.WriteLine($"\n**TOTAL SALES**  {moneyHandling.TotalRevenue.ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error Writing The File.");
                Console.ReadKey();
            }
        }
        public void LogReport()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFileName, true))
                {
                    foreach (string line in moneyHandling.logList)
                    {      
                        sw.WriteLine($"{line}");
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error Writing The File.");
                Console.ReadKey();
            }
        }

        public string SelectedItem(string key)
        {
            string result = "";
            string keyName = Inventory[key].Name;
            decimal keyPrice = Inventory[key].Price;
            string keyType = Inventory[key].Type;
            Item keyItem = new Item(keyName, keyPrice, keyType);
            

            if (moneyHandling.UserCurrentBalance > keyPrice == false)
            {
                result = $"\nPlease Deposit Money to Purchase the Item";
            }
            else if (Inventory[key].Quantity == 0)
            {
                result = $"\nThe Item in {key} is Sold Out";
            }
            else if (Inventory.ContainsKey(key))
            {
                string salesReportItem = $"{keyName}";

                moneyHandling.UserCurrentBalance -= keyPrice;
                moneyHandling.TotalRevenue += keyPrice;
                Inventory[key].Quantity--;
                //add the transaction for the log
                moneyHandling.logList.Add($"{DateTime.UtcNow} {keyName} {key} {keyPrice.ToString("c", CultureInfo.GetCultureInfo("en-US"))} {moneyHandling.UserCurrentBalance.ToString("c", CultureInfo.GetCultureInfo("en-US"))}");

                result = $"\n{keyItem.SnackTypeMessage(keyType)}\n\nYour New Balance is {moneyHandling.UserCurrentBalance.ToString("c", CultureInfo.GetCultureInfo("en-US"))}";
            }
            else
            {
                result = $"\nThe {key} does not exist.";
            }
            return result;
            
        }

        //methods to call information from Money Handling class
        public decimal UpdateBalance(int depositAmount)
        {
            return moneyHandling.UpdateBalance(depositAmount);
        }
        public decimal Balance()
        {
            return moneyHandling.UserCurrentBalance;
        }
        public string ReceivingChange(decimal change)
        {
            string result = $"{moneyHandling.ReceivingChange(change)}";
            if (change > 0)
            {
                result += $"for a total of {moneyHandling.TotalChange}.";
            }
    
            return result;
        }
    }
}
