using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2019.Program;

namespace AdventOfCode2019
{
    public class Day16 : AdventOfCodeDay
    {
        public Day16(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            string line = File.ReadAllLines("Data/Day16.txt")[0];
            line = "12345678,4";
            line = "80871224585914546619083218645595,100";
            //line = "19617804207202209144916044189917,100";
            //line = "69317163492948606335995924319873,100";

            string stringOfNumber = line.Split(',')[0];
            string stringOfPhaseCount = line.Split(',')[1];

            long basePatternSize = 4;
            long[] basePattern = new long[4] { 0, 1, 0, -1 };

            long phases = long.Parse(stringOfPhaseCount);
            long currentPhase = 1;

            long number = long.Parse(stringOfNumber);
            long currentNumber = number;

            long patternSize = basePatternSize * currentPhase - 1;
            List<List<long>> patterns = new List<List<long>>();

            long currentNumberLength = currentNumber.ToString().Length;
            long[] currentNumberDigits = currentNumber.ToString().Select(x => ((long)x) - 48).ToArray();

            while (patterns.Count < currentNumberLength)
            {
                var pattern = new List<long>();

                long repeater = patterns.Count + 1;

                for (int i = 0; pattern.Count <= currentNumberLength; i++)
                {
                    for (int j = 0; j < repeater; j++)
                    {
                        pattern.Add(basePattern[i % basePatternSize]);
                    }
                }

                pattern.RemoveAt(0);

                patterns.Add(pattern);
            }

            for (int phase = 0; phase < phases; phase++)
            {
                currentNumberLength = currentNumber.ToString().Length;
                currentNumberDigits = currentNumber.ToString().Select(x => ((long)x) - 48).ToArray();

                string newNumber = string.Empty;

                for (int i = 0; i < currentNumberLength; i++)
                {
                    var currentPattern = patterns[i];

                    long sumPerPhase = 0;

                    for (int j = 0; j < currentNumberLength; j++)
                    {
                        sumPerPhase += currentNumberDigits[j] * currentPattern[j];
                    }

                    if (sumPerPhase < 0)
                        sumPerPhase *= -1;

                    newNumber += sumPerPhase.ToString().Last();
                }

                currentNumber = long.Parse(newNumber);
            }

            Console.WriteLine("After phase " + phases + ": " + currentNumber);
        }
    }
}
