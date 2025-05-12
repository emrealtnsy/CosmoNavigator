using CosmoNavigator.Enums;
using CosmoNavigator.Interfaces;

namespace CosmoNavigator;

public class SimulateManager(ISurfaceService surface, IRoverService roverService)
{
    public void Run()
    {
        Console.Write("Please input grid size:");
        var gridParts = Console.ReadLine()!.Split();
        surface.SetSurface(
            maxX: int.Parse(gridParts[0]),
            maxY: int.Parse(gridParts[1])
        );

        for (int i = 0; i < 2; i++)
        {
            Console.Write("Please input coordinate (x y d):");
            var locParts = Console.ReadLine()!.Split();
            int x = int.Parse(locParts[0]);
            int y = int.Parse(locParts[1]);
            var dir = Enum.Parse<DirectionType>(locParts[2], true);
            Console.Write("Please input commands (LRM etc.):");
            var commands = Console.ReadLine()!;
            
            roverService.DeployRover(x, y, dir, surface);
            roverService.ExecuteCommands(commands, surface);
            Console.WriteLine($"{roverService.Position.X} {roverService.Position.Y} {roverService.Heading}");
        }

     
    }
}