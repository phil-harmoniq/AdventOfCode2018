using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle07
    {
        public static readonly string[] Input = File.ReadAllLines("Inputs/Input07.txt");

        public static string Part1()
        {
            var taskPool = new TaskPool(Input);
            var correctStepOrder = new List<char>();
            while (!taskPool.Finished) { correctStepOrder.Add(taskPool.Part1FinishStep().Id); }
            return new string(correctStepOrder.ToArray());
        }

        public static int Part2()
        {
            var minutesElapsed = 0;
            var taskPool = new TaskPool(Input);
            var workers = new Worker[]
            {
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker(),
                new Worker()
            };

            while (!taskPool.Finished)
            {
                var idleWorkers = workers.Where(w => w.CurrentTask == null);
                
                while (idleWorkers.Count() > 0 && taskPool.AvailableSteps.Count > 0)
                {
                    taskPool.AssignNextAvailableTask(idleWorkers.First());
                    idleWorkers = idleWorkers.Skip(1);
                }

                var activeWorkers = workers.Where(w => w.CurrentTask != null).OrderBy(w => w.MinutesLeft);
                var nextWorkerCompletion = activeWorkers.First();
                int timeElapsed = nextWorkerCompletion.MinutesLeft;

                foreach (var worker in activeWorkers)
                {
                    worker.MinutesLeft -= timeElapsed;
                    if (worker.MinutesLeft < 1)
                    {
                        taskPool.FinishStep(worker.CurrentTask);
                        worker.CurrentTask = null;
                    }
                }
                minutesElapsed += timeElapsed;
            }

            return minutesElapsed;
        }

        private class TaskPool
        {
            internal SortedDictionary<char, Step> AvailableSteps { get; }
            internal Dictionary<char, Step> BlockedSteps { get; }
            internal bool Finished => AvailableSteps.Count == 0;

            internal TaskPool(string[] input)
            {
                AvailableSteps = new SortedDictionary<char, Step>();
                BlockedSteps = new Dictionary<char, Step>();
                var steps = new Dictionary<char, Step>();
                for (var c = 'A'; c <= 'Z'; c++) { steps.Add(c, new Step(c)); }
            
                foreach (var line in Input)
                {
                    var split = line.Split(' ');
                    steps[split[7][0]].AddDependency(steps[split[1][0]]);
                }
                
                foreach (var step in steps.Values) { AddStep(step); }
            }

            internal void AddStep(Step step)
            {
                if (!step.HasDependencies) { AvailableSteps.Add(step.Id, step); }
                else { BlockedSteps.Add(step.Id, step); }
            }

            internal Step Part1FinishStep()
            {
                return FinishStep(AvailableSteps.First().Value);
            }

            internal Step FinishStep(Step currentStep)
            {
                var newAvailableSteps = new List<Step>();

                foreach (var blockedStep in BlockedSteps.Values)
                {
                    if (blockedStep.HasDependencyOn(currentStep))
                    {
                        blockedStep.RemoveDependency(currentStep);
                        if (!blockedStep.HasDependencies)
                        {
                            newAvailableSteps.Add(blockedStep);
                        }
                    }
                }

                foreach (var step in newAvailableSteps)
                {
                    BlockedSteps.Remove(step.Id);
                    AvailableSteps.Add(step.Id, step);
                }

                currentStep.CompleteStep();
                AvailableSteps.Remove(currentStep.Id);
                return currentStep;
            }

            internal void AssignNextAvailableTask(Worker worker)
            {
                var nextAvailableTask = AvailableSteps.First();
                worker.CurrentTask = nextAvailableTask.Value;
                worker.MinutesLeft = nextAvailableTask.Value.Id - 4;
                AvailableSteps.Remove(nextAvailableTask.Key);
            }
        }

        private class Step
        {
            internal char Id { get; }
            internal bool IsCompleted { get; private set; }
            internal Dictionary<char, Step> Dependencies { get; }
            internal bool HasDependencies => Dependencies.Count > 0;
            internal int MinutesNeeded { get; }
            internal void AddDependency(Step step) => Dependencies.Add(step.Id, step);
            internal void RemoveDependency(Step step) => Dependencies.Remove(step.Id);
            internal bool HasDependencyOn(Step step) => Dependencies.ContainsKey(step.Id);
            internal void CompleteStep() => IsCompleted = true;

            internal Step(char id)
            {
                Id = id;
                MinutesNeeded = Id - 4;
                Dependencies = new Dictionary<char, Step>();
            }
        }

        private class Worker
        {
            internal Step CurrentTask { get; set; }
            internal int MinutesLeft { get; set; }
            internal void NewTask(Step step) => CurrentTask = step;
        }
    }
}