using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class ProgramDay12
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay12");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            partOne();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            //Console.WriteLine("Part two");
            //watch.Restart();
            //partTwo();
            //watch.Stop();
            //Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");
        }

        static void partOne()
        {
            //partOne(parse(initialStateTest), rulesTest.Select(x => new Rule(x)).ToList(), 20);
            //partOne(parse(initialState), rules.Select(x => new Rule(x)).ToList(), 20);

            //partOne(parse(initialStateTest), rulesTest.Select(x => new Rule(x)).ToList(), 50000000000);
            partOne(parse(initialState), rules.Select(x => new Rule(x)).ToList(), 50000000000);
        }

        private static void partOne(List<bool> pots, List<Rule> list, long iterationAmount)
        {
            int zeroPoint = 5000;

            pots.InsertRange(0, Enumerable.Range(0, zeroPoint).Select(x => false).ToList());
            pots.AddRange(Enumerable.Range(0, zeroPoint).Select(x => false).ToList());

            int trendCounter = 0;

            long generation = 0;

            for (int iteration = 1; iteration <= iterationAmount; iteration++)
            {
                bool[] temp = new bool[pots.Count];

                pots.CopyTo(temp);

                for (int i = 0; i < pots.Count; i++)
                {
                    temp[i] = false;

                    if (i - 2 >= 0 && i + 2 <= pots.Count)
                    {
                        foreach (Rule rule in list)
                        {
                            bool[] compare = pots.Skip(i - 2).Take(5).ToArray();

                            if (compare.SequenceEqual(rule.Positions))
                            {
                                temp[i] = rule.Result;

                                break;
                            }
                        }
                    }
                }

                var trendSkip1 = temp.Skip(1);
                var trendSkip1Equal = pots.Take(pots.Count - 1);

                if (trendSkip1.SequenceEqual(trendSkip1Equal))
                {
                    trendCounter++;

                }
                else
                {
                    trendCounter = 0;
                }

                pots = temp.ToList();

                if (trendCounter >= 100)
                {
                    generation = iteration;

                    break;
                }
            }

            long potCounter = 0;
            long amount = 0;

            for (int i = 0; i < pots.Count; i++)
            {
                if (pots[i])
                {
                    amount++;

                    potCounter += i - zeroPoint;
                }
            }

            long generationsLeft = iterationAmount - generation;

            potCounter += generationsLeft * amount;

            Console.WriteLine("Pot count: " + potCounter);
        }

        static List<bool> parse(string state)
        {
            List<bool> list = new List<bool>();

            for (int i = 0; i < state.Length; i++)
            {
                list.Add(state[i] == '#');
            }

            return list;
        }

        public class Rule
        {
            public bool[] Positions { get; set; }

            public bool Result { get; set; }

            public Rule(string rule)
            {
                string[] parts = rule.Split(' ');

                this.Positions = new bool[5];
                this.Positions[0] = parts[0][0] == '#';
                this.Positions[1] = parts[0][1] == '#';
                this.Positions[2] = parts[0][2] == '#';
                this.Positions[3] = parts[0][3] == '#';
                this.Positions[4] = parts[0][4] == '#';

                this.Result = parts[2][0] == '#';
            }
        }

        public class Pot
        {
            public int Position { get; set; }
            public bool Filled { get; set; }

            public Pot(int position, char pot)
            {
                this.Position = position;
                this.Filled = pot == '#';
            }
        }

        public static string initialStateTest = "#..#.#..##......###...###";

        public static string[] rulesTest = new string[]
        {
            "...## => #",
            "..#.. => #",
            ".#... => #",
            ".#.#. => #",
            ".#.## => #",
            ".##.. => #",
            ".#### => #",
            "#.#.# => #",
            "#.### => #",
            "##.#. => #",
            "##.## => #",
            "###.. => #",
            "###.# => #",
            "####. => #"
        };

        public static string initialState = "#.#.#...#..##..###.##.#...#.##.#....#..#.#....##.#.##...###.#...#######.....##.###.####.#....#.#..##";

        public static string[] rules = new string[]
        {
            "#...# => #",
            "....# => .",
            "##..# => #",
            ".#.## => #",
            "##.## => .",
            "###.# => #",
            "..... => .",
            "...#. => .",
            ".#.#. => #",
            "#.##. => #",
            "..#.# => #",
            ".#... => #",
            "#.#.. => .",
            "##.#. => .",
            ".##.. => #",
            "#..#. => .",
            ".###. => .",
            "..#.. => .",
            "#.### => .",
            "..##. => .",
            ".#..# => #",
            ".##.# => .",
            ".#### => .",
            "...## => #",
            "#.#.# => #",
            "..### => .",
            "#..## => .",
            "####. => #",
            "##### => .",
            "###.. => #",
            "##... => #",
            "#.... => ."
        };
    }
}
