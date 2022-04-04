using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace JourneyThroughTheMountain.GameStates
{
    public class SettingsState : BaseGameState
    {

       public SettingsState()
        {
            _desktop = new Desktop();
        }

        private const string Background = @"Backgrounds/MenuScreenBackGround";

        private const string HowlingWind = @"Sounds/BackgroundMusic/WindBlowing";

        private const string Button = @"UI/Button";
        private const string ButtonSelected = @"UI/ButtonSelected";

        private string SaveSettingsLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Content/Settings.xml";


        public override void HandleInput(GameTime time)
        {
           
        }

        public override void LoadContent()
        {
           
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



            var MasterVolumeSlider = new HorizontalSlider()
            {
             
               
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center
                

            };
            var MasterVolumeLabel = new Label()
            {
                Text = "Master Volume",
                HorizontalAlignment = HorizontalAlignment.Center
            };

            VerticalStackPannel.Widgets.Add(MasterVolumeLabel);
            VerticalStackPannel.Widgets.Add(MasterVolumeSlider);

            var PitchSlider = new HorizontalSlider()
            {
              
               
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center
              
            };

            var PitchVolumeLabel = new Label()
            {
                Text = "Pitch Volume",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            VerticalStackPannel.Widgets.Add(PitchVolumeLabel);
            VerticalStackPannel.Widgets.Add(PitchSlider);

            var PanVolumeSlider = new HorizontalSlider()
            {
                
               
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center
                


            };

            var PanVolumeLabel = new Label()
            {
                Text = "Pan Volume",
                HorizontalAlignment = HorizontalAlignment.Center
            };
            VerticalStackPannel.Widgets.Add(PanVolumeLabel);
            VerticalStackPannel.Widgets.Add(PanVolumeSlider);


            //
            var SaveTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))
            };
            SaveTextButton.TouchDown += (s, a) =>
            {
                XmlTextWriter txtwriter = new XmlTextWriter(SaveSettingsLocation, null);
                txtwriter.WriteStartDocument();
                txtwriter.WriteStartElement("Root");
                txtwriter.WriteElementString("Master_Volume", MasterVolumeSlider.Value.ToString());


                //
      
                txtwriter.WriteElementString("Pitch_Volume", PitchSlider.Value.ToString());

                //

                txtwriter.WriteElementString("Pan_Volume", PanVolumeSlider.Value.ToString());
                txtwriter.WriteEndElement();

                txtwriter.WriteEndDocument();
                txtwriter.Close();
            };
            SaveTextButton.Text = "Save Settings";
            VerticalStackPannel.Widgets.Add(SaveTextButton);

            var BackTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected))
            };

            BackTextButton.TouchDown += (s, a) =>
            {
                Game1.MasterVolume = MasterVolumeSlider.Value;
                Game1.PitchVolume = PitchSlider.Value;
                Game1.PanVolume = PanVolumeSlider.Value;
                SwitchState(new MenuGameState());
            };
            BackTextButton.Text = "Back To Main Menu";
            VerticalStackPannel.Widgets.Add(BackTextButton);

            if (File.Exists(SaveSettingsLocation))
            {
                XmlTextReader txtreader = new XmlTextReader(SaveSettingsLocation);

                while (txtreader.Read())
                {
                    if (txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Master_Volume")
                    {
                        MasterVolumeSlider.Value = txtreader.ReadElementContentAsFloat();
                    }
                    else if(txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Pitch_Volume")
                    {
                        PitchSlider.Value = txtreader.ReadElementContentAsFloat();
                    }
                    else if(txtreader.NodeType == XmlNodeType.Element && txtreader.Name == "Pan_Volume")
                    {
                        PanVolumeSlider.Value = txtreader.ReadElementContentAsFloat();
                    }
                }
                txtreader.Close();
            }
           

            _desktop.Root = VerticalStackPannel;
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
