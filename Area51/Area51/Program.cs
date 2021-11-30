
using Area51;

class Program
{
    static async Task Main(string[] args)
    {

        SimulationController simulation = new SimulationController();
        await simulation.Simulator();
        Console.ReadLine();
    }
}