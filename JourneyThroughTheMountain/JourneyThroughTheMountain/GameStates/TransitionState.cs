using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.GameStates
{
   public class TransitionState : BaseGameState
    {

        private float _C_Amount = 0.0f;

        private Color StartColor;
        private Color FinalColor;
        private Color CurrentColor;

        private bool Pong;

        private BaseGameState StateToTransitionTo;

        private const string LoadImage = @"Splash screen";

        private Texture2D Texture;

        public TransitionState(BaseGameState state)
        {
            StateToTransitionTo = state;
        }

        public override void HandleInput(GameTime time)
        {
          
        }

        public override void LoadContent()
        {
            GameGUI = new Desktop();
            _desktop = new Desktop();
            StartColor = Color.White;
            FinalColor = Color.Black;

            Texture = LoadTexture(LoadImage);
        }

        public override void UpdateGameState(GameTime time)
        {
            float DeltaSeconds = (float)time.ElapsedGameTime.TotalSeconds;

            if (_C_Amount <= 1.5f && !Pong)
            {
                _C_Amount += DeltaSeconds;
            }
            else if(_C_Amount >= 1.5f && !Pong)
            {
                Pong = true;
            }
            else if (Pong)
            {
                _C_Amount -= DeltaSeconds;
             
            }


            if (_C_Amount < 0)
            {
                SwitchState(StateToTransitionTo);
            }

            CurrentColor = Color.Lerp(StartColor, FinalColor, _C_Amount);

        }

        protected override void SetupInputManager()
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, CurrentColor);
            base.Render(spriteBatch);
        }
    }
}
