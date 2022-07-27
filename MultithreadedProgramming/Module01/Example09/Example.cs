namespace MultithreadedProgramming.Module01.Example09
{
    /// <summary>
    /// Example of the Producer / Consumer challenge.
    /// </summary>
    internal static class Example
    {
        internal static void Init()
        {
            var countProducers = 5;
            var countConsumers = 5;

            var buffer = new Buffer();

            for (var index = 0; index < countProducers; index++)
            {
                new Thread(() => Produce(buffer)).Start();
            }

            for (var index = 0; index < countConsumers; index++)
            {
                new Thread(() => Consume(buffer)).Start();
            }
        }

        private static void Produce(Buffer buffer)
        {
            while (true)
            {
                buffer.Produce(Random.Shared.Next(10));
                buffer.Show();
                Thread.Sleep(1000);
            }
        }

        private static void Consume(Buffer buffer)
        {
            while (true)
            {
                buffer.Consume();
                buffer.Show();
                Thread.Sleep(1000);
            }
        }

        private class Buffer
        {
            private readonly int _max = 10;

            private readonly object _sync = new();

            private readonly Queue<int> _list = new();

            public void Produce(int number)
            {
                lock (_sync)
                {
                    while (_list.Count == _max)
                    {
                        Monitor.Wait(_sync);
                    }

                    _list.Enqueue(number);

                    Monitor.PulseAll(_sync);
                }
            }

            public int Consume()
            {
                lock (_sync)
                {
                    while (_list.Count == 0)
                    {
                        Monitor.Wait(_sync);
                    }

                    var number = _list.Dequeue();

                    Monitor.PulseAll(_sync);

                    return number;
                }
            }

            public void Show()
            {
                lock (_sync)
                {
                    Console.WriteLine(
                        "-> {0}",
                        string.Join(", ", _list));
                }
            }
        }
    }
}
