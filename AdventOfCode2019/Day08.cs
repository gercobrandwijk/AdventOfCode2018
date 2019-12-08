using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day08 : AdventOfCodeDay
    {
        public Day08(int number) : base(number)
        {
        }

        public override void Run()
        {
            string[] lines = File.ReadAllLines("Data/Day08.txt");
            //string[] lines = File.ReadAllLines("Data/Day08Part1Example.txt");
            //string[] lines = File.ReadAllLines("Data/Day08Part2Example.txt");

            string size = lines[0];
            string data = lines[1];

            int width = int.Parse(size.Split(',')[0]);
            int height = int.Parse(size.Split(',')[1]);

            int sizePerLayer = width * height;

            List<Layer> layers = new List<Layer>();

            for (int i = 0; i < data.Length; i++)
            {
                int value = (int)data[i] - 48;

                int layerIndex = i / sizePerLayer;

                if (layers.Count < layerIndex + 1)
                    layers.Add(new Layer(width, height));

                Layer layer = layers[layerIndex];

                int iMinusIndexLayer = i - layerIndex * sizePerLayer;

                int row = iMinusIndexLayer / width;
                int column = iMinusIndexLayer - row * width;

                layer.Data[column, row] = value;
            }

            Layer layerLowest0DigitAmount = null;
            int layerLowest0DigitAmountValue = int.MaxValue;

            foreach (Layer layer in layers)
            {
                int value = layer.GetAmountOfDigit(0);

                if (layerLowest0DigitAmount == null || value < layerLowest0DigitAmountValue)
                {
                    layerLowest0DigitAmount = layer;
                    layerLowest0DigitAmountValue = value;
                }
            }

            int result = layerLowest0DigitAmount.GetAmountOfDigit(1) * layerLowest0DigitAmount.GetAmountOfDigit(2);

            Console.WriteLine("Part 1: " + result);

            int[,] image = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    image[x, y] = 2;
                }
            }

            for (int i = layers.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (layers[i].Data[x, y] != 2)
                            image[x, y] = layers[i].Data[x, y];
                    }
                }
            }

            Console.WriteLine("Part 2");

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(image[x, y] == 1 ? "X" : " ");
                }

                Console.WriteLine();
            }
        }

        public class Layer
        {
            public int[,] Data { get; set; }
            public int Width { get; }
            public int Height { get; }

            public Layer(int width, int height)
            {
                this.Width = width;
                this.Height = height;
                this.Data = new int[this.Width, this.Height];
            }

            public int GetAmountOfDigit(int digit)
            {
                int amount = 0;

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        if (this.Data[x, y] == digit)
                            amount++;
                    }
                }

                return amount;
            }
        }
    }
}
