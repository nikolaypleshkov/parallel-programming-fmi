using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


namespace BarSimulator
{
    class Warehouse
    {
        public static List<Drink> drinks;

        public static List<Drink> Drinks
        {
            get
            {
                if (drinks.Count == 0)
                {
                    AddDrinks();
                }
                return drinks;
            }
            set
            {
                drinks = value;
            }
        }
        private static void AddDrinks()
        {
            Drinks = AllDrinks();
        }
        private void SaveDrink()
        {
            Save(drinks);
        }
        public void Add(Drink drink)
        {
            Drinks.Add(drink);
            SaveDrink();
        }

        private static List<Drink> AllDrinks()
        {
            List<Drink> lsDrinks = new List<Drink>();

            if (File.Exists("data.json"))
            {
                string json = File.ReadAllText("data.json");
                if (!string.IsNullOrWhiteSpace(json))
                {
                    lsDrinks = JsonConvert.DeserializeObject<List<Drink>>(json);
                }
            };

            return lsDrinks;
        }

        private static void Save(List<Drink> drinks)
        {
            if (!File.Exists("data.json"))
            {
                File.Create("data.json");
            }
            string json = JsonConvert.SerializeObject(drinks);

            File.WriteAllText("data.json", json);
        }

        public Warehouse()
        {
            Drinks = new List<Drink>();
        }


    }
}
