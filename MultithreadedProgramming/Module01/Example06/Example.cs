namespace MultithreadedProgramming.Module01.Example06
{
    /// <summary>
    /// Example of the SemaphoreSlim that limits the number of threads that can access a resource or pool of resources concurrently.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            var manager = new Manager();

            for (var index = 0; index < 10; index++)
            {
                new Thread(() => Run(manager))
                {
                    Name = $"Thread #{index}"
                }.Start();
            }
        }

        private static void Run(Manager manager)
        {
            for (var test = 0; test < 5; test++)
            {
                manager.Print(Thread.CurrentThread.Name!);
                Thread.Sleep(Random.Shared.Next(1000));
            }
        }

        private class Printer
        {
            public Printer(int id) => Id = id;

            public int Id { get; private set; }
            public bool IsUsed { get; private set; }

            public void Used() => IsUsed = true;
            public void Unused() => IsUsed = false;

            public override string ToString() => $"Printer {Id}";
        }

        private class Manager
        {
            private const int Count = 3;

            private readonly List<Printer> _printers = new(Count);
            private readonly SemaphoreSlim _semaphore = new(Count);

            public Manager()
            {
                for (var index = 0; index < Count; index++)
                {
                    _printers.Add(new(index));
                }
            }

            public void Print(string name)
            {
                _semaphore.Wait();

                try
                {
                    var printer = GetPrinter();

                    Console.WriteLine(
                        "Printer {0} started printing for {1}.",
                        printer,
                        name);

                    Thread.Sleep(Random.Shared.Next(1000));

                    Console.WriteLine(
                        "Printer {0} finished printing for {1}.",
                        printer,
                        name);

                    printer.Unused();
                }
                finally
                {
                    _semaphore.Release();
                }
            }

            private Printer GetPrinter()
            {
                lock (_printers)
                {
                    var printer = _printers.First(printer => !printer.IsUsed);

                    printer.Used();

                    return printer;
                }
            }
        }
    }
}
