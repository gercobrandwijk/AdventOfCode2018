using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2018
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
            Node firstNode = new Node(0);
            Node previousNode = firstNode;

            Console.WriteLine("Game starting: " + game);

            int currentMarble = 0;
            int playerIndex = 0;

            while (currentMarble < game.LastMarble)
            {
                playerIndex = playerIndex % game.Players.Count;

                Player player = game.Players[playerIndex];

                currentMarble++;

                if (previousNode.Previous == null && previousNode.Next == null)
                {
                    Node node = new Node(currentMarble, previousNode, previousNode);

                    previousNode.Previous = node;
                    previousNode.Next = node;

                    previousNode = node;
                }
                else
                {
                    bool is23 = currentMarble % 23 == 0;

                    if (is23)
                    {
                        player.Score += currentMarble;

                        Node previous7Node = getNodePrevious(previousNode, 7);

                        player.Score += previous7Node.Value;

                        previous7Node.Previous.Next = previous7Node.Next;
                        previous7Node.Next.Previous = previous7Node.Previous;

                        previousNode = previous7Node.Next;
                    }
                    else
                    {
                        Node node = new Node(currentMarble);

                        Node next2Node = getNodeNext(previousNode, 2);

                        node.Previous = next2Node.Previous;
                        node.Next = next2Node;

                        next2Node.Previous.Next = node;
                        next2Node.Previous = node;

                        previousNode = node;
                    }
                }

                playerIndex++;

                //Console.Write("Player " + player.Number + ": ");

                //Node writeNode = firstNode;
                //Console.Write(writeNode.Value + " ");

                //Node next = writeNode.Next;

                //while (next != null && next.Value != firstNode.Value)
                //{
                //    Console.Write(next.Value + " ");

                //    next = next.Next;
                //}

                //Console.WriteLine();
            }

            Console.WriteLine("Game (" + game + ") winner is: " + game.Players.OrderBy(x => x.Score).Last());
        }

        private static Node getNodePrevious(Node previousNode, int amount)
        {
            int counter = amount;
            Node temp = previousNode;

            while (counter > 0)
            {
                temp = temp.Previous;

                counter--;
            }

            return temp;
        }

        private static Node getNodeNext(Node previousNode, int amount)
        {
            int counter = amount;
            Node temp = previousNode;

            while (counter > 0)
            {
                temp = temp.Next;

                counter--;
            }

            return temp;
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
            public Int64 Score { get; set; }

            public Player(int number)
            {
                this.Number = number;
            }

            public override string ToString()
            {
                return this.Number + ", score: " + this.Score;
            }
        }

        public class Node
        {
            public Node Previous { get; set; }
            public Node Next { get; set; }

            public int Value { get; set; }

            public Node(int value)
            {
                this.Value = value;
            }

            public Node(int value, Node previous)
            {
                this.Value = value;
                this.Previous = previous;
            }

            public Node(int value, Node previous, Node next)
            {
                this.Value = value;
                this.Previous = previous;
                this.Next = next;
            }

            public override string ToString()
            {
                return this.Value.ToString();
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
