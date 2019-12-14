using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day07 : AdventOfCodeDay
    {
        public Day07(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            RunPart1();
            RunPart2();
        }

        public void RunPart1()
        {
            string[] lines = File.ReadAllLines("Data/Day07.txt");

            string line = lines[0];

            int[] numbers;

            numbers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            int amplifierAmount = 5;

            Dictionary<int, AmplifierPart1> amplifiers = new Dictionary<int, AmplifierPart1>();

            IEnumerable<IEnumerable<int>> allPhaseCombinations = this.GetPermutations(Enumerable.Range(0, amplifierAmount));

            //// Example 1: 43210
            //numbers = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0".Split(',').Select(x => int.Parse(x)).ToArray();
            //allPhaseCombinations = new List<List<int>>() { new List<int>() { 4, 3, 2, 1, 0 } };
            //// Example 2: 54321
            //numbers = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0".Split(',').Select(x => int.Parse(x)).ToArray();
            //allPhaseCombinations = new List<List<int>>() { new List<int>() { 0, 1, 2, 3, 4 } };
            //// Example 3: 65210
            //numbers = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0".Split(',').Select(x => int.Parse(x)).ToArray();
            //allPhaseCombinations = new List<List<int>>() { new List<int>() { 1, 0, 4, 3, 2 } };

            for (int i = 0; i < amplifierAmount; i++)
            {
                char name = (char)(i + 65);

                amplifiers.Add(i, new AmplifierPart1(name, numbers));
            }

            int highestOutput = 0;

            var allPhaseCombinationsList = allPhaseCombinations.ToList();

            for (int x = 0; x < allPhaseCombinationsList.Count; x++)
            {
                var phases = allPhaseCombinationsList[x].ToList();

                int output = 0;
                int input = 0;

                for (int i = 0; i < amplifierAmount; i++)
                {
                    output = amplifiers[i].Calculate(phases[i], input);
                    input = output;
                }

                if (output > highestOutput)
                    highestOutput = output;
            }

            Console.WriteLine("Part 1: " + highestOutput);
        }

        public void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day07.txt");

            string line = lines[0];

            int[] numbers;

            numbers = line.Split(',').Select(x => int.Parse(x)).ToArray();

            int amplifierAmount = 5;

            IEnumerable<IEnumerable<int>> allPhaseCombinations = this.GetPermutations(Enumerable.Range(5, amplifierAmount));

            //// Example 1: 139629729
            //numbers = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5".Split(',').Select(x => int.Parse(x)).ToArray();
            //allPhaseCombinations = new List<List<int>>() { new List<int>() { 9, 8, 7, 6, 5 } };
            //// Example 2: 18216
            //numbers = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10".Split(',').Select(x => int.Parse(x)).ToArray();
            //allPhaseCombinations = new List<List<int>>() { new List<int>() { 9, 7, 8, 5, 6 } };

            int highestOutput = 0;
            Dictionary<int, AmplifierPart2> amplifiers;

            var allPhaseCombinationsList = allPhaseCombinations.ToList();

            for (int phaseIndex = 0; phaseIndex < allPhaseCombinationsList.Count; phaseIndex++)
            {
                var phases = allPhaseCombinationsList[phaseIndex].ToList();

                amplifiers = new Dictionary<int, AmplifierPart2>();

                for (int amplifierAndPhaseIndex = 0; amplifierAndPhaseIndex < amplifierAmount; amplifierAndPhaseIndex++)
                {
                    char name = (char)(amplifierAndPhaseIndex + 65);

                    amplifiers.Add(amplifierAndPhaseIndex, new AmplifierPart2(name, numbers));
                }

                for (int amplifierAndPhaseIndex = 0; amplifierAndPhaseIndex < amplifierAmount; amplifierAndPhaseIndex++)
                {
                    amplifiers[amplifierAndPhaseIndex].Next = amplifiers[amplifierAndPhaseIndex + 1 >= amplifierAmount ? 0 : amplifierAndPhaseIndex + 1];
                    amplifiers[amplifierAndPhaseIndex].AddInput(phases[amplifierAndPhaseIndex]);
                }

                bool executeNext = true;

                int input = 0;
                amplifiers[0].AddInput(input);

                while (executeNext)
                {
                    for (int amplifierAndPhaseIndex = 0; amplifierAndPhaseIndex < amplifierAmount; amplifierAndPhaseIndex++)
                    {
                        executeNext = amplifiers[amplifierAndPhaseIndex].Calculate();

                        input = amplifiers[amplifierAndPhaseIndex].Output;

                        if (!executeNext)
                            break;

                        amplifiers[amplifierAndPhaseIndex].Next.AddInput(input);
                    }
                }

                if (amplifiers[4].Output > highestOutput)
                    highestOutput = amplifiers[4].Output;
            }

            Console.WriteLine("Part 2: " + highestOutput);
        }

        public class AmplifierPart1
        {
            public AmplifierPart1(char name, int[] numbers)
            {
                this.Name = name;
                this.NumbersBase = (int[])numbers.Clone();
            }

            public char Name { get; }
            public int[] NumbersBase { get; }
            public int[] Numbers { get; set; }

            public int Calculate(int phase, int input)
            {
                int operationCode3Count = 0;

                int[] numbers = (int[])this.NumbersBase.Clone();

                int output = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    int operationCode = numbers[i] % 10;

                    bool param1IsImmediate = ((numbers[i] / 100) % 10) == 1;
                    bool param2IsImmediate = ((numbers[i] / 1000) % 10) == 1;

                    if (operationCode == 1)
                    {
                        doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate);

                        i += 3;
                    }
                    else if (operationCode == 2)
                    {
                        doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate);

                        i += 3;
                    }
                    else if (operationCode == 3)
                    {
                        if (operationCode3Count == 0)
                            numbers[numbers[i + 1]] = phase;
                        else
                            numbers[numbers[i + 1]] = input;

                        operationCode3Count++;

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        output = numbers[numbers[i + 1]];

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
                            numbers[numbers[i + 3]] = 1;
                        else
                            numbers[numbers[i + 3]] = 0;

                        i += 3;
                    }
                    else if (operationCode == 8)
                    {
                        int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                        int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                        if (firstParam == secondParam)
                            numbers[numbers[i + 3]] = 1;
                        else
                            numbers[numbers[i + 3]] = 0;

                        i += 3;
                    }
                    else
                    {
                        break;
                    }
                }

                return output;
            }

            private static void doCalculation(int[] numbers, int operationCode, int i, bool param1IsImmediate, bool param2IsImmediate)
            {
                int leftSide = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                int rightSide = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                numbers[numbers[i + 3]] = operationCode == 1 ? leftSide + rightSide : leftSide * rightSide;
            }

            private static int getParamPosition(int[] numbers, int i, bool paramIsImmediate)
            {
                return paramIsImmediate ? i : numbers[i];
            }
        }

        public class AmplifierPart2
        {
            public AmplifierPart2(char name, int[] numbers)
            {
                this.Name = name;
                this.NumbersBase = (int[])numbers.Clone();
            }

            public char Name { get; }
            public int[] NumbersBase { get; }
            public bool IsDone { get; set; }

            public int Output { get; set; }
            public List<int> Inputs { get; set; } = new List<int>();
            public int InputIndex { get; set; }
            public int Index { get; set; }

            public AmplifierPart2 Next { get; set; }

            public void AddInput(int input)
            {
                this.Inputs.Add(input);
            }

            public bool Calculate()
            {
                int[] numbers = this.NumbersBase;

                int i;

                for (i = this.Index; i < numbers.Length; i++)
                {
                    int operationCode = numbers[i] % 100;

                    bool param1IsImmediate = ((numbers[i] / 100) % 10) == 1;
                    bool param2IsImmediate = ((numbers[i] / 1000) % 10) == 1;

                    if (operationCode == 1)
                    {
                        doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate);

                        i += 3;
                    }
                    else if (operationCode == 2)
                    {
                        doCalculation(numbers, operationCode, i, param1IsImmediate, param2IsImmediate);

                        i += 3;
                    }
                    else if (operationCode == 3)
                    {
                        numbers[numbers[i + 1]] = this.Inputs[this.InputIndex % this.Inputs.Count];

                        this.InputIndex++;

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        this.Output = numbers[numbers[i + 1]];

                        i += 2;

                        break;
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
                            numbers[numbers[i + 3]] = 1;
                        else
                            numbers[numbers[i + 3]] = 0;

                        i += 3;
                    }
                    else if (operationCode == 8)
                    {
                        int firstParam = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                        int secondParam = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                        if (firstParam == secondParam)
                            numbers[numbers[i + 3]] = 1;
                        else
                            numbers[numbers[i + 3]] = 0;

                        i += 3;
                    }
                    else if (operationCode == 99)
                    {
                        this.IsDone = true;

                        break;
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }

                this.Index = i;

                return !this.IsDone;
            }

            private static void doCalculation(int[] numbers, int operationCode, int i, bool param1IsImmediate, bool param2IsImmediate)
            {
                int leftSide = numbers[getParamPosition(numbers, i + 1, param1IsImmediate)];
                int rightSide = numbers[getParamPosition(numbers, i + 2, param2IsImmediate)];

                numbers[numbers[i + 3]] = operationCode == 1 ? leftSide + rightSide : leftSide * rightSide;
            }

            private static int getParamPosition(int[] numbers, int i, bool paramIsImmediate)
            {
                return paramIsImmediate ? i : numbers[i];
            }
        }

        public IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                var itemAsEnumerable = Enumerable.Repeat(item, 1);
                var subSet = items.Except(itemAsEnumerable);

                if (!subSet.Any())
                {
                    yield return itemAsEnumerable;
                }
                else
                {
                    foreach (var sub in this.GetPermutations(items.Except(itemAsEnumerable)))
                    {
                        yield return itemAsEnumerable.Union(sub).ToList();
                    }
                }
            }
        }
    }
}
