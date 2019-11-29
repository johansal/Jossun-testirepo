using System;
using System.IO;
using System.Text;

namespace Template
{
    class Program
    {
        static void Main(string[] args)
        {
            string year;
            string day;
            string puzzle;
            string input;
            string output = "";
            var watch = new System.Diagnostics.Stopwatch();

            Console.WriteLine("Loading input...");

            //Allowed arguments: year(int) day(int) puzzle(1/2)

            if (args.Length < 2 || args.Length > 3 || !Int32.TryParse(args[0], out int r1) || !Int32.TryParse(args[1], out int r2))
            {
                year = DateTime.Now.Year.ToString();
                day = DateTime.Now.Day.ToString();
                if (args.Length == 1)
                    puzzle = args[0];
                else 
                    puzzle = "1";
            }
            else
            {
                year = args[0];
                day = args[1];
                if (args.Length == 3 && (args[2].Equals("1") || args[2].Equals("2")))
                    puzzle = args[2];
                else
                    puzzle = "1";
            }
            input = GetInputString(year, day);
            //Console.WriteLine("debug :: " + input);

            Console.Write("Running puzzle " + puzzle + "...  ");

            watch.Start();

            if (year.Equals("2019") && day.Equals("27"))
            {
                output = Aoc_2019_27(input);
            }
            else if (year.Equals("2019") && day.Equals("28"))
            {
                if (puzzle.Equals("1"))
                    output = Day_2019_28.firstPuzzle(input);
                else if (puzzle.Equals("2"))
                    output = Day_2019_28.secondPuzzle(input);
            }
            else
            {
                output = year + "/" + day + " not implemented!";
            }

            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds + "ms");
            Console.WriteLine("Output:");
            Console.WriteLine(output);
        }

        #region Helper methods
        private static string GetInputString(string year, string day)
        {
            string s;
            string location = "./inputs/" + year + "_" + day + ".txt";
            try
            {
                s = File.ReadAllText(@location, Encoding.UTF8);
                Console.WriteLine("Got input for " + year + "/" + day + "!");
            }
            catch (FileNotFoundException)
            {
                s = null;
                Console.WriteLine("Missing input for " + year + "/" + day + "!");
            }
            return s;
        }
        #endregion

        #region ADVENT OF CODE DAILIES
        private static string Aoc_2019_27(string input)
        {
            return "fooBar";
        }
        #endregion
    }
}
