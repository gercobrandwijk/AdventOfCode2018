using System;
using System.Diagnostics;

namespace AdventOfCode
{
    public class ProgramDay11
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay11");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            partOne();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");
        }

        static void partOne()
        {
            //partOne(8, 3, 5);

            //partOne(57, 122, 79);
            //partOne(39, 217, 196);
            //partOne(71, 101, 153);

            partOne(18);//, 33, 45);
            partOne(42);//, 21, 61);
            partOne(7165);
        }

        static void partOne(int gridSerialNumber, int? resultX = null, int? resultY = null)
        {
            int gridSize = 300;

            GridPoint[,] grid = new GridPoint[gridSize + 1, gridSize + 1];

            for (int y = 1; y <= gridSize; y++)
            {
                for (int x = 1; x <= gridSize; x++)
                {
                    int power = x;
                    power += +10;
                    power *= y;
                    power += gridSerialNumber;
                    power *= (x + 10);
                    power = (power / 100) % 10;
                    power -= 5;

                    int totalPower = power;

                    // Sum previous items
                    if (x > 1)
                    {
                        // Left
                        totalPower += grid[x - 1, y].Power;
                    }

                    if (y > 1)
                    {
                        if (x > 1)
                        {
                            // Top left
                            totalPower += grid[x - 1, y - 1].Power;
                        }

                        // Top
                        totalPower += grid[x, y - 1].Power;

                        if (x + 1 <= gridSize)
                            // Top right
                            totalPower += grid[x + 1, y - 1].Power;
                    }

                    // Update previous items
                    if (x > 1)
                    {
                        // Left
                        grid[x - 1, y].TotalPower += power;
                    }

                    if (y > 1)
                    {
                        if (x > 1)
                        {
                            // Top left
                            grid[x - 1, y - 1].TotalPower += power;
                        }

                        // Top
                        grid[x, y - 1].TotalPower += power;

                        if (x + 1 <= gridSize)
                            // Top right
                            grid[x + 1, y - 1].TotalPower += power;
                    }

                    grid[x, y] = new GridPoint()
                    {
                        X = x,
                        Y = y,
                        Power = power,
                        TotalPower = totalPower
                    };
                }
            }

            if (resultX.HasValue && resultY.HasValue)
            {
                Console.WriteLine(grid[resultX.Value, resultY.Value].Power + " - " + grid[(resultX + 1).Value, resultY.Value].Power + " - " + grid[(resultX + 2).Value, resultY.Value].Power);
                Console.WriteLine(grid[resultX.Value, (resultY + 1).Value].Power + " - " + grid[(resultX + 1).Value, (resultY + 1).Value].Power + " - " + grid[(resultX + 2).Value, (resultY + 1).Value].Power);
                Console.WriteLine(grid[resultX.Value, (resultY + 2).Value].Power + " - " + grid[(resultX + 1).Value, (resultY + 2).Value].Power + " - " + grid[(resultX + 2).Value, (resultY + 2).Value].Power);

                Console.WriteLine("Serial number: " + gridSerialNumber + ", X: " + resultX.Value + ", Y: " + resultY.Value + ", Power: " + grid[resultX.Value, resultY.Value].Power + ", Total power: " + grid[resultX.Value + 1, resultY.Value + 1].TotalPower);
            }
            else
            {
                GridPoint totalPowerHighest = null;

                for (int y = 1; y <= gridSize; y++)
                {
                    for (int x = 1; x <= gridSize; x++)
                    {
                        if (totalPowerHighest == null || grid[x, y].TotalPower > totalPowerHighest.TotalPower)
                            totalPowerHighest = grid[x, y];
                    }
                }

                if (totalPowerHighest != null)
                {
                    GridPoint topLeft = grid[totalPowerHighest.X - 1, totalPowerHighest.Y - 1];

                    Console.WriteLine("X: " + topLeft.X + ", Y: " + topLeft.Y + ", Total power: " + totalPowerHighest.TotalPower);
                }
            }
        }

        public class GridPoint
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Power { get; set; }

            public int TotalPower { get; set; }
        }
    }
}
