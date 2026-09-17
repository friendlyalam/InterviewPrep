using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._16_Channel
{
    public static class Channel_Demo
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("CHANNEL<T> DEMONSTRATION");
            Console.WriteLine("========================================");

            await DemonstrateBasicChannelAsync();

            Console.WriteLine();

            await DemonstrateBoundedChannelAsync();

            Console.WriteLine();

            await DemonstrateMultipleConsumersAsync();

            Console.WriteLine();

            await DemonstrateTryWriteTryReadAsync();

            Console.WriteLine();

            await DemonstrateCancellationAsync();

            Console.WriteLine();

            Console.WriteLine("All Channel<T> demonstrations completed.");
        }

        private static async Task DemonstrateBasicChannelAsync()
        {
            Console.WriteLine("1. BASIC CHANNEL");
            Console.WriteLine("----------------------------------------");

            Channel<int> channel =
                Channel.CreateUnbounded<int>();

            Task producer = ProduceAsync(
                channel.Writer);

            Task consumer = ConsumeAsync(
                channel.Reader);

            await Task.WhenAll(
                producer,
                consumer);

            Console.WriteLine(
                "Basic channel processing completed.");
        }

        private static async Task ProduceAsync(
            ChannelWriter<int> writer)
        {
            for (int i = 1; i <= 5; i++)
            {
                await writer.WriteAsync(i);

                Console.WriteLine(
                    $"Produced: {i}");

                await Task.Delay(200);
            }

            writer.Complete();

            Console.WriteLine(
                "Producer completed.");
        }

        private static async Task ConsumeAsync(
            ChannelReader<int> reader)
        {
            await foreach (
                int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"Consumed: {item}");

                await Task.Delay(400);
            }

            Console.WriteLine(
                "Consumer completed.");
        }

        private static async Task DemonstrateBoundedChannelAsync()
        {
            Console.WriteLine("2. BOUNDED CHANNEL");
            Console.WriteLine("----------------------------------------");

            BoundedChannelOptions options =
                new(capacity: 2)
                {
                    FullMode = BoundedChannelFullMode.Wait
                };

            Channel<int> channel =
                Channel.CreateBounded<int>(options);

            Task producer = ProduceFastAsync(
                channel.Writer);

            Task consumer = ConsumeSlowlyAsync(
                channel.Reader);

            await Task.WhenAll(
                producer,
                consumer);

            Console.WriteLine(
                "Bounded channel completed.");
        }

        private static async Task ProduceFastAsync(
            ChannelWriter<int> writer)
        {
            for (int i = 1; i <= 6; i++)
            {
                Console.WriteLine(
                    $"Producer attempting to write: {i}");

                await writer.WriteAsync(i);

                Console.WriteLine(
                    $"Produced: {i}");
            }

            writer.Complete();
        }

        private static async Task ConsumeSlowlyAsync(
            ChannelReader<int> reader)
        {
            await foreach (
                int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"Consumed: {item}");

                await Task.Delay(700);
            }
        }

        private static async Task DemonstrateMultipleConsumersAsync()
        {
            Console.WriteLine("3. MULTIPLE CONSUMERS");
            Console.WriteLine("----------------------------------------");

            Channel<int> channel =
                Channel.CreateBounded<int>(5);

            Task producer = ProduceItemsAsync(
                channel.Writer);

            Task consumer1 = ConsumeWithWorkerAsync(
                "Consumer 1",
                channel.Reader);

            Task consumer2 = ConsumeWithWorkerAsync(
                "Consumer 2",
                channel.Reader);

            Task consumer3 = ConsumeWithWorkerAsync(
                "Consumer 3",
                channel.Reader);

            await Task.WhenAll(
                producer,
                consumer1,
                consumer2,
                consumer3);

            Console.WriteLine(
                "Multiple consumers completed.");
        }

        private static async Task ProduceItemsAsync(
            ChannelWriter<int> writer)
        {
            for (int i = 1; i <= 10; i++)
            {
                await writer.WriteAsync(i);

                Console.WriteLine(
                    $"Produced item: {i}");
            }

            writer.Complete();
        }

        private static async Task ConsumeWithWorkerAsync(
            string consumerName,
            ChannelReader<int> reader)
        {
            await foreach (
                int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"{consumerName} processing item {item}");

                await Task.Delay(300);
            }

            Console.WriteLine(
                $"{consumerName} completed.");
        }

        private static async Task DemonstrateTryWriteTryReadAsync()
        {
            Console.WriteLine("4. TRYWRITE / TRYREAD");
            Console.WriteLine("----------------------------------------");

            Channel<int> channel =
                Channel.CreateBounded<int>(2);

            bool firstWrite =
                channel.Writer.TryWrite(10);

            bool secondWrite =
                channel.Writer.TryWrite(20);

            bool thirdWrite =
                channel.Writer.TryWrite(30);

            Console.WriteLine(
                $"Write 10 successful: {firstWrite}");

            Console.WriteLine(
                $"Write 20 successful: {secondWrite}");

            Console.WriteLine(
                $"Write 30 successful: {thirdWrite}");

            if (channel.Reader.TryRead(
                out int firstValue))
            {
                Console.WriteLine(
                    $"Read: {firstValue}");
            }

            if (channel.Reader.TryRead(
                out int secondValue))
            {
                Console.WriteLine(
                    $"Read: {secondValue}");
            }

            channel.Writer.Complete();

            await Task.CompletedTask;
        }

        private static async Task DemonstrateCancellationAsync()
        {
            Console.WriteLine("5. CANCELLATION");
            Console.WriteLine("----------------------------------------");

            Channel<int> channel =
                Channel.CreateBounded<int>(5);

            using CancellationTokenSource cts =
                new();

            Task producer = ProduceWithCancellationAsync(
                channel.Writer,
                cts.Token);

            Task consumer = ConsumeWithCancellationAsync(
                channel.Reader,
                cts.Token);

            await Task.Delay(700);

            Console.WriteLine(
                "Requesting cancellation...");

            cts.Cancel();

            try
            {
                await Task.WhenAll(
                    producer,
                    consumer);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Channel processing was cancelled.");
            }
        }

        private static async Task ProduceWithCancellationAsync(
            ChannelWriter<int> writer,
            CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 1; i <= 20; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await writer.WriteAsync(
                        i,
                        cancellationToken);

                    Console.WriteLine(
                        $"Produced: {i}");

                    await Task.Delay(
                        200,
                        cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Producer cancelled.");

                throw;
            }
            finally
            {
                writer.TryComplete();
            }
        }

        private static async Task ConsumeWithCancellationAsync(
            ChannelReader<int> reader,
            CancellationToken cancellationToken)
        {
            try
            {
                await foreach (
                    int item in reader.ReadAllAsync(
                        cancellationToken))
                {
                    Console.WriteLine(
                        $"Consumed: {item}");

                    await Task.Delay(
                        300,
                        cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Consumer cancelled.");

                throw;
            }
        }
    }
}

//call: await Channel_Demo.RunAsync();