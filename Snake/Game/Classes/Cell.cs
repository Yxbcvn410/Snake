using System;

namespace Snake.Game.Classes {
    public struct Cell {
        public bool Equals(Cell other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            return obj is Cell other && Equals(other);
        }

        public override int GetHashCode() => base.GetHashCode();

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

        public static Cell operator +(Cell a, Cell b) => new Cell(a.X + b.X, a.Y + b.Y);
    }
}