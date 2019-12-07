using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Template
{
    public class IntComputer
    {
        const int ADD = 1;
        const int MULTIPLY = 2;
        const int INPUT = 3;
        const int OUTPUT = 4;
        const int JUMP_IF_T = 5;
        const int JUMP_IF_F = 6;
        const int LESS_THAN = 7;
        const int EQUALS = 8;
        const int STOP = 99;
        int position { get; set; }
        int[] memory { get; set; }

        public bool isHalted { get; set; }
        public bool isWaiting { get; set; }
        public List<int> inputs { get; set; }
        public List<int> outputs { get; set; }

        public IntComputer(int[] m)
        {
            position = 0;
            memory = m;
            isHalted = false;
            isWaiting = false;
            inputs = new List<int>();
            outputs = new List<int>();
        }
        public bool compute()
        {
            while (position < memory.Length)
            {
                //Parse int code and parameter mode (0 position, 1 immediate)
                int[] digits = memory[position].ToString().Select(t => int.Parse(t.ToString())).ToArray();
                int intCode = digits.Length >= 2 ? digits[digits.Length - 2] * 10 + digits[digits.Length - 1] : digits[0];
                if (intCode == ADD)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    memory[memory[position + 3]] = param1 + param2;
                    position += 4;
                }
                else if (intCode == MULTIPLY)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    memory[memory[position + 3]] = param1 * param2;
                    position += 4;
                }
                else if (intCode == INPUT)
                {
                    if (inputs.Count > 0)
                    {
                        if(isWaiting)
                            isWaiting = false;
                        memory[memory[position + 1]] = inputs.First();
                        inputs.RemoveAt(0);
                        position += 2;
                    }
                    else
                    {
                        isWaiting = true;
                        return true;
                    }
                }
                else if (intCode == OUTPUT)
                {
                    position += 2;
                    outputs.Add(memory[memory[position - 1]]);
                }
                else if (intCode == JUMP_IF_T)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    if (param1 != 0)
                        position = param2;
                    else
                        position += 3;
                }
                else if (intCode == JUMP_IF_F)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    if (param1 == 0)
                        position = param2;
                    else
                        position += 3;
                }
                else if (intCode == LESS_THAN)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    memory[memory[position + 3]] = param1 < param2 ? 1 : 0;
                    position += 4;
                }
                else if (intCode == EQUALS)
                {
                    int param1 = digits.Length > 2 && digits[digits.Length - 3] == 1 ? memory[position + 1] : memory[memory[position + 1]];
                    int param2 = digits.Length > 3 && digits[digits.Length - 4] == 1 ? memory[position + 2] : memory[memory[position + 2]];
                    memory[memory[position + 3]] = param1 == param2 ? 1 : 0;
                    position += 4;
                }
                else if (intCode == STOP)
                {
                    isHalted = true;
                    return true;
                }
                else
                {
                    throw new Exception("Unsupported opcode " + memory[position].ToString() + " at " + position);
                }
            }
            Console.WriteLine("We shouldn't be here!");
            return false;
        }
    }
}