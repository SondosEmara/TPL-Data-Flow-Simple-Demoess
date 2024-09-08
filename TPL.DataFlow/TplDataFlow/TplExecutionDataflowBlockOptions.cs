using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow
{
    //that part to ExecutionDataflowBlockOptions  in the Tpl Data Flow blocks 
    internal partial class TplDataFlow
    {
        #region Degree of Parallelism

            /*
            * one message at a time. You can also specify the degree of parallelism
            *to enable ActionBlock<TInput>, TransformBlock<TInput,TOutput> and TransformManyBlock<TInput,TOutput> objects to process multiple messages concurrently
            */
            public async static Task ApplyTPlParallelism()
            {
                var numberConcurrentThread = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 2 };

                var actionBlock = new ActionBlock<int>(async i =>
                {
                    Console.WriteLine($"Before---Processing {i} on Thread {Thread.CurrentThread.ManagedThreadId}");
                    await Task.Delay(2000); //await+long proccess.
                    Console.WriteLine($"After---Processing {i} on Thread {Thread.CurrentThread.ManagedThreadId}");
                }, numberConcurrentThread);


                foreach (var item in data)
                {
                    await actionBlock.SendAsync( item );
                }
                actionBlock.Complete();
                await actionBlock.Completion;
            }
        #endregion
    }
}
