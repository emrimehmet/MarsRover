using MarsRover.Data.Enums;
using System;
using System.Collections.Generic;

namespace MarsRover.Infrastructure.Persistance {
    public class Rovers : IRovers {
        private readonly int _oneUnit = 1;
        private const char _move = 'M';
        private const char _left = 'L';
        private const char _right = 'R';
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public CardinalCompassPoints CardinalCompassPoints { get; set; }

        public void ControlRovers(List<int> coordinates, string controlCommands) {
            foreach (var controlCommand in controlCommands) {
                switch (controlCommand) {
                    case _move:
                        MoveForward();
                        break;
                    case _left:
                        RotateLeft();
                        break;
                    case _right:
                        RotateRight();
                        break;
                    default:
                        throw new ArgumentException($"{controlCommand}");
                }
            }
        }

        private void RotateLeft() {
            CardinalCompassPoints = CardinalCompassPoints switch {
                CardinalCompassPoints.N => CardinalCompassPoints.W,
                CardinalCompassPoints.S => CardinalCompassPoints.E,
                CardinalCompassPoints.E => CardinalCompassPoints.N,
                CardinalCompassPoints.W => CardinalCompassPoints.S,
                _ => throw new ArgumentException($"{CardinalCompassPoints}")
            };
        }

        private void RotateRight() {
            CardinalCompassPoints = CardinalCompassPoints switch {
                CardinalCompassPoints.N => CardinalCompassPoints.E,
                CardinalCompassPoints.S => CardinalCompassPoints.W,
                CardinalCompassPoints.E => CardinalCompassPoints.S,
                CardinalCompassPoints.W => CardinalCompassPoints.N,
                _ => throw new ArgumentException($"{CardinalCompassPoints}")
            };
        }

        private void MoveForward() {
            switch (CardinalCompassPoints) {
                case CardinalCompassPoints.N:
                    YCoordinate += _oneUnit;
                    break;
                case CardinalCompassPoints.S:
                    YCoordinate -= _oneUnit;
                    break;
                case CardinalCompassPoints.E:
                    XCoordinate += _oneUnit;
                    break;
                case CardinalCompassPoints.W:
                    XCoordinate -= _oneUnit;
                    break;
                default:
                    throw new ArgumentException($"{CardinalCompassPoints}");
            }
        }
    }
}