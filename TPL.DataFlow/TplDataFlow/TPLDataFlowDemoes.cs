using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow
{
    internal partial class TplDataFlow
    {
        #region Apply Different Filter
        public static async Task ApplyFilterDemo()
        {
            #region Blokcs

            var transformBlock = new TransformBlock<int, int>(num => num);

            var evenActionBlock = new ActionBlock<int>(num => Console.WriteLine($"Even Number {num} {Thread.CurrentThread.ManagedThreadId}"));

            var oddActionBlock = new ActionBlock<int>(num => Console.WriteLine($"Odd Number {num} {Thread.CurrentThread.ManagedThreadId}"));

            // to  make sure that at least one target block receives each message (not deadlock)
            var failedActionBlock = new ActionBlock<int>(num => Console.WriteLine($"Faild Number {num}"));
            #endregion


            #region link-blocks
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            transformBlock.LinkTo(evenActionBlock, linkOptions, num => num % 2 == 0);
            transformBlock.LinkTo(oddActionBlock, linkOptions, num => num % 2 != 0);
            transformBlock.LinkTo(evenActionBlock, linkOptions, num => true);
            #endregion


            #region Config
            for (int i = 1; i <= 100; i++)
            {
                await transformBlock.SendAsync(i);
            }

            transformBlock?.Complete();
            await Task.WhenAll(evenActionBlock.Completion, oddActionBlock.Completion);
            #endregion

        }
        #endregion
    }
}
