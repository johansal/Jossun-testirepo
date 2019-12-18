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
                tmp = fftPhase(tmp,1);
                //Console.WriteLine("After phase " + i + ": " + tmp.Substring(0, 8));
            }
            return tmp.Substring(0, 8);
        }

        public static string secondPuzzle(string input)
        {

            string tmp = input;
            Console.WriteLine(tmp.Length);
            int messageOffSet = int.Parse(tmp.Substring(0,7));
            for (int i = 1; i <= 100; i++)
            {
                tmp = fftPhase(tmp,10000);
            }
            return tmp.Substring(messageOffSet, 8);
        }

        public static string fftPhase(string input, int m)
        {
            int[] pattern = { 0, 1, 0, -1 };
            string output = "";
            //use m to repeat input string
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
                    signal += int.Parse(input[j-1].ToString()) * pattern[offset];
                }
                signal = Math.Abs(signal);
                //Console.Write("\n");
                //Console.Write("=" + signal + " (" + (signal % 10).ToString() + ")\n");
                output += (signal % 10).ToString();
            }
            return output;
        }
    }
}