using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace throw_apple2 {
    class Entity {
        protected Vector2 _position, _velocity;
        protected float _rotation;
        protected Texture2D _texture;
        protected Color _color;
        protected float _radius;
        protected bool _isAlive;

        public Entity(Game game, string texturename, Vector2 position) {
            _position = position;
            _texture = game.Content.Load<Texture2D>(texturename);
            _radius = _texture.Width / 2;
            _color = Color.White;
            _isAlive = true;
        }

        public virtual void Update() {
            Position += _velocity;
        }
        

        public Rectangle getRectangle() {
            return new Rectangle(_position.ToPoint(), new Point((int)_radius * 2, (int)_radius * 2));
        }
        public Vector2 getCenter() {
            return new Vector2(_radius, _radius);
        }

        public float Radius { get => _radius; set => _radius = value; }
        public Color Color { get => _color; set => _color = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float Rotation { get => _rotation; set => _rotation = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    }
}
