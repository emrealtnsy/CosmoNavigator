using CosmoNavigator.Enums;
using CosmoNavigator.Interfaces;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Services;

public class SurfaceService : ISurface
{
    public int MaxX { get; private set; }
    public int MaxY { get; private set; }
    
    private readonly ICollection<Coordinate> _occupied = new List<Coordinate>();

    public void SetSurface(int maxX, int maxY)
    {
        if (maxX < 0 || maxY < 0) throw new ArgumentException("Grid size must be non-negative");
        MaxX = maxX; MaxY = maxY;
    }

    // Case: Gezgin yeni koordinata taşınırken x veya y değeri max sınırı aşıyorsa
    // clamp ile sınırda tutulur (örneğin (7,4)->(5,4))
    public Coordinate Clamp(Coordinate c)
        => new(
            Math.Clamp(c.X, 0, MaxX),
            Math.Clamp(c.Y, 0, MaxY)
        );

    // Case: Bir gezginin o an bulunduğu hücre işgal edilir; ikinci gezgin aynı
    // hücreyi işgal edemez
    public void Occupy(Coordinate c)
    {
        if (_occupied.Contains(c))
            throw new InvalidOperationException($"Position {c} already occupied");
        _occupied.Add(c);
    }

    public IRover CreateRover(int x, int y, DirectionType dir)
    {
        var rover = new RoverService();

        var initial = new Coordinate(x, y);
        var clamped = Clamp(initial);
        
        if (!clamped.Equals(initial))
            throw new ArgumentException("Initial position outside grid");
        
        Occupy(initial);

        rover.Position = initial;
        rover.Heading = dir;
        return rover;
    }
}
