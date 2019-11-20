using Capstone;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        VendingMachine vendingMachine = new VendingMachine();
        MoneyHandling moneyHandling = new MoneyHandling();

        //item method
        [TestMethod]
        public void SnackTypeMessageTestChip()
        {
            string name = "";
            decimal price = 2.34M;
            string type = "Chip";

            Item item = new Item(name, price, type);

            var result = item.SnackTypeMessage(item.Type); 
            Assert.AreEqual("Crunch Crunch, Yum!", result, "Snack types should have seperate output messages.");
        }
        [TestMethod]
        public void SnackTypeMessageTestCandy()
        {
            string name = "";
            decimal price = 2.34M;
            string type = "Candy";

            Item item = new Item(name, price, type);

            var result = item.SnackTypeMessage(item.Type); 
            Assert.AreEqual("Munch Munch, Yum!", result, "Snack types should have seperate output messages.");
        }

        //vending machine methods
        [TestMethod]
        public void SelectedItemTest()
        {

        }

        //money handling methods
        [TestMethod]
        public void UpdateBalanceTestFromZero()
        {
            int deposit = 5;
            moneyHandling.UserCurrentBalance = 0;

            var result = moneyHandling.UpdateBalance(deposit);
            Assert.AreEqual(5, result);
        }
        [TestMethod]
        public void UpdateBalanceTestWithPreviousBalance()
        {
            int deposit = 5;
            moneyHandling.UserCurrentBalance = 5;

            var result = moneyHandling.UpdateBalance(deposit);
            Assert.AreEqual(10, result);
        }
        [TestMethod]
        public void ReceivingNoChangeTest()
        {
            decimal change = 0;
            var result = moneyHandling.ReceivingChange(change);
            Assert.AreEqual("There is no change.", result);
        }
        [TestMethod]
        public void ReceivingChangeJustQuartersTest()
        {
            decimal change = 3.25M;
            var result = moneyHandling.ReceivingChange(change);
            Assert.AreEqual("\nThank you for your business!\n\nYour Change Is 13 Quarter(s) ", result);
        }
        [TestMethod]
        public void ReceivingChangeQuartersDimesTest()
        {
            decimal change = 2.95M;
            var result = moneyHandling.ReceivingChange(change);
            Assert.AreEqual("\nThank you for your business!\n\nYour Change Is 11 Quarter(s) and 2 Dime(s) ", result);
        }
        [TestMethod]
        public void ReceivingChangeQuartersNickelsTest()
        {
            decimal change = 2.80M;
            var result = moneyHandling.ReceivingChange(change);
            Assert.AreEqual("\nThank you for your business!\n\nYour Change Is 11 Quarter(s) and 1 Nickel(s) ", result);
        }
        [TestMethod]
        public void ReceivingChangeAllCoinsTest()
        {
            decimal change = .40M;
            var result = moneyHandling.ReceivingChange(change);
            Assert.AreEqual("\nThank you for your business!\n\nYour Change Is 1 Quarter(s) and 1 Dime(s) and 1 Nickel(s) ", result);
        }


    }
}
