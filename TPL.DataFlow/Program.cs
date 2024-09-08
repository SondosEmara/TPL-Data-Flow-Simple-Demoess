using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.ThreadPoolConfig;

namespace TPL.DataFlow
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region Thread-Pool

                //Number of Threads in thread Pool
                var (avaibleWorkerThreads, avaibleIOThreads) = ThreadPoolData.GetAvaiableThreads();
                Console.WriteLine($"The Current Avaialble Threads in the ThreadPool avaibleWorkerThreads {avaibleWorkerThreads}");

            #endregion

            #region ParallelFor
            //TPLParallel.ApplyParllelForSync();

            //Console.WriteLine("--------------------------------------");
            //await TPLParallel.ApplyParllelForAsync();

            //Console.WriteLine("--------------------------------------");
            //await TPLParallel.ApplyParllelForOptionsAsync();
            #endregion

            #region TPL-Data-Flow-Blocks

            //TplDataFlow.ApplyTraditionalApproach();
            // await TplDataFlow.ApplyBufferBlock();

            //await TplDataFlow.ApplyTransformBlock();

            //await TplDataFlow.ApplyTransformManyBlock();

            //await TplDataFlow.ApplyBroadCastBlock();
            await TplDataFlow.ApplyBatchBlock();
            #endregion


            #region TPL-Data-Flow-Parllism
            //await TplDataFlow.ApplyTPlParallelism();
            #endregion


            #region More-Features

            //await TplDataFlow.ApplyFilterDemo();
            #endregion
        }
    }
}
