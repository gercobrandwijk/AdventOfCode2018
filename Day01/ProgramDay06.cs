//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;

//namespace AdventOfCode
//{
//    public class ProgramDay06
//    {
//        public static void Run()
//        {
//            Console.WriteLine("ProgramDay06");

//            Stopwatch watch = Stopwatch.StartNew();

//            Console.WriteLine("Part one");
//            watch.Restart();
//            partOne();
//            watch.Stop();
//            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

//            //Console.WriteLine("Part two");
//            //watch.Restart();
//            //partTwo();
//            //watch.Stop();
//            //Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

//            Console.ReadLine();
//        }

//        static void partOne()
//        {
//            partOne(inputValuesTest);
//            partOne(realInputValues);
//        }

//        static void partOne(List<Coord> values)
//        {
//            int maxCoordX = values.Max(x => x.X);
//            int maxCoordY = values.Max(x => x.Y);

//            Dictionary<Coord, CoordValue> coords = new Dictionary<Coord, CoordValue>();

//            foreach (Coord pair in values)
//            {
//                coords.Add(pair, new CoordValue(pair, 0));
//            }

//            foreach (Coord pair in values)
//            {
//                for (int x = 0; x < maxCoordX; x++)
//                {
//                    for (int y = 0; y < maxCoordY; y++)
//                    {
//                        Coord coord = new Coord(x, y);

//                        if (!coords.ContainsKey(coord))
//                            coords.Add(coord, new CoordValue(pair, 1000));

//                        CoordValue coordValue = coords[coord];

//                        if (coordValue.Distance == 0)
//                            continue;

//                        int distance = Math.Abs(y - pair.Y) + Math.Abs(x - pair.X);

//                        if (distance < coordValue.Distance)
//                        {
//                            if (coordValue.Coord?.Name != null)
//                                coordValue.Coord.Size--;

//                            pair.Size++;

//                            coordValue.Coord = pair;
//                            coordValue.Distance = distance;
//                        }
//                        else if (distance == coordValue.Distance)
//                        {
//                            if (coordValue.Coord?.Name != null)
//                                coordValue.Coord.Size--;

//                            coordValue.Coord = null;
//                            coordValue.Distance = distance;
//                        }
//                    }
//                }
//            }

//            coords = coords.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);

//            List<Coord> finiteValues = new List<Coord>(values);

//            for (int x = 0; x < maxCoordX; x++)
//            {
//                for (int y = 0; y < maxCoordY; y++)
//                {
//                    if (x != 0 && x != maxCoordX - 1)
//                        continue;

//                    if (y != 0 && y != maxCoordY - 1)
//                        continue;

//                    Coord coord = new Coord(x, y);

//                    if (coords[coord].Coord != null)
//                    {
//                        finiteValues.RemoveAll(e => e..Name == coords[coord].Coord.Name);
//                    }
//                }
//            }

//            Console.WriteLine("Largest finite area: " + finiteValues.OrderBy(x => x.Size).Last());
//        }

//        public class Coord : IComparable
//        {
//            public int X { get; set; }
//            public int Y { get; set; }

//            public bool IsElement { get; set; }

//            public int Size { get; set; }

//            public Coord(int x, int y, bool isElement = true)
//            {
//                this.X = x;
//                this.Y = y;
//                this.IsElement = isElement;

//                if (this.IsElement)
//                    this.Size = 1;
//            }

//            public override int GetHashCode()
//            {
//                return this.X.GetHashCode() + this.Y.GetHashCode();
//            }

//            public override bool Equals(object obj)
//            {
//                return this.X == ((Coord)obj).X && this.Y == ((Coord)obj).Y;
//            }

//            public int CompareTo(object obj)
//            {
//                if (this.X < ((Coord)obj).X)
//                {
//                    return -1;
//                }
//                else if (this.X == ((Coord)obj).X)
//                {
//                    if (this.Y < ((Coord)obj).Y)
//                        return -1;
//                    else
//                        return 1;
//                }
//                else
//                {
//                    return 1;
//                }
//            }

//            public override string ToString()
//            {
//                return "[" + this.X + ", " + this.Y + "] " + this.IsElement + " (" + this.Size + ")";
//            }
//        }

//        public class CoordValue
//        {
//            public Coord Coord { get; set; }
//            public int Distance { get; set; }

//            public CoordValue(Coord coord, int distance)
//            {
//                this.Coord = coord;
//                this.Distance = distance;
//            }

//            public override string ToString()
//            {
//                return (this.Coord != null ? this.Coord.ToString() : "[_, _]") + ": " + this.Distance;
//            }
//        }

//        static List<Coord> inputValuesTest = new List<Coord>()
//        {
//            new Coord(1, 1),
//            new Coord(1, 6),
//            new Coord(8, 3),
//            new Coord(3, 4),
//            new Coord(5, 5),
//            new Coord(8, 9)
//        };

//        static List<Coord> realInputValues = new List<Coord>()
//        {
//            new Coord(350, 353),
//            new Coord(238, 298),
//            new Coord(248, 152),
//            new Coord(168, 189),
//            new Coord(127, 155),
//            new Coord(339, 202),
//            new Coord(304, 104),
//            new Coord(317, 144),
//            new Coord( 83, 106),
//            new Coord( 78, 106),
//            new Coord(170, 230),
//            new Coord(115, 194),
//            new Coord(350, 272),
//            new Coord(159, 69 ),
//            new Coord(197, 197),
//            new Coord(190, 288),
//            new Coord(227, 215),
//            new Coord(228, 124),
//            new Coord(131, 238),
//            new Coord(154, 323),
//            new Coord( 54, 185),
//            new Coord(133,  75),
//            new Coord(242, 184),
//            new Coord(113, 273),
//            new Coord( 65, 245),
//            new Coord(221,  66),
//            new Coord(148,  82),
//            new Coord(131, 351),
//            new Coord( 97, 272),
//            new Coord( 72,  93),
//            new Coord(203, 116),
//            new Coord(209, 295),
//            new Coord(133, 115),
//            new Coord(355, 304),
//            new Coord(298, 312),
//            new Coord(251,  58),
//            new Coord( 81, 244),
//            new Coord(138, 115),
//            new Coord(302, 341),
//            new Coord(286, 103),
//            new Coord(111,  95),
//            new Coord(148, 194),
//            new Coord(235, 262),
//            new Coord( 41, 129),
//            new Coord(270, 275),
//            new Coord(234, 117),
//            new Coord(273, 257),
//            new Coord( 98, 196),
//            new Coord(176, 122),
//            new Coord(121, 258)
//        };
//    }
//}
