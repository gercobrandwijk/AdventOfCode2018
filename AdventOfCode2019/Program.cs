using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatchAll = new Stopwatch();
            Stopwatch stopwatch = new Stopwatch();

            int runOnlyDay = 0;

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
                new Day11(11),
                new Day12(12),
                new Day13(13),
                new Day14(14),
            };

            if (runOnlyDay != 0)
                days = days.Where(x => x.Number == runOnlyDay).ToList();

            stopwatchAll.Reset();
            stopwatchAll.Start();
            foreach (IAdventOfCodeDay day in days)
            {
                Console.WriteLine("Day " + day.Number);
                stopwatch.Reset();
                stopwatch.Start();
                await day.Run();
                Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
                Console.WriteLine();
            }

            if (runOnlyDay == 0)
                Console.WriteLine("ALL DONE IN: " + stopwatchAll.ElapsedMilliseconds + "ms");

            Console.ReadLine();
        }
    }
    public interface IAdventOfCodeDay
    {
        int Number { get; }

        Task Run();
    }

    public abstract class AdventOfCodeDay : IAdventOfCodeDay
    {
        public int Number { get; }

        public AdventOfCodeDay(int number)
        {
            this.Number = number;
        }

        public abstract Task Run();
    }

}
