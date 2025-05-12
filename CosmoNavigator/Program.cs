using CosmoNavigator.Interfaces;
using CosmoNavigator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CosmoNavigator;

public static class Program
{
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<ISurfaceService, SurfaceService>();
                services.AddTransient<IRoverService, RoverService>();
                services.AddTransient<SimulateManager>();
            })
            .Build();

        host.Services.GetRequiredService<SimulateManager>().Run();
    }
}