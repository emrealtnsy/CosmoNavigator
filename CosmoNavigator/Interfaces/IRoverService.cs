using CosmoNavigator.Enums;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Interfaces;

public interface IRoverService
{
    Coordinate Coordinate { get; }
    DirectionType Heading { get; }
    Position Position { get; }
    void DeployRover(int x, int y, DirectionType dir, ISurfaceService surface);
    void ExecuteCommands(string commands, ISurfaceService surface);
}