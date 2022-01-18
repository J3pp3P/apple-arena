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
            

            move(_player1, _player2);
            collision(_wall1, _player1);
            collision(_wall1, _player2);
            playerCollision(_player2, _player1);
            playerCollision(_player1, _player2);
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

        private void move(Player a, Player b)
        {
            KeyboardState _ks = Keyboard.GetState();
            if (_ks.IsKeyDown(Keys.W)) {
                a.Forward = true;
            }
            else {
                a.Forward = false;
            }
            //backwards
            if (_ks.IsKeyDown(Keys.S)) {
                a.Back = true;
            }
            else {
                a.Back = false;
            }
            //rotation
            if (_ks.IsKeyDown(Keys.A)) {
                a.Rotation -= 0.1f;
            }
            else if (_ks.IsKeyDown(Keys.D)) {
                a.Rotation += 0.1f;
            }

            //player2
            if (_ks.IsKeyDown(Keys.Up)) {
                b.Forward = true;
            }
            else {
                b.Forward = false;
            }
            //backwards
            if (_ks.IsKeyDown(Keys.Down)) {
                b.Back = true;
            }
            else {
                b.Back = false;
            }
            //rotation
            if (_ks.IsKeyDown(Keys.Left)) {
                b.Rotation -= 0.1f;
            }
            else if (_ks.IsKeyDown(Keys.Right)) {
                b.Rotation += 0.1f;
            }
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

                //krockar från ovansidan
                if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                    b.Position = new Vector2(b.Position.X, a.Position.Y - a.HalfHeight - b.HalfHeight);
                }
                //krokar från vänster sida
                else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI) {
                    b.Position = new Vector2(a.Position.X - a.HalfWidth - b.HalfWidth, b.Position.Y);
                }
                //krockar från nedansidan
                else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                    b.Position = new Vector2(b.Position.X, a.Position.Y + a.HalfHeight + b.HalfHeight);
                }
                //krockar från höger sida
                else if (angle < cornerAngle && angle > -cornerAngle) {
                    b.Position = new Vector2(a.Position.X + a.HalfWidth + b.HalfWidth, b.Position.Y);
                }
            }
        }
        private void playerCollision(Player p1, Player p2)
        {
            Vector2 delta = p1.Position - p2.Position;
            float distance = MathF.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
            if (distance < p1.HalfHeight + p2.HalfHeight) {
                double angle = p1.playerAngle(p2);
                double cornerAngle = Math.PI/4;
                if (p1.Velocity.X == 0 || p1.Velocity.Y == 0) {
                    //krockar från ovansidan
                    if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                        p2.Position = new Vector2(p2.Position.X, p1.Position.Y - p1.HalfHeight - p2.HalfHeight);
                    }
                    //krokar från vänster sida
                    else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI) {
                        p2.Position = new Vector2(p1.Position.X - p1.HalfWidth - p2.HalfWidth, p2.Position.Y);
                    }
                    //krockar från nedansidan
                    else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                        p2.Position = new Vector2(p2.Position.X, p1.Position.Y + p1.HalfHeight + p2.HalfHeight);
                    }
                    //krockar från höger sida
                    else if (angle < cornerAngle && angle > -cornerAngle) {
                        p2.Position = new Vector2(p1.Position.X + p1.HalfWidth + p2.HalfWidth, p2.Position.Y);
                    }
                } else if(p2.Velocity.X == 0 || p2.Velocity.Y == 0) {
                    //krockar från ovansidan
                    if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                        p1.Position = new Vector2(p1.Position.X, p2.Position.Y - p2.HalfHeight - p1.HalfHeight);
                    }
                    //krokar från vänster sida
                    else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI) {
                        p1.Position = new Vector2(p2.Position.X - p2.HalfWidth - p1.HalfWidth, p1.Position.Y);
                    }
                    //krockar från nedansidan
                    else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                        p1.Position = new Vector2(p1.Position.X, p2.Position.Y + p2.HalfHeight + p1.HalfHeight);
                    }
                    //krockar från höger sida
                    else if (angle < cornerAngle && angle > -cornerAngle) {
                        p1.Position = new Vector2(p2.Position.X + p2.HalfWidth + p1.HalfWidth, p1.Position.Y);
                    }
                }

            }
        }
    }
}
