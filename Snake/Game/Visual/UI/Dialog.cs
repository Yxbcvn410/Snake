using System;
using System.Collections.Generic;
using Snake.Game.Classes;

namespace Snake.Game.UI {
    public class Dialog : Widget {
        private readonly TextBox _captionBox;

        public string Caption {
            set => _captionBox.Text = value;
        }

        private int _selectedIndex;
        private bool _selectionDone;

        public int SelectedIndex {
            get => _selectionDone ? _selectedIndex : -1;
            set {
                if (!_selectionDone && value < _variants.Length && value >= 0)
                    _selectedIndex = value;
            }
        }

        private TextBox[] _variants;

        public string[] Variants {
            set {
                _variants = new TextBox[value.Length];
                for (int i = 0; i < value.Length; i++)
                    _variants[i] = new TextBox(new Cell(0, i + 2)) {Alignment = TextAlignment.Center, Text = value[i]};

                _selectionDone = false;
                SubWidgets = new SortedSet<Widget>(_variants);
                SubWidgets.Add(_captionBox);
            }
        }

        public Dialog(Cell center) {
            RelativeLocation = center;
            _captionBox = new TextBox(new Cell(0, 0)) {Alignment = TextAlignment.Center};
            _variants = new TextBox[0];
            _selectedIndex = 0;
            ZIndex = 3;
            SubWidgets.Add(_captionBox);
        }

        public void ProcessKey(ConsoleKey key) {
            if (key == ConsoleKey.UpArrow && _selectedIndex > 0)
                _selectedIndex--;
            if (key == ConsoleKey.DownArrow && _selectedIndex < _variants.Length - 1)
                _selectedIndex++;
            if (key == ConsoleKey.Enter)
                _selectionDone = true;
        }

        protected override void UpdateSelf() {
            if (_selectedIndex >= _variants.Length || _selectedIndex < 0)
                _selectedIndex = 0;
        }


        protected override void DrawSelf(GraphicsContext cxt) {
            if (_selectedIndex < _variants.Length)
                cxt.WriteStr(new Cell(-_variants[_selectedIndex].Text.Length / 2 - 2, _selectedIndex + 2), "> ");
        }
    }
}