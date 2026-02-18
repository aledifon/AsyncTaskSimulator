using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal static class CancellationManager
    {
        public static CancellationTokenSource CancellTokenSource { get; private set; }     // This object points which CancelToken should stops the Task
        public static CancellationToken CancellToken { get; private set; }                 // Cancellation Token                 
        public static bool AreTasksRunning 
        { 
            get { return AreTasksRunning; }
            set { AreTasksRunning = value; } 
        }

        static CancellationManager()
        {
            CancellTokenSource = new CancellationTokenSource();
            CancellToken = CancellTokenSource.Token;
            AreTasksRunning = false;
        }

        public static async void MonitoringLoop()
        {
            AreTasksRunning = true;

            while (!CancellToken.IsCancellationRequested && AreTasksRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.X)
                    {
                        // Cancel Task
                        CancellTokenSource.Cancel();
                        break;
                    }
                }                
                await Task.Delay(100);
            }                                           
        }
    }
}
