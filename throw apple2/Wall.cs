using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace throw_apple2 {
    class Wall : Entity {

        public Wall(Game game, string texturename, Vector2 position) : base(game, texturename, position) {
        }

        public void playerAngle(Entity a, Entity b) {
            getCenter();
            //float radian = Math.Atan2(b.Position.Y - a.Position.Y, b.Position.X - a.Position.X);
        }
    }
}
