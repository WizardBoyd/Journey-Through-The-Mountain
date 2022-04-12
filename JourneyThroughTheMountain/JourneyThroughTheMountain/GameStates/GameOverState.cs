using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.TextureAtlases;
using Microsoft.Xna.Framework.Graphics;

namespace JourneyThroughTheMountain.GameStates
{
    class GameOverState : BaseGameState
    {
        private const string BackGround = @"GameOverScreen";
        private const string GameOverSFX = @"Sounds/SoundEffects/GameOverSound";
        private const string Button = @"UI/Button";
        private const string ButtonSelected = @"UI/ButtonSelected";

        public GameOverState()
        {
            _desktop = new Myra.Graphics2D.UI.Desktop();
            GameGUI = new Myra.Graphics2D.UI.Desktop();
        }

        public override void HandleInput(GameTime time)
        {

        }

        public override void LoadContent()
        {
            var verticalStackPannel = new VerticalStackPanel()
            {
                Width = _viewportWidth,
                Height = _viewportHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
                
               
            };

            verticalStackPannel.Background = new TextureRegion(LoadTexture(BackGround));

            var Track1 = LoadSound(GameOverSFX).CreateInstance();
            _soundManager.SetSoundTrack(new List<Microsoft.Xna.Framework.Audio.SoundEffectInstance>() { Track1});

            var WhiteSpace = new Label()
            {
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            verticalStackPannel.Widgets.Add(WhiteSpace);

            var PlayAgainButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "Play Again"
                
            };
            verticalStackPannel.Widgets.Add(PlayAgainButton);

            var BackToMainMenu = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "Back To Main Menu"

            };
            verticalStackPannel.Widgets.Add(BackToMainMenu);

            var QuitButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "Quit Game"
            };
            verticalStackPannel.Widgets.Add(QuitButton);

            QuitButton.TouchDown += (s, a) =>
             {
                 NotifyEvent(new BaseGameStateEvent.GameQuit());
             };
            BackToMainMenu.TouchDown += (s, a) =>
            {
                SwitchState(new TransitionState(new MenuGameState()));
            };
            PlayAgainButton.TouchDown += (s, a) =>
            {
                SwitchState(new TransitionState(new GameplayState(_viewportHeight, _viewportWidth, Game1.MasterVolume, Game1.PitchVolume, Game1.PanVolume)));
            };
            _desktop.Root = verticalStackPannel;

        }

        public override void UpdateGameState(GameTime time)
        {
            
        }

        protected override void SetupInputManager()
        {
           
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            _desktop.Render();
            base.Render(spriteBatch);
        }
    }
}
