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
            Console.WriteLine("Boiling water started...");

            Console.WriteLine("Waiting for water to be hot...");
            await Task.Delay(duration, CancellationManager.CancellToken);
            Console.WriteLine("Boling water complete...");

            return "hot water";
        }

    }
}
