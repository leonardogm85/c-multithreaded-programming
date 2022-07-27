namespace MultithreadedProgramming.Module01.Example11
{
    /// <summary>
    /// Example of the System.Threading.Timer class that provides a mechanism for executing a method on a thread pool thread at specified intervals.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            using var timer = new Timer(
                OnTime,
                null,
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(1));

            Console.ReadLine();
        }

        private static void OnTime(object? param)
        {
            Console.WriteLine("Timer Triggered!");
        }
    }
}
