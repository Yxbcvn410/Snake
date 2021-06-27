using System;
using System.Linq;
using System.Text;
using Snake.Game.Classes;

namespace Snake.Game.Visual {
    static class GraphicsBackend {
        private static StringBuilder[] _canvas, _buffer;
        private static int _w, _h;
        private static readonly object OutLock = new object();

        private static StringBuilder[] AllocateBuffer(int width, int height) {
            _w = width;
            _h = height;
            var buffer = new StringBuilder[height];
            for (int i = 0; i < height; i++)
                buffer[i] = new StringBuilder(String.Concat(Enumerable.Repeat(" ", width)));

            return buffer;
        }

        public static void Init(int width, int height) {
            Console.Clear();
            _w = width;
            _h = height;
            _canvas = AllocateBuffer(width, height);
            _buffer = AllocateBuffer(width, height);
        }

        public static void WriteStr(Cell cell, string str) {
            lock (OutLock) {
                if (cell.X < 0 || cell.X >= _w || cell.Y < 0 || cell.Y >= _h)
                    return;
                _buffer[cell.Y].Insert(cell.X, str);
                _buffer[cell.Y].Remove(Math.Min(_w, cell.X + str.Length), str.Length);
            }
        }

        public static void Flush() {
            lock (OutLock) {
                for (int i = 0; i < _h; i++) {
                    if (_canvas[i].Equals(_buffer[i])) continue;
                    Console.SetCursorPosition(0, i);
                    Console.Write(_buffer[i]);
                }

                _buffer.CopyTo((Memory<StringBuilder>) _canvas);
                _buffer = AllocateBuffer(_w, _h);
            }
        }
    }

    public class GraphicsContext {
        public virtual void WriteStr(Cell cell, string str) {
            GraphicsBackend.WriteStr(cell, str);
        }

        public virtual void WriteStr(String str) {
            WriteStr(new Cell(0, 0), str);
        }
    }

    public class BoxGraphicsContext : GraphicsContext {
        private readonly GraphicsContext _wrappee;
        private Cell _location;

        public BoxGraphicsContext(GraphicsContext wrappee, Cell location) {
            _wrappee = wrappee;
            _location = location;
        }

        public override void WriteStr(Cell cell, string str) {
            _wrappee.WriteStr(cell + _location, str);
        }

        public override void WriteStr(String str) {
            WriteStr(new Cell(0, 0), str);
        }
    }
}