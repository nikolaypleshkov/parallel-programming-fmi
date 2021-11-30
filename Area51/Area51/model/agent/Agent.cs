using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51
{
    internal class Agent
    {  
        private Random rand = new Random(); 
        enum AgentActivity { Move, CallElevator, Leave};

        public string Name { get; set; }
        public string SecurityLevel { get; set; } 
        public int SecLvl { get; set; }
        public int StartFloor { get; set; }
        public int TargetFloor { get; set; }
        public int CurrentFloor { get; set; }
        public bool isDead { get; set; }

        public Agent(string name, string securityLevel, int secLvl, int startFloor, int targetFloor, int currentFloor, bool IsDead)
        {
            Name = name;
            SecurityLevel = securityLevel;
            StartFloor = startFloor;
            secLvl = secLvl;
            TargetFloor = targetFloor;  
            CurrentFloor = currentFloor;
            isDead = IsDead;
        }

        private AgentActivity GetRanomAgentActivity()
        {
            int n = rand.Next(10);
            if(n < 3) return AgentActivity.Move;
            if(n < 8) return AgentActivity.CallElevator;
            return AgentActivity.Leave;
        }

        private void Leave()
        {
            Console.WriteLine($"{Name} is leving the base");
            Thread.Sleep(100);
        }

        private void EnterBase()
        {
            Console.WriteLine($"{Name} is entering the base");
            Console.WriteLine($"{Name} is calling the elevator");
            CurrentFloor = 0;
            bool isLeaving = false;

            while (!isLeaving)
            {
                var nextActivity = GetRanomAgentActivity();
                switch (nextActivity)
                {
                    case AgentActivity.Move:
                        Console.WriteLine($"{Name} is executing secret missions.");
                        break;
                    case AgentActivity.CallElevator:
                        Console.WriteLine($"{Name} is calling the elevator.");
                        break;
                    case AgentActivity.Leave:
                        Console.WriteLine($"{Name} is leaving the base.");
                        break;
                    default: throw new NotImplementedException();

                }
            }
        }
        private void CallElevator()
        {

        }
       

    }
}
