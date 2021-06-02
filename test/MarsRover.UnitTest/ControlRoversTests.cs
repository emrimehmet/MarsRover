using MarsRover.Data.Enums;
using MarsRover.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarsRover.UnitTest {
    [TestFixture]
    public class ControlRoversTests {
        [TestCase("5,5", "1,2,N", "LMLMLMLMM", "1 3 N")]
        [TestCase("5,5", "3,3,E", "MMRMMRMRRM", "5 1 E")]
        public void ControlRovers_WhenCalled_Asserts(string coordinates, string positions, string controlCommands, string expectedOutput) {
            var serviceProvider = new ServiceCollection()
               .AddSingleton<IRovers, Rovers>()
               .BuildServiceProvider();

            var testPositions = positions.Trim().Split(',');
            var rovers = serviceProvider.GetService<IRovers>();
            rovers.XCoordinate = Convert.ToInt32(testPositions[0]);
            rovers.YCoordinate = Convert.ToInt32(testPositions[1]);
            rovers.CardinalCompassPoints = (CardinalCompassPoints)Enum.Parse(typeof(CardinalCompassPoints), testPositions[2]);

            rovers.ControlRovers(coordinates.Trim().Split(',').Select(int.Parse).ToList(), controlCommands);
            Assert.AreEqual(expectedOutput, $"{rovers.XCoordinate} {rovers.YCoordinate} {rovers.CardinalCompassPoints}");
        }
    }
}