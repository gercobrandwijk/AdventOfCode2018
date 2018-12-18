using System;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode
{
    public class ProgramDay14
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay14");

            Stopwatch watch = Stopwatch.StartNew();

            //Console.WriteLine("Part one");
            //watch.Restart();
            //partOne();
            //watch.Stop();
            //Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.WriteLine("Part two");
            watch.Restart();
            partTwo();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");
        }

        //static void partOne()
        //{
        //    executePartOne(inputTest.ToList(), 5, "0124515891");
        //    executePartOne(inputTest.ToList(), 9, "5158916779");
        //    executePartOne(inputTest.ToList(), 18, "9251071085");
        //    executePartOne(inputTest.ToList(), 2018, "5941429882");
        //    executePartOne(inputTest.ToList(), 077201);
        //}

        //static void executePartOne(List<int> values, int rounds, string sequence = null)
        //{
        //    Elf elf1 = new Elf() { Position = 0, Value = values[0] };
        //    Elf elf2 = new Elf() { Position = 1, Value = values[1] };

        //    while (values.Count < rounds + 10)
        //    {
        //        int newValue = elf1.Value + elf2.Value;

        //        string newValueString = newValue.ToString();

        //        foreach (char newValuePart in newValueString)
        //        {
        //            values.Add((int)newValuePart - 48);
        //        }

        //        elf1.Position = (elf1.Position + 1 + elf1.Value) % values.Count;
        //        elf1.Value = values[elf1.Position];

        //        elf2.Position = (elf2.Position + 1 + elf2.Value) % values.Count;
        //        elf2.Value = values[elf2.Position];

        //        //for (int j = 0; j < values.Count; j++)
        //        //{
        //        //    int value = values[j];

        //        //    if (elf1.Position == j)
        //        //    {
        //        //        Console.Write("(" + value + ") ");
        //        //    }
        //        //    else if (elf2.Position == j)
        //        //    {
        //        //        Console.Write("[" + value + "] ");
        //        //    }
        //        //    else
        //        //    {
        //        //        Console.Write(" " + value + "  ");
        //        //    }
        //        //}

        //        //Console.WriteLine();
        //    }

        //    string foundResult = string.Join("", values).Substring(rounds, 10);

        //    if (sequence != null)
        //        Console.WriteLine("Output: " + foundResult + ", equal = " + (foundResult == sequence));
        //    else
        //        Console.WriteLine("Output: " + foundResult);
        //}

        static void partTwo()
        {
            executePartTwo(baseInput, "01245", 5);
            executePartTwo(baseInput, "51589", 9);
            executePartTwo(baseInput, "92510", 18);
            executePartTwo(baseInput, "59414", 2018);
            executePartTwo(baseInput, "077201");
        }

        static void executePartTwo(string valuesString, string sequence, int? rounds = null)
        {
            Elf elf1 = new Elf() { Position = 0, Value = valuesString[0] };
            Elf elf2 = new Elf() { Position = 1, Value = valuesString[1] };

            int wishedResultLength = sequence.Length;

            bool validLength = false;
            StringBuilder values = new StringBuilder(valuesString);
            StringBuilder valueToCompare = new StringBuilder(valuesString);
            int skip = 0;

            while (true)
            {
                int newValue = elf1.ValueInt + elf2.ValueInt;

                string newValueString = newValue.ToString();

                foreach (char newValuePart in newValueString)
                {
                    values.Append(newValuePart);

                    if (validLength || valueToCompare.Length >= wishedResultLength)
                    {
                        validLength = true;

                        valueToCompare.Remove(0, 1);
                        valueToCompare.Append(newValuePart);
                    }
                    else
                    {
                        valueToCompare.Append(newValuePart);
                    }
                }

                int elf1NewPosition = elf1.Position + 1 + elf1.ValueInt;

                while (elf1NewPosition >= values.Length)
                    elf1NewPosition -= values.Length;

                elf1.Position = elf1NewPosition;
                elf1.Value = values[elf1.Position];

                int elf2NewPosition = elf2.Position + 1 + elf2.ValueInt;

                while (elf2NewPosition >= values.Length)
                    elf2NewPosition -= values.Length;

                elf2.Position = elf2NewPosition;
                elf2.Value = values[elf2.Position];

                if (validLength)
                {
                    if (skip == 0)
                    {
                        string valueToCompareString = valueToCompare.ToString();

                        int indexOfZero = valueToCompareString.IndexOf(sequence[0]);

                        if (indexOfZero >= 0)
                        {
                            int indexOf1 = valueToCompareString.IndexOf(sequence[1]);

                            if (indexOf1 >= 0)
                            {
                                int indexOf2 = valueToCompareString.IndexOf(sequence[2]);

                                if (indexOf2 >= 0)
                                {
                                    if (sequence.Equals(valueToCompareString))
                                        break;
                                }
                                else
                                {
                                    skip += 1;
                                }
                            }
                            else
                            {
                                skip += 2;
                            }
                        }
                        else
                        {
                            skip += 3;
                        }
                    }
                    else
                    {
                        skip--;
                    }
                }

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

            int foundResult = values.Length - wishedResultLength;

            if (rounds != null)
                Console.WriteLine("Output: " + foundResult + ", equal = " + (foundResult == rounds));
            else
                Console.WriteLine("Output: " + foundResult);
        }

        public class Elf
        {
            public int Position { get; set; }

            public char value;
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

        static string baseInput = "37";
    }
}
