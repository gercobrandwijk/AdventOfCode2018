using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    public class Day14 : AdventOfCodeDay
    {
        List<Chemical> chemicals = new List<Chemical>();
        List<Reaction> reactions = new List<Reaction>();

        public Day14(int number) : base(number)
        {
        }

        public override async Task Run()
        {
            string[] lines = File.ReadAllLines("Data/Day14.txt");
            //lines = File.ReadAllLines("Data/Day14Part1Example1.txt");
            //lines = File.ReadAllLines("Data/Day14Part1Example2.txt");
            //lines = File.ReadAllLines("Data/Day14Part1Example3.txt");
            //lines = File.ReadAllLines("Data/Day14Part1Example4.txt");
            //lines = File.ReadAllLines("Data/Day14Part1Example5.txt");

            foreach (string line in lines)
            {
                string[] lineSplitted = line.Split("=>");

                Dictionary<Chemical, long> inputs = lineSplitted[0].Trim().Split(",").Select(x =>
                {
                    string[] inputSplitted = x.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    Chemical chemical = this.getChemical(inputSplitted[1].Trim());

                    return new Tuple<Chemical, long>(chemical, long.Parse(inputSplitted[0]));
                }).ToDictionary(x => x.Item1, x => x.Item2);

                string[] outputSplitted = lineSplitted[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                Tuple<Chemical, long> output = new Tuple<Chemical, long>(this.getChemical(outputSplitted[1].Trim()), long.Parse(outputSplitted[0]));

                Reaction reaction = new Reaction()
                {
                    Inputs = inputs,
                    Output = output
                };

                if (reaction.Inputs.Count == 1 && reaction.Inputs.First().Key.Name == "ORE")
                    reaction.IsOre = true;

                output.Item1.ReactionToProduce = reaction;

                this.reactions.Add(reaction);
            }

            //foreach (var reaction in reactions)
            //{
            //    Console.WriteLine(reaction.ToString());
            //}

            Tuple<Chemical, long> chemicalFuel = reactions.First(x => x.Output.Item1.Name == "FUEL").Output;

            long oreAmount = this.produce(chemicalFuel.Item1, chemicalFuel.Item2);

            Console.WriteLine("Part 1: " + oreAmount);

            long oreAmountTotal = 1000000000000;
            long bsMax = 1;
            long bsMin = 0;
            long bsMid;

            while (true)
            {
                long oreAmountCurrent = this.produce(chemicalFuel.Item1, bsMax);

                if (oreAmountCurrent < oreAmountTotal)
                    bsMax *= 2;
                else
                    break;
            }

            while (bsMin < bsMax)
            {
                bsMid = (bsMin + bsMax) / 2;

                if (this.produce(chemicalFuel.Item1, bsMid) <= oreAmountTotal)
                    bsMin = bsMid + 1;
                else
                    bsMax = bsMid;
            }

            Console.WriteLine("Part 2: " + (bsMin - 1));
        }

        private long produce(Chemical chemical, long amount)
        {
            if (chemical.Name == "FUEL")
            {
                foreach (Chemical chemicalClean in chemicals)
                {
                    chemicalClean.AmountAvailable = 0;
                }
            }

            long oreAmount = 0;

            if (chemical.AmountAvailable >= amount)
            {
                chemical.AmountAvailable -= amount;
                return oreAmount;
            }

            if (chemical.AmountAvailable > 0)
            {
                amount -= chemical.AmountAvailable;
                chemical.AmountAvailable = 0;
            }

            long amountOfReactions;

            if (amount <= chemical.ReactionToProduce.Output.Item2)
            {
                amountOfReactions = 1;
            }
            else
            {
                amountOfReactions = amount / chemical.ReactionToProduce.Output.Item2;

                if (amount % chemical.ReactionToProduce.Output.Item2 != 0)
                    amountOfReactions += 1;
            }

            foreach (var input in chemical.ReactionToProduce.Inputs)
            {
                long amountNeeded = input.Value * amountOfReactions;

                if (input.Key.Name == "ORE")
                    oreAmount += amountNeeded;
                else
                {
                    oreAmount += this.produce(input.Key, amountNeeded);
                }
            }

            chemical.AmountAvailable += amountOfReactions * chemical.ReactionToProduce.Output.Item2;
            chemical.AmountAvailable -= amount;

            return oreAmount;
        }

        public Chemical getChemical(string name)
        {
            Chemical chemical = chemicals.FirstOrDefault(x => x.Name == name);

            if (chemical == null)
            {
                chemical = new Chemical() { Name = name };

                chemicals.Add(chemical);
            }

            return chemical;
        }

        public class Chemical
        {
            public string Name { get; set; }

            public long AmountAvailable { get; set; }

            public Reaction ReactionToProduce { get; set; }

            public override string ToString()
            {
                return this.Name;
            }
        }

        public class Reaction
        {
            public Dictionary<Chemical, long> Inputs { get; set; } = new Dictionary<Chemical, long>();
            public Tuple<Chemical, long> Output { get; set; }
            public bool IsOre { get; set; }

            public override string ToString()
            {
                return this.Inputs.Select(x => x.Value + " " + x.Key.Name).Aggregate("", (x, y) => string.IsNullOrWhiteSpace(x) ? y : x + ", " + y) + " => " + this.Output.Item2 + " " + this.Output.Item1.Name;
            }
        }
    }
}
