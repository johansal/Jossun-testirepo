﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_16
    {
        public static string firstPuzzle(string input)
        {
            string tmp = input;
            //Console.WriteLine("After phase 0: " + tmp);
            for (int i = 1; i <= 100; i++)
            {
                tmp = fftPhase(tmp);
                //Console.WriteLine("After phase " + i + ": " + tmp.Substring(0, 8));
            }
            return tmp.Substring(0, 8);
        }

        public static string secondPuzzle(string input)
        {

            string tmp = input;
            Console.WriteLine(tmp.Length);
            int messageOffSet = int.Parse(tmp.Substring(0, 7));
            for (int i = 1; i <= 100; i++)
            {
                tmp = fftPhase(tmp);
            }
            return tmp.Substring(messageOffSet, 8);
        }

        public static string fftPhase(string input)
        {
            int[] pattern = { 0, 1, 0, -1 };
            string output = "";

            for (int i = 1; i <= input.Length; i++)
            {
                int signal = 0;
                int offset = 0;
                for (int j = 1; j <= input.Length; j++)
                {
                    if (j % i == 0)
                        offset = offset == 3 ? 0 : offset + 1;
                    //Console.Write(offset + " (" + pattern[offset] + ") ");
                    //Console.Write(input[j-1].ToString() + "*" + pattern[offset] + " + ");
                    signal += int.Parse(input[j - 1].ToString()) * pattern[offset];
                }
                signal = Math.Abs(signal);
                //Console.Write("\n");
                //Console.Write("=" + signal + " (" + (signal % 10).ToString() + ")\n");
                output += (signal % 10).ToString();
            }
            return output;
        }

        /*
        The first observation is that on row N, we skip the first N-1 elements, 
        then sum the next N ones, skip N, subtract N-1, etc, until at the end of the row. 
        We don't need any multiplications, just sums.

        The second observation is that we can compute the partial sums of the input 
        once at the start of one of the 100 steps, 
        and then use cumulative_sum[k + n] - cumulative_sum[k] to compute the sum of elements 
        from k to k + n in a single operation.

        With that, the work for the last 2/3 lines needs a single operation 
        (since the last 2/3 lines only contain a single large block of ones, see picture). 
        The next 1/3 - 1/5 need 2 operations, the next 1/5 - 1/7 need 3, and so on.
        */
    }
}