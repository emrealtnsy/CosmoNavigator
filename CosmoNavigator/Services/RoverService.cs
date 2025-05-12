using CosmoNavigator.Constants;
using CosmoNavigator.Enums;
using CosmoNavigator.Helpers;
using CosmoNavigator.Interfaces;
using CosmoNavigator.ValueObjects;

namespace CosmoNavigator.Services;

public class RoverService : IRover
{
    // IRover arayüzündeki event implementasyonu
    public Coordinate Position { get; internal set; }
    public DirectionType Heading { get; internal set; }

    // Removed CreateRover method

    // Case: Komut dizisi (örn. "LMLMLMLMM") sırasıyla işlenir; bir gezgin bitmeden diğeri başlamaz
    public void ExecuteCommands(string commands, ISurface surface)
    {
        // Case: Komut seti sadece 'L','R','M' içerir; 'L'/'R' için dönüş, 'M' için ileri hareket uygulanır, aksi halde hata fırlatılır
        foreach (var cmd in commands)
        {
            switch (cmd)
            {
                case Commands.Left:  Heading = Heading.TurnLeft(); break;
                case Commands.Right: Heading = Heading.TurnRight(); break;
                case Commands.Move:
                    var next = Heading.StepForward(Position);
                    Position = surface.Clamp(next);
                    break;
                default:
                    throw new ArgumentException($"Invalid command '{cmd}'");
            }
        }
        // Son pozisyonu de işgal et
        surface.Occupy(Position);
    }
}
