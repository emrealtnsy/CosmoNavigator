using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Interfaces;

public interface ISurfaceService
{
    Coordinate Clamp(Coordinate c);
    void Occupy(Coordinate c);
    void SetSurface(int maxX, int maxY);
}