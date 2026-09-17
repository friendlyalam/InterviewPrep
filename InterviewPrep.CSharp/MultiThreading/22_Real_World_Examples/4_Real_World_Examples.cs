using System.Collections.Concurrent;
using System.Threading.Channels;

namespace InterviewPrep.CSharp.MultiThreading._22_Real_World_Examples
{
    public static class Real_World_Examples
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("REAL-WORLD MULTITHREADING EXAMPLES");
            Console.WriteLine("========================================");

            await DemonstrateParallelApiCallsAsync();

            Console.WriteLine();

            await DemonstrateOrderProcessingAsync();

            Console.WriteLine();

            await DemonstrateInventoryConcurrencyAsync();

            Console.WriteLine();

            await DemonstrateIdempotentPaymentAsync();

            Console.WriteLine();

            await DemonstrateProducerConsumerAsync();

            Console.WriteLine();

            Console.WriteLine("All real-world demonstrations completed.");
        }

        // ============================================================
        // 1. PARALLEL API CALLS
        // ============================================================

        private static async Task DemonstrateParallelApiCallsAsync()
        {
            Console.WriteLine("1. PARALLEL API CALLS");
            Console.WriteLine("----------------------------------------");

            StopwatchTimer timer = new();

            Task<Customer> customerTask =
                GetCustomerAsync(1001);

            Task<Order[]> ordersTask =
                GetOrdersAsync(1001);

            Task<Product[]> recommendationsTask =
                GetRecommendationsAsync(1001);

            await Task.WhenAll(
                customerTask,
                ordersTask,
                recommendationsTask);

            Customer customer = await customerTask;
            Order[] orders = await ordersTask;
            Product[] recommendations = await recommendationsTask;

            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Orders: {orders.Length}");
            Console.WriteLine($"Recommendations: {recommendations.Length}");
            Console.WriteLine($"Elapsed: {timer.ElapsedMilliseconds} ms");

            Console.WriteLine(
                "Independent I/O operations were started concurrently.");
        }

        private static async Task<Customer> GetCustomerAsync(
            int customerId)
        {
            await Task.Delay(500);

            return new Customer(
                customerId,
                "John");
        }

        private static async Task<Order[]> GetOrdersAsync(
            int customerId)
        {
            await Task.Delay(800);

            return
            [
                new Order(1001, customerId, 500),
            new Order(1002, customerId, 750)
            ];
        }

        private static async Task<Product[]> GetRecommendationsAsync(
            int customerId)
        {
            await Task.Delay(300);

            return
            [
                new Product(501, "Laptop"),
            new Product(502, "Monitor")
            ];
        }

        // ============================================================
        // 2. ORDER PROCESSING
        // ============================================================

        private static async Task DemonstrateOrderProcessingAsync()
        {
            Console.WriteLine("2. ORDER PROCESSING");
            Console.WriteLine("----------------------------------------");

            OrderService orderService = new();

            OrderProcessingResult result =
                await orderService.ProcessOrderAsync(
                    new CreateOrderRequest(
                        OrderId: 2001,
                        CustomerId: 1001,
                        ProductId: 501,
                        Quantity: 2,
                        Amount: 1000),
                    CancellationToken.None);

            Console.WriteLine($"Order ID: {result.OrderId}");
            Console.WriteLine($"Status: {result.Status}");
            Console.WriteLine($"Payment: {result.PaymentStatus}");
            Console.WriteLine($"Inventory: {result.InventoryStatus}");

            Console.WriteLine(
                "Order processing demonstrates validation, " +
                "inventory reservation, payment, and final confirmation.");
        }

        private sealed class OrderService
        {
            private readonly InventoryService _inventoryService = new();
            private readonly PaymentService _paymentService = new();

            public async Task<OrderProcessingResult> ProcessOrderAsync(
                CreateOrderRequest request,
                CancellationToken cancellationToken)
            {
                ValidateOrder(request);

                InventoryReservationResult inventoryResult =
                    await _inventoryService.ReserveAsync(
                        request.ProductId,
                        request.Quantity,
                        cancellationToken);

                if (!inventoryResult.Success)
                {
                    return new OrderProcessingResult(
                        request.OrderId,
                        OrderStatus.Cancelled,
                        PaymentStatus.NotStarted,
                        InventoryStatus.Failed);
                }

                PaymentResult paymentResult;

                try
                {
                    paymentResult =
                        await _paymentService.ProcessPaymentAsync(
                            request.OrderId,
                            request.Amount,
                            cancellationToken);
                }
                catch
                {
                    await _inventoryService.ReleaseAsync(
                        request.ProductId,
                        request.Quantity);

                    throw;
                }

                if (!paymentResult.Success)
                {
                    await _inventoryService.ReleaseAsync(
                        request.ProductId,
                        request.Quantity);

                    return new OrderProcessingResult(
                        request.OrderId,
                        OrderStatus.Cancelled,
                        PaymentStatus.Failed,
                        InventoryStatus.Released);
                }

                return new OrderProcessingResult(
                    request.OrderId,
                    OrderStatus.Confirmed,
                    PaymentStatus.Paid,
                    InventoryStatus.Reserved);
            }

            private static void ValidateOrder(
                CreateOrderRequest request)
            {
                if (request.OrderId <= 0)
                {
                    throw new ArgumentException(
                        "Order ID must be greater than zero.",
                        nameof(request));
                }

                if (request.CustomerId <= 0)
                {
                    throw new ArgumentException(
                        "Customer ID must be greater than zero.",
                        nameof(request));
                }

                if (request.ProductId <= 0)
                {
                    throw new ArgumentException(
                        "Product ID must be greater than zero.",
                        nameof(request));
                }

                if (request.Quantity <= 0)
                {
                    throw new ArgumentException(
                        "Quantity must be greater than zero.",
                        nameof(request));
                }

                if (request.Amount <= 0)
                {
                    throw new ArgumentException(
                        "Amount must be greater than zero.",
                        nameof(request));
                }
            }
        }

        private sealed class InventoryService
        {
            private readonly ConcurrentDictionary<int, int> _inventory =
                new(
                    new Dictionary<int, int>
                    {
                        [501] = 10
                    });

            private readonly object _sync = new();

            public Task<InventoryReservationResult> ReserveAsync(
                int productId,
                int quantity,
                CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();

                lock (_sync)
                {
                    if (!_inventory.TryGetValue(
                            productId,
                            out int availableQuantity))
                    {
                        return Task.FromResult(
                            new InventoryReservationResult(false));
                    }

                    if (availableQuantity < quantity)
                    {
                        return Task.FromResult(
                            new InventoryReservationResult(false));
                    }

                    _inventory[productId] =
                        availableQuantity - quantity;

                    Console.WriteLine(
                        $"Inventory reserved: Product={productId}, " +
                        $"Quantity={quantity}, " +
                        $"Remaining={_inventory[productId]}");

                    return Task.FromResult(
                        new InventoryReservationResult(true));
                }
            }

            public Task ReleaseAsync(
                int productId,
                int quantity)
            {
                lock (_sync)
                {
                    _inventory.AddOrUpdate(
                        productId,
                        quantity,
                        (_, currentQuantity) =>
                            currentQuantity + quantity);

                    Console.WriteLine(
                        $"Inventory released: Product={productId}, " +
                        $"Quantity={quantity}");
                }

                return Task.CompletedTask;
            }
        }

        private sealed class PaymentService
        {
            public async Task<PaymentResult> ProcessPaymentAsync(
                int orderId,
                decimal amount,
                CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(500, cancellationToken);

                Console.WriteLine(
                    $"Payment processed: Order={orderId}, Amount={amount}");

                return new PaymentResult(
                    Success: true);
            }
        }

        // ============================================================
        // 3. INVENTORY CONCURRENCY
        // ============================================================

        private static async Task DemonstrateInventoryConcurrencyAsync()
        {
            Console.WriteLine("3. INVENTORY CONCURRENCY");
            Console.WriteLine("----------------------------------------");

            InventoryCounter inventory =
                new(initialQuantity: 5);

            Task[] purchaseTasks =
            [
                PurchaseAsync(inventory, 1, "Customer A"),
            PurchaseAsync(inventory, 1, "Customer B"),
            PurchaseAsync(inventory, 1, "Customer C"),
            PurchaseAsync(inventory, 1, "Customer D"),
            PurchaseAsync(inventory, 1, "Customer E"),
            PurchaseAsync(inventory, 1, "Customer F"),
            PurchaseAsync(inventory, 1, "Customer G")
            ];

            await Task.WhenAll(purchaseTasks);

            Console.WriteLine(
                $"Final inventory: {inventory.AvailableQuantity}");

            Console.WriteLine(
                "The atomic reservation prevents inventory from becoming negative.");
        }

        private static async Task PurchaseAsync(
            InventoryCounter inventory,
            int quantity,
            string customerName)
        {
            await Task.Delay(Random.Shared.Next(50, 150));

            bool success =
                inventory.TryReserve(quantity);

            Console.WriteLine(
                $"{customerName}: " +
                $"{(success ? "Purchase successful" : "Out of stock")}");
        }

        private sealed class InventoryCounter
        {
            private int _availableQuantity;

            public InventoryCounter(int initialQuantity)
            {
                if (initialQuantity < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(initialQuantity));
                }

                _availableQuantity = initialQuantity;
            }

            public int AvailableQuantity =>
                Volatile.Read(ref _availableQuantity);

            public bool TryReserve(int quantity)
            {
                if (quantity <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(quantity));
                }

                while (true)
                {
                    int current =
                        Volatile.Read(ref _availableQuantity);

                    if (current < quantity)
                    {
                        return false;
                    }

                    int updated =
                        current - quantity;

                    int original =
                        Interlocked.CompareExchange(
                            ref _availableQuantity,
                            updated,
                            current);

                    if (original == current)
                    {
                        return true;
                    }
                }
            }
        }

        // ============================================================
        // 4. IDEMPOTENT PAYMENT
        // ============================================================

        private static async Task DemonstrateIdempotentPaymentAsync()
        {
            Console.WriteLine("4. IDEMPOTENT PAYMENT");
            Console.WriteLine("----------------------------------------");

            IdempotentPaymentService paymentService =
                new();

            const string idempotencyKey =
                "PAYMENT-ORDER-3001";

            Task<PaymentResult> firstRequest =
                paymentService.ProcessAsync(
                    idempotencyKey,
                    500,
                    CancellationToken.None);

            Task<PaymentResult> retryRequest =
                paymentService.ProcessAsync(
                    idempotencyKey,
                    500,
                    CancellationToken.None);

            PaymentResult[] results =
                await Task.WhenAll(
                    firstRequest,
                    retryRequest);

            Console.WriteLine(
                $"First result:  Payment ID={results[0].PaymentId}");

            Console.WriteLine(
                $"Retry result:  Payment ID={results[1].PaymentId}");

            Console.WriteLine(
                $"Payments created: {paymentService.PaymentCount}");

            Console.WriteLine(
                "The same idempotency key produced one payment.");
        }

        private sealed class IdempotentPaymentService
        {
            private readonly ConcurrentDictionary<
                string,
                Lazy<Task<PaymentResult>>> _operations =
                new();

            private int _paymentCount;

            public int PaymentCount =>
                Volatile.Read(ref _paymentCount);

            public Task<PaymentResult> ProcessAsync(
                string idempotencyKey,
                decimal amount,
                CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(idempotencyKey))
                {
                    throw new ArgumentException(
                        "Idempotency key is required.",
                        nameof(idempotencyKey));
                }

                if (amount <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(amount));
                }

                Lazy<Task<PaymentResult>> operation =
                    _operations.GetOrAdd(
                        idempotencyKey,
                        _ => new Lazy<Task<PaymentResult>>(
                            () => ProcessPaymentAsync(
                                amount,
                                cancellationToken),
                            LazyThreadSafetyMode.ExecutionAndPublication));

                return operation.Value;
            }

            private async Task<PaymentResult> ProcessPaymentAsync(
                decimal amount,
                CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(
                    500,
                    cancellationToken);

                int paymentNumber =
                    Interlocked.Increment(
                        ref _paymentCount);

                string paymentId =
                    $"PAY-{paymentNumber:0000}";

                Console.WriteLine(
                    $"Payment created: {paymentId}, Amount={amount}");

                return new PaymentResult(
                    Success: true,
                    PaymentId: paymentId);
            }
        }

        // ============================================================
        // 5. PRODUCER-CONSUMER WITH CHANNEL
        // ============================================================

        private static async Task DemonstrateProducerConsumerAsync()
        {
            Console.WriteLine("5. PRODUCER-CONSUMER");
            Console.WriteLine("----------------------------------------");

            Channel<OrderWorkItem> channel =
                Channel.CreateBounded<OrderWorkItem>(
                    new BoundedChannelOptions(3)
                    {
                        FullMode = BoundedChannelFullMode.Wait,
                        SingleWriter = true,
                        SingleReader = false
                    });

            Task producer =
                ProduceOrdersAsync(
                    channel.Writer,
                    CancellationToken.None);

            Task consumer1 =
                ConsumeOrdersAsync(
                    "Consumer 1",
                    channel.Reader,
                    CancellationToken.None);

            Task consumer2 =
                ConsumeOrdersAsync(
                    "Consumer 2",
                    channel.Reader,
                    CancellationToken.None);

            await producer;

            channel.Writer.TryComplete();

            await Task.WhenAll(
                consumer1,
                consumer2);

            Console.WriteLine(
                "Producer-consumer processing completed.");
        }

        private static async Task ProduceOrdersAsync(
            ChannelWriter<OrderWorkItem> writer,
            CancellationToken cancellationToken)
        {
            for (int orderId = 1; orderId <= 10; orderId++)
            {
                OrderWorkItem workItem =
                    new(orderId);

                await writer.WriteAsync(
                    workItem,
                    cancellationToken);

                Console.WriteLine(
                    $"Produced Order {orderId}");

                await Task.Delay(
                    50,
                    cancellationToken);
            }
        }

        private static async Task ConsumeOrdersAsync(
            string consumerName,
            ChannelReader<OrderWorkItem> reader,
            CancellationToken cancellationToken)
        {
            await foreach (OrderWorkItem item
                in reader.ReadAllAsync(cancellationToken))
            {
                Console.WriteLine(
                    $"{consumerName} processing Order {item.OrderId}");

                await Task.Delay(
                    200,
                    cancellationToken);

                Console.WriteLine(
                    $"{consumerName} completed Order {item.OrderId}");
            }
        }

        // ============================================================
        // SUPPORTING MODELS
        // ============================================================

        private sealed record Customer(
            int Id,
            string Name);

        private sealed record Order(
            int Id,
            int CustomerId,
            decimal Amount);

        private sealed record Product(
            int Id,
            string Name);

        private sealed record CreateOrderRequest(
            int OrderId,
            int CustomerId,
            int ProductId,
            int Quantity,
            decimal Amount);

        private sealed record OrderProcessingResult(
            int OrderId,
            OrderStatus Status,
            PaymentStatus PaymentStatus,
            InventoryStatus InventoryStatus);

        private sealed record InventoryReservationResult(
            bool Success);

        private sealed record PaymentResult(
            bool Success,
            string PaymentId = "");

        private sealed record OrderWorkItem(
            int OrderId);

        private enum OrderStatus
        {
            Pending,
            Confirmed,
            Cancelled
        }

        private enum PaymentStatus
        {
            NotStarted,
            Paid,
            Failed
        }

        private enum InventoryStatus
        {
            Reserved,
            Released,
            Failed
        }

        // ============================================================
        // SIMPLE TIMER
        // ============================================================

        private sealed class StopwatchTimer
        {
            private readonly System.Diagnostics.Stopwatch _stopwatch;

            public StopwatchTimer()
            {
                _stopwatch =
                    System.Diagnostics.Stopwatch.StartNew();
            }

            public long ElapsedMilliseconds =>
                _stopwatch.ElapsedMilliseconds;
        }
    }
}
