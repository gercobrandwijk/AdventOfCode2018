using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day04
    {
        public static void Run()
        {
            bool part1Test1 = IsValidPart1("111111") == true;
            bool part1Test2 = IsValidPart1("223450") == false;
            bool part1Test3 = IsValidPart1("123789") == false;

            int start = 172851;
            int end = 675869;

            int validCount;

            validCount = 0;

            for (int i = start; i < end; i++)
            {
                if (IsValidPart1(i.ToString()))
                    validCount++;
            }

            Console.WriteLine("Part 1: " + validCount);

            bool part2Test1 = IsValidPart2("112233") == true;
            bool part2Test2 = IsValidPart2("123444") == false;
            bool part2Test3 = IsValidPart2("111122") == true;

            validCount = 0;

            for (int i = start; i < end; i++)
            {
                if (IsValidPart2(i.ToString()))
                    validCount++;
            }

            Console.WriteLine("Part 2: " + validCount);
        }

        public static bool IsValidPart1(string input)
        {
            int[] numbers = input.ToCharArray().Select(x => (int)(x - 48)).ToArray();

            if (numbers.Length > 6)
                return false;

            bool hasDouble = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i < numbers.Length - 1)
                {
                    // Next digit is smaller
                    if (numbers[i + 1] < numbers[i])
                        return false;

                    // Next digit is same
                    if (numbers[i + 1] == numbers[i])
                        hasDouble = true;
                }
            }

            return hasDouble;
        }

        public static bool IsValidPart2(string input)
        {
            int[] numbers = input.ToCharArray().Select(x => (int)(x - 48)).ToArray();

            if (numbers.Length > 6)
                return false;

            bool hasDouble = false;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (i < numbers.Length - 1)
                {
                    // Next digit is smaller
                    if (numbers[i + 1] < numbers[i])
                        return false;

                    int sameNumberCounter = 0;

                    if (i > 0 && numbers[i - 1] == numbers[i])
                    {
                        sameNumberCounter++;

                        if (i > 1 && numbers[i - 2] == numbers[i])
                            sameNumberCounter++;
                    }

                    if (i < numbers.Length - 1 && numbers[i + 1] == numbers[i])
                    {
                        sameNumberCounter++;

                        if (i < numbers.Length - 2 && numbers[i + 2] == numbers[i])
                        {
                            sameNumberCounter++;

                            // Increase index of loop an extra time, because we already know that it will not be valid
                            i++;
                        }
                    }

                    if (sameNumberCounter == 1)
                        hasDouble = true;
                }
            }

            return hasDouble;
        }
    }
}
