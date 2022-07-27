using System.Diagnostics;

namespace MultithreadedProgramming.Module01.Example10
{
    /// <summary>
    /// Example of the frog race challenge.
    /// </summary>
    internal static class Example
    {
        internal static void Init() => new Race(500, 10).Init();

        private class Race
        {
            private readonly CountdownEvent countdown;
            private readonly Stopwatch stopwatch;

            public int MaxDistance { get; private set; }
            public List<Frog> Frogs { get; private set; }
            public List<Frog> Ranking { get; private set; }

            public Race(int maxDistance, int numberFrog)
            {
                countdown = new(numberFrog);
                stopwatch = new();

                MaxDistance = maxDistance;
                Frogs = new(numberFrog);
                Ranking = new(numberFrog);

                for (var index = 0; index < numberFrog; index++)
                {
                    Frogs.Add(new(index + 1, HasArrived));
                }
            }

            private void HasArrived(Frog frog)
            {
                lock (Ranking)
                {
                    frog.SetTime(stopwatch.Elapsed);
                    Ranking.Add(frog);
                }
            }

            public void Init()
            {
                stopwatch.Start();

                foreach (var frog in Frogs)
                {
                    new Thread(() => frog.Jump(MaxDistance, countdown)).Start();
                }

                countdown.Wait();
                stopwatch.Stop();

                Console.WriteLine("The race is over. \n");

                for (var index = 0; index < Ranking.Count; index++)
                {
                    Console.WriteLine(
                        "{0}o. place \t Frog {1:00} \t {2:00}:{3:000}",
                        index + 1,
                        Ranking[index].Id,
                        Ranking[index].Time.Seconds,
                        Ranking[index].Time.Milliseconds);
                }
            }
        }

        private class Frog
        {
            public Frog(int id, Action<Frog> hasArrived)
            {
                Id = id;
                HasArrived = hasArrived;
            }

            public int Id { get; private set; }
            public Action<Frog> HasArrived { get; private set; }
            public int Distance { get; private set; }
            public TimeSpan Time { get; private set; }

            public void Jump(int maxDistance, CountdownEvent countdown)
            {
                while (true)
                {
                    Distance += Random.Shared.Next(60);

                    Console.WriteLine(
                        "Frog {0:00} has reached {1:000} cm.",
                        Id,
                        Distance);

                    if (Distance >= maxDistance)
                    {
                        HasArrived(this);

                        break;
                    }

                    Thread.Sleep(200);
                }

                countdown.Signal();
            }

            public void SetTime(TimeSpan time) => Time = time;
        }
    }
}
