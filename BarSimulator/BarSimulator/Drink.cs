using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarSimulator
{
    class Drink
    {
        double[] prices = new double[] { 22.49, 32.49, 41.29, 43.00, 52.79, 3.99, 4.99 };
        string[] names = new string[] { "Jack Yordan's", "Johnnie Sitter", "B & J", "Bean Jim", "Russian Unusule", "Cola-Coca", "Orange Juice" };
        Random rand = new Random();
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        List<Drink> drinkList = new List<Drink>();
        List<Drink> outOfStock = new List<Drink>();
        private Drink(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        private Drink(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public Drink()
        {
            CreateProducts();
        }

        private void CreateProducts()
        {
            
            for (int i = 0; i < names.Length; i++)
            {
                drinkList.Add(new Drink(names[i], prices[i], rand.Next(2)));
            }
        }

        private double Order(string visitorName,string name, double budget)
        {
            foreach(var item in drinkList)
            {
                if(item.Name.Contains(name))
                {
                    if (item.Quantity.Equals(0))
                    { 
                        Console.WriteLine("Out of stock.");
                        outOfStock.Add(new Drink(item.Name, item.Price));
                        break;
                    }
                    if (item.Price > budget) { Console.WriteLine($"{visitorName} is getting glass of water instead because he don't have enough money."); break; }
                    budget = budget - item.Price;
                    item.Quantity -= 1;
                }
            }
            Console.WriteLine($"{visitorName} is left with ${budget}");
            return Math.Round(budget, 2);

        }

        public void GetItemOutOfStock()
        {
            foreach(var item in outOfStock)
            {
                Console.WriteLine($"{item.Name}: {item.Price}");
            }
            Console.WriteLine($"{outOfStock.Count} drinks out of stock.");
        }

        public string GetDrinkName(int n)
        {
            return names[n];
        }

        public void MakeOrder(string name, double budget, string drinkName)
        {
            Console.WriteLine($"{name} purchase {drinkName}");
            Order(name, drinkName, budget);

        }
    }
}
