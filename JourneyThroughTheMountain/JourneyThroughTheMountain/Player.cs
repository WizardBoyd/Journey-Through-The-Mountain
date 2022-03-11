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
    public class Player : GameObject
    {
        private Vector2 fallspeed = new Vector2(0, 20);
        private float moveScale = 180.0f;
        private bool dead = false;

        public bool Dead
        {
            get { return dead; }
        }

        #region Constuctors
        public Player(ContentManager content)
        {
            animations.Add("idle", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "idle"));
            animations.Add("Run", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "Run"));
            animations.Add("Jump", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "Jump"));

            animations["Jump"].LoopAnimation = false;
            animations["Jump"].FrameLength = 0.08f;
            animations["Jump"].NextNaimation = "idle";

            animations.Add("Die", new AnimationStrip(content.Load<Texture2D>(@"Idle_Run_Jump_SpriteSheet"), 32, "Die"));
            animations["Die"].LoopAnimation = false;

            frameWidth = 32;
            frameHeight = 32;
            collisionRectangle = new Rectangle(9, 1, 30, 46);

            drawDepth = 0.825f;

            enabled = true;
            codeBasedBlocks = false;
            PlayAnimation("idle");

        }
        #endregion
    }
}
