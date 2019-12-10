using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day10 : AdventOfCodeDay
    {
        public Day10(int number) : base(number)
        {
        }

        public override void Run()
        {
            //this.RunPart1("Part 1 Example 1", File.ReadAllLines("Data/Day10Part1Example1.txt"));
            //this.RunPart1("Part 1 Example 2", File.ReadAllLines("Data/Day10Part1Example2.txt"));
            //this.RunPart1("Part 1 Example 3", File.ReadAllLines("Data/Day10Part1Example3.txt"));
            //this.RunPart1("Part 1 Example 4", File.ReadAllLines("Data/Day10Part1Example4.txt"));
            this.RunPart1("Part 1", File.ReadAllLines("Data/Day10.txt"));

            //this.RunPart2("Part 2 Example 1", File.ReadAllLines("Data/Day10Part1Example4.txt"));
            this.RunPart2("Part 2", File.ReadAllLines("Data/Day10.txt"));
        }

        private Dictionary<Tuple<int, int>, Asteroid> parseAndCalculateAngles(string[] lines)
        {
            Dictionary<Tuple<int, int>, Asteroid> asteroids = new Dictionary<Tuple<int, int>, Asteroid>();

            int width = lines[0].Length;
            int height = lines.Length;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (lines[y][x] == '#')
                        asteroids.Add(new Tuple<int, int>(x, y), new Asteroid(x, y));
                }
            }

            //this.Print(asteroids, width, height);

            foreach (Asteroid current in asteroids.Values)
            {
                List<Asteroid> others = asteroids.Values.Except(new List<Asteroid>() { current }).ToList();

                foreach (Asteroid other in others)
                {
                    float xDiff = other.X - current.X;
                    float yDiff = other.Y - current.Y;

                    double distance = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                    double angle = ((Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI) + 90 + 360) % 360;

                    current.AddIfInLineOfSight(angle, distance, other);
                }
            }

            return asteroids;
        }

        public Asteroid RunPart1(string name, string[] lines)
        {
            Dictionary<Tuple<int, int>, Asteroid> asteroids = this.parseAndCalculateAngles(lines);

            int bestAsteroidAmount = asteroids.Values.Max(x => x.AsteroidsInDirectSight.Count);
            Asteroid bestAsteroid = asteroids.Values.FirstOrDefault(x => x.AsteroidsInDirectSight.Count == bestAsteroidAmount);

            Console.WriteLine(name + ": " + bestAsteroidAmount + " (" + bestAsteroid.X + "," + bestAsteroid.Y + ")");

            return bestAsteroid;
        }

        public void RunPart2(string name, string[] lines)
        {
            Dictionary<Tuple<int, int>, Asteroid> asteroids = this.parseAndCalculateAngles(lines);

            int bestAsteroidAmount = asteroids.Values.Max(x => x.AsteroidsInDirectSight.Count);
            Asteroid bestAsteroid = asteroids.Values.FirstOrDefault(x => x.AsteroidsInDirectSight.Count == bestAsteroidAmount);

            List<Tuple<double, double, Asteroid>> sortedOnAngleAndDistance = bestAsteroid.Asteroids.OrderBy(x => x.Item1).ThenBy(x => x.Item2).ToList();

            Dictionary<int, Asteroid> vaporized = new Dictionary<int, Asteroid>();

            for (int i = 0; i < sortedOnAngleAndDistance.Count; i++)
            {
                double currentAngle = sortedOnAngleAndDistance[i].Item1;
                Asteroid current = sortedOnAngleAndDistance[i].Item3;

                vaporized.Add(vaporized.Count, current);

                while (i + 1 < sortedOnAngleAndDistance.Count && sortedOnAngleAndDistance[i + 1].Item1 == currentAngle)
                    i++;
            }

            if (vaporized.Count >= 200)
                Console.WriteLine(name + ": vaporized as 200th is (" + vaporized[199].X + "," + vaporized[199].Y + "). Result is " + (vaporized[199].X * 100 + vaporized[199].Y) + ".");
        }

        public void Print(Dictionary<Tuple<int, int>, Asteroid> asteroids, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (asteroids.ContainsKey(new Tuple<int, int>(x, y)))
                        Console.Write('#');
                    else
                        Console.Write(' ');
                }

                Console.WriteLine();
            }
        }

        public class Asteroid
        {
            public int X { get; set; }
            public int Y { get; set; }

            public HashSet<double> AsteroidsInDirectSight { get; set; }

            public List<Tuple<double, double, Asteroid>> Asteroids { get; set; }

            public Asteroid(int x, int y)
            {
                this.X = x;
                this.Y = y;
                this.AsteroidsInDirectSight = new HashSet<double>();
                this.Asteroids = new List<Tuple<double, double, Asteroid>>();
            }

            internal void AddIfInLineOfSight(double angle, double distance, Asteroid asteroid)
            {
                if (!this.AsteroidsInDirectSight.Contains(angle))
                    this.AsteroidsInDirectSight.Add(angle);

                this.Asteroids.Add(new Tuple<double, double, Asteroid>(angle, distance, asteroid));
            }
        }
    }
}
