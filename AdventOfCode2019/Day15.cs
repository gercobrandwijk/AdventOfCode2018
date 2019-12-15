using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day15 : AdventOfCodeDay
    {
        public Day15(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            string[] lines = File.ReadAllLines("Data/Day15.txt");
            //lines = new string[] { "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99" };
            //lines = new string[] { "1102,34915192,34915192,7,4,7,99,0" };
            //lines = new string[] { "104,1125899906842624,99" };

            string line = lines[0];

            Int64[] numbers;

            numbers = line.Split(',').Select(x => Int64.Parse(x)).ToArray();
            Array.Resize(ref numbers, numbers.Length * 10);

            long size = 50;
            State[,] board = new State[size, size];

            long currentX = size / 2;
            long currentY = size / 2;
            long startX = size / 2;
            long startY = size / 2;
            long previousX = size / 2;
            long previousY = size / 2;

            int cursorTop = Console.CursorTop;

            int part1WayCounter = 0;
            int part2MaxPath = 0;
            int part2MaxPathCurrent = 0;

            Amplifier amplifier = new Amplifier('A', numbers);

            board[currentX, currentY] = State.Way;

            List<Tuple<long, long, int, Direction>> stepsOnPath = new List<Tuple<long, long, int, Direction>>();

            doMoves(board, size, cursorTop, ref currentX, ref currentY, ref previousX, ref previousY, amplifier, stepsOnPath, ref part2MaxPath, ref part2MaxPathCurrent, false, true);

            //this.print(board, size, currentX, currentY, cursorTop, 0, 0);

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (board[x, y] == State.Way)
                        part1WayCounter++;
                }
            }

            Console.WriteLine("Part 1: " + part1WayCounter + " (station at [" + currentX + "," + currentY + "])");

            cursorTop = Console.CursorTop;
            part2MaxPath = 0;
            part2MaxPathCurrent = 0;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    board[x, y] = State.Unknown;
                }
            }

            board[currentX, currentY] = State.Way;

            //this.print(board, size, currentX, currentY, cursorTop, 0, 0);

            doMoves(board, size, cursorTop, ref currentX, ref currentY, ref previousX, ref previousY, amplifier, stepsOnPath, ref part2MaxPath, ref part2MaxPathCurrent, false, false);

            Console.WriteLine("Part 2: " + (part2MaxPath - 1));
        }

        private void doMoves(State[,] board, long size, int cursorTop, ref long currentX, ref long currentY, ref long previousX, ref long previousY, Amplifier amplifier, List<Tuple<long, long, int, Direction>> stepsOnPath, ref int maxPath, ref int maxPathCurrent, bool print, bool part1)
        {
            while (true)
            {
                State north = board[currentX, currentY - 1];
                State east = board[currentX + 1, currentY];
                State south = board[currentX, currentY + 1];
                State west = board[currentX - 1, currentY];

                int amountOfWays = 0;

                if (north == State.Way)
                    amountOfWays++;
                if (east == State.Way)
                    amountOfWays++;
                if (south == State.Way)
                    amountOfWays++;
                if (west == State.Way)
                    amountOfWays++;

                Direction directionToGo;

                if (north == State.Unknown)
                    directionToGo = Direction.N;
                else if (east == State.Unknown)
                    directionToGo = Direction.E;
                else if (south == State.Unknown)
                    directionToGo = Direction.S;
                else if (west == State.Unknown)
                    directionToGo = Direction.W;
                else
                {
                    // No where to go (means that part 2 is finished)
                    if (amountOfWays == 0)
                    {
                        break;
                    }
                    // Dead end
                    else if (amountOfWays == 1)
                    {
                        if (maxPathCurrent > maxPath)
                            maxPath = maxPathCurrent;

                        Tuple<long, long, int, Direction> previousStep = stepsOnPath.LastOrDefault();

                        if (previousStep != null)
                        {
                            board[previousStep.Item1, previousStep.Item2] = State.DeadEnd;

                            stepsOnPath.Remove(previousStep);

                            maxPathCurrent -= 2;
                        }

                        if (north == State.Way)
                            directionToGo = Direction.N;
                        else if (east == State.Way)
                            directionToGo = Direction.E;
                        else if (south == State.Way)
                            directionToGo = Direction.S;
                        else if (west == State.Way)
                            directionToGo = Direction.W;
                        else
                            throw new NotSupportedException();
                    }
                    else
                    {
                        // Moved up or down from last move
                        if (currentX == previousX)
                        {
                            // Last move was move to the top
                            if (previousY > currentY)
                                directionToGo = Direction.N;
                            // Last move was move to the bottom
                            else
                                directionToGo = Direction.S;
                        }
                        // Moved left or right from last move
                        else
                        {
                            // Last move was move to the left
                            if (previousX > currentX)
                                directionToGo = Direction.W;
                            // Last move was move to the right
                            else
                                directionToGo = Direction.E;
                        }
                    }
                }

                amplifier.Inputs.Add((int)directionToGo);

                amplifier.Calculate();

                Tuple<long, long, bool> result = this.finishMovement(board, stepsOnPath, amountOfWays, currentX, currentY, directionToGo, amplifier.Output);

                if (previousX != currentX || previousY != currentY)
                    maxPathCurrent++;

                previousX = currentX;
                previousY = currentY;

                currentX = result.Item1;
                currentY = result.Item2;

                if (part1)
                {
                    // Station found
                    if (result.Item3)
                        break;
                }

                if (print)
                {
                    this.print(board, size, currentX, currentY, cursorTop, maxPath, maxPathCurrent);

                    Thread.Sleep(100);
                }
            }
        }

        private void print(State[,] board, long size, long currentX, long currentY, int cursorTop, int maxPath, int maxPathCurrent)
        {
            Console.SetCursorPosition(0, cursorTop);

            for (int y = 0; y < size; y++)
            {
                string line = "";
                for (int x = 0; x < size; x++)
                {
                    if (x == currentX && y == currentY)
                    {
                        line += "O";
                    }
                    else
                    {
                        switch (board[x, y])
                        {
                            case State.Unknown:
                                line += "?";
                                break;
                            case State.Wall:
                                line += "#";
                                break;
                            case State.Way:
                                line += ".";
                                break;
                            case State.Station:
                                line += "@";
                                break;
                            case State.DeadEnd:
                                line += " ";
                                break;
                            default:
                                break;
                        }
                    }
                }

                Console.WriteLine(line);
            }

            if (maxPath != 0 || maxPathCurrent != 0)
                Console.WriteLine("Length of path: max=" + maxPath + ", current=" + maxPathCurrent);
        }

        public Tuple<long, long, bool> finishMovement(State[,] board, List<Tuple<long, long, int, Direction>> stepsOnPath, int amountOfWays, long currentX, long currentY, Direction direction, long output)
        {
            State newState = output == 0 ? State.Wall : output == 1 ? State.Way : State.Station;

            switch (direction)
            {
                case Direction.N:
                    board[currentX, currentY - 1] = newState;

                    if (newState != State.Wall)
                        currentY--;
                    break;
                case Direction.E:
                    board[currentX + 1, currentY] = newState;

                    if (newState != State.Wall)
                        currentX++;
                    break;
                case Direction.S:
                    board[currentX, currentY + 1] = newState;

                    if (newState != State.Wall)
                        currentY++;
                    break;
                case Direction.W:
                    board[currentX - 1, currentY] = newState;

                    if (newState != State.Wall)
                        currentX--;
                    break;
            }

            if (newState != State.Wall)
                stepsOnPath.Add(new Tuple<long, long, int, Direction>(currentX, currentY, amountOfWays, direction));

            return new Tuple<long, long, bool>(currentX, currentY, newState == State.Station);
        }

        public enum State
        {
            Unknown,
            Wall,
            Way,
            Station,
            DeadEnd
        }

        public enum Direction
        {
            N = 1,
            S = 2,
            W = 3,
            E = 4
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

                        if (this.InputIndex > this.Inputs.Count - 1)
                            break;

                        this.writeAddress(numbers, param[0].Address, this.Inputs[this.InputIndex % this.Inputs.Count]);

                        this.InputIndex++;

                        i += 1;
                    }
                    else if (operationCode == 4)
                    {
                        var param = getParams(numbers, i, paramModes, 1);

                        this.Output = param[0].Value;

                        i += 2;

                        break;
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
