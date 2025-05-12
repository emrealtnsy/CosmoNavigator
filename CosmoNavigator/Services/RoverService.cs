using CosmoNavigator.Constants;
using CosmoNavigator.Enums;
using CosmoNavigator.Helpers;
using CosmoNavigator.Interfaces;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Services;

public class RoverService : IRoverService
{
    public Coordinate Coordinate { get; private set; }
    public DirectionType Heading { get; private set; }

    
    
    public void DeployRover(int x, int y, DirectionType dir, ISurfaceService surface)
    {
        Heading  = dir;
        Coordinate = new Coordinate(x, y);
        
        var clamped = surface.Clamp(Coordinate);
        if (!clamped.Equals(Coordinate))
            throw new ArgumentException("Initial position outside grid");

        surface.Occupy(Coordinate);
    }

    public void ExecuteCommands(string commands, ISurfaceService surface)
    {
        foreach (var cmd in commands)
        {
            switch (cmd)
            {
                case Commands.Left:  Heading = Heading.TurnLeft(); break;
                case Commands.Right: Heading = Heading.TurnRight(); break;
                case Commands.Move:
                    var next = Heading.StepForward(Coordinate);
                    Coordinate = surface.Clamp(next);
                    break;
                default:
                    throw new ArgumentException($"Invalid command '{cmd}'");
            }
        }

        surface.Occupy(Coordinate);
    }

    public Position Position => new(Coordinate.X, Coordinate.Y, Heading);
}