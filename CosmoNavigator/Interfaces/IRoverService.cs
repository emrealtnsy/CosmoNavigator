using CosmoNavigator.Enums;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Interfaces;

public interface IRoverService
{
    void ExecuteCommands(string commands, ISurface surface);
    Coordinate Coordinate { get; }
    DirectionType Heading { get; }
    Position Position { get; }
    void DeployRover(int x, int y, DirectionType dir, ISurface surface);
}