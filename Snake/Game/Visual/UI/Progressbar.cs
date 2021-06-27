using System;
using System.Linq;
using Snake.Game.Classes;

namespace Snake.Game.Visual.UI {
    public class Progressbar : Widget {
        public Progressbar(Cell location, int length) {
            Length = length;
            RelativeLocation = location;
        }

        public double Value = 0;
        private int Length;

        protected override void DrawSelf(GraphicsContext cxt) {
            int marks = (int) (Length * Value);
            cxt.WriteStr(String.Concat(Enumerable.Repeat("#", marks).Concat(Enumerable.Repeat("-", Length - marks))));
        }
    }
}