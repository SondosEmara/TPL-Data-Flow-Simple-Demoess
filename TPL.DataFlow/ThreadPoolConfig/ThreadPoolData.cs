using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL.DataFlow.ThreadPoolConfig
{
    internal static class ThreadPoolData
    {
        public static Tuple<int, int> GetAvaiableThreads()
        {
            ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int availableIoThreads);
            return Tuple.Create(availableWorkerThreads, availableIoThreads);
        }
    }
}
