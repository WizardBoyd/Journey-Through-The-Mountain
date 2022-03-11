using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TileEngine;

namespace JourneyThroughTheMountain
{
    public class Player : GameObject
    {
        private Vector2 fallspeed = new Vector2(0, 20);
        private float moveScale = 180.0f;
        private bool dead = false;

        public bool Dead
        {
            get { return dead; }
        }

        #region Constuctors
        public Player(ContentManager content)
        {
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "idle"));
            animations.Add("run", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 64, "run"));
            animations.Add("Jump", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "Jump"));

            animations["Jump"].LoopAnimation = false;
            animations["Jump"].FrameLength = 0.08f;
            animations["Jump"].NextNaimation = "idle";

            animations.Add("Die", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "Die"));
            animations["Die"].LoopAnimation = false;

            frameWidth = 32;
            frameHeight = 32;
            collisionRectangle = new Rectangle(9, 1, 30, 46);

            drawDepth = 0.825f;

            enabled = true;
            codeBasedBlocks = false;
            PlayAnimation("idle");

        }
        #endregion

        #region Public Methods

        public override void Update(GameTime gameTime)
        {
            if (!Dead)
            {
                string newAnimation = "idle";

                velocity = new Vector2(0, velocity.Y);
                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.Left) || gamePad.ThumbSticks.Left.X < -0.3f)
                {
                    fliped = false;
                    newAnimation = "run";
                    velocity = new Vector2(-moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.Right) || gamePad.ThumbSticks.Left.X > 0.3f)
                {
                    fliped = true;
                    newAnimation = "run";
                    velocity = new Vector2(moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.Space)|| gamePad.Buttons.A == ButtonState.Pressed)
                {
                    if (onGround)
                    {
                        jump();
                        newAnimation = "Jump";
                    }
                }

                if (currentAnimation == "Jump")
                {
                    newAnimation = "Jump";
                }

                if (newAnimation != currentAnimation)
                {
                    PlayAnimation(newAnimation);
                }
            }

            velocity += fallspeed;

            RepositionCamera();
            base.Update(gameTime);
        }

        public void jump()
        {
            velocity.Y = -500;
        }

        #endregion

        #region Helper Methods
        private void RepositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;

            if (screenLocX > 500)
            {
                Camera.Move(new Vector2(screenLocX - 500, 0));
            }

            if (screenLocX < 200)
            {
                Camera.Move(new Vector2(screenLocX - 200, 0));
            }
        }
        #endregion
    }
}
