using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day03
    {
        static int size = 17000;
        static int centerPointX;
        static int centerPointY;
        static int currentX;
        static int currentY;
        static Field[][] board;

        static int nearestCrossDistanceManhattan = size * 2;
        static int nearestCrossDistanceSteps = size * 2;

        public static void Run()
        {
            string[] lines = File.ReadAllLines("Day03.txt");
            //string[] lines = File.ReadAllLines("Day03Part1Example1.txt");
            //string[] lines = File.ReadAllLines("Day03Part1Example2.txt");
            //string[] lines = File.ReadAllLines("Day03Part1Example3.txt");

            Move[] moves1 = lines[0].Split(',').Select(x => new Move(x)).ToArray();
            Move[] moves2 = lines[1].Split(',').Select(x => new Move(x)).ToArray();

            board = new Field[size][];

            centerPointX = size / 2;
            centerPointY = size / 2;

            board[centerPointX] = new Field[size];
            board[centerPointX][centerPointY] = new Field()
            {
                Type = FieldType.CenterPoint
            };

            doMoves(moves1, 1);

            doMoves(moves2, 2);

            //writeBoardToFile();

            Console.WriteLine("Part 1: " + nearestCrossDistanceManhattan);

            Console.WriteLine("Part 2: " + nearestCrossDistanceSteps);
        }

        public static void writeBoardToFile()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                string row = string.Empty;

                for (int j = 0; j < size; j++)
                {
                    char character;

                    if (board[j][i] != null)
                    {
                        switch (board[j][i].Type)
                        {
                            case FieldType.Empty:
                                character = ' ';
                                break;
                            case FieldType.CenterPoint:
                                character = 'O';
                                break;
                            case FieldType.LineVertical:
                                character = '|';
                                break;
                            case FieldType.LineHorizontal:
                                character = '-';
                                break;
                            case FieldType.Cross:
                                character = '#';
                                break;
                            default:
                                throw new NotSupportedException();
                        }
                    }
                    else
                    {
                        character = ' ';
                    }

                    row += character;
                }

                stringBuilder.AppendLine(row);
            }

            File.WriteAllText("day03board.txt", stringBuilder.ToString(), Encoding.UTF8);
        }

        public static void doMoves(Move[] moves, int lineNumber)
        {
            currentX = centerPointX;
            currentY = centerPointY;

            int stepAmount = 0;

            foreach (Move move in moves)
            {
                switch (move.Direction)
                {
                    case Direction.Up:
                        int upStartY = currentY - 1;
                        int upEndY = upStartY - move.Amount;

                        for (int i = upStartY; i > upEndY; i--)
                        {
                            stepAmount++;

                            setPoint(lineNumber, true, currentX, i, stepAmount);

                            currentY--;
                        }
                        break;
                    case Direction.Down:
                        int downStartY = currentY + 1;
                        int downEndY = downStartY + move.Amount;

                        for (int i = downStartY; i < downEndY; i++)
                        {
                            stepAmount++;

                            setPoint(lineNumber, true, currentX, i, stepAmount);

                            currentY++;
                        }
                        break;
                    case Direction.Left:
                        int leftStartX = currentX - 1;
                        int leftEndX = leftStartX - move.Amount;

                        for (int i = leftStartX; i > leftEndX; i--)
                        {
                            stepAmount++;

                            setPoint(lineNumber, false, i, currentY, stepAmount);

                            currentX--;
                        }
                        break;
                    case Direction.Right:
                        int rightStartX = currentX + 1;
                        int rightEndX = rightStartX + move.Amount;

                        for (int i = rightStartX; i < rightEndX; i++)
                        {
                            stepAmount++;

                            setPoint(lineNumber, false, i, currentY, stepAmount);

                            currentX++;
                        }
                        break;
                }
            }
        }

        private static void setPoint(int lineNumber, bool upOrDown, int x, int y, int stepAmount)
        {
            if (board[x] == null)
                board[x] = new Field[size];

            if (board[x][y] == null)
                board[x][y] = new Field();

            Field field = board[x][y];

            if (field.Type == FieldType.LineVertical || field.Type == FieldType.LineHorizontal)
            {
                field.Type = FieldType.Cross;

                // First time for this line to reach this position
                if ((!field.Line1Hit && lineNumber == 1) || (!field.Line2Hit && lineNumber == 2))
                {
                    int distanceManhattan = Math.Abs(x - centerPointX) + Math.Abs(y - centerPointY);

                    if (distanceManhattan < nearestCrossDistanceManhattan)
                        nearestCrossDistanceManhattan = distanceManhattan;
                }
            }
            else if (upOrDown)
            {
                field.Type = FieldType.LineVertical;
            }
            else
            {
                field.Type = FieldType.LineHorizontal;
            }

            if (!field.Line1Hit && lineNumber == 1)
            {
                field.Line1Hit = true;
                field.Line1StepSizeForCross = stepAmount;
            }
            else if (!field.Line2Hit && lineNumber == 2)
            {
                field.Line2Hit = true;
                field.Line2StepSizeForCross = stepAmount;
            }

            // More than 1 line reached
            if (field.Type == FieldType.Cross && field.Line1Hit && field.Line2Hit)
            {
                int distanceSteps = field.Line1StepSizeForCross + field.Line2StepSizeForCross;

                if (distanceSteps < nearestCrossDistanceSteps)
                    nearestCrossDistanceSteps = distanceSteps;
            }
        }

        public class Field
        {
            public FieldType Type { get; set; } = FieldType.Empty;
            public bool Line1Hit { get; set; }
            public int Line1StepSizeForCross { get; set; }
            public bool Line2Hit { get; set; }
            public int Line2StepSizeForCross { get; set; }
        }

        public enum FieldType
        {
            Empty,
            CenterPoint,
            LineVertical,
            LineHorizontal,
            Cross
        }

        public class Move
        {
            public int Amount { get; set; }
            public Direction Direction { get; set; }

            public Move(string move)
            {
                switch (move[0])
                {
                    case 'U':
                        this.Direction = Direction.Up;
                        break;
                    case 'D':
                        this.Direction = Direction.Down;
                        break;
                    case 'L':
                        this.Direction = Direction.Left;
                        break;
                    case 'R':
                        this.Direction = Direction.Right;
                        break;
                }

                this.Amount = int.Parse(move.Substring(1));
            }

            public override string ToString()
            {
                return this.Direction + "_" + this.Amount;
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
