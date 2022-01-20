using Microsoft.Xna.Framework;
using System;

namespace throw_apple2 {
    class Player : Entity {
        private bool _forward;
        private bool _back;
        private float _speed = 0.4f;

        public Player(Game game, string texturename, Vector2 position) : base(game, texturename, position) {
            _rotation = -MathHelper.Pi / 2;
            _speed = 2f;
        }

        public override void Update() {
            float aX = MathF.Cos(_rotation) * _speed;
            float aY = MathF.Sin(_rotation) * _speed;
            //forward
            if (_forward) {
                _velocity.X = aX;
                _velocity.Y = aY;
            } else if (_back) {
                _velocity.X = -aX;
                _velocity.Y = -aY;
            } else {
                _velocity.X = 0;
                _velocity.Y = 0;
            }
            base.Update();
        }
        public void Size(int s) {         
            HalfHeight = s;
            HalfWidth = s;
        }
        public double playerAngle(Player a)
        {
            return Math.Atan2(Position.Y - a.Position.Y, a.Position.X - Position.X);
        }
        
        public double cornerAngle()
        {
            return Math.Atan2(HalfHeight, HalfWidth);
        }
        public bool Forward { get => _forward; set => _forward = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public bool Back { get => _back; set => _back = value; }
    }
}
