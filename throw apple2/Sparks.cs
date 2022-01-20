using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace throw_apple2
{
    class Sparks : Entity
    {
        public Sparks(Game game, string texturename, Vector2 position) : base(game, texturename, position)
        {

        }

        public void DrawParticles (SpriteBatch spritebatch)
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
    class particle : Entity
    {
        private float _speed;

        private float _direction;
        private float _TTL;
        private byte _alpha = 0;

        public particle(Game game, string texturename, Vector2 Position, float direction, int TTL, float speed ) : base(game, texturename, Position)
        {
            _TTL = TTL;
            _speed = speed;
            _direction = direction;
            _velocity = new Vector2(MathF.Cos(_direction) * speed, MathF.Sin(_direction)*speed);

        }
    }
}
