    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace throw_apple2 {
    class Entity {
        protected Vector2 _position, _velocity;
        protected float _rotation;
        protected Texture2D _texture;
        protected Color _color;
        protected float _halfWidth;
        protected float _halfHeight;
        protected bool _isAlive;
        protected float _speed;

        public Entity(Game game, string texturename, Vector2 position) {
            _position = position;
            _texture = game.Content.Load<Texture2D>(texturename);
            _halfWidth = _texture.Width / 2;
            _halfHeight = _texture.Height / 2;
            _color = Color.White;
            _isAlive = true;
        }

        public virtual void Update() {
            Position += _velocity;
        }

        virtual public Rectangle getRectangle() {
            return new Rectangle(_position.ToPoint(), new Point((int)_halfWidth * 2, (int)_halfHeight * 2));
        }
        public Vector2 getCenter() {
            return new Vector2(_halfWidth, _halfHeight);
        }
        /*public double playerAngle(Entity a, Entity b){
            return Math.Atan2(b.Position.Y - a.Position.Y, a.Position.X - b.Position.X);
        }
        public double cornerAngle(Entity a){
            return Math.Atan2(a.HalfHeight, a.HalfWidth);
        }*/

        public float HalfWidth { get => _halfWidth; set => _halfWidth = value; }
        public Color Color { get => _color; set => _color = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float Rotation { get => _rotation; set => _rotation = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        public float HalfHeight { get => _halfHeight; set => _halfHeight = value; }
    }
}
