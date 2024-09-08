using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow
{

    /*
     * This File For Only Source Block
     * If a block produces data, then we call it a source block
    */
    internal static partial class TplDataFlow
    {
        #region Source Block
            public async static Task ApplyBufferBlock()
            {
                var bufferBlock = new BufferBlock<int>();

                /*
                 * The Post() method sends an item to the block without waiting for the item to be accepted
                 * The SendAsync() method is asynchronous and will wait (without blocking the thread) until the item is accepted by the block.
                 *  This means that if the block (input queue) is full, SendAsync() will await until there is space available for the new item to be sent.
                 *  Buffer Block<TOuput> --> (TInput,TOutput)--> the TInput using the (Post/SendAsync) ,TOuput --> data returned in the output queue.
                 *  Not take Predicate 
                 */
                foreach (var item in data)
                {
                    await bufferBlock.SendAsync(item);
                }

                bufferBlock.Complete();

                foreach (var item in data)
                {
                    Console.WriteLine(await bufferBlock.ReceiveAsync());
                }

            }

            public async static Task ApplyTransformBlock()
            {
                //TransformBlock<TInput, TOutput> Tinput-->From Post/SendAsyn or from another source block

                #region Blocks
                var bufferBlock = new BufferBlock<int>();
                var transformBlock = new TransformBlock<int, int>(async (num) =>
                {
                    await Task.Delay(500); //await + long running function to free the thread.
                    return num * num;
                });

                var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
                bufferBlock.LinkTo(transformBlock, linkOptions);

                #endregion


                #region Send-Data

                foreach (var item in data)
                {
                    await bufferBlock.SendAsync(item);
                }
                bufferBlock.Complete();

                foreach (var item in data)
                {
                    Console.WriteLine(await transformBlock.ReceiveAsync());
                }

                #endregion
            }

            public async static Task ApplyTransformManyBlock()
            {
                //TransformManyBlock<int,Ienumerable<int>> return the IEnumerable.....
                var tranformManyBlock = new TransformManyBlock<int, int>(async (num) =>
                {
                    await Task.Delay(500);
                    return new List<int> { num, num * num };
                });
                var actionBlock = new ActionBlock<int>(result => Console.WriteLine(result));

                var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
                tranformManyBlock.LinkTo(actionBlock, linkOptions);

                foreach (var item in data)
                {
                    await tranformManyBlock.SendAsync(item);
                }
                tranformManyBlock.Complete();
                await actionBlock.Completion;
            }

            public async static Task ApplyBroadCastBlock()
        {
            var broadCast = new BroadcastBlock<int>(num => num);
            var actionBlock_1 = new ActionBlock<int>(result => Console.WriteLine(result));
            var actionBlock_2 = new ActionBlock<int>(result => Console.WriteLine(result));

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            broadCast.LinkTo(actionBlock_1, linkOptions);
            broadCast.LinkTo(actionBlock_2, linkOptions);

            foreach (var item in data)
            {
                await broadCast.SendAsync(item);
            }
            broadCast.Complete();

            await Task.WhenAll(actionBlock_1.Completion, actionBlock_2.Completion);

        }
        #endregion

        #region Target Block
            public async static Task ApplyActionBlock()
            {
                var actionBlock = new ActionBlock<int>(n => Console.WriteLine(n));
                var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
         
                foreach (var item in data)
                {
                    await actionBlock.SendAsync(item);
                }
                actionBlock.Complete();

                await actionBlock.Completion;
            }
        #endregion

        #region Grouping Blocks

        public async static Task ApplyBatchBlock()
        {

            //Take Data from array [1,2,3] and send it to the action block.
            //Then Take Batch array [4,5,6]
            var batchBlock = new BatchBlock<int>(3);

            var actionBllock = new ActionBlock<int[]>(batchArray => {
                Console.WriteLine("Batch Prossing.......!");
                foreach(var i in batchArray) { Console.WriteLine($"Prossing {i}"); }
            
            });

            var linkOptions=new DataflowLinkOptions { PropagateCompletion=true };
            batchBlock.LinkTo(actionBllock,linkOptions);
            foreach (var item in data)
            {
                await batchBlock.SendAsync(item);
            }
            batchBlock.Complete();
            await actionBllock.Completion;
        }
        #endregion
    }
}
