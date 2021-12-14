using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace throw_apple2 {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth, _screenHeight, _screenCenterY, _screenCenterX;
        private Player _player;
        private Wall _wall1;
        private int playerRadius = 20;
        //d
        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;
            _screenCenterX = _screenWidth / 2;
            _screenCenterY = _screenHeight / 2;
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _wall1 = new Wall(this, "wall1", new Vector2(_screenCenterX, _screenCenterY));

            _player = new Player(this, "redHatRotated", new Vector2(0, 0));
            _player.Radius = playerRadius;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState _ks = Keyboard.GetState();

            //rotation
            

            //forward
            if (_ks.IsKeyDown(Keys.W)) {
                _player.Forward = true;
            } else {
                _player.Forward = false;
            }
            //backwards
            if (_ks.IsKeyDown(Keys.S)) {
                _player.Back = true;
            } else {
                _player.Back = false;
            }
            if (_ks.IsKeyDown(Keys.A)) {
                _player.Rotation -= 0.1f;
            } else if (_ks.IsKeyDown(Keys.D)) {
                _player.Rotation += 0.1f;
            }

            //wallcollision
            //left
            if (_player.Position.X > _wall1.Position.X - _wall1.Radius && _player.Position.Y < _wall1.Position.Y + _wall1.Radius) {
                _player.Position = new Vector2(_wall1.Position.X - _wall1.Radius, _player.Position.Y);
            } else if (true) {

            }
            //right

            //top

            //bottom

            //objectBounds(_player, _wall1);
            worldBounds(_player);
            _player.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player.Texture, _player.getRectangle(), null, _player.Color, _player.Rotation, _player.getCenter(), SpriteEffects.None, 0f);
            _spriteBatch.Draw(_wall1.Texture, _wall1.getRectangle(), null, _wall1.Color, _wall1.Rotation, _wall1.getCenter(), SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        

        private void worldBounds(Entity e) {
            if (e.Position.X - e.Radius < 0) {
                e.Position = new Vector2(e.Radius, e.Position.Y);
            } else if (e.Position.X + e.Radius > _screenWidth) {
                e.Position = new Vector2(_screenWidth - e.Radius, e.Position.Y);
            }
            if (e.Position.Y - e.Radius < 0) {
                e.Position = new Vector2(e.Position.X, 0 + e.Radius);
            } else if (e.Position.Y > _screenHeight - e.Radius) {
                e.Position = new Vector2(e.Position.X, _screenHeight - e.Radius);
            }
        }
        private void objectBounds(Entity a, Entity b) {
            
            if (a.Position.X + a.Radius < b.Position.X - b.Radius && 
                a.Position.Y < b.Position.Y - b.Radius && 
                a.Position.Y > b.Position.Y + b.Radius) {

                
                a.Position = new Vector2(b.Position.X + b.Radius, a.Position.Y);
            } 
            /*else if (a.Position.X + a.Radius > aw) {
                a.Position = new Vector2(_screenWidth - a.Radius, a.Position.Y);
            }
            if (a.Position.Y - a.Radius < 0) {
                a.Position = new Vector2(a.Position.X, 0 + a.Radius);
            } else if (a.Position.Y > _screenHeight - a.Radius) {
                a.Position = new Vector2(a.Position.X, _screenHeight - a.Radius);
            }*/
        } 
    }
}
