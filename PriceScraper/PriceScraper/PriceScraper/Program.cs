using CsvHelper;
using HtmlAgilityPack;
using Microsoft.VisualBasic.FileIO;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriceScraper
{
    public class _Title
    {
        public string Title { get; set; }
    }

    public class _Price
    {
        public string Price { get; set; }

    }

    class Program
    {

        public static void Scrape()
        {
            var c_th = Thread.CurrentThread;
            Console.WriteLine("Managed thread #{0}", c_th.ManagedThreadId);
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.amazon.de/s?bbn=427957031&rh=n%3A427957031%2Cp_89%3AApple&dc&language=en&qid=1635956033&rnid=669059031&ref=lp_427957031_nr_p_89_0");

            var Headers = doc.DocumentNode.CssSelect("h2.a-size-mini > a");
            var Price = doc.DocumentNode.CssSelect("span.a-price-whole");

            var titles = new List<_Title>();
            var price = new List<_Price>();
            foreach (var item in Headers)
            {
                titles.Add(new _Title { Title = item.InnerText });
            }
            foreach (var item in Price)
            {
                price.Add(new _Price { Price = item.InnerText });
            }

            var products = titles.Zip(price, (key, value) => new { key, value })
                                 .ToDictionary(x => x.key, x => x.value);

            using (var writer = new StreamWriter("hubspot.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {

                csv.WriteRecords(products);

            }
        }

        public static void LoadItems()
        {
            var c_th = Thread.CurrentThread;
            Console.WriteLine("Managed thread #{0}", c_th.ManagedThreadId);
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\Nikolay Pleshkov\Documents\GitHub\hw-tpl\PriceScraper\PriceScraper\PriceScraper\bin\Debug\net5.0\hubspot.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        Console.WriteLine(field); ;
                    }
                }
            }

        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Task scrape = new Task(Scrape);
            Task load = new Task(LoadItems);
            scrape.Start();
            Console.WriteLine("Getting the items from amazon.");
            while (!scrape.IsCompleted)
            {
                Console.Write(".");
                Task.Delay(300).Wait();
            }
            scrape.Wait();
            /*https://blog.stephencleary.com/2014/05/a-tour-of-task-part-1-constructors.html*/
            Task.Run(() =>
            {
                try
                {
                    load.Start();
                    load.Wait();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
            Console.ReadLine();
        }
    }

}
