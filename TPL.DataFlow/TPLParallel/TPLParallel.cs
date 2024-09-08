using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL.DataFlow.TPLParallel
{
    internal static class TPLParallel
    {
        private static readonly IEnumerable<int> data = Enumerable.Range(1, 5);
        public static void ApplyParllelForSync()
        {
            /*
              Problems:
              1.Each Item Take Thread in thread pool in the same Time.
              2.if exist a long procssing inside the for each --> make thread busy more time --> can exist problem Thread Starvation.   
                Different Threads Id Output.
            */

            Parallel.ForEach(data, (item, CancellationToken) =>
            {
                Console.WriteLine($"Item {item} in Thread {Thread.CurrentThread.ManagedThreadId}");
            });
        }

        public async static Task ApplyParllelForAsync()
        {
            //if exist a long procssing inside the for each --> make thread not busy because exist await + Long Proccess.
            //the thread free when reach to the await keyword.
            await Parallel.ForEachAsync(data, async (item, CancellationToken) =>
            {
                Console.WriteLine($"Before---Item {item} in Thread {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1000);
                Console.WriteLine($"After---Item {item} in Thread {Thread.CurrentThread.ManagedThreadId}");
            });


        }

        public async static Task ApplyParllelForOptionsAsync()
        {
            //To Limit the number of data parlell in the same time.
            var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };
            await Parallel.ForEachAsync(data, options, async (item, CancellationToken) =>
            {
                Console.WriteLine($"Before---Item {item} in Thread {Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(1000);
                Console.WriteLine($"After---Item {item} in Thread {Thread.CurrentThread.ManagedThreadId}");
            });
        }
    }
}
