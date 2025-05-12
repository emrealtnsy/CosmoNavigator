using CosmoNavigator.Enums;

namespace CosmoNavigator.ValueObjects;

public readonly record struct Position(int X, int Y, DirectionType Direction);
