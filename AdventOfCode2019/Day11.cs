using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day11 : AdventOfCodeDay
    {
        int size = 150;
        int[,] board;
        int positionX;
        int positionY;
        Direction direction = Direction.Up;
        int panelCounter = 1;
        Amplifier amplifier;

        public Day11(int number) : base(number)
        {
            board = new int[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    board[x, y] = -1;
                }
            }
            positionX = size / 4;
            positionY = size / 2;
            board[positionX, positionY] = 0;
        }

        public override void Run()
        {
            RunPart1();
            RunPart2();
        }

        public void RunPart1()
        {
            string[] lines = File.ReadAllLines("Data/Day11.txt");

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            amplifier = new Amplifier('A', numbers, this);

            while (!amplifier.IsDone)
            {
                this.amplifier.Inputs.Add(board[positionX, positionY] == 1 ? 1 : 0);
                var instructions = amplifier.Calculate();

                if (!amplifier.IsDone)
                    doMove(instructions);
            }

            Console.WriteLine("Part 1: " + panelCounter);
        }

        public void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day11.txt");

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            Console.WriteLine("Part 2: ");

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    board[x, y] = 1;
                }
            }

            amplifier = new Amplifier('A', numbers, this);

            while (!amplifier.IsDone)
            {
                this.amplifier.Inputs.Add(board[positionX, positionY] == 1 ? 1 : 0);
                var instructions = amplifier.Calculate();

                if (!amplifier.IsDone)
                    doMove(instructions);
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Console.Write(board[x, y] == 1 ? " " : "X");
                }

                Console.WriteLine();
            }
        }

        public void doMove(int[] instructions)
        {
            int colorToPaint = instructions[0];
            bool moveLeft = instructions[1] == 0;

            // Not painted yet
            if (board[positionX, positionY] == -1)
                panelCounter++;

            board[positionX, positionY] = colorToPaint;

            if (moveLeft)
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Left;
                        positionX -= 1;
                        break;
                    case Direction.Down:
                        direction = Direction.Right;
                        positionX += 1;
                        break;
                    case Direction.Left:
                        direction = Direction.Down;
                        positionY += 1;
                        break;
                    case Direction.Right:
                        direction = Direction.Up;
                        positionY -= 1;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        direction = Direction.Right;
                        positionX += 1;
                        break;
                    case Direction.Down:
                        direction = Direction.Left;
                        positionX -= 1;
                        break;
                    case Direction.Left:
                        direction = Direction.Up;
                        positionY -= 1;
                        break;
                    case Direction.Right:
                        direction = Direction.Down;
                        positionY += 1;
                        break;
                    default:
                        break;
                }
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public class Amplifier
        {
            public Amplifier(char name, Int64[] numbers, Day11 program)
            {
                this.Name = name;
                this.NumbersBase = (Int64[])numbers.Clone();
                this.Program = program;
            }

            public Day11 Program { get; set; }

            public char Name { get; }
            public Int64[] NumbersBase { get; }
            public bool IsDone { get; set; }

            public List<Int64> Outputs { get; set; } = new List<long>();
            public List<int> Inputs { get; set; } = new List<int>();
            public int InputIndex { get; set; }
            public Int64 Index { get; set; }

            public Int64 RelativeBase { get; set; }

            public Amplifier Next { get; set; }

            public void AddInput(int input)
            {
                this.Inputs.Add(input);
            }

            public int[] Calculate()
            {
                Int64[] numbers = this.NumbersBase;

                Int64 i;

                bool newInstructions = false;

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

                        this.writeAddress(numbers, param[0].Address, this.Inputs[this.InputIndex]);

                        this.InputIndex++;

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.Outputs.Add(param[0].Value);

                        if (this.Outputs.Count % 2 == 0)
                        {
                            newInstructions = true;

                            i += 2;

                            break;
                        }

                        i += 1;
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

                if (newInstructions)
                {
                    var outputs = new int[2];

                    outputs[0] = (int)this.Outputs[this.Outputs.Count - 2];
                    outputs[1] = (int)this.Outputs[this.Outputs.Count - 1];

                    return outputs;
                }

                return null;// !this.IsDone;
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
