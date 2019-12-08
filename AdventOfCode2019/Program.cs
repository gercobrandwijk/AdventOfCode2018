using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            List<IAdventOfCodeDay> days = new List<IAdventOfCodeDay>() {
                new Day01(1),
                new Day02(2),
                new Day03(3),
                new Day04(4),
                new Day05(5),
                new Day06(6),
                new Day07(7),
                new Day08(8),
            };

            foreach (IAdventOfCodeDay day in days)
            {
                Console.WriteLine("Day " + day.Number);
                stopwatch.Reset();
                stopwatch.Start();
                day.Run();
                Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
    public interface IAdventOfCodeDay
    {
        int Number { get; }

        void Run();
    }

    public abstract class AdventOfCodeDay : IAdventOfCodeDay
    {
        public int Number { get; }

        public AdventOfCodeDay(int number)
        {
            this.Number = number;
        }

        public abstract void Run();
    }

}
