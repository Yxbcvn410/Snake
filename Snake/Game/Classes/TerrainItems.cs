using System.Linq;
using Snake.Game.Visual;

namespace Snake.Game.Classes {
    public class FoodItem : TerrainItem {
        public FoodItem(Cell point, Terrain terrain) : base(point, terrain) {
        }

        public override void ApplyBehaviour() {
            LifeTime = 0;
            Terrain.Score += 10;
            Terrain.Snake.Length += 1;
            if (Terrain.FieldItems.Count(item => item is FoodItem) == 1)
                Terrain.AddItem(new FoodItem(Terrain.FreeCell(), Terrain));
            if (Terrain.Snake.Length % 4 == 0 && Terrain.FieldItems.Count(item => item is LargeFoodItem) == 0)
                Terrain.AddItem(new LargeFoodItem(Terrain.FreeCell(), Terrain));
        }

        protected override void DrawSelf(GraphicsContext cxt) {
            cxt.WriteStr("0");
        }
    }

    public class LargeFoodItem : TerrainItem {
        private readonly int _totalLifetime;
        public LargeFoodItem(Cell point, Terrain terrain) : base(point, terrain, terrain.Diameter) {
            Terrain.LargeFoodIndicator.IsVisible = true;
            _totalLifetime = LifeTime;
        }

        public override void ApplyBehaviour() {
            LifeTime = 0;
            Terrain.Score += 40;
            Terrain.Snake.Length += 1;
            Terrain.LargeFoodIndicator.IsVisible = false;
        }

        protected override void UpdateSelf() {
            Terrain.LargeFoodIndicator.Value = (double) LifeTime / _totalLifetime;
        }

        protected override void DrawSelf(GraphicsContext cxt) {
            cxt.WriteStr(LifeTime % 2 == 0 ? "a" : "@");
        }
    }

    public class WallItem : TerrainItem {
        public WallItem(Cell point, Terrain terrain) : base(point, terrain) {
        }

        public override void ApplyBehaviour() {
            Terrain.Snake.IsDead = true;
            LifeTime = 0;
        }

        protected override void DrawSelf(GraphicsContext cxt) {
            // Console.BackgroundColor = ConsoleColor.White;
            cxt.WriteStr("\u2593");
        }
    }
}