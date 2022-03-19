using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class BoundingBox
    {
        //private Vector2 _position;
        public Vector2 Position;


        public float Width { get; set; }

        public float Height { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
            }
        }
        public BoundingBox(Vector2 position, float width, float height)
        {
            Position = position;
            Width = width;
            Height = height;
        }
        public bool CollidesWith(BoundingBox otherBB)
        {
            if (Position.X < otherBB.Position.X + otherBB.Width &&
                Position.X + Width > otherBB.Position.X &&
                Position.Y < otherBB.Position.Y + otherBB.Height &&
                Position.Y + Height > otherBB.Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollidesWith(Vector2 p)
        {
            if (p.X < Position.X + Width &&
                p.X > Position.X &&
                p.Y < Position.Y + Height &&
                p.Y > Position.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Segment Line needs to be done as well eventually

    }
}
