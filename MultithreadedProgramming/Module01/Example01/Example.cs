namespace MultithreadedProgramming.Module01.Example01
{
    /// <summary>
    /// Example of the Thread class that allows parallel processing.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            Thread.CurrentThread.Name = "Main";

            var threadA = new Thread(Run)
            {
                Name = "Thread 01"
            };
            var threadB = new Thread(Run)
            {
                Name = "Thread 02"
            };

            threadA.Start("A");
            threadB.Start("B");

            threadA.Join();
            threadB.Join();

            Run("C");
        }

        private static void Run(object? parameter)
        {
            for (var index = 0; index < 10; index++)
            {
                Console.WriteLine(
                    "{0:00}: Name {1,-10} [Parameter {2}]",
                    index,
                    Thread.CurrentThread.Name,
                    parameter);
                Thread.Sleep(Random.Shared.Next(200));
            }
        }
    }
}
