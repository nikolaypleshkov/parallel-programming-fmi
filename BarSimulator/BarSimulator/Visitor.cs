using System;
using System.Threading;
namespace BarSimulator
{
    class Visitor
    {
        enum NightlifeActivities { Walk, VisitBar, GoHome };
        enum BarActivities { Drink, Dance, PurchaseDrink, Leave };

        Random random = new Random();

        public string Name { get; set; }
        public double Budget { get; set; }
        public int Age { get; set; }
        public Bar Bar { get; set; }
        
        Drink drink = new Drink();

        public int Sales { get; set; }

        private NightlifeActivities GetRandomNightlifeActivity()
        {
            int n = random.Next(10);
            if (n < 3) return NightlifeActivities.Walk;
            if (n < 8) return NightlifeActivities.VisitBar;
            return NightlifeActivities.GoHome;
        }

        private BarActivities GetRandomBarActivity()
        {
            int n = random.Next(10);
            if (n < 3) return BarActivities.Dance;
            if (n < 6) return BarActivities.PurchaseDrink;
            if (n < 9) return BarActivities.Leave;
            return BarActivities.Leave;
        }

        private void WalkOut()
        {
            Console.WriteLine($"{Name} is walking in the streets.");
            Thread.Sleep(100);
        }

        private void VisitBar()
        {
            if (Age < 18) Console.WriteLine($"{Name} is getting kicked out because he is {Age} years old.");
            else
            {
                Console.WriteLine($"{Name} is getting in the line to enter the bar.");
                
                Bar.Enter(this);
                Console.WriteLine($"{Name} entered the bar!");
                bool staysAtBar = true;
                while (staysAtBar)
                {
                    var nextActivity = GetRandomBarActivity();
                    switch (nextActivity)
                    {
                        case BarActivities.Dance:
                            Console.WriteLine($"{Name} is dancing.");
                            Thread.Sleep(100);
                            break;
                        case BarActivities.PurchaseDrink:
                            string drinkName = drink.GetDrinkName(random.Next(7));
                            drink.MakeOrder(Name, Budget, drinkName);
                            Thread.Sleep(100);
                            break;
                        case BarActivities.Drink:
                            Console.WriteLine($"{Name} is drinking.");
                            Thread.Sleep(100);
                            break;
                        case BarActivities.Leave:
                            Console.WriteLine($"{Name} is leaving the bar.");
                            Bar.Leave(this);
                            staysAtBar = false;
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }
        public void PaintTheTownRed()
        {
            WalkOut();
            bool staysOut = true;
            while (staysOut)
            {
                var nextActivity = GetRandomNightlifeActivity();
                switch (nextActivity)
                {
                    case NightlifeActivities.Walk:
                        WalkOut();
                        break;
                    case NightlifeActivities.VisitBar:
                        VisitBar();
                        staysOut = false;
                        break;
                    case NightlifeActivities.GoHome:
                        staysOut = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }

        public Visitor(string name,int age, Bar bar, double budget)
        {
            Name = name;
            Age = age;
            Bar = bar;
            Budget = budget;
        }
        public Visitor() { }
    }
}
