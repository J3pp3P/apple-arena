using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace throw_apple2 {
    class Entity {
        protected Vector2 _position, _velocity;
        protected float _rotation;
        protected Texture2D _texture;
        protected Color _color;
        protected float _halfWidth;
        private float _halfHeight;
        protected bool _isAlive;
        private int _downSide;
        private int _leftSide;
        private int _rightSide;
        private int _upSide;

        public Entity(Game game, string texturename, Vector2 position) {
            _position = position;
            _texture = game.Content.Load<Texture2D>(texturename);
            _halfWidth = _texture.Width / 2;
            _halfHeight = _texture.Height / 2;
            _color = Color.White;
            _isAlive = true;
            _downSide = _texture.Height;
            _upSide = (int)-_halfWidth;
            _leftSide = (int)-_halfWidth;
            _rightSide = _texture.Width;
        }

        public virtual void Update() {
            Position += _velocity;
        }

        public Rectangle getRectangle() {
            return new Rectangle(_position.ToPoint(), new Point((int)_halfWidth * 2, (int)_halfWidth * 2));
        }
        public Vector2 getCenter() {
            return new Vector2(_halfWidth, _halfWidth);
        }

        public float HalfWidth { get => _halfWidth; set => _halfWidth = value; }
        public Color Color { get => _color; set => _color = value; }
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public float Rotation { get => _rotation; set => _rotation = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        public int UpSide { get => _upSide; set => _upSide = value; }
        public int RightSide { get => _rightSide; set => _rightSide = value; }
        public int LeftSide { get => _leftSide; set => _leftSide = value; }
        public int DownSide { get => _downSide; set => _downSide = value; }
        protected float HalfHeight { get => _halfHeight; set => _halfHeight = value; }
    }
}
