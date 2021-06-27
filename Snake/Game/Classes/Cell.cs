using System;

namespace Snake.Game.Classes {
    public struct Cell {
        public int X, Y;

        public Cell(int x, int y) {
            X = x;
            Y = y;
        }

        public void ShiftBy(Direction direction) {
            switch (direction) {
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool operator ==(Cell a, Cell b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Cell a, Cell b) => !(a == b);

        public static Cell operator +(Cell a, Cell b) => new Cell(a.X + b.X, a.Y + b.Y);
    }
}