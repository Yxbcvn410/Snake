using System;

namespace Snake {
    internal static class _ {
        private static void Main() {
            var game = new Game.Game(40, 20, 100);
            while (game.Ui.IsActive) {
                var key = Console.ReadKey(true).Key;
                game.Ui.ProcessKey(key);
            }
        }
    }
}