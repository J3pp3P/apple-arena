
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace throw_apple2 {
    public class Game1 : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth, _screenHeight, _screenCenterY, _screenCenterX;
        private Player _player1, _player2;
        private List<Wall> _walls = new List<Wall>();
        private List<Apple> _redApples = new List<Apple>();
        private List<Apple> _greenApples = new List<Apple>();
        private int _antalApplen = 3;
        private Wall _wall1;
        private Wall _wall2;
        private Wall _wall3;
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

            _wall1 = new Wall(this, "storträd", new Vector2(_screenCenterX, _screenCenterY));
            
            _wall2 = new Wall(this, "hinder2", new Vector2(0, 0));
            _wall2.Position = new Vector2(100, _screenHeight - _wall2.HalfHeight);
            _wall3 = new Wall(this, "hinder2", new Vector2(0, 0));
            _wall3.Position = new Vector2(_screenWidth - 100, _wall3.HalfHeight);

            _walls.Add(_wall1);
            _walls.Add(_wall2);
            _walls.Add(_wall3);

            _player1 = new Player(this, "rcap", new Vector2(0, _screenHeight));
            _player2 = new Player(this, "gcap", new Vector2(_screenWidth, 0));

            //redApple
            for (int i = 0; i < _antalApplen; i++) {
                Apple tempApple = new Apple(this, "träd", new Vector2(_player1.Position.X, _player1.Position.Y));
                _redApples.Add(tempApple);
            }


        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))Exit();
            KeyboardState _ks = Keyboard.GetState();
            

            if (_ks.IsKeyDown(Keys.Space)) {
                foreach (Apple a in _redApples) {
                    if (a.IsAlive) {
                        a._speed = 2;
                        a.Position = _player1.Position;
                    }
                }
                
            }
            






            move(_player1, _player2);
            collision(_walls, _player1);
            collision(_walls, _player2);
            playerCollision(_player2, _player1);
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
            for (int i = 0; i < _walls.Count; i++) {
                _spriteBatch.Draw(_walls[i].Texture, _walls[i].getRectangle(), null, _walls[i].Color, _walls[i].Rotation, _walls[i].getCenter(), SpriteEffects.None, 0f);
            }
            foreach (Apple a in _redApples) {
                _spriteBatch.Draw(a.Texture, a.getRectangle(), null, a.Color, a.Rotation, a.getCenter(), SpriteEffects.None, 0f);
            }
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
        private void collision(List<Wall> wall, Player b)
        {
            for (int i = 0; i < wall.Count; i++) {
                if (b.Position.Y + b.HalfHeight > wall[i].Position.Y - wall[i].HalfHeight &&
                    b.Position.Y - b.HalfHeight < wall[i].Position.Y + wall[i].HalfHeight &&
                    b.Position.X + b.HalfWidth > wall[i].Position.X - wall[i].HalfWidth &&
                    b.Position.X - b.HalfWidth < wall[i].Position.X + wall[i].HalfWidth) {
                    double angle = wall[i].playerAngle(b);
                    double cornerAngle = wall[i].cornerAngle();

                    //krockar från ovansidan
                    if (angle > cornerAngle && angle < Math.PI - cornerAngle) {
                        b.Position = new Vector2(b.Position.X, wall[i].Position.Y - wall[i].HalfHeight - b.HalfHeight);
                    }
                    //krokar från vänster sida
                    else if (angle > Math.PI - cornerAngle || angle < cornerAngle - Math.PI) {
                        b.Position = new Vector2(wall[i].Position.X - wall[i].HalfWidth - b.HalfWidth, b.Position.Y);
                    }
                    //krockar från nedansidan
                    else if (angle < -cornerAngle && angle >  cornerAngle - Math.PI) {
                        b.Position = new Vector2(b.Position.X, wall[i].Position.Y + wall[i].HalfHeight + b.HalfHeight);
                    }
                    //krockar från höger sida
                    else if (angle < cornerAngle && angle > -cornerAngle) {
                        b.Position = new Vector2(wall[i].Position.X + wall[i].HalfWidth + b.HalfWidth, b.Position.Y);
                    }
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
                }

                //fixade kollision (typ)

                if (distance < p1.HalfHeight + p2.HalfHeight)
                {
                    double angle2 = p2.playerAngle(p1);
                    double cornerAngle2 = Math.PI / 4;
                    if (p2.Velocity.X == 0 || p2.Velocity.Y == 0)
                    {
                        //krockar från ovansidan
                        if (angle2 > cornerAngle && angle < Math.PI - cornerAngle2)
                        {
                            p1.Position = new Vector2(p1.Position.X, p2.Position.Y - p2.HalfHeight - p1.HalfHeight);
                        }
                        //krokar från vänster sida
                        else if (angle2 > Math.PI - cornerAngle2|| angle2 < cornerAngle2 - Math.PI)
                        {
                            p1.Position = new Vector2(p2.Position.X - p2.HalfWidth - p1.HalfWidth, p1.Position.Y);
                        }
                        //krockar från nedansidan
                        else if (angle2 < -cornerAngle2 && angle > cornerAngle2 - Math.PI)
                        {
                            p1.Position = new Vector2(p1.Position.X, p2.Position.Y + p2.HalfHeight + p1.HalfHeight);
                        }
                        //krockar från höger sida
                        else if (angle2 < cornerAngle2 && angle > -cornerAngle2)
                        {
                            p1.Position = new Vector2(p2.Position.X + p2.HalfWidth + p1.HalfWidth, p1.Position.Y);
                        }
                    }
                }
            }
        }
    }
}
