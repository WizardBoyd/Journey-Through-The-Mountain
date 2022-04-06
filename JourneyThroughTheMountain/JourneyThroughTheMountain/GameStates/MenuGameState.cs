using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Myra;
using Myra.Graphics2D.UI;
using JourneyThroughTheMountain.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using System.IO;
using System.Xml;
using System.Reflection;

namespace JourneyThroughTheMountain.GameStates
{
    public class MenuGameState : BaseGameState
    {

        private string SaveSettingsLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Content/Settings.xml";
        private const string Background = @"Backgrounds/MenuScreenBackGround";

        private const string HowlingWind = @"Sounds/BackgroundMusic/WindBlowing";

        private const string Button = @"UI/Button";
        private const string ButtonSelected = @"UI/ButtonSelected";

        public MenuGameState()
        {
            _desktop = new Desktop();
            GameGUI = new Desktop();
        }

        public override void HandleInput(GameTime time)
        {
           
        }

        public override void LoadContent()
        {
            _desktop = new Desktop();
            GameGUI = new Desktop();
            var Windblowing = LoadSound(HowlingWind).CreateInstance();

            //AddGameObject(new SplashImage(LoadTexture(Background)));

            _soundManager.SetSoundTrack(new List<Microsoft.Xna.Framework.Audio.SoundEffectInstance>() { Windblowing });

            var VerticalStackPannel = new VerticalStackPanel()
            {

                Width = _viewportWidth,
                Height = _viewportHeight,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,

                
                

            };


            VerticalStackPannel.Background = new TextureRegion(LoadTexture(Background));



            var PlayGameTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))
                
            };
            PlayGameTextButton.Text = "Start New Game";
            PlayGameTextButton.TouchDown += (s, e) =>
            {
                SwitchState(new GameplayState(_viewportHeight, _viewportWidth, Game1.MasterVolume, Game1.PitchVolume, Game1.PanVolume));
            };

            VerticalStackPannel.Widgets.Add(PlayGameTextButton);

            var LoadGameTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))
        };
            LoadGameTextButton.Text = "Load Game";
            VerticalStackPannel.Widgets.Add(LoadGameTextButton);

            var SettingsTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))


    };
            SettingsTextButton.Text = "Settings";
            SettingsTextButton.TouchDown += (s, a) =>
            {
                SwitchState(new SettingsState());
            };
            VerticalStackPannel.Widgets.Add(SettingsTextButton);

            var QuitTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))
        };
            QuitTextButton.Text = "Quit";
            QuitTextButton.TouchDown += (s, a) =>
            {
                NotifyEvent(new BaseGameStateEvent.GameQuit());
            };
            VerticalStackPannel.Widgets.Add(QuitTextButton);

            if (File.Exists(SaveSettingsLocation))
            {
                XmlTextReader txtreader = new XmlTextReader(SaveSettingsLocation);

                while (txtreader.Read())
                {
                    if (txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Master_Volume")
                    {
                        Game1.MasterVolume = txtreader.ReadElementContentAsFloat();
                    }
                    else if (txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Pitch_Volume")
                    {
                        Game1.PitchVolume = txtreader.ReadElementContentAsFloat();
                    }
                    else if (txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Pan_Volume")
                    {
                        Game1.PanVolume = txtreader.ReadElementContentAsFloat();
                    }
                }
                txtreader.Close();
            }

            _desktop.Root = VerticalStackPannel;
        }

        public override void UpdateGameState(GameTime time)
        {
           
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            _desktop.Render();
            base.Render(spriteBatch);
        }

        protected override void SetupInputManager()
        {
            
        }
    }
}
