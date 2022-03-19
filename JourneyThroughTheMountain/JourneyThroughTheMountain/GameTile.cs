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
            _boundingboxes.Add(new BoundingBox(new Vector2(collisionrectangle.X, collisionrectangle.Y), collisionrectangle.Width, collisionrectangle.Height));
            _triggerboxes.Add(new BoundingBox(new Vector2(collisionrectangle.X, collisionrectangle.Y), collisionrectangle.Width+1, collisionrectangle.Height+1));
            WorldLocation = new Vector2(collisionrectangle.X, collisionrectangle.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.BoundingBox, TileEngine.Camera.WorldToScreen(_collisionRectangle), Color.White);
            base.Draw(spriteBatch);
        }

    }
}
