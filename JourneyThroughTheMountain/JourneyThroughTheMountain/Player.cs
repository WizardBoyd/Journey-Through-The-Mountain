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
        private Vector2 fallSpeed = new Vector2(0, 20);
        private float moveScale = 180.0f;
        private bool dead = false;
        private int score = 0;
        private int livesRemaining = 3;
        public Rectangle triggercollision;

        public bool Dead
        {
            get { return dead; }
        }

        public Rectangle TriggerCollision
        {
            get
            {
                return new Rectangle(
                 (int)worldLocation.X + triggercollision.X,
                 (int)WorldRectangle.Y + triggercollision.Y,
                 triggercollision.Width,
                 triggercollision.Height);
            }

            set
            {
                triggercollision = value;
            }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int LivesRemaining
        {
            get { return livesRemaining; }
            set { livesRemaining = value; }
        }


        #region Constructor
        public Player(ContentManager content)
        {
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_idle"), 48, "idle"));

            animations["idle"].LoopAnimation = true;

            animations.Add("run", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_run"), 48, "run"));
            animations["run"].LoopAnimation = true;
            animations.Add("Jump", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_jump"), 48, "Jump"));

            animations["Jump"].LoopAnimation = false;
            animations["Jump"].FrameLength = 0.08f;
            animations["Jump"].NextAnimation = "idle";

            animations.Add("Die", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_death"), 48, "Die"));
            animations["Die"].LoopAnimation = false;

            frameWidth = 48;
            frameHeight = 48;
            collisionRectangle = new Rectangle(9, 1, 30, 46);
            TriggerCollision = collisionRectangle;
            triggercollision.Inflate(2.5f, 2.5f);

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

                if (keyState.IsKeyDown(Keys.Left) ||
                    (gamePad.ThumbSticks.Left.X < -0.3f))
                {
                    flipped = true;
                    newAnimation = "run";
                    velocity = new Vector2(-moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.Right) ||
                    (gamePad.ThumbSticks.Left.X > 0.3f))
                {
                    flipped = false;
                    newAnimation = "run";
                    velocity = new Vector2(moveScale, velocity.Y);
                }

                if (keyState.IsKeyDown(Keys.Space) ||
                    (gamePad.Buttons.A == ButtonState.Pressed))
                {
                    if (onGround)
                    {
                        Jump();
                        newAnimation = "Jump";
                    }
                }

                if (!onGround)
                {
                    checkLevelTransition();
                }


                if (currentAnimation == "Jump")
                    newAnimation = "Jump";

                if (newAnimation != currentAnimation)
                {
                    PlayAnimation(newAnimation);
                }
            }

            velocity += fallSpeed;

            repositionCamera();
            base.Update(gameTime);
        }

        public void Jump()
        {
            velocity.Y = -500;
        }

        public void Kill()
        {
            PlayAnimation("Die");
            LivesRemaining--;
            velocity.X = 0;
            dead = true;
        }

        public void Revive()
        {
            PlayAnimation("idle");
            dead = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.BoundingBox, TriggerCollision, Color.Red);
            base.Draw(spriteBatch);
        }

        #endregion

        #region Helper Methods
        private void repositionCamera()
        {
            int screenLocX = (int)Camera.WorldToScreen(worldLocation).X;
            int screenLocY = (int)Camera.WorldToScreen(worldLocation).Y;

            if (screenLocX > 500)
            {
                Camera.Move(new Vector2(screenLocX - 500, 0));
            }

            if (screenLocX < 200)
            {
                Camera.Move(new Vector2(screenLocX - 200, 0));
            }

            if (screenLocY > 500)
            {
                Camera.Move(new Vector2(0, screenLocY - 500));
            }

            if (screenLocY < 200)
            {
                Camera.Move(new Vector2(0, screenLocY - 200));
            }
        }

        private void checkLevelTransition()
        {
            Vector2 centerCell = TileMap.GetCellByPixel(WorldCenter);
            if (TileMap.CellCodeValue(centerCell).StartsWith("T_"))
            {
                string[] code = TileMap.CellCodeValue(centerCell).Split('_');

                if (code.Length != 4)
                    return;

                LevelManager.LoadLevel(int.Parse(code[1]));

                WorldLocation = new Vector2(
                    int.Parse(code[2]) * TileMap.TileWidth,
                    int.Parse(code[3]) * TileMap.TileHeight);

                LevelManager.RespawnLocation = WorldLocation;

                velocity = Vector2.Zero;
            }
        }

        #endregion

    }
}

