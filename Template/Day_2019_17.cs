﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_17
    {
        public static string firstPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);

            ic.compute();

            int[,] scaffolding = new int[49,42];
            int i = 0, j = 0;

            int alignmentParam = 0;

            foreach (var o in ic.outputs)
            {
                if(o == 10) {
                    i++;
                    j = 0;
                }
                else {
                    scaffolding[i,j] = (int)o;
                    j++;
                }
                Console.Write((char)o);
            }
            for(i = 1; i < 48; i++) {
                for(j = 1; j < 41; j++) {
                    if(scaffolding[i,j] == 35) {
                        //check if its intersection
                        if(scaffolding[i-1,j] == 35 && scaffolding[i+1,j] == 35 && scaffolding[i,j-1] == 35 && scaffolding[i,j+1] == 35) {
                            alignmentParam += i*j;
                            //Console.WriteLine("Intersection at " + i + ", " + j + " alignment params is (" + n + "*" + m + ")=" + alignmentParam);
                        }
                    }
                }
                    
            }
            return alignmentParam.ToString();
        }

        public static string secondPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);

            long score = 0;

            return score.ToString();
        }
    }
}