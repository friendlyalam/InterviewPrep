using System.Collections.Immutable;


namespace InterviewPrep.CSharp.MultiThreading._17_Immutability
{
    public static class Immutability_Demo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("IMMUTABILITY");
            Console.WriteLine("========================================");

            DemonstrateImmutableClass();

            Console.WriteLine();

            DemonstrateInitProperties();

            Console.WriteLine();

            DemonstrateRecord();

            Console.WriteLine();

            DemonstrateWithExpression();

            Console.WriteLine();

            DemonstrateImmutableCollection();

            Console.WriteLine();

            DemonstrateReadOnlyCollection();

            Console.WriteLine();

            await DemonstrateSafeSharingAsync();

            Console.WriteLine();

            Console.WriteLine("All Immutability demonstrations completed.");
        }

        private static void DemonstrateImmutableClass()
        {
            Console.WriteLine("1. IMMUTABLE CLASS");
            Console.WriteLine("----------------------------------------");

            Customer customer =
                new(
                    "John",
                    30);

            Console.WriteLine(
                $"Name: {customer.Name}");

            Console.WriteLine(
                $"Age: {customer.Age}");

            Console.WriteLine(
                "Customer state cannot be changed after creation.");
        }

        private static void DemonstrateInitProperties()
        {
            Console.WriteLine("2. INIT PROPERTIES");
            Console.WriteLine("----------------------------------------");

            CustomerWithInit customer = new()
            {
                Name = "David",
                Age = 35
            };

            Console.WriteLine(
                $"Name: {customer.Name}");

            Console.WriteLine(
                $"Age: {customer.Age}");

            Console.WriteLine(
                "init properties can be assigned during object initialization.");
        }

        private static void DemonstrateRecord()
        {
            Console.WriteLine("3. RECORD");
            Console.WriteLine("----------------------------------------");

            CustomerRecord customer =
                new(
                    "Alice",
                    28);

            Console.WriteLine(
                $"Name: {customer.Name}");

            Console.WriteLine(
                $"Age: {customer.Age}");

            Console.WriteLine(
                $"Hash Code: {customer.GetHashCode()}");
        }

        private static void DemonstrateWithExpression()
        {
            Console.WriteLine("4. WITH EXPRESSION");
            Console.WriteLine("----------------------------------------");

            CustomerRecord customer1 =
                new(
                    "John",
                    30);

            CustomerRecord customer2 =
                customer1 with
                {
                    Age = 31
                };

            Console.WriteLine(
                $"Original Age: {customer1.Age}");

            Console.WriteLine(
                $"New Age:      {customer2.Age}");

            Console.WriteLine(
                "The original record remains unchanged.");
        }

        private static void DemonstrateImmutableCollection()
        {
            Console.WriteLine("5. IMMUTABLE COLLECTION");
            Console.WriteLine("----------------------------------------");

            ImmutableList<int> numbers =
                ImmutableList.Create(
                    1,
                    2,
                    3);

            ImmutableList<int> updatedNumbers =
                numbers.Add(4);

            Console.WriteLine(
                "Original collection:");

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();

            Console.WriteLine(
                "New collection:");

            foreach (int number in updatedNumbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine();

            Console.WriteLine(
                "The original collection was not modified.");
        }

        private static void DemonstrateReadOnlyCollection()
        {
            Console.WriteLine("6. READONLY COLLECTION");
            Console.WriteLine("----------------------------------------");

            List<string> internalItems =
                new()
                {
                "Order",
                "Payment",
                "Inventory"
                };

            IReadOnlyList<string> items =
                internalItems;

            Console.WriteLine(
                "Items exposed as IReadOnlyList:");

            foreach (string item in items)
            {
                Console.WriteLine(item);
            }

            internalItems.Add("Shipping");

            Console.WriteLine();

            Console.WriteLine(
                $"Items after internal modification: {items.Count}");

            Console.WriteLine(
                "IReadOnlyList prevents mutation through the interface,");
            Console.WriteLine(
                "but the underlying collection can still change.");
        }

        private static async Task DemonstrateSafeSharingAsync()
        {
            Console.WriteLine("7. SAFE SHARING BETWEEN TASKS");
            Console.WriteLine("----------------------------------------");

            CustomerRecord customer =
                new(
                    "Michael",
                    40);

            Task task1 = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task 1 reading: {customer.Name}, {customer.Age}");
            });

            Task task2 = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task 2 reading: {customer.Name}, {customer.Age}");
            });

            Task task3 = Task.Run(() =>
            {
                Console.WriteLine(
                    $"Task 3 reading: {customer.Name}, {customer.Age}");
            });

            await Task.WhenAll(
                task1,
                task2,
                task3);

            Console.WriteLine(
                "The same immutable object can safely be shared for reading.");
        }

        private sealed class Customer
        {
            public string Name { get; }

            public int Age { get; }

            public Customer(
                string name,
                int age)
            {
                Name = name;
                Age = age;
            }
        }

        private sealed class CustomerWithInit
        {
            public string Name { get; init; } = string.Empty;

            public int Age { get; init; }
        }

        private record CustomerRecord(
            string Name,
            int Age);
    }
}
