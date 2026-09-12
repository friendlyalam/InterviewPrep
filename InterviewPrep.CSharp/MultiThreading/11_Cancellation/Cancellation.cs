using InterviewPrep.CSharp.MultiThreading._11_Cancellation;

namespace InterviewPrep.CSharp.MultiThreading._11_Cancellation
{
    public static class Cancellation
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("CANCELLATION TOKEN DEMONSTRATION");
            Console.WriteLine("========================================");

            await DemonstrateBasicCancellationAsync();

            Console.WriteLine();

            await DemonstrateThrowIfCancellationRequestedAsync();

            Console.WriteLine();

            await DemonstrateTaskDelayCancellationAsync();

            Console.WriteLine();

            await DemonstrateCancellationPropagationAsync();

            Console.WriteLine();

            await DemonstrateCancelAfterAsync();

            Console.WriteLine();

            await DemonstrateLinkedCancellationAsync();

            Console.WriteLine();

            await DemonstrateCpuBoundCancellationAsync();

            Console.WriteLine();

            await DemonstrateWhenAnyWithCancellationAsync();

            Console.WriteLine();

            DemonstrateCancellationRegistration();

            Console.WriteLine();

            Console.WriteLine(
                "All CancellationToken demonstrations completed.");
        }

        private static async Task DemonstrateBasicCancellationAsync()
        {
            Console.WriteLine("1. BASIC CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task task = ProcessAsync(
                cts.Token);

            await Task.Delay(1000);

            Console.WriteLine(
                "Requesting cancellation...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Operation cancelled successfully.");
            }
        }

        private static async Task DemonstrateThrowIfCancellationRequestedAsync()
        {
            Console.WriteLine("2. THROWIFCANCELLATIONREQUESTED");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task task = Task.Run(
                () => ProcessWithExplicitCancellation(
                    cts.Token));

            await Task.Delay(700);

            Console.WriteLine(
                "Requesting cancellation...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "OperationCanceledException received.");
            }
        }

        private static void ProcessWithExplicitCancellation(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 20; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine(
                    $"Processing item {i}");

                Thread.Sleep(200);
            }
        }

        private static async Task DemonstrateTaskDelayCancellationAsync()
        {
            Console.WriteLine("3. TASK.DELAY WITH CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task task = Task.Delay(
                TimeSpan.FromSeconds(5),
                cts.Token);

            await Task.Delay(1000);

            Console.WriteLine(
                "Cancelling the delay...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Task.Delay was cancelled.");
            }
        }

        private static async Task DemonstrateCancellationPropagationAsync()
        {
            Console.WriteLine("4. CANCELLATION PROPAGATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task task = GetOrderAsync(
                101,
                cts.Token);

            await Task.Delay(1200);

            Console.WriteLine(
                "Cancelling request...");

            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Cancellation propagated through all layers.");
            }
        }

        private static async Task<string> GetOrderAsync(
            int orderId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Service: Getting order {orderId}");

            return await GetOrderFromRepositoryAsync(
                orderId,
                cancellationToken);
        }

        private static async Task<string> GetOrderFromRepositoryAsync(
            int orderId,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(
                $"Repository: Loading order {orderId}");

            await Task.Delay(
                3000,
                cancellationToken);

            return $"Order {orderId}";
        }

        private static async Task DemonstrateCancelAfterAsync()
        {
            Console.WriteLine("5. CANCELAFTER");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            cts.CancelAfter(
                TimeSpan.FromSeconds(2));

            try
            {
                await LongRunningOperationAsync(
                    cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Operation cancelled automatically after timeout.");
            }
        }

        private static async Task LongRunningOperationAsync(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine(
                    $"Long-running operation: step {i}");

                await Task.Delay(
                    500,
                    cancellationToken);
            }
        }

        private static async Task DemonstrateLinkedCancellationAsync()
        {
            Console.WriteLine("6. LINKED CANCELLATION TOKENS");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource requestCts = new();

            using CancellationTokenSource shutdownCts = new();

            using CancellationTokenSource linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(
                    requestCts.Token,
                    shutdownCts.Token);

            Task task = ProcessWithLinkedTokenAsync(
                linkedCts.Token);

            await Task.Delay(1000);

            Console.WriteLine(
                "Request cancellation triggered.");

            requestCts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Linked token detected cancellation.");
            }
        }

        private static async Task ProcessWithLinkedTokenAsync(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine(
                    $"Linked operation step {i}");

                await Task.Delay(
                    400,
                    cancellationToken);
            }
        }

        private static async Task DemonstrateCpuBoundCancellationAsync()
        {
            Console.WriteLine("7. CPU-BOUND CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task<long> task = Task.Run(
                () => CalculateWithCancellation(
                    cts.Token),
                cts.Token);

            await Task.Delay(500);

            Console.WriteLine(
                "Requesting CPU operation cancellation...");

            cts.Cancel();

            try
            {
                long result = await task;

                Console.WriteLine(
                    $"Calculation result: {result}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "CPU-bound operation was cancelled.");
            }
        }

        private static long CalculateWithCancellation(
            CancellationToken cancellationToken)
        {
            long result = 0;

            for (int i = 1; i <= 100_000_000; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                result += i % 10;
            }

            return result;
        }

        private static async Task DemonstrateWhenAnyWithCancellationAsync()
        {
            Console.WriteLine("8. WHENANY + CANCELLATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            Task serverA = SimulateServerAsync(
                "Server A",
                3000,
                cts.Token);

            Task serverB = SimulateServerAsync(
                "Server B",
                1000,
                cts.Token);

            Task serverC = SimulateServerAsync(
                "Server C",
                4000,
                cts.Token);

            Task firstCompleted = await Task.WhenAny(
                serverA,
                serverB,
                serverC);

            Console.WriteLine(
                "First server operation completed.");

            if (firstCompleted == serverA)
            {
                Console.WriteLine("Server A completed first.");
            }
            else if (firstCompleted == serverB)
            {
                Console.WriteLine("Server B completed first.");
            }
            else
            {
                Console.WriteLine("Server C completed first.");
            }

            Console.WriteLine(
                "Cancelling remaining operations...");

            cts.Cancel();

            try
            {
                await Task.WhenAll(
                    serverA,
                    serverB,
                    serverC);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Remaining operations were cancelled.");
            }
        }

        private static async Task SimulateServerAsync(
            string serverName,
            int delayMilliseconds,
            CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine(
                    $"{serverName} started.");

                await Task.Delay(
                    delayMilliseconds,
                    cancellationToken);

                Console.WriteLine(
                    $"{serverName} completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    $"{serverName} cancelled.");

                throw;
            }
        }

        private static void DemonstrateCancellationRegistration()
        {
            Console.WriteLine("9. CANCELLATION REGISTRATION");
            Console.WriteLine("----------------------------------------");

            using CancellationTokenSource cts = new();

            using CancellationTokenRegistration registration =
                cts.Token.Register(
                    () => Console.WriteLine(
                        "Cancellation callback executed."));

            Console.WriteLine(
                "Requesting cancellation...");

            cts.Cancel();

            Console.WriteLine(
                "Cancellation request completed.");
        }

        private static async Task ProcessAsync(
            CancellationToken cancellationToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Console.WriteLine(
                    $"Processing item {i}");

                await Task.Delay(
                    400,
                    cancellationToken);
            }
        }
    }
}
//Call it with:

//await Cancellation.RunAsync();