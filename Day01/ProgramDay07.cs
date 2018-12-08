using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class ProgramDay07
    {
        public static void Run()
        {
            Console.WriteLine("ProgramDay07");

            Stopwatch watch = Stopwatch.StartNew();

            Console.WriteLine("Part one");
            watch.Restart();
            partOne();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");

            Console.WriteLine("Part two");
            watch.Restart();
            partTwo();
            watch.Stop();
            Console.WriteLine($"Done in: {watch.Elapsed.TotalMilliseconds}ms");
        }

        static List<Step> parse(string[] values)
        {
            List<Step> allSteps = new List<Step>();

            foreach (string value in values)
            {
                Step step = allSteps.SingleOrDefault(x => x.Name == value[5]);

                if (step == null)
                {
                    step = new Step(value[5]);

                    allSteps.Add(step);
                }

                Step nextStep = allSteps.SingleOrDefault(x => x.Name == value[36]);

                if (nextStep == null)
                {
                    nextStep = new Step(value[36]);

                    allSteps.Add(nextStep);
                }

                step.Next.Add(nextStep);

                nextStep.Conditions.Add(step);
            }

            return allSteps;
        }

        static void partOne()
        {
            partOne(parse(inputValuesTest));
            partOne(parse(realInputValues));
        }

        static void partOne(List<Step> values)
        {
            List<Step> stepsToExecute = values.Where(x => !x.Conditions.Any()).ToList();

            String result = "";

            do
            {
                Step step = stepsToExecute.OrderBy(x => x.Name).First();
                step.Executed = true;

                result += step;

                stepsToExecute.Remove(step);

                foreach (Step nextStep in step.Next)
                {
                    nextStep.Conditions.Remove(step);

                    if (!nextStep.Conditions.Any())
                        stepsToExecute.Add(nextStep);
                }
            }
            while (stepsToExecute.Any());

            Console.WriteLine("Execution order: " + result);
        }

        static void partTwo()
        {
            partTwo(parse(inputValuesTest));
            partTwo(parse(realInputValues));
        }

        static void partTwo(List<Step> values)
        {
            List<Step> stepsToExecute = values.Where(x => !x.Conditions.Any()).ToList();

            List<Step> stepsExecuting = new List<Step>();

            List<Worker> workers = new List<Worker>();

            for (int i = 0; i < 5; i++)
                workers.Add(new Worker(null));

            String result = "";

            int ticks = 0;

            do
            {
                ticks++;

                List<Worker> busyWorkers = workers.Where(x => x.Step != null).ToList();

                foreach (Worker worker in busyWorkers)
                {
                    if (worker.Step.Progress >= worker.Step.Duration)
                    {
                        worker.Step.Executed = true;

                        result += worker.Step;

                        stepsToExecute.Remove(worker.Step);

                        foreach (Step nextStep in worker.Step.Next)
                        {
                            nextStep.Conditions.Remove(worker.Step);

                            if (!nextStep.Conditions.Any())
                                stepsToExecute.Add(nextStep);
                        }

                        worker.Step = null;
                    }
                    else
                    {
                        worker.Step.Progress++;
                    }
                }

                List<Worker> idleWorkers = workers.Where(x => x.Step == null).ToList();

                foreach (Worker worker in idleWorkers)
                {
                    Step step = stepsToExecute.Where(x => !x.Executing).OrderBy(x => x.Name).FirstOrDefault();

                    if (step != null)
                    {
                        worker.Step = step;
                        worker.Step.Progress = 1;
                        worker.Step.Executing = true;
                    }
                }
            }
            while (stepsToExecute.Any());

            ticks--;

            Console.WriteLine("Execution order: " + result + " (in " + ticks + " ticks)");
        }

        public class Worker
        {
            public Step Step { get; set; }

            public Worker(Step step)
            {
                this.Step = step;
            }
        }

        public class Step
        {
            public List<Step> Next { get; set; }

            public List<Step> Conditions { get; set; }

            public char Name { get; set; }

            public bool Executing { get; set; }

            public bool Executed { get; set; }

            public int Progress { get; set; }

            public int Duration { get; set; }

            public Step(char name)
            {
                this.Name = name;
                this.Next = new List<Step>();
                this.Conditions = new List<Step>();

                this.Duration = (int)name - 4;
            }

            public override string ToString()
            {
                return this.Name.ToString();
            }
        }

        static string[] inputValuesTest = new string[]
        {
            "Step C must be finished before step A can begin.",
            "Step C must be finished before step F can begin.",
            "Step A must be finished before step B can begin.",
            "Step A must be finished before step D can begin.",
            "Step B must be finished before step E can begin.",
            "Step D must be finished before step E can begin.",
            "Step F must be finished before step E can begin."
        };

        static string[] realInputValues = new string[]
        {
            "Step X must be finished before step C can begin.",
            "Step C must be finished before step G can begin.",
            "Step F must be finished before step G can begin.",
            "Step U must be finished before step Y can begin.",
            "Step O must be finished before step S can begin.",
            "Step D must be finished before step N can begin.",
            "Step M must be finished before step H can begin.",
            "Step J must be finished before step Q can begin.",
            "Step G must be finished before step R can begin.",
            "Step I must be finished before step N can begin.",
            "Step R must be finished before step K can begin.",
            "Step A must be finished before step Z can begin.",
            "Step Y must be finished before step L can begin.",
            "Step H must be finished before step P can begin.",
            "Step K must be finished before step S can begin.",
            "Step Z must be finished before step P can begin.",
            "Step T must be finished before step S can begin.",
            "Step N must be finished before step P can begin.",
            "Step E must be finished before step S can begin.",
            "Step S must be finished before step W can begin.",
            "Step W must be finished before step V can begin.",
            "Step L must be finished before step V can begin.",
            "Step P must be finished before step B can begin.",
            "Step Q must be finished before step V can begin.",
            "Step B must be finished before step V can begin.",
            "Step P must be finished before step Q can begin.",
            "Step S must be finished before step V can begin.",
            "Step C must be finished before step Q can begin.",
            "Step I must be finished before step H can begin.",
            "Step A must be finished before step E can begin.",
            "Step H must be finished before step Q can begin.",
            "Step G must be finished before step V can begin.",
            "Step N must be finished before step L can begin.",
            "Step R must be finished before step Q can begin.",
            "Step W must be finished before step L can begin.",
            "Step X must be finished before step L can begin.",
            "Step X must be finished before step J can begin.",
            "Step W must be finished before step P can begin.",
            "Step U must be finished before step B can begin.",
            "Step P must be finished before step V can begin.",
            "Step O must be finished before step P can begin.",
            "Step W must be finished before step Q can begin.",
            "Step S must be finished before step Q can begin.",
            "Step U must be finished before step Z can begin.",
            "Step Z must be finished before step T can begin.",
            "Step M must be finished before step T can begin.",
            "Step A must be finished before step P can begin.",
            "Step Z must be finished before step B can begin.",
            "Step N must be finished before step S can begin.",
            "Step H must be finished before step N can begin.",
            "Step J must be finished before step E can begin.",
            "Step M must be finished before step J can begin.",
            "Step R must be finished before step A can begin.",
            "Step A must be finished before step Y can begin.",
            "Step F must be finished before step V can begin.",
            "Step L must be finished before step P can begin.",
            "Step K must be finished before step L can begin.",
            "Step F must be finished before step P can begin.",
            "Step G must be finished before step L can begin.",
            "Step I must be finished before step Q can begin.",
            "Step C must be finished before step L can begin.",
            "Step I must be finished before step Y can begin.",
            "Step G must be finished before step B can begin.",
            "Step H must be finished before step L can begin.",
            "Step X must be finished before step U can begin.",
            "Step I must be finished before step K can begin.",
            "Step R must be finished before step N can begin.",
            "Step I must be finished before step L can begin.",
            "Step M must be finished before step I can begin.",
            "Step K must be finished before step V can begin.",
            "Step G must be finished before step E can begin.",
            "Step F must be finished before step B can begin.",
            "Step O must be finished before step Y can begin.",
            "Step Y must be finished before step Q can begin.",
            "Step F must be finished before step K can begin.",
            "Step N must be finished before step W can begin.",
            "Step O must be finished before step R can begin.",
            "Step N must be finished before step E can begin.",
            "Step M must be finished before step V can begin.",
            "Step H must be finished before step T can begin.",
            "Step Y must be finished before step T can begin.",
            "Step F must be finished before step J can begin.",
            "Step F must be finished before step O can begin.",
            "Step W must be finished before step B can begin.",
            "Step T must be finished before step E can begin.",
            "Step T must be finished before step P can begin.",
            "Step F must be finished before step M can begin.",
            "Step U must be finished before step I can begin.",
            "Step H must be finished before step S can begin.",
            "Step S must be finished before step P can begin.",
            "Step T must be finished before step W can begin.",
            "Step A must be finished before step N can begin.",
            "Step O must be finished before step N can begin.",
            "Step L must be finished before step B can begin.",
            "Step U must be finished before step K can begin.",
            "Step Z must be finished before step W can begin.",
            "Step X must be finished before step D can begin.",
            "Step Z must be finished before step L can begin.",
            "Step I must be finished before step T can begin.",
            "Step O must be finished before step W can begin.",
            "Step I must be finished before step B can begin."
        };
    }
}
