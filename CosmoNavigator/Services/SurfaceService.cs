using CosmoNavigator.Interfaces;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Services;

public class SurfaceService : ISurfaceService
{
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }
    
    private readonly HashSet<Coordinate> _occupied = new();
    
    public void SetSurface(int maxX, int maxY)
    {
        if (maxX < 0 || maxY < 0) throw new ArgumentException("Grid size must be non-negative");
        MaxX = maxX; MaxY = maxY;
    }
    
    public Coordinate Clamp(Coordinate c)
        => new(
            Math.Clamp(c.X, 0, MaxX),
            Math.Clamp(c.Y, 0, MaxY)
        );
    
    public void Occupy(Coordinate c)
    {
        if (!_occupied.Add(c))
            throw new InvalidOperationException($"Position {c} already occupied");
    }
}
