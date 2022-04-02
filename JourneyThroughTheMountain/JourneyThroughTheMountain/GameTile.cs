using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.Extended;

namespace JourneyThroughTheMountain
{
    public class GameTile: GameObject
    {
        private Rectangle _collisionRectangle;

        public string CodeValue;

        public bool Passable;

        private Rectangle WorldCollsionRectnagle;
        public Rectangle CollisionRectangle
        {
            get {
                return _collisionRectangle; }
            private set { }
        }

        public override List<BoundingBox> TriggerBoxes
        {
            get
            {
                List<BoundingBox> result = new List<BoundingBox>();

                foreach (BoundingBox bb in _boundingboxes)
                {

                    result.Add(new BoundingBox(new Vector2((int)worldLocation.X, (int)WorldRectangle.Y - 5),
                        bb.Width, bb.Height));
                }

                return result;
            }

            set
            {
                _triggerboxes = value;
            }
        }

        public override List<BoundingBox> BoundingBoxes
        {
            get
            {
                List<BoundingBox> result = new List<BoundingBox>();

                    foreach (BoundingBox bb in _boundingboxes)
                    {

                        result.Add(new BoundingBox(new Vector2((int)worldLocation.X, (int)WorldRectangle.Y - 5),
                            bb.Width, bb.Height));
                    }

              

                return result;
            }

            set
            {
                _boundingboxes = value;
            }
        }

        public Segment GroundCollisionSegment
        {
            get
            {
                Vector2 seg = new Vector2(0, 1) * frameHeight;
                return new Segment(WorldLocation, Vector2.Add(WorldLocation, new Vector2(seg.X, seg.Y - 5))); //should always face down hopefully
            }
        }

        public GameTile(Rectangle collisionrectangle, string CodeValue)
        {
            _collisionRectangle = collisionrectangle;
            //Vector2 screenpos = TileEngine.Camera.WorldToScreen(new Vector2(collisionrectangle.X, collisionrectangle.Y));
            _boundingboxes.Add(new BoundingBox(new Vector2(collisionrectangle.X, collisionrectangle.Y) , collisionrectangle.Width, collisionrectangle.Height));
            _triggerboxes.Add(new BoundingBox(new Vector2(collisionrectangle.X, collisionrectangle.Y) , collisionrectangle.Width, collisionrectangle.Height-10));
            WorldLocation = new Vector2(collisionrectangle.X, collisionrectangle.Y);//Might need to change to center
            this.CodeValue = CodeValue;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.BoundingBox, new Rectangle(
                //(int)_triggerboxes[0].Position.X, (int)_triggerboxes[0].Position.Y, (int)_triggerboxes[0].Width, (int)_triggerboxes[0].Height), Color.White);
            //spriteBatch.DrawString(Game1.pericles8, $"{TriggerBoxes[0].Position.X}", TileEngine.Camera.WorldToScreen(new Vector2(_collisionRectangle.X, _collisionRectangle.Y - 10)), Color.White); ///Takes in a world (pixel position) then transforms it with a camera
            //spriteBatch.Draw(Game1.BoundingBox, TileEngine.Camera.WorldToScreen(new Rectangle((int)_collisionRectangle.X, (int)_collisionRectangle.Y - 10, (int)TriggerBoxes[0].Width, (int)TriggerBoxes[0].Height)), Color.White);
            //spriteBatch.Draw(Game1.BoundingBox, TileEngine.Camera.WorldToScreen(new Rectangle((int)TriggerBoxes[0].Position.X, (int)TriggerBoxes[0].Position.Y - 10, (int)TriggerBoxes[0].Width, (int)TriggerBoxes[0].Height)), Color.White);
            //spriteBatch.DrawLine(GroundCollisionSegment.P1, GroundCollisionSegment.P2, Color.Red, 5, 1.25f);
            //System.Diagnostics.Debug.WriteLine($"{_boundingboxes[0].Position.X},     {_boundingboxes[0].Position}");
            //System.Diagnostics.Debug.WriteLine($"{GroundCollisionSegment.P1.X},{GroundCollisionSegment.P1.Y}, {GroundCollisionSegment.P2.X}, {GroundCollisionSegment.P2.Y}");
            //System.Diagnostics.Debug.WriteLine(_collisionRectangle.X);
            
        }

    }
}
