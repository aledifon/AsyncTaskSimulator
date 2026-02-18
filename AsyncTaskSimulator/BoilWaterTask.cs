using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal class BoilWaterTask
    {        
        public static async Task<string> BoilWaterAsync(int duration)
        {
            Console.WriteLine("Boiling water started...");
            Console.WriteLine("Waiting for water to be hot...");

            try
            {
                await Task.Delay(duration, CancellationManager.CancellToken);
                Console.WriteLine("Boling water complete...");
                return "hot water";
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Boling water canceled!");
                return null;
            }
        }
    }
}
