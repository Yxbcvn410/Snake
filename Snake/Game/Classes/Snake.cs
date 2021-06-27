using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Game.UI;
using Snake.Game.Visual;

namespace Snake.Game.Classes {
    public enum Direction {
        Up = 0,
        Down = 2,
        Left = 1,
        Right = 3
    }

    public class Snake : Widget {
        private List<Cell> _points;
        private Direction _direction;

        public Direction Direction {
            set {
                if ((_direction - value) % 2 != 0)
                    _direction = value;
            }
        }

        private int _length;

        public int Length {
            get => _length;
            set {
                _length = value;
                _points = _points.Take(_length).ToList();
            }
        }

        private bool _isDead;

        public bool IsDead {
            get => _isDead;
            set {
                if (value)
                    _isDead = true;
                else
                    throw new ArgumentException("Snake cannot be revived");
            }
        }

        public Snake() {
            _points = new List<Cell>
                {new Cell(1, 3), new Cell(1, 2), new Cell(1, 1)};
            _direction = Direction.Down;
            _length = 3;
            _isDead = false;
        }

        protected override void UpdateSelf() {
            Cell first = _points.First();
            first.ShiftBy(_direction);
            _points.Insert(0, first);
            _points = _points.Take(Length).ToList();
            if (_points.Skip(1).Contains(Head))
                _isDead = true;
        }

        public bool Contains(Cell cell) => _points.Contains(cell);

        public Cell Head => _points.First();

        protected override void DrawSelf(GraphicsContext cxt) {
            if (IsDead)
                cxt.WriteStr(Head, "%");
            _points.ForEach(point => cxt.WriteStr(point, "#"));
        }
    }
}