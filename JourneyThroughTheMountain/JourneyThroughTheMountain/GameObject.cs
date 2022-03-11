using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using TileEngine;

namespace JourneyThroughTheMountain
{
    public class GameObject
    {
        #region Declarations
        protected Vector2 worldLocation;
        protected Vector2 velocity;
        protected int frameWidth;
        protected int frameHeight;

        protected bool enabled;
        protected bool fliped = false;
        protected bool onGround;

        protected Rectangle collisionRectangle;
        protected int collideWidth;
        protected int collideHeight;
        protected bool codeBasedBlocks = true;

        protected float drawDepth = 0.85f;
        protected Dictionary<string, AnimationStrip> animations = new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;
        #endregion

        #region properties
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Vector2 WorldLocation
        {
            get { return worldLocation; }
            set { worldLocation = value; }
        }

        public Rectangle worldRectangle
        {
            get
            {
                return new Rectangle((int)worldLocation.X,
                    (int)worldLocation.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        public Vector2 WorldCenter
        {
            get {
                return new Vector2((int)worldLocation.X + (int)(frameWidth / 2),
              (int)worldLocation.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle((int)worldLocation.X + collisionRectangle.X,
                    (int)worldRectangle.Y + collisionRectangle.Y, collisionRectangle.Width, collisionRectangle.Height);
            }
            set
            {
                collisionRectangle = value;
            }
        }
        #endregion

        #region Helper Methods
        private void UpdateAnimation(GameTime gametime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextNaimation);
                }
                else
                {
                    animations[currentAnimation].Update(gametime);
                }
            }
        }
        #endregion

        #region Public Methods
        public void PlayAnimation(string name)
        {
            if (!(name == null) && animations.ContainsKey(name))
            {
                currentAnimation = name;
                animations[name].Play();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
            {
                return;
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateAnimation(gameTime);

            if (velocity.Y != 0)
            {
                onGround = false;
            }

            Vector2 moveAmount = velocity * elapsed;

            moveAmount = horizontalCollisionTest(moveAmount);
            moveAmount = verticalCollisionTest(moveAmount);

            Vector2 newPosition = worldLocation + moveAmount;

            newPosition = new Vector2(MathHelper.Clamp(newPosition.X, 0, Camera.WorldRectangle.Width - frameWidth),
                MathHelper.Clamp(newPosition.Y, 2 * (-TileMap.TileHeight),
                Camera.WorldRectangle.Height - frameHeight));

            WorldLocation = newPosition;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled)
            {
                return;
            }

            if (animations.ContainsKey(currentAnimation))
            {
                SpriteEffects effect = SpriteEffects.None;

                if (fliped)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                spriteBatch.Draw(animations[currentAnimation].Texture,
                    Camera.WorldToScreen(worldRectangle),//Camera.WorldToScreen(worldRectangle)
                    animations[currentAnimation].FrameRectangle,
                    Color.White, 0.0f, Vector2.Zero, effect, drawDepth);
            }
        }
        #endregion

        #region Map-Based Collision Detection Methods
        private Vector2 horizontalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.X == 0)
            {
                return moveAmount;
            }

            Rectangle afterMoveRect = collisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, 0);
            Vector2 corner1, corner2;

            if (moveAmount.X < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left, afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Left,
                    afterMoveRect.Bottom - 1);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Right, afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Right, afterMoveRect.Bottom - 1);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapcell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(mapCell1) || !TileMap.CellIsPassable(mapcell2))
            {
                moveAmount.X = 0;
                velocity.X = 0;
            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" || TileMap.CellCodeValue(mapcell2) == "BLOCK")
                {
                    moveAmount.X = 0;
                    velocity.X = 0;
                }
            }

            return moveAmount;
        }

        private Vector2 verticalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.Y == 0)
            {
                return moveAmount;
            }

            Rectangle afterMoveRect = collisionRectangle;
            afterMoveRect.Offset((int)moveAmount.X, (int)moveAmount.Y);
            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left + 1, afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right - 1, afterMoveRect.Top);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Left + 1, afterMoveRect.Bottom);
                corner2 = new Vector2(afterMoveRect.Right - 1, afterMoveRect.Bottom);
            }

            Vector2 MapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 MapCell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(MapCell1) || !TileMap.CellIsPassable(MapCell2))
            {
                if (moveAmount.Y > 0)
                {
                    onGround = true;
                    
                }
                moveAmount.Y = 0;
                velocity.Y = 0;
            }
            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(MapCell1) == "BLOCK" || TileMap.CellCodeValue(MapCell2) == "BLOCK")
                {
                    if (moveAmount.Y > 0)
                    {
                        onGround = true;
                        moveAmount.Y = 0;
                        velocity.Y = 0;
                    }
                }
            }
            return moveAmount;
        }
        #endregion


    }
}
