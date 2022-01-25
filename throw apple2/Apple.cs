using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace throw_apple2
{
    
    class Apple : Entity {
        Vector2 _direction;
        public float _speed;
        public bool _thrust;

        public Apple(Game game, string texturename, Vector2 position) : base(game, texturename, position) { 
       }

        public override void Update()
        {
            float aX = MathF.Cos(_rotation) * _speed;
            float aY = MathF.Sin(_rotation) * _speed;
                _velocity += new Vector2(aX, aY);
            base.Update();
        }
        public Vector2 Direction { get => _direction; set => _direction = value; }
    }
}
