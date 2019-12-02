using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day01
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("Day01.txt");
            //string[] lines = File.ReadAllLines("Day01Part1Example.txt");
            //string[] lines = File.ReadAllLines("Day01Part2Example.txt");

            List<int> numbers = lines.Select(x => int.Parse(x)).ToList();
            List<int> fuelsPart1 = numbers.Select(x => part1Calculate(x)).ToList();

            //fuelsPart1.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Part 1: " + fuelsPart1.Sum());

            List<int> fuelsPart2 = numbers.Select(x => part2Calculate(x)).ToList();

            //fuelsPart2.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Part 2: " + fuelsPart2.Sum());

            Console.ReadLine();
        }

        public static int part1Calculate(int input)
        {
            return ((int)(input / 3)) - 2;
        }

        public static int part2Calculate(int input)
        {
            int output = ((int)(input / 3)) - 2;

            if (output > 0)
            {
                int nested = part2Calculate(output);

                if (nested > 0)
                    output += nested;
            }

            return output;
        }
    }
}
