using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JourneyThroughTheMountain
{
    public class AnimationStrip
    {
        #region Declartions
        private Texture2D texture;
        private int framewidth;
        private int frameHeight;

        private float frametimer = 0f;
        private float frameDelay = .05f;

        private int currentFrame;

        private bool loopAniamtion = true;
        private bool finishedPlaying = false;

        private string name;
        private string nextAniamtion;
        #endregion

        #region Properties
        public int FrameWidth
        {
            get { return framewidth; }
            set { framewidth = value; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string NextNaimation
        {
            get { return nextAniamtion; }
            set { nextAniamtion = value; }
        }

        public bool LoopAnimation
        {
            get { return loopAniamtion; }
            set { loopAniamtion = value; }
        }

        public bool FinishedPlaying
        {
            get { return finishedPlaying; }
        }

        public int FrameCount
        {
            get { return texture.Width / framewidth; }
        }

        public float FrameLength
        {
            get { return frameDelay; }
            set { frameDelay = value; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(currentFrame * framewidth, 0, framewidth, frameHeight);
            }
        }
        #endregion

        #region Constructor

        public AnimationStrip(Texture2D texture, int frameWidth, string name)
        {
            this.texture = texture;
            this.framewidth = frameWidth;
            this.frameHeight = texture.Height;
            this.name = name;
        }

        #endregion

        #region Public Methods
        public void Play()
        {
            currentFrame = 0;
            finishedPlaying = false;
        }

        public void Update(GameTime gametime)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;

            frametimer += elapsed;

            if (frametimer >= frameDelay) 
            {
                currentFrame++;
                if (currentFrame >= FrameCount)
                {
                    if (loopAniamtion)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        currentFrame = FrameCount - 1;
                        finishedPlaying = true;
                    }
                }
            }
            frametimer = 0;
        }
        #endregion


    }
}
