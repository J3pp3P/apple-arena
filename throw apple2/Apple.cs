using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace throw_apple2
{
    
    class Apple : Entity {
        Vector2 _direction;
        private int _speed;

        public Apple(Game game, string texturename, Vector2 position, int speed) : base(game, texturename, position) {
            Speed = speed;
        }

        public override void Update()
        {
            float aX = MathF.Cos(_rotation) * Speed;
            float aY = MathF.Sin(_rotation) * Speed;
                _velocity = new Vector2(aX, aY);
            base.Update();
        }
        public Vector2 Direction { get => _direction; set => _direction = value; }
        public int Speed { get => _speed; set => _speed=value; }
    }
}
