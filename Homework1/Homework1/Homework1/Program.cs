using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Homework1
{
    class Program
    {                                                    //Your path:
        static string text = System.IO.File.ReadAllText(@"C:\Users\NikolayPleshkov\Desktop\hw\book.txt");
        private static IDictionary<string, int> GetWordOccurrence(string txt)
        {
            String[] deli_c = txt.Split(' ', '.', ',', '-', '_', '*', ':', ';', '`', '?', '!', '<', '>', '&', '[', ']', '(', ')', '{', '}');
            IDictionary<string, int> words = new SortedDictionary<string, int>();
            foreach (string ct in deli_c)
            {
                if (string.IsNullOrEmpty(ct.Trim())) continue;

                int count;
                if (!words.TryGetValue(ct, out count)) count = 0;

                words[ct] = count + 1;
            }

            return words;
        }

        public static void PrintWordOccurrence(IDictionary<string, int> wordOccurrence)
        {
            StreamWriter sw = new StreamWriter("wordOccurrence.txt");
            using (sw)
            {
                foreach (KeyValuePair<string, int> word in wordOccurrence)
                {
                    sw.WriteLine($"{word.Key} - {word.Value}");
                }
            }

            Console.WriteLine("File is generated in ./obj/wordOccurrence.txt .");
        }


        public static void PrintNumberOfWords(IDictionary<string, int> wordOccurrence)
        {

            int count = 0;
            foreach (int value in wordOccurrence.Values)
            {
                count += value;
            }
            var c_th = Thread.CurrentThread;
            Console.WriteLine("Managed thread #{0}", c_th.ManagedThreadId);
            Console.WriteLine($"Number of words: {count}");
        }


        public static void PrintLessCommonWords()
        {
            var c_th = Thread.CurrentThread;
            Console.WriteLine("\nManaged thread #{0}", c_th.ManagedThreadId);
            Console.WriteLine("Less common words:");
            var deli_chars = new char[] { ' ', ',', ':', '\t', '\"', '\r', '{', '}', '[', ']', '=', '/', '1', '2', '3' };
            var mostCommon = text
                .Split(deli_chars)
                .Where(x => x.Length >= 2)
                .Select(x => x.ToLower())
                .GroupBy(x => x)
                .Select(x => new { Word = x.Key, Count = x.Count() })
                .OrderBy(x => x.Count)
                .Take(5)
                .ToDictionary(x => x.Word, x => x.Count);

            foreach (var word in mostCommon)
            {
                Console.WriteLine($"Word: {word.Key}\t|\tOccurrence: {word.Value} times\t|");
            }
        }

        private static void PrintMostCommongWords()
        {
            var c_th = Thread.CurrentThread;
            Console.WriteLine("\nManaged thread #{0}", c_th.ManagedThreadId);
            Console.WriteLine("Most common words:");
            var deli_chars = new char[] { ' ', ',', ':', '\t', '\"', '\r', '{', '}', '[', ']', '=', '/' };
            var mostCommon = text
                .Split(deli_chars)
                .Where(x => x.Length >= 2) //OR Length > 3
                .Select(x => x.ToLower())
                .GroupBy(x => x)
                .Select(x => new { Word = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToDictionary(x => x.Word, x => x.Count);

            foreach (var line in mostCommon)
            {
                Console.WriteLine($"Word: {line.Key} \t|\tOccurrence: {line.Value} times \t|");
            }

        }

        public static void AvgWordLength()
        {

            double countedWords = text.Split(' ').Average(n => n.Length);
            var c_th = Thread.CurrentThread;
            Console.WriteLine("\nManaged thread #{0}", c_th.ManagedThreadId);
            Console.WriteLine("Avg word length: " + Math.Round(countedWords, 1));
        }

        public static void GetLongestWord()
        {

            string longestWord = "";
            string pattern = @"(\w+)\s";
            string input = text;
            Match m = Regex.Match(input, pattern);

            while (m.Success)
            {
                if (longestWord.Length < m.Value.Length)
                {
                    longestWord = m.Value;
                }

                m = m.NextMatch();
            }
            var c_th = Thread.CurrentThread;
            Console.WriteLine("\nManaged thread #{0}", c_th.ManagedThreadId);
            Console.WriteLine("Longest word: " + longestWord);
        }

        public static void GetShortestWord()
        {

            text = text + " ";
            int len = text.Length;
            String k = " ";
            String shWord = " ";
            char ch;

            int p;
            int min1 = len;

            for (int i = 0; i < len; i++)
            {
                ch = text[i];
                if (ch != ' ')
                {
                    k = k + ch;
                }
                else
                {
                    p = k.Length - 1;
                    if (p < min1)
                    {
                        min1 = p;
                        shWord = k;
                    }

                    k = " ";
                }
            }
            var c_th = Thread.CurrentThread;
            Console.WriteLine("\nManaged thread #{0}", c_th.ManagedThreadId);
            Console.Write("Shortest Word = " + shWord + "\n");
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Thread> threads = new List<Thread>();
            List<Action> funcs = new List<Action>();

            Action numOfWords = delegate () { PrintNumberOfWords(GetWordOccurrence(text)); };
            Action leastCommon = delegate () { PrintLessCommonWords(); };
            Action mostCommon = delegate () { PrintMostCommongWords(); };
            Action longestWord = delegate () { GetLongestWord(); };
            Action shortestWord = delegate () { GetShortestWord(); };
            Action avgLen = delegate () { AvgWordLength(); };

            funcs.Add(numOfWords);
            funcs.Add(leastCommon);
            funcs.Add(mostCommon);
            funcs.Add(longestWord);
            funcs.Add(shortestWord);
            funcs.Add(avgLen);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Exec time: ~1.24s
            for (int i = 0; i < funcs.Count; i++)
            {
                Thread _thread = new Thread(funcs[i].Invoke);
                _thread.IsBackground = true;
                _thread.Start();
                threads.Add(_thread);
                Thread.Sleep(200);
            }

            //foreach (var t in threads) t.Join();

            //Exec time: ~2.20s
            //PrintWordOccurrence(GetWordOccurrence(text));
            //PrintNumberOfWords(GetWordOccurrence(text));
            //PrintLessCommonWords();
            //Console.WriteLine();
            //PrintMostCommongWords();
            //GetLongestWord();
            //GetShortestWord();
            //AvgWordLength();

            stopwatch.Stop();
            Console.WriteLine("\nPress ENTER to exit.");
            Console.ReadKey();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

            Console.WriteLine("\nRunTime " + elapsedTime);

        }
    }
