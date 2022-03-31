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
    public class Player : GameObject, IGameObjectWithHealth, IGameObjectWithDamage
    {
        public Vector2 fallSpeed = new Vector2(0, 20);
        private Vector2 Climbspeed = new Vector2(0, 10);
        private float moveScale = 180.0f;
        public float FallTreshold = 10.5f;
        private float LastFallSpeed;
        public bool TakeDamageOnLand;
        private bool dead = false;
        private int score = 0;
        private int livesRemaining = 3;
        private float Damage_Scale = 0.2f;
        public bool Attacking;
        private string newAnimation;
        static KeyboardState previousstate;
       static KeyboardState CurrentState;
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

            animations.Add("Attack", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_attack1"), 48, "Attack"));
            animations["Attack"].LoopAnimation = false;

            animations.Add("Climbing", new AnimationStrip(content.Load<Texture2D>(@"Animations/Player/Woodcutter_climb"), 48, "Climbing"));


            frameWidth = 48;
            frameHeight = 48;
            //CollisionRectangle = new Rectangle(0, 0, 30, 46);
            _boundingboxes.Add(new BoundingBox(new Vector2(0,0), 30, 46));
            _triggerboxes.Add( new BoundingBox(new Vector2(0, 0), (int)_boundingboxes[0].Width + 1, (int)_boundingboxes[0].Height + 10));

            drawDepth = 0.7f;
            Health = 10;
            enabled = true;
            codeBasedBlocks = false;
            PlayAnimation("idle");
            Damage = 2;
        }
        #endregion

        #region Public Methods
        public static bool IsKeyPressed(Keys key, bool Oneshot)
        {
            if (!Oneshot) return CurrentState.IsKeyDown(key);
            return CurrentState.IsKeyDown(key) && !previousstate.IsKeyDown(key);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled && animations[currentAnimation].FinishedPlaying)
            {
                return;
            }
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Dead)
            {
                string newAnimation = "idle";
                Attacking = false;

                velocity = new Vector2(0, velocity.Y);

                GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
                KeyboardState keyState = Keyboard.GetState();
                CurrentState = keyState;
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

                if (CurrentState.IsKeyDown(Keys.E))
                {
                    newAnimation = "Attack";
                    if (IsKeyPressed(Keys.E, true))
                    {
                        Attacking = true;
                    }

                }

                if (keyState.IsKeyDown(Keys.S))
                {
                    Vector2 Location = TileMap.GetCellByPixel(new Vector2(WorldLocation.X, WorldLocation.Y));
                    //Impleemnt Climb logic
                    if (TileMap.CellCodeValue((int)Location.X + 1, (int)Location.Y + 2) == "LADDER")
                    {
                        newAnimation = "Climbing";
                        WorldLocation = new Vector2(WorldLocation.X, WorldLocation.Y + 1);
                    }
                    else
                    {
                        fallSpeed = new Vector2(0, 20);
                    }


                }

                if (keyState.IsKeyDown(Keys.W))
                {
                    Vector2 Location = TileMap.GetCellByPixel(new Vector2(WorldLocation.X, WorldLocation.Y));
                    if (TileMap.CellCodeValue((int)Location.X + 1, (int)Location.Y - 2) == "LADDER")
                    {
                        //newAnimation = "Climbing";
                        WorldLocation = new Vector2(WorldLocation.X, WorldLocation.Y - 1);
                        newAnimation = "Climbing";
                        fallSpeed = Vector2.Zero;
                    }
                    else
                    {
                        fallSpeed = new Vector2(0, 20);
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

            previousstate = CurrentState;
            repositionCamera();
            base.Update(gameTime);
        }

        public void MoveRight()
        {
            flipped = false;
            newAnimation = "run";
            velocity = new Vector2(moveScale, velocity.Y);
        }

        public void MoveLeft()
        {
            flipped = true;
            newAnimation = "run";
            velocity = new Vector2(-moveScale, velocity.Y);
        }

        public void Jump()
        {
            newAnimation = "Jump";
            velocity.Y = -500;
        }

        public void Kill()
        {
            PlayAnimation("Die");
            LivesRemaining--;
            velocity.X = 0;
            dead = true;
        }

        public void ClimbUp()
        {
            Vector2 Location = TileMap.GetCellByPixel(new Vector2(WorldLocation.X, WorldLocation.Y));
            if (TileMap.CellCodeValue((int)Location.X + 1, (int)Location.Y - 2) == "LADDER")
            {
                //newAnimation = "Climbing";
                WorldLocation = new Vector2(WorldLocation.X, WorldLocation.Y - 1);
                newAnimation = "Climbing";
                fallSpeed = Vector2.Zero;
            }
            else
            {
                fallSpeed = new Vector2(0, 20);
            }
        }

        public void ClimbDown()
        {
            Vector2 Location = TileMap.GetCellByPixel(new Vector2(WorldLocation.X, WorldLocation.Y));
            //Impleemnt Climb logic
            if (TileMap.CellCodeValue((int)Location.X + 1, (int)Location.Y + 2) == "LADDER")
            {
                newAnimation = "Climbing";
                WorldLocation = new Vector2(WorldLocation.X, WorldLocation.Y + 1);
            }
            else
            {
                fallSpeed = new Vector2(0, 20);
            }
        }

        public void Attack()
        {
            newAnimation = "Attack";
           
            Attacking = true;
            
        }

        public void Revive()
        {
            PlayAnimation("idle");
            dead = false;
        }


        public override void OnNotify(BaseGameStateEvent Event)
        {
            switch (Event)
            {
                case GameplayEvents.PlayerFallDamage m:
                    if (m.Damage > 0)
                    {
                        TakeDamage(m.Damage);
                    }
                    break;
                case GameplayEvents.PlayerCoinPickupEvent m:
                    score++;
                    break;
                case GameplayEvents.DamageDealt m:
                    if (m.Damage > 0)
                    {
                        TakeDamage(m.Damage);
                        KnockBack();
                    }
                    break;
                case GameplayEvents.PlayerKilledEnemyEvent m:
                    score++;
                    break;
            }
            
        }

        public void TakeDamage(IGameObjectWithDamage o)
        {
            Health -= o.Damage;
            animations[currentAnimation].Tint = Color.Red;
            KnockBack();
            
        }

        public void TakeDamage(int Amount)
        {
            Health -= Amount;
            animations[currentAnimation].Tint = Color.Red;
            
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
            double a = (LastFallSpeed - velocity.Y) * time.ElapsedGameTime.TotalSeconds;

             if( a > FallTreshold)
            {
                return (int)(a * Damage_Scale);
            }

            return 0;
        }

        #endregion

    }
}

