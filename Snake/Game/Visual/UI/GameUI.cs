using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Game.Classes;
using Snake.Game.UI;

namespace Snake.Game.Visual.UI {
    public class GameUi : Widget {
        private Terrain _terrain;
        private readonly int _width, _height;
        public static TextBox ScoreCounter;
        private TextBox _pauseBox, _helpBox;
        private Dialog _continueDialog, _exitDialog;

        public GameUi(int width, int height, Cell location) {
            RelativeLocation = location;
            _width = width;
            _height = height;
            InitUi();
        }

        private void InitUi() {
            const int margin = 3;
            _terrain = new Terrain(new Cell(margin, margin), _width - 2 * margin, _height - 2 * margin);

            ScoreCounter = new TextBox(new Cell(_width / 2, margin - 1))
                {Alignment = TextAlignment.Center};

            _pauseBox = new TextBox(new Cell(_width / 2, _height / 2))
                {Text = "--- PAUSED ---", Alignment = TextAlignment.Center, IsVisible = false, ZIndex = 2};

            _helpBox = new TextBox(new Cell(_width / 2, _height - margin + 1))
                {Text = "P: Pause    ESC: Quit", Alignment = TextAlignment.Center};

            _continueDialog = new Dialog(new Cell(_width / 2, _height / 2))
                {Variants = new[] {"Yes", "No"}, Caption = "Game over. Play again?", IsVisible = false};

            _exitDialog = new Dialog(new Cell(_width / 2, _height / 2))
                {Variants = new[] {"Yes", "No"}, Caption = "Do you want to quit the game?", IsVisible = false};

            SubWidgets = new SortedSet<Widget> {
                _terrain,
                ScoreCounter,
                _pauseBox,
                _helpBox,
                _continueDialog,
                _exitDialog
            };
        }

        private IEnumerable<Dialog> VisibleDialogs => SubWidgets.OfType<Dialog>().Where(dialog => dialog.IsVisible);

        public void ProcessKey(ConsoleKey key) {
            foreach (var visibleDialog in VisibleDialogs) {
                visibleDialog.ProcessKey(key);
                return;
            }

            switch (key) {
                case ConsoleKey.UpArrow:
                    _terrain.Snake.Direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    _terrain.Snake.Direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    _terrain.Snake.Direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    _terrain.Snake.Direction = Direction.Right;
                    break;
                case ConsoleKey.P:
                    _terrain.IsActive = !_terrain.IsActive;
                    break;
                case ConsoleKey.Escape:
                    _terrain.IsActive = false;
                    _exitDialog.IsVisible = true;
                    _exitDialog.SelectedIndex = 0;
                    break;
            }
        }

        protected override void UpdateSelf() {
            _pauseBox.IsVisible = !VisibleDialogs.Any() && _terrain.IsActive == false;
            if (_continueDialog.IsVisible && _continueDialog.SelectedIndex != -1) {
                if (_continueDialog.SelectedIndex == 0) {
                    _continueDialog.IsVisible = false;
                    InitUi();
                }
                else {
                    Environment.Exit(0);
                }
            }
            else if (_exitDialog.IsVisible && _exitDialog.SelectedIndex != -1) {
                if (_exitDialog.SelectedIndex == 0) {
                    Environment.Exit(0);
                }
                else {
                    _exitDialog.IsVisible = false;
                }
            }
            else {
                var hitItems = _terrain.FieldItems.Where(item => item.Cell == _terrain.Snake.Head);
                foreach (var item in hitItems)
                    item.ApplyBehaviour();

                if (!_terrain.Snake.IsDead) return;

                _terrain.IsActive = false;
                _continueDialog.IsVisible = true;
            }
        }

        protected override void DrawSelf(GraphicsContext cxt) {
        }
    }
}