using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day13 : AdventOfCodeDay
    {
        int size = 40;
        int[,] board;
        Amplifier amplifier;
        int amountOfBlocks;
        int score;
        int paddleX;
        int paddleY;
        int ballX;
        int ballY;
        int writeLineNumber;

        public Day13(int number) : base(number)
        {
            board = new int[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    board[x, y] = 0;
                }
            }
        }

        public override async Task Run()
        {
            RunPart1();
            RunPart2();
        }

        public void RunPart1()
        {
            string[] lines = File.ReadAllLines("Data/Day13.txt");

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            amplifier = new Amplifier('A', numbers, this);
            writeLineNumber = Console.CursorTop;

            while (!amplifier.IsDone)
            {
                var instructions = amplifier.Calculate();

                if (!amplifier.IsDone)
                    doMove(instructions);
            }

            Console.WriteLine("Part 1: " + amountOfBlocks);

            //this.print();
        }

        public void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day13.txt");

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);
            numbers[0] = 2;

            amplifier = new Amplifier('A', numbers, this);
            writeLineNumber = Console.CursorTop;

            while (!amplifier.IsDone)
            {
                var instructions = amplifier.Calculate();

                if (!amplifier.IsDone)
                    doMove(instructions);
            }

            Console.WriteLine("Part 2: " + score);

            //this.print();
        }

        private void print()
        {
            Console.SetCursorPosition(0, writeLineNumber);
            for (int y = 0; y < 30; y++)
            {
                string line = "";
                for (int x = 0; x < size; x++)
                {
                    char value = ' ';

                    switch (board[x, y])
                    {
                        case 1:
                            value = '|';
                            break;
                        case 2:
                            value = '#';
                            break;
                        case 3:
                            value = '-';
                            break;
                        case 4:
                            value = '0';
                            break;
                    }

                    line += value;
                }

                Console.WriteLine(line);
            }
        }

        public void doMove(int[] instructions)
        {
            int x = instructions[0];
            int y = instructions[1];
            int tileId = instructions[2];

            if (x == -1 && y == 0)
            {
                score = tileId;

                return;
            }

            switch (tileId)
            {
                case 0: // empty
                    break;
                case 1: // wall
                    board[x, y] = 1;
                    break;
                case 2: // block
                    board[x, y] = 2;
                    amountOfBlocks++;
                    break;
                case 3: // paddle
                    if (board[paddleX, paddleY] == 3)
                        board[paddleX, paddleY] = 0;

                    paddleX = x;
                    paddleY = y;

                    board[paddleX, paddleY] = 3;
                    break;
                case 4: // ball

                    if (board[ballX, ballY] == 4)
                        board[ballX, ballY] = 0;

                    bool movingLeftTop = x < ballX && y < ballY;
                    bool movingRightTop = x > ballX && y < ballY;
                    bool movingLeftDown = x > ballX && y > ballY;
                    bool movingRightDown = x < ballX && y > ballY;

                    ballX = x;
                    ballY = y;

                    if (movingLeftTop || movingRightTop)
                    {
                        if (board[ballX, ballY - 1] == 2)
                        {
                            board[ballX, ballY - 1] = 0;
                        }
                    }
                    else if (movingLeftDown || movingRightDown)
                    {
                        if (board[ballX, ballY + 1] == 2)
                        {
                            board[ballX, ballY + 1] = 0;
                        }
                    }
                    else if (movingLeftTop)
                    {
                        if (board[ballX - 1, ballY - 1] == 2)
                        {
                            board[ballX - 1, ballY - 1] = 0;
                        }
                    }
                    else if (movingRightTop)
                    {
                        if (board[ballX + 1, ballY - 1] == 2)
                        {
                            board[ballX + 1, ballY - 1] = 0;
                        }
                    }
                    else if (movingLeftDown)
                    {
                        if (board[ballX - 1, ballY + 1] == 2)
                        {
                            board[ballX - 1, ballY + 1] = 0;
                        }
                    }
                    else if (movingRightDown)
                    {
                        if (board[ballX + 1, ballY + 1] == 2)
                        {
                            board[ballX + 1, ballY + 1] = 0;
                        }
                    }

                    board[ballX, ballY] = 4;

                    break;
            }
        }

        public class Amplifier
        {
            public Amplifier(char name, Int64[] numbers, Day13 program)
            {
                this.Name = name;
                this.NumbersBase = (Int64[])numbers.Clone();
                this.Program = program;
            }

            public char Name { get; }
            public Int64[] NumbersBase { get; }
            public Day13 Program { get; }

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

                        this.writeAddress(numbers, param[0].Address, this.Program.ballX > this.Program.paddleX ? 1 : this.Program.ballX < this.Program.paddleX ? -1 : 0);

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.Outputs.Add(param[0].Value);

                        if (this.Outputs.Count % 3 == 0)
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
                    var outputs = new int[3];

                    outputs[0] = (int)this.Outputs[this.Outputs.Count - 3];
                    outputs[1] = (int)this.Outputs[this.Outputs.Count - 2];
                    outputs[2] = (int)this.Outputs[this.Outputs.Count - 1];

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
