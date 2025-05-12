using CosmoNavigator.Enums;
using CosmoNavigator.Services;
using Xunit;

namespace CosmoNavigator.Tests.Tests;

public class RoverServiceTests
    {
        private readonly SurfaceService _surface;
        private readonly RoverService _rover;

        public RoverServiceTests()
        {
            _surface = new SurfaceService();
            _surface.SetSurface(5, 5); // REQ-1
            _rover = new RoverService();
        }

        [Fact]
        // REQ-2 & REQ-11: Başlangıç pozisyonu ve yön doğru atanıyor mu?
        public void DeployRover_WithinBounds_SetsPositionAndHeading()
        {
            _rover.DeployRover(1, 2, DirectionType.N, _surface); // REQ-2

            Assert.Equal(1, _rover.Position.X);
            Assert.Equal(2, _rover.Position.Y);
            Assert.Equal(DirectionType.N, _rover.Heading);
        }

        [Fact]
        // REQ-8: Başlangıç pozisyonu grid dışındaysa exception fırlatılır
        public void DeployRover_OutsideBounds_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => 
                _rover.DeployRover(6, 6, DirectionType.N, _surface)); // REQ-8
        }

        [Fact]
        // REQ-4, REQ-5, REQ-7, REQ-9: Komutları uygulayıp doğru son konuma ulaşıyor mu?
        public void ExecuteCommands_MoveWithinBounds_UpdatesPosition()
        {
            _rover.DeployRover(1, 2, DirectionType.N, _surface); // REQ-2
            _rover.ExecuteCommands("LMLMLMLMM", _surface); // REQ-3,4,5,9

            Assert.Equal(1, _rover.Position.X);
            Assert.Equal(3, _rover.Position.Y);
            Assert.Equal(DirectionType.N, _rover.Heading);
        }

        [Fact]
        // REQ-8: Komut sonucunda taşma olması durumunda clamp uygulanıyor mu?
        public void ExecuteCommands_MoveExceedsBounds_ClampsPosition()
        {
            _rover.DeployRover(5, 5, DirectionType.N, _surface); // REQ-2
            _rover.ExecuteCommands("M", _surface); // REQ-3,4,8

            Assert.Equal(5, _rover.Position.X);
            Assert.Equal(5, _rover.Position.Y);
        }

        [Fact]
        // REQ-3: Geçersiz komut karakteri exception fırlatmalı
        public void ExecuteCommands_InvalidCommand_ThrowsArgumentException()
        {
            _rover.DeployRover(0, 0, DirectionType.N, _surface); // REQ-2
            Assert.Throws<ArgumentException>(() => _rover.ExecuteCommands("X", _surface)); // REQ-3
        }

        [Fact]
        // REQ-7 & REQ-9: İkinci rover, birinci rover'ın işgal ettiği pozisyona hareket ederse çakışma hatası
        public void ExecuteCommands_OccupyCollision_ThrowsInvalidOperationException()
        {
            var rover1 = new RoverService();
            rover1.DeployRover(1, 1, DirectionType.N, _surface); // REQ-2,7,9

            var rover2 = new RoverService();
            rover2.DeployRover(0, 1, DirectionType.E, _surface); // REQ-2
            Assert.Throws<InvalidOperationException>(() => rover2.ExecuteCommands("M", _surface)); // REQ-9
        }
    }