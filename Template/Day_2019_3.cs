using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_3
    {
        public static string firstPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);

            //Map all lines
            List<int[]> line1 = mapLine(lines[0]);
            Console.WriteLine(line1.Count.ToString());
            List<int[]> line2 = mapLine(lines[1]);
            Console.WriteLine(line2.Count.ToString());
            //Find all intersections
            List<int[]> intersections = intersect(line1, line2);
            Console.WriteLine(intersections.Count.ToString());
            //Calculate manhattan distances from O
            List<int> distFromIntersects = new List<int>();
            foreach (int[] intersection in intersections)
            {
                distFromIntersects.Add(manhattanDistance(intersection));
            }
            distFromIntersects.Sort();
            //Output the nearest
            return distFromIntersects.Count > 0 ? distFromIntersects[0].ToString() : distFromIntersects.Count.ToString();
        }
        public static string secondPuzzle(string location)
        {
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);

            //Map all lines
            List<int[]> line1 = mapLine(lines[0]);
            Console.WriteLine(line1.Count.ToString());
            List<int[]> line2 = mapLine(lines[1]);
            Console.WriteLine(line2.Count.ToString());
            //Find all intersections
            List<int[]> intersections = intersect(line1, line2);
            Console.WriteLine(intersections.Count.ToString());
            //Find smalles step distances from O
            int stepDistance = 0;
            foreach (int[] intersection in intersections)
            {
                if (stepDistance == 0 || intersection[2] < stepDistance)
                    stepDistance = intersection[2];
            }
            //Output the nearest
            return stepDistance.ToString(); //19242 for real and 610 for test input
        }
        private static List<int[]> mapLine(string line)
        {
            int[] currentPosition = { 0, 0 };
            List<int[]> lineCoordinates = new List<int[]>();
            lineCoordinates.Add(currentPosition);
            string[] directions = line.Split(',');
            foreach (string direction in directions)
            {
                lineCoordinates.AddRange(mapPoints(direction, currentPosition));
                currentPosition = lineCoordinates[lineCoordinates.Count - 1];
            }
            return lineCoordinates;
        }
        private static List<int[]> mapPoints(string direction, int[] currentPoint)
        {
            List<int[]> points = new List<int[]>();
            //R, L, U, D
            string angle = direction.Substring(0, 1);
            int length = Convert.ToInt32(direction.Substring(1));
            if (angle.Equals("R"))
            {
                for (int i = 1; i <= length; i++)
                {
                    int[] point = { currentPoint[0] + i, currentPoint[1] };
                    points.Add(point);
                }
            }
            else if (angle.Equals("L"))
            {
                for (int i = 1; i <= length; i++)
                {
                    int[] point = { currentPoint[0] - i, currentPoint[1] };
                    points.Add(point);
                }
            }
            else if (angle.Equals("U"))
            {
                for (int i = 1; i <= length; i++)
                {
                    int[] point = { currentPoint[0], currentPoint[1] + i };
                    points.Add(point);
                }
            }
            else if (angle.Equals("D"))
            {
                for (int i = 1; i <= length; i++)
                {
                    int[] point = { currentPoint[0], currentPoint[1] - i };
                    points.Add(point);
                }
            }
            return points;
        }
        private static List<int[]> intersect(List<int[]> A, List<int[]> B)
        {
            List<int[]> c = new List<int[]>();
            int i = 0;
            foreach (int[] a in A)
            {
                int j = 0;
                foreach (int[] b in B)
                {
                    //Console.WriteLine("(" + a[0] + "," + a[1] + ") <>" + "(" + b[0] + "," + b[1] + ")");
                    if (a[0] == b[0] && a[1] == b[1])
                        if (a[0] != 0 || a[1] != 0)
                        {
                            int[] x = { a[0], a[1], i + j };//add sum of steps (index of a + b)
                            c.Add(x);
                        }
                    j++;
                }
                i++;
            }
            return c;
        }
        private static int manhattanDistance(int[] point)
        {
            if (point[0] < 0)
                point[0] *= -1;
            if (point[1] < 0)
                point[1] *= -1;
            return point[0] + point[1];
        }

    }
}
