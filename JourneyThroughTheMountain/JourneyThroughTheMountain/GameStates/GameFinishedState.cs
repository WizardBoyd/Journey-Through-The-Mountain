using JourneyThroughTheMountain.Input.Commands;
using JourneyThroughTheMountain.Input.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.GameStates
{
    class GameFinishedState : BaseGameState
    {
        private const string BackGround = @"EndOfGame";
        private const string GameOverSFX = @"Sounds/SoundEffects/GameOverSound";

        public GameFinishedState()
        {
            _desktop = new Myra.Graphics2D.UI.Desktop();
            GameGUI = new Myra.Graphics2D.UI.Desktop();
        }

        public override void HandleInput(GameTime time)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GameFinishedInputCommands.BackToMainScreen)
                {
                    SwitchState(new MenuGameState());
                }
            });
        }

        public override void LoadContent()
        {
            var Track1 = LoadSound(GameOverSFX).CreateInstance();
            _soundManager.SetSoundTrack(new List<Microsoft.Xna.Framework.Audio.SoundEffectInstance>() { Track1 });

            var verticalStackPannel = new VerticalStackPanel()
            {
                Width = _viewportWidth,
                Height = _viewportHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center


            };
            verticalStackPannel.Background = new TextureRegion(LoadTexture(BackGround));

            _desktop.Root = verticalStackPannel;

        }

        public override void UpdateGameState(GameTime time)
        {
        }

        protected override void SetupInputManager()
        {
            InputManager = new Input.InputManager(new GameFinishedInputMapper());
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            _desktop.Render();
            base.Render(spriteBatch);
        }
    }
}
