using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTaskSimulator
{
    internal class HeatMilkTask
    {        
        public static async Task<string> HeatMilkAsync(int duration)
        {
            Console.WriteLine("Heating milk started...");

            Console.WriteLine("Waiting for milk to be hot...");
            await Task.Delay(duration, CancellationManager.CancellToken);
            Console.WriteLine("Heating milk complete...");

            return "hot milk";
        }

    }
}
