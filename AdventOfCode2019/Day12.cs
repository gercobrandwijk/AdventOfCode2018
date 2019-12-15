using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public static class MathExtensions
    {
        public static Int64 GCD(Int64 a, Int64 b)
        {
            if (a == 0) return b;
            return MathExtensions.GCD(b % a, a);
        }

        public static Int64 LCM(Int64 a, Int64 b)
        {
            return (a * b) / MathExtensions.GCD(a, b);
        }
    }

    public class Day12 : AdventOfCodeDay
    {
        public Day12(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            RunPart1();
            RunPart2();
        }

        private void RunPart1()
        {
            int amountOfSteps = 1000;
            string[] lines = File.ReadAllLines("Data/Day12.txt");

            //amountOfSteps = 10;
            //lines = File.ReadAllLines("Data/Day12Part1Example.txt");

            List<Moon> moons = new List<Moon>();

            foreach (string line in lines)
                moons.Add(new Moon(line));

            Int64 totalEnergy = 0;

            for (int i = 0; i < amountOfSteps; i++)
            {
                this.Step(moons);

                totalEnergy = 0;

                foreach (Moon moon in moons)
                {
                    totalEnergy += moon.Energy;
                }
            }

            Console.WriteLine("Part 1: " + totalEnergy);
        }

        private void RunPart2()
        {
            string[] lines = File.ReadAllLines("Data/Day12.txt");
            //lines = File.ReadAllLines("Data/Day12Part1Example.txt");
            //lines = File.ReadAllLines("Data/Day12Part2Example.txt");

            List<Moon> moons = new List<Moon>();

            foreach (string line in lines)
                moons.Add(new Moon(line));

            int steps = 0;

            int periodX = 0;
            int periodY = 0;
            int periodZ = 0;

            string repeatingX = moons[0].XPos + "_" + moons[0].XVel + "_" + moons[1].XPos + "_" + moons[1].XVel + "_" + moons[2].XPos + "_" + moons[2].XVel + "_" + moons[3].XPos + "_" + moons[3].XVel;
            string repeatingY = moons[0].YPos + "_" + moons[0].YVel + "_" + moons[1].YPos + "_" + moons[1].YVel + "_" + moons[2].YPos + "_" + moons[2].YVel + "_" + moons[3].YPos + "_" + moons[3].YVel;
            string repeatingZ = moons[0].ZPos + "_" + moons[0].ZVel + "_" + moons[1].ZPos + "_" + moons[1].ZVel + "_" + moons[2].ZPos + "_" + moons[2].ZVel + "_" + moons[3].ZPos + "_" + moons[3].ZVel;

            while (periodX == 0 || periodY == 0 || periodZ == 0)
            {
                this.Step(moons);

                steps++;

                if (periodX == 0 && repeatingX == moons[0].XPos + "_" + moons[0].XVel + "_" + moons[1].XPos + "_" + moons[1].XVel + "_" + moons[2].XPos + "_" + moons[2].XVel + "_" + moons[3].XPos + "_" + moons[3].XVel)
                    periodX = steps;

                if (periodY == 0 && repeatingY == moons[0].YPos + "_" + moons[0].YVel + "_" + moons[1].YPos + "_" + moons[1].YVel + "_" + moons[2].YPos + "_" + moons[2].YVel + "_" + moons[3].YPos + "_" + moons[3].YVel)
                    periodY = steps;

                if (periodZ == 0 && repeatingZ == moons[0].ZPos + "_" + moons[0].ZVel + "_" + moons[1].ZPos + "_" + moons[1].ZVel + "_" + moons[2].ZPos + "_" + moons[2].ZVel + "_" + moons[3].ZPos + "_" + moons[3].ZVel)
                    periodZ = steps;
            }

            Int64 result = MathExtensions.LCM(periodZ, MathExtensions.LCM(periodX, periodY));

            Console.WriteLine("Part 2: " + result);
        }

        public void Step(List<Moon> moons)
        {
            foreach (Moon moon in moons)
            {
                moon.XVelPending = 0;
                moon.YVelPending = 0;
                moon.ZVelPending = 0;

                foreach (Moon other in moons)
                {
                    if (other == moon)
                        continue;

                    if (moon.XPos < other.XPos)
                        moon.XVelPending += 1;
                    else if (moon.XPos > other.XPos)
                        moon.XVelPending -= 1;

                    if (moon.YPos < other.YPos)
                        moon.YVelPending += 1;
                    else if (moon.YPos > other.YPos)
                        moon.YVelPending -= 1;

                    if (moon.ZPos < other.ZPos)
                        moon.ZVelPending += 1;
                    else if (moon.ZPos > other.ZPos)
                        moon.ZVelPending -= 1;
                }
            }

            foreach (Moon moon in moons)
            {
                moon.XVel += moon.XVelPending;
                moon.YVel += moon.YVelPending;
                moon.ZVel += moon.ZVelPending;

                moon.XPos += moon.XVel;
                moon.YPos += moon.YVel;
                moon.ZPos += moon.ZVel;
            }
        }

        public class Moon
        {
            public Int64 XPos { get; set; }
            public Int64 YPos { get; set; }
            public Int64 ZPos { get; set; }

            public Int64 XVel { get; set; }
            public Int64 YVel { get; set; }
            public Int64 ZVel { get; set; }

            public Int64 XVelPending { get; set; }
            public Int64 YVelPending { get; set; }
            public Int64 ZVelPending { get; set; }

            public Int64 EnergyPot
            {
                get
                {
                    return Math.Abs(this.XPos) + Math.Abs(this.YPos) + Math.Abs(this.ZPos);
                }
            }

            public Int64 EnergyKin
            {
                get
                {
                    return Math.Abs(this.XVel) + Math.Abs(this.YVel) + Math.Abs(this.ZVel);
                }
            }

            public Int64 Energy
            {
                get
                {
                    return this.EnergyPot * this.EnergyKin;
                }
            }

            public Moon(string input)
            {
                input = input.Substring(1, input.Length - 2);

                var coords = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

                this.XPos = int.Parse(coords[0].Split('=')[1]);
                this.YPos = int.Parse(coords[1].Split('=')[1]);
                this.ZPos = int.Parse(coords[2].Split('=')[1]);
            }

            public override string ToString()
            {
                return "pos=<x=" + this.XPos + ", y=" + this.YPos + ", z=" + this.ZPos + ">, vel=<x=" + this.XVel + ", y=" + this.YVel + ", z=" + this.ZVel + ">, energy<pot=" + this.EnergyPot + ", kin=" + this.EnergyKin + ", total=" + this.Energy + ">";
            }
        }
    }
}
