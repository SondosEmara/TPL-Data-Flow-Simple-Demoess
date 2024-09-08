using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL.DataFlow
{

    /*Here Exist the Tradional Ways Before the TPL Data Flow (Problems) and  How the TPL Solve It*/
    internal static partial class TplDataFlow
    {
        private readonly static List<int>data=new List<int>() { 1,2,3,4,5,6 };
        public static void ApplyTraditionalApproach()
        {
            int sharedCounter = 0;
            object lockObject = new object();

            Thread[] threads = new Thread[10];

            for (int j = 0; j < 10; j++)
            {
                threads[j] = new Thread(() => {
                        lock (lockObject)
                        {
                            sharedCounter++;
                        }
                });
                threads[j].Start();
            }

            // Wait for all threads to finish
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Final Counter Value: {sharedCounter}");

        }
    }
}
