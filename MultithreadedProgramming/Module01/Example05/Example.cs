namespace MultithreadedProgramming.Module01.Example05
{
    /// <summary>
    /// Example of the Mutex class that provides a mechanism used for interprocess synchronization.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            var mutex = new Mutex(false, "AppMutex");

            var acquired = mutex.WaitOne(1000);

            if (!acquired)
            {
                Console.WriteLine("App is already running.");

                return;
            }

            Console.WriteLine("App is running");

            Console.ReadLine();

            mutex.ReleaseMutex();
        }
    }
}
