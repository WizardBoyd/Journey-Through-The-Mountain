using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileEngine;
namespace JourneyThroughTheMountain
{
    public class Coin : GameObject
    {
        #region Constructor
        public Coin(ContentManager Content, int cellX, int cellY)
        {
            worldLocation.X = TileMap.TileWidth * cellX;
            worldLocation.Y = TileMap.TileHeight * cellY;

            frameWidth = TileMap.TileWidth;
            frameHeight = TileMap.TileHeight;

            animations.Add("idle",
                new AnimationStrip(
                    Content.Load<Texture2D>(@"Compass"),
                    48,
                    "idle"));

            animations["idle"].LoopAnimation = true;
            animations["idle"].FrameLength = 0.15f;
            PlayAnimation("idle");
            drawDepth = 0.875f;

            CollisionRectangle = new Rectangle(9, 24, 30, 24);
            enabled = true;
        }
        #endregion


    }
}
