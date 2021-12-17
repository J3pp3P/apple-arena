using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace throw_apple2 {
    class Wall : Entity {

        public Wall(Game game, string texturename, Vector2 position) : base(game, texturename, position) {
        }

        public double playerAngle(Entity a) {
            return Math.Atan2(Position.Y - a.Position.Y, a.Position.X - Position.X);
        }
        public double cornerAngle() {
            return Math.Atan2(HalfHeight, HalfWidth);
        }
    }
}
