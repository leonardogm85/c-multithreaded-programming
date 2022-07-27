namespace MultithreadedProgramming.Module01.Example03
{
    /// <summary>
    /// Example of the Monitor class that provides a mechanism that synchronizes access to objects.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            for (var test = 0; test < 10; test++)
            {
                var counter = new Counter();

                var threads = new List<Thread>();

                for (var index = 0; index < 10; index++)
                {
                    threads.Add(new Thread(() => Run(counter)));
                }

                threads.ForEach(thread => thread.Start());

                threads.ForEach(thread => thread.Join());

                Console.WriteLine(
                    "Counter: {0}",
                    counter.Value);
            }
        }

        private static void Run(Counter counter) => counter.Increment();

        private class Counter
        {
            private readonly object _sync = new();

            private int _value;

            public int Value => _value;

            public void Increment()
            {
                var lockTaken = false;

                try
                {
                    Monitor.Enter(_sync, ref lockTaken);
                    _value++;
                }
                finally
                {
                    if (lockTaken)
                    {
                        Monitor.Exit(_sync);
                    }
                }
            }
        }
    }
}
