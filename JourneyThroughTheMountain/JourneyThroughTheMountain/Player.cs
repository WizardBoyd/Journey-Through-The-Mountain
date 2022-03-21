﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TileEngine;

namespace JourneyThroughTheMountain
{
    public class Player : GameObject, IGameObjectWithHealth, IGameObjectWithDamage
    {
        public Vector2 fallSpeed = new Vector2(0, 20);
        private float moveScale = 180.0f;
        public float FallTreshold = 6f;
        private float LastFallSpeed;
        public bool TakeDamageOnLand;
        private bool dead = false;
        private int score = 0;
        private int livesRemaining = 3;
        private int Damage_Scale = 1;
        //public Rectangle triggercollision;


        public bool Dead
        {
            get { return dead; }
        }

        //public Rectangle TriggerCollision
        //{
        //    get
        //    {
        //        return new Rectangle(
        //         (int)worldLocation.X + triggercollision.X,
        //         (int)WorldRectangle.Y + triggercollision.Y,
        //         triggercollision.Width,
        //         triggercollision.Height);
        //    }

        //    set
        //    {
        //        triggercollision = value;
        //    }
        //}

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

        public int Damage { get; set; }


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
            //CollisionRectangle = new Rectangle(0, 0, 30, 46);
            _boundingboxes.Add(new BoundingBox(new Vector2(0,0), 30, 46));
            _triggerboxes.Add( new BoundingBox(new Vector2(0, 0), (int)_boundingboxes[0].Width + 1, (int)_boundingboxes[0].Height + 10));

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
                    if ((LastFallSpeed - velocity.Y)/gameTime.ElapsedGameTime.TotalSeconds > FallTreshold)
                    {
                        TakeDamageOnLand = true;
                    }

                    LastFallSpeed = velocity.Y;
                }
                else
                {
                    TakeDamageOnLand = false;
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
            spriteBatch.Draw(Game1.BoundingBox, Camera.WorldToScreen(new Rectangle((int)TriggerBoxes[0].Position.X, (int)TriggerBoxes[0].Position.Y,
                (int)TriggerBoxes[0].Width, (int)TriggerBoxes[0].Height)), Color.White);

            //TileMap.DrawRectangle(spriteBatch, new Rectangle(1,1,32,32), 100);
            System.Diagnostics.Debug.WriteLine(WorldLocation);
            System.Diagnostics.Debug.WriteLine(Camera.Position);
            base.Draw(spriteBatch);
        }

        public override void OnNotify(BaseGameStateEvent Event)
        {
            switch (Event)
            {
                case GameplayEvents.PlayerFallDamage m:
                    TakeDamage(m.Damage);
                    break;
            }
            
        }

        public void TakeDamage(IGameObjectWithDamage o)
        {
            Health -= o.Damage;
        }

        public void TakeDamage(int Amount)
        {
            Health -= Amount;
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

                //WorldLocation = new Vector2(
                //    int.Parse(code[2]) * TileMap.TileWidth,
                //    int.Parse(code[3]) * TileMap.TileHeight);

                LevelManager.RespawnLocation = WorldLocation; // SEE THIS JUST KEEP EYE ON IT

                velocity = Vector2.Zero;
            }
        }

        public int CalculateFallDamage(GameTime time)
        {
            double a = (LastFallSpeed - velocity.Y) / time.ElapsedGameTime.TotalSeconds;

             if( a > FallTreshold)
            {
                return (int)a * Damage_Scale;
            }

            return 0;
        }

        #endregion

    }
}

