﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System; 


namespace OhMuhGawwVidjaGames
{        public class Ball : Sprite
        {

            public Boolean showTutorial = true; 

            private Paddle _attachedToPaddle;
            
            private float hitCount = 0f; 
            //private readonly
            public Ball(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
                : base(texture, location, gameBoundaries)
            {
                
            }
            public void AttachTo(Paddle paddle)
            {
                _attachedToPaddle = paddle; 
                Location.X = _attachedToPaddle.Location.X + _attachedToPaddle.Width + 1f;
                Location.Y = _attachedToPaddle.Location.Y + ((_attachedToPaddle.Height / 2) - (Height / 2));
                Velocity = Vector2.Zero; 
            }
            protected override void CheckBounds()
            {
                if ((Location.Y >= gameBoundaries.Height - _texture.Height) || Location.Y <= 0)
                { 
                    var newVelocity = new Vector2(Velocity.X, -Velocity.Y);
                    Velocity = newVelocity; 
                }
            }
            public void launchBall()
            {
                if (_attachedToPaddle != null)
                {
                    showTutorial = false;
                    var newVelocity = new Vector2(10f, _attachedToPaddle.Velocity.Y * .75f);
                    Velocity = newVelocity;
                    _attachedToPaddle = null;
                }
            }
            public override void Update(GameTime gameTime, GameObjects gameObjects)
            {

                if (_attachedToPaddle != null)
                {
                    Location.X = _attachedToPaddle.Location.X + _attachedToPaddle.Width + 1f;
                    Location.Y = _attachedToPaddle.Location.Y + ((_attachedToPaddle.Height/2) - (Height/2));
                    hitCount = 0; 
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        launchBall();
                    }
                }
                else if (boundingBox.Intersects(gameObjects.PlayerPaddle.boundingBox))
                    {

                        Velocity = new Vector2(-Velocity.X, ((gameObjects.PlayerPaddle.Velocity.Y *.75f) + Velocity.Y * .5f));
                    }

                else if( boundingBox.Intersects(gameObjects.ComputerPaddle.boundingBox))
                       {

                           Velocity = new Vector2(-Velocity.X - hitCount, ((gameObjects.ComputerPaddle.Velocity.Y * .75f) + Velocity.Y * .5f));
                           hitCount = hitCount + 0.1f; 
                    }
                base.Update(gameTime, gameObjects);
            }
        }
    }