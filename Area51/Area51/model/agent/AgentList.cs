using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.model.agent
{
    internal class AgentList
    {
        List <Agent> agents = new List <Agent> ();
        Random random = new Random ();
        
        public AgentList()
        {
            for(int i = 0; i < 20; i++)
            {
                Agent agent = new Agent("AG-01","Undefined",1, 1, 1, 1, false);

                int genId = random.Next(1, 25);
                agent.Name = "AG-0" + genId;

                int lvl = random.Next(10);
                if (lvl < 3) agent.SecurityLevel = "Confidential";
                if (lvl < 7) agent.SecurityLevel = "Secret";
                else agent.SecurityLevel = "TopSecret";

                int secLvl = random.Next(1,4);
                agent.SecLvl = secLvl;

                agent.StartFloor = 1;

                int targetFloor = random.Next(1, 9);
                agent.TargetFloor = targetFloor;

                agent.CurrentFloor = 1;

                agents.Add(agent);
            }
        }

        public List<Agent> GetAgent()
        {
            return agents;
        }
    }
}
