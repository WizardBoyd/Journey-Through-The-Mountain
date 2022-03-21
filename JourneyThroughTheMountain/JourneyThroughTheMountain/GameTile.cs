using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class GameTile: GameObject
    {
        private Rectangle _collisionRectangle;

        public Rectangle CollisionRectangle
        {
            get { return _collisionRectangle; }
            private set { }
        }

        public GameTile(Rectangle collisionrectangle)
        {
            _collisionRectangle = collisionrectangle;
            Vector2 screenpos = TileEngine.Camera.WorldToScreen(new Vector2(collisionrectangle.X, collisionrectangle.Y));
            _boundingboxes.Add(new BoundingBox(screenpos , collisionrectangle.Width, collisionrectangle.Height));
            _triggerboxes.Add(new BoundingBox(screenpos , collisionrectangle.Width+1, collisionrectangle.Height+1));
            WorldLocation = screenpos;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.BoundingBox,new Rectangle(
                (int)_triggerboxes[0].Position.X,(int)_triggerboxes[0].Position.Y, (int)_triggerboxes[0].Width, (int)_triggerboxes[0].Height), Color.White);
            base.Draw(spriteBatch);
        }

    }
}
