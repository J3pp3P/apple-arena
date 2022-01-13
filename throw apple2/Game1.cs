using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace throw_apple2 {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth, _screenHeight, _screenCenterY, _screenCenterX;
        private Player _player1, _player2;
        private Wall _wall1;
        private int _playerSize = 20;
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

            _player1 = new Player(this, "player1", new Vector2(0, 0));
            _player2 = new Player(this, "player2", new Vector2(_screenWidth, 0));
            //_player.Size(_playerSize);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();
            KeyboardState _ks = Keyboard.GetState();
            //forward
            if (_ks.IsKeyDown(Keys.W)) {
                _player1.Forward = true;
            } else {
                _player1.Forward = false;
            }
            //backwards
            if (_ks.IsKeyDown(Keys.S)) {
                _player1.Back = true;
            } else {
                _player1.Back = false;
            }
            //rotation
            if (_ks.IsKeyDown(Keys.A)) {
                _player1.Rotation -= 0.1f;
            } else if (_ks.IsKeyDown(Keys.D)) {
                _player1.Rotation += 0.1f;
            }

            //player2
            if (_ks.IsKeyDown(Keys.Up)) {
                _player2.Forward = true;
            }
            else {
                _player2.Forward = false;
            }
            //backwards
            if (_ks.IsKeyDown(Keys.Down)) {
                Debug.WriteLine("hej");
                _player2.Back = true;
            }else {
                _player2.Back = false;
            }
            //rotation
            if (_ks.IsKeyDown(Keys.Left)) {
                _player2.Rotation -= 0.1f;
            }
            else if (_ks.IsKeyDown(Keys.Right)) {
                _player2.Rotation += 0.1f;
            }

           
            collision(_wall1, _player1);
            collision(_wall1, _player2);
            worldBounds(_player1);
            worldBounds(_player2);
            _player1.Update();
            _player2.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player1.Texture, _player1.getRectangle(), null, _player1.Color, _player1.Rotation, _player1.getCenter(), SpriteEffects.None, 0f);
            _spriteBatch.Draw(_player2.Texture, _player2.getRectangle(), null, _player2.Color, _player2.Rotation, _player2.getCenter(), SpriteEffects.None, 0f);
            _spriteBatch.Draw(_wall1.Texture, _wall1.getRectangle(), null, _wall1.Color, _wall1.Rotation, _wall1.getCenter(), SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        

        private void worldBounds(Entity e) {
            if (e.Position.X - e.HalfWidth < 0) {
                e.Position = new Vector2(e.HalfWidth, e.Position.Y);
            } else if (e.Position.X + e.HalfWidth > _screenWidth) {
                e.Position = new Vector2(_screenWidth - e.HalfWidth, e.Position.Y);
            }
            if (e.Position.Y - e.HalfWidth < 0) {
                e.Position = new Vector2(e.Position.X, 0 + e.HalfWidth);
            } else if (e.Position.Y > _screenHeight - e.HalfWidth) {
                e.Position = new Vector2(e.Position.X, _screenHeight - e.HalfWidth);
            }
        }
        private void collision(Wall a, Player b)
        {
            if (b.Position.Y + b.HalfHeight > a.Position.Y - a.HalfHeight &&
                b.Position.Y - b.HalfHeight < a.Position.Y + a.HalfHeight &&
                b.Position.X + b.HalfWidth > a.Position.X - a.HalfWidth &&
                b.Position.X - b.HalfWidth < a.Position.X + a.HalfWidth) {
                double angle = a.playerAngle(b);
                double cornerAngle = a.cornerAngle();

                if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                    b.Position = new Vector2(b.Position.X, a.Position.Y - a.HalfHeight - b.HalfHeight);

                }
                else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI) {
                    b.Position = new Vector2(a.Position.X - a.HalfWidth - b.HalfWidth, b.Position.Y);
                }
                else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                    b.Position = new Vector2(b.Position.X, a.Position.Y + a.HalfHeight + b.HalfHeight);
                }
                else if (angle < cornerAngle && angle > -cornerAngle) {
                    b.Position = new Vector2(a.Position.X + a.HalfWidth + b.HalfWidth, b.Position.Y);
                }
            }
        }
    }
}
