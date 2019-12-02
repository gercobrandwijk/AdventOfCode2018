using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day02
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("Day02.txt");
            //string[] lines = File.ReadAllLines("Day02Part1Example.txt");

            int[] numbers;
            int[] numbersClone;

            foreach (string line in lines)
            {
                numbers = line.Split(',').Select(x => int.Parse(x)).ToArray();
                numbersClone = (int[])numbers.Clone();

                calculate(numbersClone);

                Console.WriteLine("Part 1: " + numbersClone[0]);

                int noun = 0;
                int verb = 0;

                bool done = false;

                for (noun = 0; noun < 100; noun++)
                {
                    for (verb = 0; verb < 100; verb++)
                    {
                        numbersClone = (int[])numbers.Clone();

                        numbersClone[1] = noun;
                        numbersClone[2] = verb;

                        calculate(numbersClone);

                        done = numbersClone[0] == 19690720;

                        if (done)
                            break;
                    }

                    if (done)
                        break;
                }

                Console.WriteLine("Part 2: " + (100 * noun + verb));
            }

            Console.ReadLine();
        }

        private static void calculate(int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 1)
                {
                    numbers[numbers[i + 3]] = numbers[numbers[i + 1]] + numbers[numbers[i + 2]];

                    i += 3;
                }
                else if (numbers[i] == 2)
                {
                    numbers[numbers[i + 3]] = numbers[numbers[i + 1]] * numbers[numbers[i + 2]];

                    i += 3;
                }
                else
                    break;
            }
        }
    }
}
