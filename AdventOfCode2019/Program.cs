using System;
using System.Diagnostics;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Reset();
            stopwatch.Start();
            Day01.Run();
            Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine();

            stopwatch.Reset();
            stopwatch.Start();
            Day02.Run();
            Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine();

            stopwatch.Reset();
            stopwatch.Start();
            Day03.Run();
            Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine();

            stopwatch.Reset();
            stopwatch.Start();
            Day04.Run();
            Console.WriteLine("Done in " + stopwatch.ElapsedMilliseconds + "ms");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
