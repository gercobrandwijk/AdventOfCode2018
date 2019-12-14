using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day09 : AdventOfCodeDay
    {
        public Day09(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            RunPart1();
            RunPart2();
        }

        public void RunPart1()
        {
            string[] lines = File.ReadAllLines("Data/Day09.txt");
            //lines = new string[] { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            //lines = new string[] { "1102,34915192,34915192,7,4,7,99,0" };
            //lines = new string[] { "104,1125899906842624,99" };

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            Amplifier amplifier = new Amplifier('A', numbers);
            amplifier.Inputs.Add(1);
            amplifier.Calculate();

            Console.WriteLine("Part 1: " + amplifier.Output);
        }

        public void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day09.txt");
            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            Amplifier amplifier = new Amplifier('A', numbers);
            amplifier.Inputs.Add(2);
            amplifier.Calculate();

            Console.WriteLine("Part 2: " + amplifier.Output);
        }

        public class Amplifier
        {
            public Amplifier(char name, Int64[] numbers)
            {
                this.Name = name;
                this.NumbersBase = (Int64[])numbers.Clone();
            }

            public char Name { get; }
            public Int64[] NumbersBase { get; }
            public bool IsDone { get; set; }

            public Int64 Output { get; set; }
            public List<int> Inputs { get; set; } = new List<int>();
            public int InputIndex { get; set; }
            public Int64 Index { get; set; }

            public Int64 RelativeBase { get; set; }

            public Amplifier Next { get; set; }

            public void AddInput(int input)
            {
                this.Inputs.Add(input);
            }

            public bool Calculate()
            {
                Int64[] numbers = this.NumbersBase;

                Int64 i;

                for (i = this.Index; i < numbers.Length; i++)
                {
                    Int64 operationCode = numbers[i] % 100;

                    Int64 paramModes = numbers[i] / 100;

                    if (operationCode == 1)
                    {
                        var param = getParams(numbers, i, paramModes, 3);

                        this.writeAddress(numbers, param[2].Address, param[0].Value + param[1].Value);

                        i += 3;
                    }
                    else if (operationCode == 2)
                    {
                        var param = getParams(numbers, i, paramModes, 3);

                        this.writeAddress(numbers, param[2].Address, param[0].Value * param[1].Value);

                        i += 3;
                    }
                    else if (operationCode == 3)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.writeAddress(numbers, param[0].Address, this.Inputs[this.InputIndex % this.Inputs.Count]);

                        this.InputIndex++;

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.Output = param[0].Value;

                        Console.WriteLine(this.Output);

                        i += 1;

                        //break;
                    }
                    else if (operationCode == 5)
                    {
                        var param = getParams(numbers, i, paramModes, 2);

                        if (param[0].Value != 0)
                            i = param[1].Value - 1;
                        else
                            i += 2;
                    }
                    else if (operationCode == 6)
                    {
                        var param = getParams(numbers, i, paramModes, 2);

                        if (param[0].Value == 0)
                            i = param[1].Value - 1;
                        else
                            i += 2;
                    }
                    else if (operationCode == 7)
                    {
                        var param = getParams(numbers, i, paramModes, 3);

                        this.writeAddress(numbers, param[2].Address, param[0].Value < param[1].Value ? 1 : 0);

                        i += 3;
                    }
                    else if (operationCode == 8)
                    {
                        var param = getParams(numbers, i, paramModes, 3);

                        this.writeAddress(numbers, param[2].Address, param[0].Value == param[1].Value ? 1 : 0);

                        i += 3;
                    }
                    else if (operationCode == 9)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.RelativeBase += param[0].Value;

                        i += 1;
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

            private List<ParamPosition> getParams(Int64[] numbers, Int64 indexPointer, Int64 paramModes, int count)
            {
                List<ParamPosition> paramPositions = new List<ParamPosition>();

                for (Int64 i = indexPointer + 1; i <= indexPointer + count; i++)
                {
                    var paramMode = paramModes % 10;

                    paramPositions.Add(this.getParamPosition(numbers, i, paramMode));

                    paramModes = paramModes / 10;
                }

                return paramPositions;
            }

            private ParamPosition getParamPosition(Int64[] numbers, Int64 i, Int64 paramMode)
            {
                var value = this.readAddress(numbers, i);

                switch (paramMode)
                {
                    case 0:
                        return new ParamPosition() { Value = this.readAddress(numbers, value), Address = value };
                    case 1:
                        return new ParamPosition() { Value = value };
                    case 2:
                        return new ParamPosition() { Value = this.readAddress(numbers, value + this.RelativeBase), Address = value + this.RelativeBase };
                }

                throw new NotSupportedException();
            }

            private Int64 readAddress(Int64[] numbers, Int64 address)
            {
                return numbers[address];
            }

            private void writeAddress(Int64[] numbers, Int64 address, Int64 value)
            {
                numbers[address] = value;
            }
        }

        public class ParamPosition
        {
            public Int64 Value { get; set; }
            public Int64 Address { get; set; }
        }
    }
}
