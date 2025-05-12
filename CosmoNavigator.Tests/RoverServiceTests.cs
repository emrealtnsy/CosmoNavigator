using CosmoNavigator.Enums;
using CosmoNavigator.Interfaces;
using CosmoNavigator.Services;
using CosmoNavigator.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CosmoNavigator.Tests;

public class RoverServiceTests
{
    private readonly ISurfaceService _surface;
    private readonly IRoverService _rover1;
    private readonly IRoverService _rover2;

    public RoverServiceTests()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ISurfaceService, SurfaceService>();
        services.AddTransient<IRoverService, RoverService>();
        var provider = services.BuildServiceProvider();

        _surface = provider.GetRequiredService<ISurfaceService>();
        _rover1 = provider.GetRequiredService<IRoverService>();
        _rover2 = provider.GetRequiredService<IRoverService>();
    }

    [Fact]
    public void DeployRover_WithValidInitialPosition_ShouldPlaceRover()
    {
        _surface.SetSurface(5, 5);
        _rover1.DeployRover(1, 2, DirectionType.N, _surface);

        Assert.Equal(1, _rover1.Position.X);
        Assert.Equal(2, _rover1.Position.Y);
        Assert.Equal(DirectionType.N, _rover1.Heading);
    }

    [Fact]
    public void MultipleRovers_ShouldMoveSequentiallyAndNotCollide()
    {
        _surface.SetSurface(5, 5);

        _rover1.DeployRover(1, 2, DirectionType.N, _surface);
        _rover1.ExecuteCommands("LMLMLMLMM", _surface); // 1 3 N

        _rover2.DeployRover(3, 3, DirectionType.E, _surface);
        _rover2.ExecuteCommands("MMRMMRMRRM", _surface); // 5 1 E

        Assert.Equal(new Coordinate(1, 3), _rover1.Coordinate);
        Assert.Equal(DirectionType.N, _rover1.Heading);

        Assert.Equal(new Coordinate(5, 1), _rover2.Coordinate);
        Assert.Equal(DirectionType.E, _rover2.Heading);
    }

    [Fact]
    public void ExecuteCommands_WithInvalidCommand_ShouldThrow()
    {
        _surface.SetSurface(5, 5);
        _rover1.DeployRover(1, 2, DirectionType.N, _surface);

        Assert.Throws<ArgumentException>(() =>
                _rover1.ExecuteCommands("LMX", _surface) 
        );
    }

    [Fact]
    public void ExecuteCommands_ShouldClampWhenMovingOutOfBounds()
    {
        _surface.SetSurface(5, 5);
        _rover1.DeployRover(3, 3, DirectionType.E, _surface);

        _rover1.ExecuteCommands("MMMMMMM", _surface); 

        Assert.Equal(5, _rover1.Position.X);
        Assert.Equal(3, _rover1.Position.Y);
    }
}