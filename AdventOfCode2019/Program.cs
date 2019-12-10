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

            int runOnlyDay = 07;

            List<IAdventOfCodeDay> days = new List<IAdventOfCodeDay>() {
                new Day01(01),
                new Day02(02),
                new Day03(03),
                new Day04(04),
                new Day05(05),
                new Day06(06),
                new Day07(07),
                new Day08(08),
                new Day09(09),
                new Day10(10),
            };

            if (runOnlyDay != 0)
                days = new List<IAdventOfCodeDay>() { days[runOnlyDay - 1] };

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
