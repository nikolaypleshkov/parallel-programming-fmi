using System;
using System.Collections.Generic;
using System.Threading;

namespace BarSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = 13, max = 69;
            Random rand = new Random();
            Bar bar = new Bar();
            List<Thread> visitorThreads = new List<Thread>();
            for (int i = 1; i < 200; i++)
            {
                int age = rand.Next(min, max);
                var visitor = new Visitor(i.ToString(),age, bar, 50.0);
                var thread = new Thread(visitor.PaintTheTownRed);
                thread.Start();
                visitorThreads.Add(thread);
            }

            foreach (var t in visitorThreads) t.Join();
            Console.WriteLine();
            Console.WriteLine("The party is over.");
            Console.ReadLine();
        }
    }
}
