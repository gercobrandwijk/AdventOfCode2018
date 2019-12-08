using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day05 : AdventOfCodeDay
    {
        public Day05(int number) : base(number)
        {
        }

        public override void Run()
        {
            RunPart1();
            RunPart2();
        }

        public void RunPart1()
        {
            string[] lines = File.ReadAllLines("Data/Day05Part1.txt");

            string line = lines[1];

            int[] numbers;
            int[] numbersClone;

            numbers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            numbersClone = (int[])numbers.Clone();

            calculatePart1(numbersClone, int.Parse(lines[0]));
        }

        public void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day05Part2.txt");

            string line = lines[1];

            int[] numbers;
            int[] numbersClone;

            numbers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            numbersClone = (int[])numbers.Clone();

            calculatePart2(numbersClone, int.Parse(lines[0]));
        }

        private static void calculatePart1(int[] numbers, int operationCode3Number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int operationCode = numbers[i];

                bool param1IsImmediate = false;
                bool param2IsImmediate = false;
                bool param3IsImmediate = false;

                if (operationCode >= 100)
                {
                    int tmp = operationCode;
                    operationCode = tmp % 10;

                    param1IsImmediate = ((tmp / 100) % 10) == 1;
                    param2IsImmediate = ((tmp / 1000) % 10) == 1;
                    param3IsImmediate = ((tmp / 10000) % 10) == 1;
                }

                if (operationCode == 1)
                {
                    doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate, param3IsImmediate);

                    i += 3;
                }
                else if (operationCode == 2)
                {
                    doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate, param3IsImmediate);

                    i += 3;
                }
                else if (operationCode == 3)
                {
                    numbers[numbers[i + 1]] = operationCode3Number;

                    i += 1;
                }
                else if (operationCode == 4)
                {
                    if(numbers[numbers[i + 1]] > 1000)
                    {
                        Console.WriteLine("Part 1: " + numbers[numbers[i + 1]]);

                        break;
                    }

                    i += 1;
                }
                else
                    break;
            }
        }

        private static void calculatePart2(int[] numbers, int operationCode3Number)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                int operationCode = numbers[i];

                bool param1IsImmediate = false;
                bool param2IsImmediate = false;
                bool param3IsImmediate = false;

                if (operationCode >= 100)
                {
                    int tmp = operationCode;
                    operationCode = tmp % 10;

                    param1IsImmediate = ((tmp / 100) % 10) == 1;
                    param2IsImmediate = ((tmp / 1000) % 10) == 1;
                    param3IsImmediate = ((tmp / 10000) % 10) == 1;
                }

                if (operationCode == 1)
                {
                    doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate, param3IsImmediate);

                    i += 3;
                }
                else if (operationCode == 2)
                {
                    doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate, param3IsImmediate);

                    i += 3;
                }
                else if (operationCode == 3)
                {
                    numbers[numbers[i + 1]] = operationCode3Number;

                    i += 1;
                }
                else if (operationCode == 4)
                {
                    if (numbers[numbers[i + 1]] > 1000)
                    {
                        Console.WriteLine("Part 2: " + numbers[numbers[i + 1]]);

                        break;
                    }

                    i += 1;
                }
                else if (operationCode == 5)
                {
                    int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                    int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                    if (firstParam != 0)
                        i = secondParam - 1; // -1 because loop also adds one
                    else
                        i += 2;
                }
                else if (operationCode == 6)
                {
                    int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                    int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                    if (firstParam == 0)
                        i = secondParam - 1; // -1 because loop also adds one
                    else
                        i += 2;
                }
                else if (operationCode == 7)
                {
                    int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                    int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                    if (firstParam < secondParam)
                        numbers[getParamPosition(numbers, i + 3, param3IsImmediate)] = 1;
                    else
                        numbers[getParamPosition(numbers, i + 3, param3IsImmediate)] = 0;

                    i += 3;
                }
                else if (operationCode == 8)
                {
                    int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                    int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                    if (firstParam == secondParam)
                        numbers[getParamPosition(numbers, i + 3, param3IsImmediate)] = 1;
                    else
                        numbers[getParamPosition(numbers, i + 3, param3IsImmediate)] = 0;

                    i += 3;
                }
                else
                    break;
            }
        }

        private static void doCalculation(int[] numbers, int operationCode, int i, bool param1IsImmediate, bool param2IsImmediate, bool param3IsImmediate)
        {
            int leftSide = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
            int rightSide = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

            numbers[getParamPosition(numbers, i + 3, param3IsImmediate)] = operationCode == 1 ? leftSide + rightSide : leftSide * rightSide;
        }

        private static int getParamPosition(int[] numbers, int i, bool paramIsImmediate)
        {
            return paramIsImmediate ? i : numbers[i];
        }
    }
}
