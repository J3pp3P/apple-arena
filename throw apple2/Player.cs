using Microsoft.Xna.Framework;
using System;

namespace throw_apple2 {
    class Player : Entity {
        private bool _thrust;
        private float _speed = 0.4f;

        public Player(Game game, string texturename, Vector2 position) : base(game, texturename, position) {

            _rotation = MathHelper.Pi / 2;
            _speed = 3f;


        }

        public override void Update() {
            float aX = MathF.Cos(_rotation) * _speed;
            float aY = MathF.Sin(_rotation) * _speed;
            if (_thrust) {
                _velocity.X = aX;
                _velocity.Y = aY;
            } else {
            _velocity.X = 0;
            _velocity.Y = 0;

            }
            //friktiones
            base.Update();
        }
        public bool Thrust { get => _thrust; set => _thrust = value; }
        public float Speed { get => _speed; set => _speed = value; }
    }
}
