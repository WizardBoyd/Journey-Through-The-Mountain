using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

using TileEngine;

namespace JourneyThroughTheMountain
{
    public class GameObject
    {

        #region Sub-classes
        public class GameObjectEventArgs : EventArgs
        {
            public int HealthValue;
            public GameObjectEventArgs(int health)
            {
                HealthValue = health;
            }
        }
        #endregion

        #region Declarations
        protected Vector2 worldLocation;
        protected Vector2 velocity;
        protected int frameWidth;
        protected int frameHeight;

        protected bool enabled;
        protected bool flipped = false;
        public bool onGround;

        protected Rectangle collisionRectangle;
        protected int collideWidth;
        protected int collideHeight;
        protected bool codeBasedBlocks = true;

        protected float _angle;
        protected Vector2 _direction;
        protected float drawDepth = 0.85f;
        protected Dictionary<string, AnimationStrip> animations =
            new Dictionary<string, AnimationStrip>();
        protected string currentAnimation;
        protected int health;
        protected List<BoundingBox> _boundingboxes = new List<BoundingBox>();
        protected List<BoundingBox> _triggerboxes = new List<BoundingBox>();
        #endregion

        #region Events

        public event EventHandler<GameObjectEventArgs> HealthChanged;

        public event EventHandler CollidedWithSomething;

        #endregion

        #region Properties
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public virtual List<BoundingBox> BoundingBoxes
        {
            get
            {

                List<BoundingBox> result = new List<BoundingBox>();

                foreach (BoundingBox bb in _boundingboxes)
                {
                    
                    result.Add(new BoundingBox(new Vector2((int)worldLocation.X + (int)bb.Position.X, (int)WorldRectangle.Y + (int)bb.Position.Y),
                        bb.Width, bb.Height));
                }

                //foreach (BoundingBox bb in _boundingboxes)
                //{
                //    bb.Position = new Vector2((int)worldLocation.X + (int)bb.Position.X, (int)WorldRectangle.Y + (int)bb.Position.Y);
                //}
                return result;
            }

            set { _boundingboxes = value; }
        }

        public virtual List<BoundingBox> TriggerBoxes
        {
            get
            {
                //foreach (BoundingBox bb in _triggerboxes)
                //{
                //    bb.Position = new Vector2((int)worldLocation.X + bb.Position.X, (int)WorldRectangle.Y + bb.Position.Y);
                //}

                List<BoundingBox> result = new List<BoundingBox>();

                foreach (BoundingBox bb in _triggerboxes)
                {
                    result.Add(new BoundingBox(new Vector2((int)worldLocation.X + (int)bb.Position.X, (int)worldLocation.Y + (int)bb.Position.Y - 5),
                        bb.Width, bb.Height));
                }

                return result;
            }

            set { _triggerboxes = value; }
        }

        public Vector2 WorldLocation
        {
            get { return worldLocation; }
            set {

                //var DeltaX = value.X - worldLocation.X;
                //var DeltaY = value.Y - worldLocation.Y;
                worldLocation = value;
                //foreach (var bb in _boundingboxes)
                //{
                //    bb.Position = new Vector2(bb.Position.X + DeltaX, bb.Position.Y + DeltaY);
                //}
                
                
                }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public Vector2 WorldCenter
        {
            get
            {
                return new Vector2(
                  (int)worldLocation.X + (int)(frameWidth / 2),
                  (int)worldLocation.Y + (int)(frameHeight / 2));
            }
        }

        public Rectangle WorldRectangle
        {
            get
            {
                return new Rectangle(
                    (int)worldLocation.X,
                    (int)worldLocation.Y,
                    frameWidth,
                    frameHeight);
            }
        }

        //public Rectangle CollisionRectangle
        //{
        //    get
        //    {
        //        return new Rectangle(
        //            (int)worldLocation.X + collisionRectangle.X,
        //            (int)WorldRectangle.Y + collisionRectangle.Y,
        //            collisionRectangle.Width,
        //            collisionRectangle.Height);
        //    }
        //    set { collisionRectangle = value; }
        //}


        #endregion 

        #region Helper Methods
        private void updateAnimation(GameTime gameTime)
        {
            if (animations.ContainsKey(currentAnimation))
            {
                if (animations[currentAnimation].FinishedPlaying)
                {
                    PlayAnimation(animations[currentAnimation].NextAnimation);
                }
                else
                {
                    animations[currentAnimation].Update(gameTime);
                }
            }
        }

        protected Vector2 CalculateDirection(float angleOffset = 0.0f)
        {
            _direction = new Vector2((float)Math.Cos(_angle - angleOffset), (float)Math.Sin(_angle - angleOffset));
            _direction.Normalize();

            return _direction;
        }
        #endregion

        #region Map-Based Collision Detection Methods
        private Vector2 horizontalCollisionTest(Vector2 moveAmount)
        {
            if (moveAmount.X == 0)
                return moveAmount;

            Rectangle afterMoveRect = new Rectangle((int)BoundingBoxes[0].Position.X, (int)BoundingBoxes[0].Position.Y, (int)BoundingBoxes[0].Width, (int)BoundingBoxes[0].Height);
            afterMoveRect.Offset((int)moveAmount.X, 0);
            Vector2 corner1, corner2;

            if (moveAmount.X < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Left,
                                      afterMoveRect.Bottom - 1);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Top + 1);
                corner2 = new Vector2(afterMoveRect.Right,
                                      afterMoveRect.Bottom - 1);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(mapCell1) ||
                !TileMap.CellIsPassable(mapCell2))
            {

                moveAmount.X = 0;
                velocity.X = 0;
            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
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
                return moveAmount;

            Rectangle afterMoveRect = new Rectangle((int)BoundingBoxes[0].Position.X, (int)BoundingBoxes[0].Position.Y, (int)BoundingBoxes[0].Width, (int)BoundingBoxes[0].Height);
            afterMoveRect.Offset((int)moveAmount.X, (int)moveAmount.Y);
            Vector2 corner1, corner2;

            if (moveAmount.Y < 0)
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Top);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Top);
            }
            else
            {
                corner1 = new Vector2(afterMoveRect.Left + 1,
                                      afterMoveRect.Bottom);
                corner2 = new Vector2(afterMoveRect.Right - 1,
                                      afterMoveRect.Bottom);
            }

            Vector2 mapCell1 = TileMap.GetCellByPixel(corner1);
            Vector2 mapCell2 = TileMap.GetCellByPixel(corner2);

            if (!TileMap.CellIsPassable(mapCell1) ||
                !TileMap.CellIsPassable(mapCell2))
            {
                if (moveAmount.Y > 0)
                    onGround = true;
                moveAmount.Y = 0;
                velocity.Y = 0;


            }

            if (codeBasedBlocks)
            {
                if (TileMap.CellCodeValue(mapCell1) == "BLOCK" ||
                    TileMap.CellCodeValue(mapCell2) == "BLOCK")
                {
                    if (moveAmount.Y > 0)
                        onGround = true;
                    moveAmount.Y = 0;
                    velocity.Y = 0;
                }
            }

            return moveAmount;
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

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!enabled)
                return;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            updateAnimation(gameTime);

            if (velocity.Y != 0)
            {
                onGround = false;
            }

            Vector2 moveAmount = velocity * elapsed;

            moveAmount = horizontalCollisionTest(moveAmount);
            moveAmount = verticalCollisionTest(moveAmount); 

            Vector2 newPosition = worldLocation + moveAmount;

            newPosition = new Vector2(
                MathHelper.Clamp(newPosition.X, 0,
                  Camera.WorldRectangle.Width - frameWidth),
               newPosition.Y);
            // MathHelper.Clamp(newPosition.Y, 2 * (-TileMap.TileHeight),
            //Camera.WorldRectangle.Height - frameHeight)

            worldLocation = newPosition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!enabled)
                return;

            if (animations.ContainsKey(currentAnimation))
            {

                SpriteEffects effect = SpriteEffects.None;

                if (flipped)
                {
                    effect = SpriteEffects.FlipHorizontally;
                }

                spriteBatch.Draw(
                    animations[currentAnimation].Texture,
                    Camera.WorldToScreen(WorldRectangle),
                    animations[currentAnimation].FrameRectangle,
                    animations[currentAnimation].Tint, 0.0f, Vector2.Zero, effect, drawDepth);

                //debug create renderboundingbox to test player
                
            }
        }

        public virtual void OnNotify(BaseGameStateEvent Event) { }

        #endregion




    }
}
