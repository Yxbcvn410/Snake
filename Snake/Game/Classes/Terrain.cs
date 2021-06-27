using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Game.UI;
using Snake.Game.Visual;
using Snake.Game.Visual.UI;

namespace Snake.Game.Classes {
    public class Terrain : Widget {
        private readonly int _width;
        private readonly int _height;
        public readonly Snake Snake;
        public readonly Progressbar LargeFoodIndicator;

        public int Score = 0;
        public int Diameter => _width + _height;

        public HashSet<TerrainItem> FieldItems => SubWidgets.OfType<TerrainItem>().ToHashSet();

        public void AddItem(TerrainItem item) => SubWidgets.Add(item);

        public Terrain(Cell location, int width, int height) {
            RelativeLocation = location;
            _width = width;
            _height = height;
            Snake = new Snake();
            LargeFoodIndicator = new Progressbar(new Cell(0, height - 1), width);
            LargeFoodIndicator.IsVisible = false;
            LargeFoodIndicator.ZIndex = 1;
            SubWidgets.Add(Snake);
            SubWidgets.Add(LargeFoodIndicator);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                        SubWidgets.Add(new WallItem(new Cell(i, j), this));
                }
            }

            SubWidgets.Add(new FoodItem(FreeCell(), this));
        }

        protected override void DrawSelf(GraphicsContext cxt) {
            if (Snake.IsDead)
                cxt.WriteStr(Snake.Head, "%");
        }

        protected override void UpdateSelf() {
            foreach (TerrainItem terrainItem in FieldItems.Where(item => item.LifeTime > 0))
                terrainItem.LifeTime--;
            SubWidgets =
                new SortedSet<Widget>(SubWidgets.Where(widget => !(widget is TerrainItem item && item.LifeTime == 0)));

            if (!SubWidgets.OfType<LargeFoodItem>().Any())
                LargeFoodIndicator.IsVisible = false;
            GameUi.ScoreCounter.Text = $"Score: {Score}";
        }

        public Cell FreeCell() {
            var freeCells = new List<Cell>();
            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    var cell = new Cell(x, y);
                    if (FieldItems == null ||
                        !FieldItems.Select(item => item.Cell).Contains(cell) &&
                        !Snake.Contains(cell))
                        freeCells.Add(cell);
                }
            }

            return freeCells[new Random().Next(freeCells.Count)];
        }
    }

    public abstract class TerrainItem : Widget {
        internal int LifeTime;

        protected readonly Terrain Terrain;

        public Cell Cell => RelativeLocation;

        public abstract void ApplyBehaviour();

        public TerrainItem(Cell cell, Terrain terrain, int lifeTime = -1) {
            RelativeLocation = cell;
            Terrain = terrain;
            LifeTime = lifeTime;
        }
    }
}