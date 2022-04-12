using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.GameObjects
{
   public class SplashImage : BaseGameObject
    {
        private bool Fade;
        private bool Pong;
        private bool fullscreen;

        public SplashImage(Texture2D texture, float Transparency = 1.0f, bool _fade = false, bool _fullscreen = false)
        {
            _texture = texture;
            Transperancy = Transparency;
            Fade = _fade;
            fullscreen = _fullscreen;
        }

        public override void Update(GameTime time)
        {
            if (Fade)
            {
                float DeltaSeconds = (float)time.ElapsedGameTime.TotalSeconds;
               
                if (Transperancy >= 1.0f && !Pong)
                {
                    Pong = true;
                }else if(Transperancy < 0)
                {
                    Destroy();
                }
                else if (Transperancy >= 0.0f && !Pong)
                {
                    Transperancy += DeltaSeconds;
                }else if (Pong)
                {
                    Transperancy -= DeltaSeconds;
                }
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            if (!Destroyed && !fullscreen)
            {
                spriteBatch.Draw(_texture, _position, Color.White * Transperancy);
            }else if(!Destroyed && fullscreen)
            {
                spriteBatch.Draw(_texture, TileEngine.Camera.WorldToScreen(TileEngine.Camera.ViewPort), Color.White * Transperancy);
            }
        }


    }
}
