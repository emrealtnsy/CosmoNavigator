using CosmoNavigator.Enums;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Helpers;

public static class DirectionExtensions
{
    public static DirectionType TurnLeft(this DirectionType d) => d switch {
        DirectionType.N => DirectionType.W,
        DirectionType.W => DirectionType.S,
        DirectionType.S => DirectionType.E,
        DirectionType.E => DirectionType.N,
        _ => throw new ArgumentOutOfRangeException()
    };
    public static DirectionType TurnRight(this DirectionType d) => d switch {
        DirectionType.N => DirectionType.E,
        DirectionType.E => DirectionType.S,
        DirectionType.S => DirectionType.W,
        DirectionType.W => DirectionType.N,
        _ => throw new ArgumentOutOfRangeException()
    };
    public static Coordinate StepForward(this DirectionType d, Coordinate pos) => d switch {
        DirectionType.N => new(pos.X, pos.Y + 1),
        DirectionType.E => new(pos.X + 1, pos.Y),
        DirectionType.S => new(pos.X, pos.Y - 1),
        DirectionType.W => new(pos.X - 1, pos.Y),
        _ => throw new ArgumentOutOfRangeException()
    };
}