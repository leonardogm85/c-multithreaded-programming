namespace MultithreadedProgramming.Module01.Example08
{
    /// <summary>
    /// Example of the AutoResetEvent that represents a thread synchronization event that, when signaled, must be reset manually.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            var gate = new Gate();

            for (var index = 0; index < 5; index++)
            {
                new Thread(() => Run(gate))
                {
                    Name = $"Thread #{index}"
                }.Start();
            }

            new Thread(() => Guard(gate)).Start();
        }

        private static void Run(Gate gate)
        {
            while (true)
            {
                gate.Pass(Thread.CurrentThread.Name!);
                Thread.Sleep(200);
            }
        }

        private static void Guard(Gate gate)
        {
            while (true)
            {
                gate.Open();
                Thread.Sleep(1000);

                gate.Close();
                Thread.Sleep(1000);
            }
        }

        private class Gate
        {
            private readonly ManualResetEventSlim _event = new(false);

            public void Open() => _event.Set();
            public void Close() => _event.Reset();

            public void Pass(string name)
            {
                _event.Wait();

                Console.WriteLine(
                    "{0} passed the gate.",
                    name);
            }
        }
    }
}
