using System;
using System.Diagnostics;
using System.Timers;
using Snake.Game.Classes;
using Snake.Game.UI;
using Snake.Game.Visual;
using Snake.Game.Visual.UI;

namespace Snake.Game {
    public class Game : Widget {
        public readonly GameUi Ui;

        public Game(int width, int height, int difficulty) {
            Console.Title = "Snake";
            Console.CursorVisible = false;
            GraphicsBackend.Init(Console.WindowWidth, Console.WindowHeight);

            int mw = (Console.WindowWidth - width) / 2, mh = (Console.WindowHeight - height) / 2;
            Ui = new GameUi(width, height, new Cell(mw, mh));
            SubWidgets.Add(Ui);

            SubWidgets.Add(
                new TextBox(new Cell(Console.WindowWidth / 2, Console.WindowHeight - 1)) {
                    Text = @"© Yxbcvn410, 2021"
                }
                );
            
            var timer = new Timer {Interval = difficulty};
            timer.Elapsed += (sender, args) => {
                try {
                    Update();
                    Draw(new GraphicsContext());
                    GraphicsBackend.Flush();
                }
                catch (Exception e) {
                    Debug.WriteLine(e);
                    throw;
                }
            };
            timer.Start();
        }

        protected override void DrawSelf(GraphicsContext cxt) { }
    }
}