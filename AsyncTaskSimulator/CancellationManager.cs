using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal static class CancellationManager
    {
        public static CancellationTokenSource? CancellTokenSource { get; private set; }     // This object points which CancelToken should stops the Task
        public static CancellationToken CancellToken { get; private set; }                 // Cancellation Token                                 
        public static bool CancelMonitoringEnabled { get; set; }

        static CancellationManager()
        {
            ResetCancellationToken();
            CancelMonitoringEnabled = false;
        }

        public static void ResetCancellationToken()
        {
            CancellTokenSource = new CancellationTokenSource();
            CancellToken = CancellTokenSource.Token;
        }

        public static async Task MonitoringLoopAsync()
        {
            CancelMonitoringEnabled = true;

            while (!CancellToken.IsCancellationRequested && CancelMonitoringEnabled)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.X)
                    {
                        // Cancel Task
                        CancellTokenSource?.Cancel();
                        break;
                    }
                }                
                await Task.Delay(100);
            }                                           
        }
    }
}
