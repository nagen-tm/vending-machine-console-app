using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; } = 5;

        public Item(string name, decimal price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }
        public string SnackTypeMessage(string keyType)
        {
            string type = keyType;
            string result = "";
            if (type == "Chip")
            {
                result = "Crunch Crunch, Yum!";
            }
            else if (type == "Candy")
            {
                result = "Munch Munch, Yum!";
            }
            else if (type == "Drink")
            {
                result = "Glug Glug, Yum!";
            }
            else if (type == "Gum")
            {
                result = "Chew Chew, Yum!";
            }
            return result;
        }

    }
}
