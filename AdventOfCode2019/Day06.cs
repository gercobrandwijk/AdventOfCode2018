using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day06 : AdventOfCodeDay
    {
        public Day06(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            string[] lines = File.ReadAllLines("Data/Day06.txt");
            //string[] lines = File.ReadAllLines("Data/Day06Part1Example.txt");
            //string[] lines = File.ReadAllLines("Data/Day06Part2Example.txt");

            Dictionary<string, ObjectData> data;

            data = new Dictionary<string, ObjectData>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(')');

                var first = parts[0];
                var second = parts[1];

                if (!data.ContainsKey(first))
                    data.Add(first, new ObjectData(first));

                if (!data.ContainsKey(second))
                    data.Add(second, new ObjectData(second));

                data[second].DirectOrbit = data[first];
            }

            int total = data.Sum(x => x.Value.AmountOfOrbits);

            Console.WriteLine("Part 1: " + total);

            data = new Dictionary<string, ObjectData>();

            string orbitYou = string.Empty;
            string orbitSanta = string.Empty;

            foreach (string line in lines)
            {
                string[] parts = line.Split(')');

                var first = parts[0];
                var second = parts[1];

                if (second == "YOU")
                {
                    orbitYou = first;

                    continue;
                }
                else if (second == "SAN")
                {
                    orbitSanta = first;

                    continue;
                }

                if (!data.ContainsKey(first))
                    data.Add(first, new ObjectData(first));

                if (!data.ContainsKey(second))
                    data.Add(second, new ObjectData(second));

                data[second].DirectOrbit = data[first];
            }

            List<string> pathToRootYou = data[orbitYou].PathToRoot;
            List<string> pathToRootSanta = data[orbitSanta].PathToRoot;

            string crossAt = string.Empty;

            foreach (var path in pathToRootSanta)
            {
                if (pathToRootYou.Contains(path))
                {
                    crossAt = path;

                    break;
                }
            }

            int stepsFromYou = pathToRootYou.IndexOf(crossAt);
            int stepsFromSanta = pathToRootSanta.IndexOf(crossAt);

            int stepTotal = stepsFromYou + stepsFromSanta;

            Console.WriteLine("Part 2: " + stepTotal);
        }

        public class ObjectData
        {
            public string Name { get; set; }
            public ObjectData DirectOrbit { get; set; }

            public ObjectData(string name)
            {
                this.Name = name;
            }

            public int AmountOfOrbits
            {
                get
                {
                    int counter = 0;

                    var orbit = this.DirectOrbit;

                    while (orbit != null)
                    {
                        counter++;

                        orbit = orbit.DirectOrbit;
                    }

                    return counter;
                }
            }

            public List<string> PathToRoot
            {
                get
                {
                    List<string> paths = new List<string>();
                    paths.Add(this.Name);

                    var orbit = this.DirectOrbit;

                    while (orbit != null)
                    {
                        paths.Add(orbit.Name);

                        orbit = orbit.DirectOrbit;
                    }

                    return paths;
                }
            }
        }
    }
}
