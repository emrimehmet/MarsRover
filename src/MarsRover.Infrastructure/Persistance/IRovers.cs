using MarsRover.Data.Enums;
using System.Collections.Generic;

namespace MarsRover.Infrastructure.Persistance {
    public interface IRovers {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public CardinalCompassPoints CardinalCompassPoints { get; set; }
        void ControlRovers(List<int> coordinates, string controlCommands);
    }
}