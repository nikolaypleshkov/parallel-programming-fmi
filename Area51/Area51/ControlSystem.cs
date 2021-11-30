using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51
{
    internal class ControlSystem
    {
        public async Task CheckControl(Agent agent, Base _base)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine("The elevator door opens and the scanning begins\n");
            if(agent.SecLvl < _base.FloorSecLvl)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine($"{agent.Name} don't have the permission to enter this floor.");
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine($"{agent.Name} has arrived at his floor: ${_base.Equals}");
            }
            
        }
    }
}
