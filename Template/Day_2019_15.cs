using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Template
{
    public class Day_2019_15
    {
        public static string firstPuzzle(string input)
        {
            long[] intcode = input.Split(',').Select(long.Parse).ToArray();
            IntComputer ic = new IntComputer(intcode);

            int[] repairDroidPosition = { 39, 49 };
            int[,] board = new int[80, 100];
            board[repairDroidPosition[0], repairDroidPosition[1]] = 8;
            int directionToMove = 1;
            ic.inputs.Add(directionToMove);
            ic.compute();
            while (ic.isWaiting)
            {
                if (ic.outputs.Count != 1)
                {
                    Console.WriteLine("output: " + ic.outputs[0] + ", " + ic.outputs[1]);
                    throw new Exception("Less or more than one output, " + ic.outputs.Count);
                }
                else
                {
                    if (ic.outputs[0] == 0)
                    {
                        int[] wallPosition = updatePosition(repairDroidPosition, directionToMove);
                        board[wallPosition[0], wallPosition[1]] = 4;
                        //sanity check
                        if (wallPosition[0] == repairDroidPosition[0] && wallPosition[1] == repairDroidPosition[1])
                            throw new Exception("Wall updater is updating the droid as well");
                        //update direction
                        directionToMove = directionToMove == 1 ? 4 : directionToMove == 4 ? 2 : directionToMove == 2 ? 3 : directionToMove == 3 ? 1 : 0;
                    }
                    else if (ic.outputs[0] == 1)
                    {
                        board[repairDroidPosition[0], repairDroidPosition[1]] = board[repairDroidPosition[0], repairDroidPosition[1]] == 2 ? 2 : 1;
                        repairDroidPosition = updatePosition(repairDroidPosition, directionToMove);
                        board[repairDroidPosition[0], repairDroidPosition[1]] = 8;
                    }
                    if (ic.outputs[0] == 2)
                    {
                        board[repairDroidPosition[0], repairDroidPosition[1]] = 1;
                        repairDroidPosition = updatePosition(repairDroidPosition, directionToMove);
                        board[repairDroidPosition[0], repairDroidPosition[1]] = 2;

                    }
                }
                ic.outputs.RemoveAt(0);

                Console.Write("\n");
                Console.SetCursorPosition(0, 5);
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write(board[i, j]);
                    }
                    Console.Write("\n");
                }
                while (!Int32.TryParse(Console.ReadLine(), out directionToMove))
                {
                    Console.WriteLine("Wrong input!");
                }
                ic.inputs.Add(directionToMove);
                ic.compute();
            }
            return repairDroidPosition[0] + "," + repairDroidPosition[1];
        }
        public static int[] updatePosition(int[] oldPosition, int direction)
        {
            int[] newPosition = { oldPosition[0], oldPosition[1] };
            if (direction == 1)
            {
                newPosition[0]--;
            }
            else if (direction == 2)
            {
                newPosition[0]++;
            }
            else if (direction == 3)
            {
                newPosition[1]--;
            }
            else if (direction == 4)
            {
                newPosition[1]++;
            }
            else
            {
                throw new Exception("Invalid direction " + direction);
            }
            return newPosition;
        }

        public static string secondPuzzle(string input)
        {
            return input;
        }
    }
}
