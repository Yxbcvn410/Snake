using System;

namespace Snake {
    internal static class _ {
        private static void Main() {
            var game = new Game.Game(40, 20, 100);
            while (true) {
                ConsoleKey key = Console.ReadKey(true).Key;
                game.Ui.ProcessKey(key);
            }
        }
    }
}