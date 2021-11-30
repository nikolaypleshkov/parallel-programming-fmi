using Area51.model.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51
{
    internal class Elevator
    {
        private Random random = new Random();

        BaseList baseList = new BaseList();
        List<Base> areaBase;
        List<Agent> agents = new List<Agent>();
        Semaphore semaphore = new Semaphore(10, 10);
        private int count = 0;
        private int CurrentFloor { get; set; }
        private int TargetFloor { get; set; }
        private int AgentLimit { get; set; }

        public Elevator(int currentFloor, int targetFloor, int agentFloor)
        {
            baseList.GetFloor();
            this.CurrentFloor = currentFloor;   
            this.TargetFloor = targetFloor;
            this.AgentLimit = agentFloor;
        }

        public void ElevatorOrderList(List<Agent> agentList)
        {
            foreach(var agent in agentList)
            {
                Console.WriteLine($"Spawn Floor: {agent.StartFloor}, Target Floor: {agent.TargetFloor}");
            }
        }

        public void CurrentElveatorFloor(List<Agent> agentList)
        {
            if(count == 0)
            {
                Console.WriteLine($"Current floor the evelator is in {areaBase[agentList[0].StartFloor].Floor}");
                CurrentFloor = agentList[0].StartFloor;
                count++;
            }
            else
            {
                Console.WriteLine($"Current floor the evelator is in {areaBase[CurrentFloor].Floor}");
            }
        }

        public void ChangeFloor(List<Agent> agentList)
        {
            TargetFloor = agentList[0].TargetFloor;
            Console.WriteLine($"The evelator is now changing floor from {areaBase[agentList[0].StartFloor].Floor} to {areaBase[agentList[0].TargetFloor].Floor}");
            CurrentFloor = TargetFloor;
        }

        public void EnterElevator(Agent agent)
        {
            AgentLimit = 6;
            semaphore.WaitOne();
            lock (agents)
            {
                if (agents.Count >= AgentLimit) { Console.WriteLine($"Elevator is full, agent need to wait for the next one."); }
                else
                {
                    agents.Add(agent);
                }
            }
        }
        public void Leave(Agent agent)
        {
            Console.WriteLine($"{agent.Name} has arrived to the targeted floor.");
            lock (agents)
            {
                agents.Remove(agent);

            }
            semaphore.Release();
        }
    }
}
