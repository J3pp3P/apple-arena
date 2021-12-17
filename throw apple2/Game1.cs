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
        private Player _player;
        private Wall _wall1;
        private int _playerSize = 100;
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

            _player = new Player(this, "player1", new Vector2(0, 0));
            _player.Size(20);
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

            if (_player.Position.Y + _player.HalfHeight > _wall1.Position.Y - _wall1.HalfHeight &&
                _player.Position.Y - _player.HalfHeight < _wall1.Position.Y + _wall1.HalfHeight &&
                _player.Position.X + _player.HalfWidth > _wall1.Position.X - _wall1.HalfWidth &&
                _player.Position.X - _player.HalfWidth < _wall1.Position.X + _wall1.HalfWidth) {
                double angle = _wall1.playerAngle(_player);
                double cornerAngle = _wall1.cornerAngle();
                
                if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                    _player.Position = new Vector2(_player.Position.X, _wall1.Position.Y - _wall1.HalfWidth - _player.HalfWidth);
                    
                } else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI ) {
                    _player.Position = new Vector2(_wall1.Position.X - _wall1.HalfWidth - _player.HalfWidth, _player.Position.Y);
                    Debug.WriteLine(angle);
                } else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                    _player.Position = new Vector2(_player.Position.X, _wall1.Position.Y + _wall1.HalfWidth + _player.HalfWidth);
                } else if (angle < cornerAngle && angle > -cornerAngle) {
                    _player.Position = new Vector2(_wall1.Position.X + _wall1.HalfWidth + _player.HalfWidth, _player.Position.Y);
                }
            }



            /*
            if (_player.Position.Y < _wall1.Position.Y) {
                if (_player.Position.Y + _player.HalfWidth > _wall1.Position.Y - _wall1.HalfWidth &&
                    _player.Position.X - _player.HalfWidth < _wall1.Position.X + _wall1.HalfWidth &&
                    _player.Position.X + _player.HalfWidth > _wall1.Position.X - _wall1.HalfWidth) {
                    _player.Position = new Vector2(_player.Position.X, _wall1.Position.Y - _wall1.HalfWidth - _player.HalfWidth);
                }
            } else {
                if (_player.Position.Y - _player.HalfWidth < _wall1.Position.Y + _wall1.HalfWidth &&
                    _player.Position.X - _player.HalfWidth < _wall1.Position.X + _wall1.HalfWidth &&
                    _player.Position.X + _player.HalfWidth > _wall1.Position.X - _wall1.HalfWidth) {
                    _player.Position = new Vector2(_player.Position.X, _wall1.Position.Y + _wall1.HalfWidth + _player.HalfWidth);
                }
            }
                
            if (_player.Position.X < _wall1.Position.X) {
            if (_player.Position.X + _player.HalfWidth > _wall1.Position.X - _wall1.HalfWidth &&
                _player.Position.Y - _player.HalfWidth < _wall1.Position.Y + _wall1.HalfWidth &&
                _player.Position.Y + _player.HalfWidth > _wall1.Position.Y - _wall1.HalfWidth) {
                        
                    _player.Position = new Vector2(_player.Position.Y, _wall1.Position.X - _wall1.HalfWidth - _player.HalfWidth);
                }
            } else {
                if (_player.Position.X - _player.HalfWidth < _wall1.Position.X + _wall1.HalfWidth &&
                    _player.Position.Y - _player.HalfWidth < _wall1.Position.Y + _wall1.HalfWidth &&
                    _player.Position.Y + _player.HalfWidth > _wall1.Position.Y - _wall1.HalfWidth) {
                    _player.Position = new Vector2(_player.Position.Y, _wall1.Position.X + _wall1.HalfWidth + _player.HalfWidth);
                }
            }*/
            


            
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
    }
}
