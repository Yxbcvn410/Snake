using System;
using Snake.Game.Classes;
using Snake.Game.UI;

namespace Snake.Game.Visual.UI {
    public enum TextAlignment {
        Left,
        Center,
        Right
    }

    public class TextBox : Widget {
        public TextBox(Cell location) {
            RelativeLocation = location;
            Text = "";
            Alignment = TextAlignment.Center;
        }

        public string Text;
        public TextAlignment Alignment;

        protected override void DrawSelf(GraphicsContext cxt) {
            switch (Alignment) {
                case TextAlignment.Left:
                    cxt.WriteStr(Text);
                    break;
                case TextAlignment.Center:
                    cxt.WriteStr(new Cell(-Text.Length / 2, 0), Text);
                    break;
                case TextAlignment.Right:
                    cxt.WriteStr(new Cell(-Text.Length, 0), Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}