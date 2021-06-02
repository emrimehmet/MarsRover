using MarsRover.Data.Enums;
using MarsRover.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MarsRover {
    public static class Program {
        static void Main(string[] args) {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRovers, Rovers>()
                .BuildServiceProvider();

            #region Enter parameters

            Console.Write("Enter the upper-right coordinates of the plateau (For Example: x,y -> 1,1): ");
            var coordinates = Console.ReadLine().Trim().Split(',').Select(int.Parse).ToList();

            Console.Write("Enter the initial position of the first rover (For Example: 0,0,N): ");
            var firstPosition = Console.ReadLine().Trim().Split(',');

            Console.Write("Enter the letters in order to control first rover (For Example: L,R,M,R,L,M): ");
            var controlCommands = Console.ReadLine().ToUpper();

            Console.Write("Enter the initial position of the second rover (For Example: 0,0,N): ");
            var secondPosition = Console.ReadLine().Trim().Split(',');

            Console.Write("Enter the letters in order to control second rover (For Example: L,R,M,R,L,M): ");
            var secondControlCommands = Console.ReadLine().ToUpper();

            #endregion Enter parameters

            try {
                #region first condition

                var rovers = serviceProvider.GetService<IRovers>();
                if (firstPosition.Length == 3) {
                    rovers.XCoordinate = Convert.ToInt32(firstPosition[0]);
                    rovers.YCoordinate = Convert.ToInt32(firstPosition[1]);
                    rovers.CardinalCompassPoints = (CardinalCompassPoints)Enum.Parse(typeof(CardinalCompassPoints), firstPosition[2]);
                }
                else
                    throw new ArgumentException($"Rovers error for count!");

                rovers.ControlRovers(coordinates, controlCommands);
                Console.WriteLine($"{rovers.XCoordinate} {rovers.YCoordinate} {rovers.CardinalCompassPoints}");

                #endregion first condition

                #region second condition

                rovers = serviceProvider.GetService<IRovers>();
                if (secondPosition.Length == 3) {
                    rovers.XCoordinate = Convert.ToInt32(secondPosition[0]);
                    rovers.YCoordinate = Convert.ToInt32(secondPosition[1]);
                    rovers.CardinalCompassPoints = (CardinalCompassPoints)Enum.Parse(typeof(CardinalCompassPoints), secondPosition[2]);
                }
                else
                    throw new ArgumentException($"Rovers error for count!");

                rovers.ControlRovers(coordinates, secondControlCommands);
                Console.WriteLine($"{rovers.XCoordinate} {rovers.YCoordinate} {rovers.CardinalCompassPoints}");

                #endregion second condition
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}