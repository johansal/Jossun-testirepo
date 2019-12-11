using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_11
    {
        public static string firstPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);
            int i = 499;
            int j = 499;
            int compass = 2;
            List<Tuple<int,int>> painted = new List<Tuple<int,int>>();
            int[,] hull = new int[1000,1000];
            ic.inputs.Add(hull[i,j]);
            ic.compute();
            while (ic.isWaiting)
            {
                //Paint
                //elem1: 0 = blck, 1 = wht, elem2: 0 = left, 1 = right
                if(hull[i,j] != (int) ic.outputs.ElementAt(0))
                {
                    hull[i,j] = (int) ic.outputs.ElementAt(0);
                    painted.Add(new Tuple<int, int>(i,j));
                }
                ic.outputs.RemoveAt(0);
                //Rotate
                compass = rotateCompass(compass, (int) ic.outputs.ElementAt(0));
                ic.outputs.RemoveAt(0);
                //Move
                if(compass == 1)
                    i--;
                else if(compass == 2)
                    j--;
                else if(compass == 3)
                    i++;
                else if(compass == 4)
                    j++;
                ic.inputs.Add(hull[i,j]);
                ic.compute();
            }
            var result = painted.GroupBy(key => key, item => 1)
                               .Select(group => new { 
                                   group.Key, 
                                   Duplicates = group.Count() 
                               }).ToList();

            return result.Count.ToString();
        }

        public static int rotateCompass(int compass, int direction)
        {
            if (direction == 0)
                compass--;
            else
                compass++;
            if (compass < 1)
                compass = 4;
            else if (compass > 4)
                compass = 1;
            return compass;
        }


        public static string secondPuzzle(string location)
        {
            //Find asteroid with max line of sight to other asteroids
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            List<Tuple<int, int>> asteroids = new List<Tuple<int, int>>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        asteroids.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            Tuple<int, int> winner = new Tuple<int, int>(0, 0);
            List<Tuple<int, int>> winnerCanSee = new List<Tuple<int, int>>();

            foreach (var asteroid in asteroids)
            {
                List<Tuple<int, int>> canSee = new List<Tuple<int, int>>();
                foreach (var asteroid2 in asteroids)
                {
                    if (!asteroid.Equals(asteroid2))
                    {
                        if (canSee.Count == 0)
                            canSee.Add(asteroid2);
                        else
                        {
                            bool sameAtan = false;
                            foreach (var seen in canSee)
                            {
                                if (Math.Atan2(asteroid2.Item2 - asteroid.Item2, asteroid2.Item1 - asteroid.Item1) == Math.Atan2(seen.Item2 - asteroid.Item2, seen.Item1 - asteroid.Item1))
                                {
                                    sameAtan = true;
                                    break;
                                }
                            }
                            if (!sameAtan)
                            {
                                canSee.Add(asteroid2);
                            }
                        }
                    }
                }
                if (winnerCanSee.Count == 0 || canSee.Count > winnerCanSee.Count)
                {
                    winner = asteroid;
                    winnerCanSee = canSee;
                }
            }

            var sortedAsteroids = winnerCanSee.OrderBy(i => Math.Atan2(i.Item2 - winner.Item2, i.Item1 - winner.Item1)).ToList();

            return (sortedAsteroids.ElementAt(sortedAsteroids.Count - 200).Item2 * 100 + sortedAsteroids.ElementAt(sortedAsteroids.Count - 200).Item1).ToString();
        }
    }
}
