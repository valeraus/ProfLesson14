using System;
using System.Threading;
using System.Threading.Tasks;

namespace AddTask
{
    class Program
    {
        static private int counter = 0;
        static private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        static private async Task Function1Async()
        {
            for (int i = 0; i < 50; ++i)
            {
                await semaphore.WaitAsync();
                Console.Write("1 ");
                Console.WriteLine("{0} з потоку {1}", ++counter, Thread.CurrentThread.GetHashCode());
                semaphore.Release();
            }
        }

        static private async Task Function2Async()
        {
            for (int i = 0; i < 50; ++i)
            {
                await semaphore.WaitAsync();
                Console.Write("2 ");
                Console.WriteLine("{0} з потоку {1}", ++counter, Thread.CurrentThread.GetHashCode());
                semaphore.Release();
            }
        }

        static private async Task Function3Async()
        {
            for (int i = 0; i < 50; ++i)
            {
                semaphore.Release();
                await semaphore.WaitAsync();
                Console.Write("3 ");
                Console.WriteLine("{0} з потоку {1}", ++counter, Thread.CurrentThread.GetHashCode());
            }
        }

        static async Task Main()
        {
            Thread[] threads = { new Thread(async () => await Function1Async()), new Thread(async () => await Function2Async()), new Thread(async () => await Function3Async()) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            Console.ReadKey();
        }
    }
}
