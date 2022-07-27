using System.Timers;

namespace MultithreadedProgramming.Module01.Example12
{
    /// <summary>
    /// Example of the System.Timers.Timer class that provides a mechanism for executing a method on a thread pool thread at specified intervals.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            using var timer = new System.Timers.Timer(2000);

            timer.Elapsed += OnTime;

            timer.Start();

            Console.ReadLine();

            timer.Stop();
        }

        private static void OnTime(object? sender, ElapsedEventArgs args)
        {
            Console.WriteLine("Timer Triggered!");
        }
    }
}
