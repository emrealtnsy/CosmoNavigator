using CosmoNavigator.Interfaces;
using CosmoNavigator.Services;
using CosmoNavigator.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CosmoNavigator.Tests;

public class SurfaceServiceTests
{
    private readonly ISurfaceService _surface;

    public SurfaceServiceTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ISurfaceService, SurfaceService>();
        var provider = services.BuildServiceProvider();

        _surface = provider.GetRequiredService<ISurfaceService>();
    }

    [Fact]
    public void SetSurface_WithValidSize_ShouldClampExceedingCoordinate()
    {
        _surface.SetSurface(5, 5);
        var clamped = _surface.Clamp(new Coordinate(7, 8));

        Assert.Equal(5, clamped.X);
        Assert.Equal(5, clamped.Y);
    }

    [Fact]
    public void SetSurface_WithNegativeSize_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _surface.SetSurface(-1, 5));
    }
}