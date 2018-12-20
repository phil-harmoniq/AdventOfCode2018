using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Puzzles
{
    public class Puzzle04
    {
        public static readonly string[] Input = File.ReadAllLines(Path.Combine("Inputs", "Input04.txt"));

        public static int Part1()
        {
            var guards = new Dictionary<int, Guard>();
            var events = Input.Select(x => Event.Parse(x)).OrderBy(x => x.TimeStamp);

            CalculateEvents(events, guards);

            var sleepiestGuard = guards.Values.OrderByDescending(g => g.TotalMinutesSlept).First();
            var highScore = sleepiestGuard.SpecificMinutesSlept.OrderByDescending(x => x.Value);
            return sleepiestGuard.Id * highScore.First().Key;
        }

        public static int Part2()
        {
            var guards = new Dictionary<int, Guard>();
            var events = Input.Select(x => Event.Parse(x)).OrderBy(x => x.TimeStamp);

            CalculateEvents(events, guards);

            foreach (var guard in guards)
            {
                if (guard.Value.TotalMinutesSlept > 0)
                {
                    var mostSlept = guard.Value.SpecificMinutesSlept.OrderByDescending(x => x.Value).First();
                    guard.Value.MostSleptDay = mostSlept.Key;
                    guard.Value.HoursInMostSleptDay = mostSlept.Value;
                }
            }

            var sleepiestGuard = guards.OrderByDescending(guard => guard.Value.HoursInMostSleptDay).First().Value;
            return sleepiestGuard.Id * sleepiestGuard.MostSleptDay;
        }

        private static void CalculateEvents(IEnumerable<Event> events, IDictionary<int, Guard> guards)
        {
            var currentGuard = new Guard(-1);

            foreach (var @event in events)
            {
                switch (@event.EventType)
                {
                    case EventType.ShiftBegin:
                        var id = @event.GuardId;
                        if (!guards.Keys.Contains(id)) { guards.Add(id, new Guard(id)); }
                        currentGuard = guards.Single(g => g.Value.Id == id).Value;
                        break;
                    case EventType.FallAsleep:
                        currentGuard.IsAsleep = true;
                        currentGuard.FellAsleepAt = @event.TimeStamp;
                        break;
                    case EventType.WakeUp:
                        var timeStamp = @event.TimeStamp.Minute;
                        var fellAsleepAt = currentGuard.FellAsleepAt.Minute;
                        currentGuard.TotalMinutesSlept += timeStamp - fellAsleepAt;
                        for (var m = fellAsleepAt; m < timeStamp; m++)
                        {
                            if (!currentGuard.SpecificMinutesSlept.Keys.Contains(m))
                            {
                                currentGuard.SpecificMinutesSlept.Add(m, 0);
                            }
                            currentGuard.SpecificMinutesSlept[m]++;
                        }
                        currentGuard.IsAsleep = false;
                        break;
                }
            }
        }

        private class Event
        {
            internal DateTime TimeStamp { get; private set; }
            internal int GuardId { get; private set; }
            internal EventType EventType { get; set; }

            internal static Event Parse(string eventString)
            {
                var splitString = eventString.Split(new string[] { "] " }, StringSplitOptions.RemoveEmptyEntries);
                int guardId = -1;
                EventType eventType;

                if (splitString[1].Contains("begins shift"))
                {
                    eventType = EventType.ShiftBegin;
                    var split = splitString[1].Split(new char[] { ' ', '#' }, StringSplitOptions.RemoveEmptyEntries);
                    guardId = int.Parse(split[1]);
                }
                else if (splitString[1].Contains("wakes up")) { eventType = EventType.WakeUp; }
                else if (splitString[1].Contains("falls asleep")) { eventType = EventType.FallAsleep; }
                else { throw new Exception($"Invalid event string {splitString[1]}"); }

                return new Event
                {
                    TimeStamp = DateTime.Parse(splitString[0].Substring(1)),
                    EventType = (EventType)eventType,
                    GuardId = guardId
                };
            }
        }

        private enum EventType
        {
            ShiftBegin,
            FallAsleep,
            WakeUp
        }

        private class Guard
        {
            internal int Id { get; }
            internal Dictionary<int, int> SpecificMinutesSlept { get; }
            internal int TotalMinutesSlept { get; set; }
            internal bool IsAsleep { get; set; }
            internal DateTime FellAsleepAt { get; set; }
            internal int MostSleptDay { get; set; }
            internal int HoursInMostSleptDay { get; set; }

            internal Guard(int id)
            {
                Id = id;
                SpecificMinutesSlept = new Dictionary<int, int>();
            }
        }
    }
}