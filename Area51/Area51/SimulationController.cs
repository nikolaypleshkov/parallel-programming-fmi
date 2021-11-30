using Area51.model.agent;
using Area51.model.baseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51
{
    internal class SimulationController
    {
        Random random = new Random();
        Elevator elevator = new Elevator(1, 1, 1);
        AgentList agentList = new AgentList();
        List<Agent> agents;
        BaseList baseList = new BaseList();
        ControlSystem  controlSystem = new ControlSystem();  
        List<Base> _base;
        int agentCount = 0;
        int n = 0;

        public async Task Simulator()
        {
            agents = agentList.GetAgent();
            _base = baseList.GetFloor();

            while(n < 100)
            {
                var ElevatorEvent = ElevatorEventAsync();
                var agentEventA = AgentEventAsync();
                await agentEventA;
                await ElevatorEvent;
                await ScanAgent();
                n++;
            }
        }

        public async Task ElevatorEventAsync()
        {
            await SpawnAgent();
            elevator.CurrentElveatorFloor(agents);
            await Task.Delay(TimeSpan.FromSeconds(10));
            elevator.ChangeFloor(agents);
        }

        public async Task AgentEventAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(4));
            Console.WriteLine($"Agent is arriving after ${agentCount} seconds with certification level: ${agents[0].SecurityLevel} ");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine($"Agent {agentCount} presses the button to call for the elevator.");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine($"After 3 seconds the elevator arrives and Agent steps inside, and presses the button for floor {_base[agents[0].TargetFloor].Floor}\n");
            agentCount++;
        }

        public async Task ScanAgent()
        {
            await controlSystem.CheckControl(agents[0], _base[agents[0].TargetFloor]);
        }

        public async Task SpawnAgent()
        {
            await Task.Delay(TimeSpan.FromSeconds(0));
            Console.WriteLine($"Agent with CertLevel: {agents[0].SecLvl}  SpawnFloor: {agents[0].StartFloor} TargetFloor: {agents[0].TargetFloor}");
        }
    }
}
