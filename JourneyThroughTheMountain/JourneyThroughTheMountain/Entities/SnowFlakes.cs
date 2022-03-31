using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TileEngine;

namespace JourneyThroughTheMountain.Entities
{
    class SnowFlakes : GameObject
    {

        public SnowFlakes(ContentManager Content, int CellX, int CellY)
        {
            worldLocation.X = TileMap.TileWidth * CellX;
            worldLocation.Y = TileMap.TileHeight * CellY;

            frameWidth = TileMap.TileWidth;
            frameHeight = TileMap.TileHeight;

            animations.Add("Idle", new AnimationStrip(Content.Load<Texture2D>(@"Animations/Snowflake"), 16, "Idle"));
            animations["Idle"].LoopAnimation = true;
            drawDepth = 0.75f;
            _boundingboxes.Add(new BoundingBox(new Vector2(0, 0),32, 32));
            enabled = true;

            PlayAnimation("Idle");
            

        }

    }
}
