using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2018
{
    public class ProgramDay14
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay14");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            partOne();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.WriteLine("Part two");
            watch.Restart();
            partTwo();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");
        }

        static void partOne()
        {
            executePartOne(new List<int>() { 3, 7 }, 5, "0124515891");
            executePartOne(new List<int>() { 3, 7 }, 9, "5158916779");
            executePartOne(new List<int>() { 3, 7 }, 18, "9251071085");
            executePartOne(new List<int>() { 3, 7 }, 2018, "5941429882");
            executePartOne(new List<int>() { 3, 7 }, 077201);
        }

        static void executePartOne(List<int> values, int rounds, string sequence = null)
        {
            Elf elf1 = new Elf() { Position = 0, Value = values[0] };
            Elf elf2 = new Elf() { Position = 1, Value = values[1] };

            while (values.Count < rounds + 10)
            {
                int newValue = elf1.Value + elf2.Value;

                string newValueString = newValue.ToString();

                foreach (char newValuePart in newValueString)
                {
                    values.Add((int)newValuePart - 48);
                }

                elf1.Position = (elf1.Position + 1 + elf1.Value) % values.Count;
                elf1.Value = values[elf1.Position];

                elf2.Position = (elf2.Position + 1 + elf2.Value) % values.Count;
                elf2.Value = values[elf2.Position];

                //for (int j = 0; j < values.Count; j++)
                //{
                //    int value = values[j];

                //    if (elf1.Position == j)
                //    {
                //        Console.Write("(" + value + ") ");
                //    }
                //    else if (elf2.Position == j)
                //    {
                //        Console.Write("[" + value + "] ");
                //    }
                //    else
                //    {
                //        Console.Write(" " + value + "  ");
                //    }
                //}

                //Console.WriteLine();
            }

            string foundResult = string.Join("", values).Substring(rounds, 10);

            if (sequence != null)
                Console.WriteLine("Output: " + foundResult + ", equal = " + (foundResult == sequence));
            else
                Console.WriteLine("Output: " + foundResult);
        }

        static void partTwo()
        {
            executePartTwo("37", "01245", 5);
            executePartTwo("37", "51589", 9);
            executePartTwo("37", "515891", 9);
            executePartTwo("37", "92510", 18);
            executePartTwo("37", "59414", 2018);
            executePartTwo("37", "077201");
        }

        static void executePartTwo(string valuesString, string sequence, int? rounds = null)
        {
            ElfPart2 elf1 = new ElfPart2() { Position = 0, Value = valuesString[0] };
            ElfPart2 elf2 = new ElfPart2() { Position = 1, Value = valuesString[1] };

            int sequenceLength = sequence.Length;
            int sequenceNumber = int.Parse(sequence);

            bool validLength = false;
            StringBuilder values = new StringBuilder(valuesString);
            StringBuilder valueToCompare = new StringBuilder(valuesString);

            string valueToCompareString;

            while (true)
            {
                int newValue = elf1.ValueInt + elf2.ValueInt;

                string newValueString = newValue.ToString();

                foreach (char newValuePart in newValueString)
                {
                    values.Append(newValuePart);

                    valueToCompare.Append(newValuePart);
                }

                if (!validLength)
                {
                    if (valueToCompare.Length >= sequenceLength)
                        validLength = true;
                }
                else
                {
                    if (valueToCompare.Length > sequenceLength + 2)
                        valueToCompare.Remove(0, 1);

                    if (valueToCompare.Length > sequenceLength + 1)
                        valueToCompare.Remove(0, 1);
                }

                int elf1NewPosition = elf1.Position + 1 + elf1.ValueInt;

                if (elf1NewPosition >= values.Length)
                    while (elf1NewPosition >= values.Length)
                        elf1NewPosition -= values.Length;

                elf1.Position = elf1NewPosition;
                elf1.Value = values[elf1.Position];

                int elf2NewPosition = elf2.Position + 1 + elf2.ValueInt;

                if (elf2NewPosition >= values.Length)
                    while (elf2NewPosition >= values.Length)
                        elf2NewPosition -= values.Length;

                elf2.Position = elf2NewPosition;
                elf2.Value = values[elf2.Position];

                if (validLength)
                {
                    valueToCompareString = valueToCompare.ToString();

                    if (valueToCompareString[0] == sequence[0] || valueToCompareString[1] == sequence[0])
                    {
                        int compareInt1 = int.Parse(valueToCompareString.Substring(0, valueToCompareString.Length - 1));

                        if (compareInt1 == sequenceNumber)
                            break;

                        int compareInt2 = int.Parse(valueToCompareString.Substring(1));

                        if (compareInt2 == sequenceNumber)
                            break;
                    }
                }
            }

            int foundResult = values.ToString().IndexOf(sequence);

            if (rounds != null)
                Console.WriteLine("Output: " + foundResult + ", equal = " + (foundResult == rounds));
            else
                Console.WriteLine("Output: " + foundResult);
        }

        public class Elf
        {
            public int Position { get; set; }

            public int Value { get; set; }
        }

        public class ElfPart2
        {
            public int Position { get; set; }

            private char value;
            public char Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;

                    this.ValueInt = (int)char.GetNumericValue(value);
                }
            }

            public int ValueInt { get; set; }
        }
    }
}
