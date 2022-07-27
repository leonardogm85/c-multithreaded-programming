namespace MultithreadedProgramming.Module02.Example01
{
    /// <summary>
    /// Example of the breakfast challenge.
    /// </summary>
    internal static class Example
    {
        internal static async Task InitAsync()
        {
            var coffee = PourCoffee();
            Console.WriteLine("Coffee is ready!");

            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task>
            {
                eggsTask,
                baconTask,
                toastTask
            };

            while (breakfastTasks.Any())
            {
                var finishedTask = await Task.WhenAny(breakfastTasks);

                if (finishedTask == eggsTask)
                {
                    Console.WriteLine("Eggs are ready!");
                }
                else if (finishedTask == baconTask)
                {
                    Console.WriteLine("Bacon is ready!");
                }
                else if (finishedTask == toastTask)
                {
                    Console.WriteLine("Toast is ready!");
                }

                breakfastTasks.Remove(finishedTask);
            }

            var oj = PourOJ();
            Console.WriteLine("OJ is ready!");

            Console.WriteLine("Breakfast is ready!");
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee.");
            return new Coffee();
        }

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice.");
            return new Juice();
        }

        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(3000);

            Console.WriteLine("Cracking {0} eggs.", howMany);

            Console.WriteLine("Cooking the eggs...");
            await Task.Delay(3000);

            Console.WriteLine("Put eggs on plate.");
            return new Egg();
        }

        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine("Putting {0} slices of bacon in the pan.", slices);

            Console.WriteLine("Cooking first side of bacon...");
            await Task.Delay(3000);

            for (var slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Flipping a slice of bacon.");
            }
            Console.WriteLine("Cooking the second side of bacon...");
            await Task.Delay(3000);

            Console.WriteLine("Put bacon on plate.");
            return new Bacon();
        }

        private static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);

            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        private static async Task<Toast> ToastBreadAsync(int slices)
        {
            for (var slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster.");
            }

            Console.WriteLine("Start toasting...");
            await Task.Delay(3000);

            Console.WriteLine("Remove toast from toaster.");
            return new Toast();
        }

        private static void ApplyButter(Toast toast) => Console.WriteLine("Putting butter on the toast.");

        private static void ApplyJam(Toast toast) => Console.WriteLine("Putting jam on the toast.");

        private class Bacon
        {
        }

        private class Coffee
        {
        }

        private class Egg
        {
        }

        private class Juice
        {
        }

        private class Toast
        {
        }
    }
}
