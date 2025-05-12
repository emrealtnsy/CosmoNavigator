using CosmoNavigator.Enums;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Interfaces;

// DIP/ISP: ISurface, yüzey işlemlerini soyutlamak için arayüz
public interface ISurfaceService
{
    Coordinate Clamp(Coordinate c);
    void Occupy(Coordinate c);
    void SetSurface(int maxX, int maxY);
}