using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class Day_2019_8
    {
        public static string firstPuzzle(string input)
        {
            int[] pixels = input.Select(x => x - 48).ToArray();
            int layers = pixels.Length / 6 / 25;
            int[,,] picture = new int[layers, 6, 25];
            var m = 0;
            var zeroCounter = 0;
            var minZero = -1;
            var minZeroIndex = 0;
            for (var i = 0; i < layers; i++)
            {
                zeroCounter = 0;
                for (var j = 0; j < 6; j++)
                {
                    for (var n = 0; n < 25; n++)
                    {
                        if(pixels[m] == 0)
                            zeroCounter++;
                        picture[i,j,n] = pixels[m];
                        m++;
                    }
                }
                if(zeroCounter < minZero || minZero < 0) {
                    minZero = zeroCounter;
                    minZeroIndex = i;
                }
            }
            var oneCounter = 0;
            var twoCOunter = 0;
            for (var j = 0; j < 6; j++)
                {
                    for (var n = 0; n < 25; n++)
                    {
                        if(picture[minZeroIndex,j,n] == 1)
                            oneCounter++;
                        if(picture[minZeroIndex,j,n] == 2)
                            twoCOunter++;
                    }
                }
            return (oneCounter * twoCOunter).ToString();
        }

        public static string secondPuzzle(string input)
        {
            return input;
        }
    }
}
