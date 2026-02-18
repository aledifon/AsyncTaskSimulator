using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal class HeatCoffeeTask
    {        
        public static async Task<string> HeatCoffeeAsync(int duration)
        {
            Console.WriteLine("Heating coffee started...");
            Console.WriteLine("Waiting for coffee to be hot...");

            try
            {
                await Task.Delay(duration, CancellationManager.CancellToken);
                Console.WriteLine("Heating Coffee complete...");
                return "hot coffee";
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Heating coffee canceled!");
                return null;
            }
        }
    }
}
