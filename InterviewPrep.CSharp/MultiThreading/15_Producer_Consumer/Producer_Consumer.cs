using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace InterviewPrep.CSharp.MultiThreading._15_Producer_Consumer
{
    public static class Producer_Consumer
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("PRODUCER-CONSUMER");
            Console.WriteLine("========================================");

            await DemonstrateChannelAsync();

            Console.WriteLine();

            await DemonstrateBoundedChannelAsync();

            Console.WriteLine();

            DemonstrateBlockingCollection();

            Console.WriteLine();

            await DemonstrateMultipleConsumersAsync();

            Console.WriteLine();

            Console.WriteLine("All Producer-Consumer demonstrations completed.");
        }

        private static async Task DemonstrateChannelAsync()
        {
            Console.WriteLine("1. CHANNEL - BASIC PRODUCER/CONSUMER");
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
                "Producer and consumer completed.");
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
            await foreach (int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"Consumed: {item}");

                await Task.Delay(500);
            }

            Console.WriteLine(
                "Consumer completed.");
        }

        private static async Task DemonstrateBoundedChannelAsync()
        {
            Console.WriteLine("2. BOUNDED CHANNEL - BACKPRESSURE");
            Console.WriteLine("----------------------------------------");

            BoundedChannelOptions options = new(2)
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
                "Bounded producer-consumer completed.");
        }

        private static async Task ProduceFastAsync(
            ChannelWriter<int> writer)
        {
            for (int i = 1; i <= 6; i++)
            {
                await writer.WriteAsync(i);

                Console.WriteLine(
                    $"Produced: {i}");
            }

            writer.Complete();

            Console.WriteLine(
                "Fast producer completed.");
        }

        private static async Task ConsumeSlowlyAsync(
            ChannelReader<int> reader)
        {
            await foreach (int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"Consumed: {item}");

                await Task.Delay(700);
            }
        }

        private static void DemonstrateBlockingCollection()
        {
            Console.WriteLine("3. BLOCKINGCOLLECTION");
            Console.WriteLine("----------------------------------------");

            using BlockingCollection<int> queue =
                new(
                    new ConcurrentQueue<int>(),
                    boundedCapacity: 2);

            Task producer = Task.Run(() =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    queue.Add(i);

                    Console.WriteLine(
                        $"Produced: {i}");

                    Thread.Sleep(200);
                }

                queue.CompleteAdding();
            });

            Task consumer = Task.Run(() =>
            {
                foreach (int item in queue.GetConsumingEnumerable())
                {
                    Console.WriteLine(
                        $"Consumed: {item}");

                    Thread.Sleep(500);
                }
            });

            Task.WaitAll(
                producer,
                consumer);

            Console.WriteLine(
                "BlockingCollection processing completed.");
        }

        private static async Task DemonstrateMultipleConsumersAsync()
        {
            Console.WriteLine("4. MULTIPLE CONSUMERS");
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
            await foreach (int item in reader.ReadAllAsync())
            {
                Console.WriteLine(
                    $"{consumerName} processing item {item}");

                await Task.Delay(300);
            }

            Console.WriteLine(
                $"{consumerName} completed.");
        }
    }
}

//call: await Producer_Consumer.RunAsync();