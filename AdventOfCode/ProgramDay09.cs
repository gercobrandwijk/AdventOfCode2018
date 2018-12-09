using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class ProgramDay09
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay09");

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

        static List<Game> parse(string[] values)
        {
            return values.Select(x => new Game(x)).ToList();
        }

        static void partOne()
        {
            partOne(parse(inputValuesTest));
            partOne(parse(realInputValues));
        }

        static void partOne(List<Game> games)
        {
            foreach (Game game in games)
            {
                partOne(game);
            }
        }

        static void partOne(Game game)
        {
            List<int> stack = new List<int>();
            stack.Add(0);

            int lastMarbleIndex = 0;

            Console.WriteLine("Game starting: " + game);

            int currentMarble = 0;
            int playerIndex = 0;

            while (currentMarble < game.LastMarble)
            {
                playerIndex = playerIndex % game.Players.Count;

                Player player = game.Players[playerIndex];

                currentMarble++;

                if (stack.Count == 1)
                {
                    stack.Add(currentMarble);

                    lastMarbleIndex = 1;
                }
                else
                {
                    bool is23 = currentMarble % 23 == 0;

                    if (is23)
                    {
                        player.Score += currentMarble;

                        int nextMarbleIndex = lastMarbleIndex - 7;

                        if (nextMarbleIndex < 0)
                            nextMarbleIndex = stack.Count + nextMarbleIndex;

                        player.Score += stack[nextMarbleIndex];

                        stack.RemoveAt(nextMarbleIndex);

                        lastMarbleIndex = nextMarbleIndex;
                    }
                    else
                    {
                        int maxMarbleIndex = stack.Count - 1;

                        int nextMarbleIndex = lastMarbleIndex + 2;

                        // Add at end
                        if (nextMarbleIndex == maxMarbleIndex + 1)
                        {
                            stack.Add(currentMarble);

                        }
                        // Insert
                        else
                        {
                            nextMarbleIndex = nextMarbleIndex % stack.Count;

                            stack.Insert(nextMarbleIndex, currentMarble);
                        }

                        lastMarbleIndex = nextMarbleIndex;
                    }
                }

                playerIndex++;

                //Console.Write("Player " + player.Number + ": ");

                //foreach (int value in stack)
                //{
                //    Console.Write(value + " ");
                //}

                //Console.WriteLine();
            }

            Console.WriteLine("Game (" + game + ") winner is: " + game.Players.OrderBy(x => x.Score).Last());
        }

        public class Game
        {
            public List<Player> Players { get; set; }

            public int LastMarble { get; set; }

            public Game(string line)
            {
                string[] lineSplit = line.Split(' ');

                this.Players = new List<Player>();

                for (int i = 1; i <= int.Parse(lineSplit[0]); i++)
                {
                    this.Players.Add(new Player(i));
                }

                this.LastMarble = int.Parse(lineSplit[6]);
            }

            public override string ToString()
            {
                return this.Players.Count + " players, last marble " + this.LastMarble;
            }
        }

        public class Player
        {
            public int Number { get; set; }
            public int Score { get; set; }

            public Player(int number)
            {
                this.Number = number;
            }

            public override string ToString()
            {
                return this.Number + ", score: " + this.Score;
            }
        }

        static string[] inputValuesTest = new string[]
        {
            "9 players; last marble is worth 25 points",
            "10 players; last marble is worth 1618 points",
            "13 players; last marble is worth 7999 points",
            "17 players; last marble is worth 1104 points",
            "21 players; last marble is worth 6111 points",
            "30 players; last marble is worth 5807 points"
        };

        static string[] realInputValues = new string[]
        {
            "428 players; last marble is worth 70825 points",
            "428 players; last marble is worth 7082500 points"
        };
    }
}
