using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2018
{
    public class ProgramDay06
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay06");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            execute();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            //Console.WriteLine("Part two");
            //watch.Restart();
            //partTwo();
            //watch.Stop();
            //Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.ReadLine();
        }

        static void execute()
        {
            execute(inputValuesTest, 32);
            execute(realInputValues, 10000);
        }

        static void execute(List<Coord> values, int threshold)
        {
            int maxCoordX = values.Max(x => x.X) + 2;
            int maxCoordY = values.Max(x => x.Y) + 2;

            CoordValue[,] grid = new CoordValue[maxCoordX, maxCoordY];

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < values.Count; i++)
            {
                values[i].Name = alphabet[i % 26];
            }

            foreach (Coord coord in values)
            {
                grid[coord.X, coord.Y] = new CoordValue(0, coord);
            }

            for (int x = 0; x < maxCoordX; x++)
            {
                for (int y = 0; y < maxCoordY; y++)
                {
                    foreach (Coord coord in values)
                    {
                        if (grid[x, y] != null && grid[x, y].Coord == coord)
                            continue;

                        int distance = Math.Abs(y - coord.Y) + Math.Abs(x - coord.X);

                        if (grid[x, y] == null)
                        {
                            grid[x, y] = new CoordValue(distance, coord);

                            grid[x, y].Coord.Coords.Add(new Tuple<int, int>(x, y));
                        }
                        else if (grid[x, y].Distance > distance)
                        {
                            if (grid[x, y].Coord != null)
                                grid[x, y].Coord.Coords.RemoveAll(c => c.Item1 == x && c.Item2 == y);

                            grid[x, y].Distance = distance;
                            grid[x, y].Coord = coord;

                            grid[x, y].Coord.Coords.Add(new Tuple<int, int>(x, y));
                        }
                        else if (grid[x, y].Distance == distance)
                        {
                            if (grid[x, y].Coord != null)
                                grid[x, y].Coord.Coords.RemoveAll(c => c.Item1 == x && c.Item2 == y);

                            grid[x, y].Distance = distance;
                            grid[x, y].Coord = null;
                        }
                    }
                }
            }

            for (int x = 0; x < maxCoordX; x++)
            {
                for (int y = 0; y < maxCoordY; y++)
                {
                    if ((x == 0 || x == maxCoordX - 1 || y == 0 || y == maxCoordY - 1) && grid[x, y].Coord != null)
                        grid[x, y].Coord.Finite = false;
                }
            }

            List<Coord> finiteValues = new List<Coord>(values.Where(x => x.Finite));

            Console.WriteLine("Largest finite area: " + finiteValues.Where(x => x.Finite).OrderBy(x => x.Size).Last());

            bool printOutGrid = false;
            int counter = 0;

            for (int y = 0; y < maxCoordY; y++)
            {
                for (int x = 0; x < maxCoordX; x++)
                {
                    char @char;

                    Console.ForegroundColor = ConsoleColor.White;

                    if (grid[x, y].Coord != null)
                    {
                        if (grid[x, y].Coord.Finite)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            int totalDistance = 0;

                            foreach (Coord coord in values)
                            {
                                int distance = Math.Abs(y - coord.Y) + Math.Abs(x - coord.X);

                                totalDistance += distance;
                            }

                            if (totalDistance < threshold)
                            {
                                counter++;

                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                        }
                        else
                        {
                            int totalDistance = 0;

                            foreach (Coord coord in values)
                            {
                                int distance = Math.Abs(y - coord.Y) + Math.Abs(x - coord.X);

                                totalDistance += distance;
                            }

                            if (totalDistance < threshold)
                            {
                                counter++;

                                Console.ForegroundColor = ConsoleColor.Blue;
                            }
                        }

                        if (grid[x, y].Coord.X == x && grid[x, y].Coord.Y == y)
                            @char = char.ToUpper(grid[x, y].Coord.Name);
                        else
                            @char = grid[x, y].Coord.Name;
                    }
                    else
                    {
                        int totalDistance = 0;

                        foreach (Coord coord in values)
                        {
                            int distance = Math.Abs(y - coord.Y) + Math.Abs(x - coord.X);

                            totalDistance += distance;
                        }

                        if (totalDistance < threshold)
                        {
                            counter++;

                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        @char = '.';
                    }

                    if (printOutGrid)
                        Console.Write(@char);
                }

                if (printOutGrid)
                    Console.WriteLine();
            }

            Console.WriteLine("Region size: " + counter);
        }

        public class Coord
        {
            public int X { get; set; }
            public int Y { get; set; }

            public bool IsElement { get; set; }

            public char Name { get; set; }

            public List<Tuple<int, int>> Coords { get; set; }

            public int Size
            {
                get
                {
                    return this.Coords.Count;
                }
            }

            public bool Finite { get; set; }

            public Coord(int x, int y, bool isElement = true)
            {
                this.X = x;
                this.Y = y;
                this.IsElement = isElement;
                this.Finite = true;

                this.Coords = new List<Tuple<int, int>>();

                if (this.IsElement)
                    this.Coords.Add(new Tuple<int, int>(this.X, this.Y));
            }

            public override string ToString()
            {
                return "[" + this.X + ", " + this.Y + "] " + this.IsElement + " (" + this.Size + ")";
            }
        }

        public class CoordValue
        {
            public Coord Coord { get; set; }
            public int Distance { get; set; }

            public CoordValue(int distance, Coord coord)
            {
                this.Coord = coord;
                this.Distance = distance;
            }

            public CoordValue(int distance)
            {
                this.Coord = null;
                this.Distance = distance;
            }

            public override string ToString()
            {
                return (this.Coord != null ? this.Coord.ToString() : "[_, _]") + ": " + this.Distance;
            }
        }

        static List<Coord> inputValuesTest = new List<Coord>()
        {
            new Coord(1, 1),
            new Coord(1, 6),
            new Coord(8, 3),
            new Coord(3, 4),
            new Coord(5, 5),
            new Coord(8, 9)
        };

        static List<Coord> realInputValues = new List<Coord>()
        {
            new Coord(350, 353),
            new Coord(238, 298),
            new Coord(248, 152),
            new Coord(168, 189),
            new Coord(127, 155),
            new Coord(339, 202),
            new Coord(304, 104),
            new Coord(317, 144),
            new Coord( 83, 106),
            new Coord( 78, 106),
            new Coord(170, 230),
            new Coord(115, 194),
            new Coord(350, 272),
            new Coord(159, 69 ),
            new Coord(197, 197),
            new Coord(190, 288),
            new Coord(227, 215),
            new Coord(228, 124),
            new Coord(131, 238),
            new Coord(154, 323),
            new Coord( 54, 185),
            new Coord(133,  75),
            new Coord(242, 184),
            new Coord(113, 273),
            new Coord( 65, 245),
            new Coord(221,  66),
            new Coord(148,  82),
            new Coord(131, 351),
            new Coord( 97, 272),
            new Coord( 72,  93),
            new Coord(203, 116),
            new Coord(209, 295),
            new Coord(133, 115),
            new Coord(355, 304),
            new Coord(298, 312),
            new Coord(251,  58),
            new Coord( 81, 244),
            new Coord(138, 115),
            new Coord(302, 341),
            new Coord(286, 103),
            new Coord(111,  95),
            new Coord(148, 194),
            new Coord(235, 262),
            new Coord( 41, 129),
            new Coord(270, 275),
            new Coord(234, 117),
            new Coord(273, 257),
            new Coord( 98, 196),
            new Coord(176, 122),
            new Coord(121, 258)
        };
    }
}
