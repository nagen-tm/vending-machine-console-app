using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capstone
{
    public class MoneyHandling
    {
        //Variables
        public decimal UserCurrentBalance { get; set; } = 0;
        public decimal TotalRevenue { get; set; }
        public string TotalChange { get; set; }

        private int quarterAmount = 0;
        private int dimeAmount = 0;
        private int nickelAmount = 0;

        public List<string> logList = new List<string>();


        //Logic dealing with deposit and balance
        public decimal UpdateBalance(int deposit)
        {
            UserCurrentBalance += deposit;
            //add the transaction for the log
            logList.Add($"{DateTime.UtcNow} FEED MONEY: {deposit.ToString("c", CultureInfo.GetCultureInfo("en-US"))} {UserCurrentBalance.ToString("c", CultureInfo.GetCultureInfo("en-US"))}");
            return UserCurrentBalance;
        }

        //Logic for giving change back
        public string ReceivingChange(decimal change)
        {
            //variables
            TotalChange = change.ToString("c", CultureInfo.GetCultureInfo("en-US"));
            const decimal QUARTERVALUE = .25M;
            const decimal DIMEVALE = .10M;
            const decimal NICKELVALUE = .05M;

            //finding number of each coin
            quarterAmount = (int)(change / QUARTERVALUE);
            change -= quarterAmount * QUARTERVALUE;
            dimeAmount = (int)(change / DIMEVALE);
            change -= dimeAmount * DIMEVALE;
            nickelAmount = (int)(change / NICKELVALUE);
            change -= nickelAmount * NICKELVALUE;
            UserCurrentBalance = 0;
            //add the transaction for the log
            logList.Add($"{DateTime.UtcNow} GIVE CHANGE: {TotalChange}");

            return FinalTransactionMessage(quarterAmount, dimeAmount, nickelAmount);
        }

        private string FinalTransactionMessage(int quarterAmount, int dimeAmount, int nickelAmount)
        {
            string result = "\nThank you for your business!\n\nYour Change Is ";
            bool noChange = quarterAmount == 0 && nickelAmount == 0 && dimeAmount == 0;
            bool justQuarters = nickelAmount == 0 && dimeAmount == 0;
            bool quartersAndNickels = dimeAmount == 0;
            bool quartersAndDimes = nickelAmount == 0;

            if (noChange)
            {
                result = "There is no change.";
            }
            else if (justQuarters)
            {
                result += $"{quarterAmount} Quarter(s) ";
            }
            else if (quartersAndNickels)
            {
                result += $"{quarterAmount} Quarter(s) and {nickelAmount} Nickel(s) ";
            }
            else if (quartersAndDimes)
            {
                result += $"{quarterAmount} Quarter(s) and {dimeAmount} Dime(s) ";
            }
            else
            {
                result += $"{quarterAmount} Quarter(s) and {dimeAmount} Dime(s) and {nickelAmount} Nickel(s) ";
            }
            return result;

        }

    }
}
