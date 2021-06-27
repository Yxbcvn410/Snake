#nullable enable
using System;
using System.Collections.Generic;
using Snake.Game.Classes;
using Snake.Game.Visual;

namespace Snake.Game.UI {
    public abstract class Widget : IComparable {
        protected SortedSet<Widget> SubWidgets = new SortedSet<Widget>();
        protected Cell RelativeLocation = new Cell(0, 0);

        public bool IsVisible = true;
        public bool IsActive = true;
        public int ZIndex = 0;

        public void Update() {
            if (!IsActive)
                return;

            foreach (var subWidget in SubWidgets) subWidget.Update();
            UpdateSelf();
        }

        protected virtual void UpdateSelf() {
        }

        public void Draw(GraphicsContext cxt) {
            if (!IsVisible)
                return;

            foreach (var subWidget in SubWidgets)
                subWidget.Draw(new BoxGraphicsContext(cxt, subWidget.RelativeLocation));
            DrawSelf(cxt);
        }

        protected abstract void DrawSelf(GraphicsContext cxt);
        public int CompareTo(object? obj) {
            if (!(obj is Widget other)) return 0;
            if (ZIndex != other.ZIndex) 
                return ZIndex.CompareTo(other.ZIndex);
            return GetHashCode().CompareTo(other.GetHashCode());
        }
    }
}