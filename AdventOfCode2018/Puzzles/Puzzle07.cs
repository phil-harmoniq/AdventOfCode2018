using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle07
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input07.txt"));

        public static string Part1()
        {
            var taskPool = new TaskPool(Input);
            var correctStepOrder = new List<char>();
            while (!taskPool.Finished) { correctStepOrder.Add(taskPool.Part1FinishStep().Id); }
            return new string(correctStepOrder.ToArray());
        }

        #warning Puzzle 07 part 2 is not correct
        public static int Part2()
        {
            var minutesElapsed = 0;
            var correctStepOrder = new List<char>();
            var taskPool = new TaskPool(Input);
            var workers = new Worker[]
            {
                new Worker(1),
                new Worker(2),
                new Worker(3),
                new Worker(4),
                new Worker(5)
            };

            while (!taskPool.Finished)
            {
                var idleWorkers = workers.Where(w => w.CurrentTask == null);
                
                while (idleWorkers.Count() > 0 && taskPool.StepsUnblocked.Count() > 0)
                {
                    taskPool.AssignNextAvailableTask(idleWorkers.First());
                    idleWorkers = idleWorkers.Skip(1);
                }

                var activeWorkers = workers.Where(w => w.CurrentTask != null).OrderBy(w => w.MinutesLeft);
                int timeElapsed = activeWorkers.First().MinutesLeft;

                foreach (var worker in activeWorkers)
                {
                    worker.ElapseMinutes(timeElapsed);

                    if (worker.MinutesLeft < 1)
                    {
                        correctStepOrder.Add(worker.CurrentTask.Id);
                        taskPool.FinishStep(worker.CurrentTask);
                        worker.ClearTask();
                    }
                }
                minutesElapsed += timeElapsed;
            }

            return minutesElapsed;
        }

        private class TaskPool
        {
            private readonly Dictionary<char, Step> _allSteps = new Dictionary<char, Step>();
            internal IOrderedEnumerable<Step> StepsUnblocked => _allSteps.Values
                .Where(s => !s.HasDependencies && !s.InProgress && !s.IsCompleted)
                .OrderBy(s => s.Id);
            internal IOrderedEnumerable<Step> StepsInProgress => _allSteps.Values
                .Where(s => s.InProgress)
                .OrderBy(s => s.AssignedWorker.MinutesLeft);
            internal IEnumerable<Step> StepsBlocked => _allSteps.Values.Where(s => s.HasDependencies);
            internal IEnumerable<Step> StepsCompleted => _allSteps.Values.Where(s => s.IsCompleted);
            internal bool Finished => StepsCompleted.Count() == _allSteps.Count;

            internal TaskPool(string[] input)
            {
                for (var c = 'A'; c <= 'Z'; c++) { _allSteps.Add(c, new Step(c)); }
            
                foreach (var line in Input)
                {
                    var split = line.Split(' ');
                    _allSteps[split[7][0]].AddDependency(_allSteps[split[1][0]]);
                }
            }

            internal Step Part1FinishStep()
            {
                return FinishStep(StepsUnblocked.First());
            }

            internal Step FinishStep(Step currentStep)
            {
                foreach (var blockedStep in StepsBlocked)
                {
                    if (blockedStep.HasDependencyOn(currentStep))
                    {
                        blockedStep.RemoveDependency(currentStep);
                    }
                }

                currentStep.MarkCompleted();
                return currentStep;
            }

            internal void AssignNextAvailableTask(Worker worker)
            {
                var step = StepsUnblocked.First();
                step.InProgress = true;
                worker.NewTask(step);
            }
        }

        private class Step
        {
            internal char Id { get; }
            internal int MinutesNeeded { get; }
            internal bool IsCompleted { get; private set; }
            internal bool InProgress { get; set; }
            internal Worker AssignedWorker { get; private set; }
            internal Dictionary<char, Step> Dependencies { get; }
            internal bool HasDependencies => Dependencies.Count > 0;
            internal void AddDependency(Step step) => Dependencies.Add(step.Id, step);
            internal void RemoveDependency(Step step) => Dependencies.Remove(step.Id);
            internal bool HasDependencyOn(Step step) => Dependencies.ContainsKey(step.Id);

            internal Step(char id)
            {
                Id = id;
                MinutesNeeded = Id - 4;
                Dependencies = new Dictionary<char, Step>();
            }

            internal void LinkToWorker(Worker worker)
            {
                AssignedWorker = worker;
                InProgress = true;
            }

            internal void MarkCompleted()
            {
                if (AssignedWorker != null) { AssignedWorker = null; }
                InProgress = false;
                IsCompleted = true;
            }
        }

        private class Worker
        {
            internal int Id { get; }
            internal Step CurrentTask { get; private set; }
            internal int MinutesLeft { get; private set; }
            internal void ClearTask() => CurrentTask = null;

            internal Worker(int id)
            {
                Id = id;
            }

            internal void NewTask(Step step)
            {
                CurrentTask = step;
                MinutesLeft = step.MinutesNeeded;
            }

            internal void ElapseMinutes(int minutesPassed)
            {
                var evaluatedMinutes = MinutesLeft - minutesPassed;
                if (evaluatedMinutes < 0)
                {
                    throw new Exception($"Worker minutes underflowed to {evaluatedMinutes}");
                }
                MinutesLeft = evaluatedMinutes;
            }
        }
    }
}