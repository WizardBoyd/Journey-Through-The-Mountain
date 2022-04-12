using JourneyThroughTheMountain.Input.Commands;
using JourneyThroughTheMountain.Input.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TileEngine;

namespace JourneyThroughTheMountain.GameStates
{
    class GameplayState : BaseGameState
    {
        private const string BackGroundTexture = @"Backgrounds/Cavern";
        private const string TileSet = @"Tiles/Tileset2";

        private const string RelaxingLevelMusic = @"Sounds/BackgroundMusic/RelaxingMusic";
        private const string TalkingSFX = @"Sounds/SoundEffects/TalkingBetter";
        private const string PickupSnowflake = @"Sounds/SoundEffects/Pickup";

        private const string AxeSwing = @"Sounds/SoundEffects/AxeSwing";
        private const string PlayerDeath = @"Sounds/SoundEffects/PlayerDeath";
        private const string PlayerHitByEnemy = @"Sounds/SoundEffects/HitByEnemy";
        private const string FallDamge = @"Sounds/SoundEffects/FallDamge";

        private const string Button = @"UI/Button";
        private const string ButtonSelected = @"UI/ButtonSelected";
        private const string HeartImage = @"UI/HeartLives";
        private const string Fade_Image = @"Fade_Heal";
        private const string SnowflakeImage = @"UI/Pixel_Snowflake";

        //UI
        public HorizontalProgressBar HealthBar;
        public Label Livesremaining;
        public Label Snowflakelabel;
        //END


        private bool Paused;

        private Vector2 ScorePosition = new Vector2(20, 580);
        Vector2 livesPosition = new Vector2(600, 580);

        private SpriteFont pericles8;

        private Texture2D Background;
        private Texture2D WhiteSpace;

        private Player MainCharacter;

        private float MasterVolume;
        private float PitchVolume;
        private float PanVolume;
        
        


        public GameplayState(int Height, int width, float _MasterVolume, float _PitchVolume, float _PanVolume)
        {
            _viewportHeight = Height;
            _viewportWidth = width;
            MasterVolume = _MasterVolume / 100f;
            PitchVolume = _PitchVolume / 100f;
            PanVolume = _PanVolume/ 100f;
            _desktop = new Desktop();
            GameGUI = new Desktop();
            
        }

        public override void HandleInput(GameTime time)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommands.GamePause)
                {
                    _desktop.Root.Enabled = !_desktop.Root.Enabled;
                    Paused = !Paused;
                    if (Paused)
                    {
                        _desktop.Root.Opacity = 1;
                        NotifyEvent(new BaseGameStateEvent.MenuUI());
                    }
                    else
                    {
                        _desktop.Root.Opacity = 0;
                        NotifyEvent(new BaseGameStateEvent.GamePlay());
                    }
        }
                if (cmd is GamePlayInputCommands.PlayerInteractsWithNPC)
                {
                    if (LevelManager.Display_EButton)
                    {
                        MainCharacter.Heal();
                        Flash();
                        LevelManager.ReloadEnemies();
                        
                    }
                }
            });
        }

        public override void LoadContent()
        {
            TileMap.Initialize(LoadTexture(TileSet));
            GameGUI = new Desktop();
            _desktop = new Desktop();
            #region UIStuff


            var Grid = new Grid
            {
                ShowGridLines = true
            };

            for (int i = 0; i < 5; i++)
                Grid.ColumnsProportions.Add(new Proportion());

            for (int i = 0; i < 5; i++)
                Grid.RowsProportions.Add(new Proportion());



            HealthBar = new HorizontalProgressBar
            {
                Width = 160,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FocusedBackground = new TextureRegion(LoadTexture(Button)),
                Border = new TextureRegion(LoadTexture(Button)),
                BorderThickness = new Myra.Graphics2D.Thickness(10, 10),
                Filler = new SolidBrush(Color.Green),
                ZIndex = 2
            };

            var LivesPannel = new VerticalStackPanel
            {
                GridRow = 2,
                GridColumn = 4,

            };

            var ScorePannel = new VerticalStackPanel
            {
                GridRow = 2,
                GridColumn = 5
            };

            var HealthPannel = new VerticalStackPanel
            {
                GridRow = 2,
                GridColumn = 1
            };

            var HealthLabel = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Health"
            };

            Snowflakelabel = new Label
            {
                Text = "0",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Livesremaining = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var PauseScreen = new VerticalStackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = _viewportWidth,
                Height = _viewportHeight,
                ZIndex = 3,
            };

            var LivesImage = new Image
            {
               Background = new TextureRegion(LoadTexture(HeartImage)),
               Width = 80,
               Height = 80
            };

            var SnowFlakeImage = new Image
            {
                Background = new TextureRegion(LoadTexture(SnowflakeImage)),
                Width = 80,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };



            var SaveTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "Save Game",
                ZIndex = 3
            };

            SaveTextButton.TouchDown += (s, a) =>
            {
                //Implement save logic
            };

            var MainMenuTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "To Main Menu",
                ZIndex = 3
            };

            MainMenuTextButton.TouchDown += (s, e) =>
            {
                SwitchState(new MenuGameState());
            };

            var ResumeTextButton = new TextButton()
            {
                Background = new TextureRegion(LoadTexture(Button)),
                Height = 180,
                Width = 320,
                HorizontalAlignment = HorizontalAlignment.Center,
                OverBackground = new TextureRegion(LoadTexture(ButtonSelected)),
                Text = "Resume Game",
                ZIndex = 3
            };

            ResumeTextButton.TouchDown += (s, a) =>
            {
                _desktop.Root.Enabled = false;
                Paused = false;
                _desktop.Root.Opacity = 0;
                NotifyEvent(new BaseGameStateEvent.GamePlay());

            };
            ScorePannel.Widgets.Add(SnowFlakeImage);
            ScorePannel.Widgets.Add(Snowflakelabel);

            LivesPannel.Widgets.Add(LivesImage);
            LivesPannel.Widgets.Add(Livesremaining);
            HealthPannel.Widgets.Add(HealthLabel);
            HealthPannel.Widgets.Add(HealthBar);
            PauseScreen.Widgets.Add(ResumeTextButton);
            PauseScreen.Widgets.Add(SaveTextButton);
            PauseScreen.Widgets.Add(MainMenuTextButton);
            Grid.Widgets.Add(HealthPannel);
            Grid.Widgets.Add(LivesPannel);
            Grid.Widgets.Add(ScorePannel);
            


            GameGUI.Root = Grid;
            _desktop.Root = PauseScreen;

            _desktop.Root.Opacity = 0;
            _desktop.Root.ZIndex = 3;
            _desktop.Root.Enabled = false;
            #endregion


            Background = LoadTexture(BackGroundTexture);
            WhiteSpace = LoadTexture(Fade_Image);
            

            var Track1 = LoadSound(RelaxingLevelMusic).CreateInstance();

            Track1.Volume = MasterVolume;

            var Talking = LoadSound(TalkingSFX);
            var PlayerAttack = LoadSound(AxeSwing);
            var FallD = LoadSound(FallDamge);
            var PlayerDeathSFX = LoadSound(PlayerDeath);
            var PlayerTakeDamageSFX = LoadSound(PlayerHitByEnemy);
            var PlayerPicksUpCoin = LoadSound(PickupSnowflake);

            _soundManager.RegisterSound(new GameplayEvents.PlayerCoinPickupEvent(), PlayerPicksUpCoin, 0.4f * MasterVolume, 0.5f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.PlayerDealtDamage(), PlayerTakeDamageSFX, 0.4f * MasterVolume, 0.1f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.PlayerDies(), PlayerDeathSFX, 0.4f * MasterVolume, 0.1f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.PlayerFallDamage(0), FallD, 0.4f * MasterVolume, 0.1f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.PlayerAttacks(), PlayerAttack, 0.4f * MasterVolume, 0.1f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.NPCTalk(), Talking, 0.4f * MasterVolume, 0.1f * PitchVolume, PanVolume);
            _soundManager.RegisterSound(new GameplayEvents.PlayerTalk(), Talking, 0.4f * MasterVolume, -0.2f * PitchVolume, PanVolume);
            

            _soundManager.SetSoundTrack(new List<Microsoft.Xna.Framework.Audio.SoundEffectInstance>() { Track1 });

            Camera.WorldRectangle = new Rectangle(0, 0, 160 * 48, 12 * 48);
            Camera.Position = Vector2.Zero;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 720;
            Camera.Gameplay = true;

            pericles8 = _contentManager.Load<SpriteFont>(@"Pericles7");
            
            MainCharacter = new Player(_contentManager, this);
            HealthBar.Value = MainCharacter.Health * 10;
            LevelManager.Initialize(_contentManager, MainCharacter, pericles8);
            startNewGame();

        }

        private void startNewGame()
        {
            MainCharacter.Revive();
            MainCharacter.LivesRemaining = 3;
            MainCharacter.WorldLocation = Vector2.Zero;
            LevelManager.LoadLevel(0);
        }

        public override void UpdateGameState(GameTime time)
        {
            if (!Paused)
            {
                
                MainCharacter.Update(time);
                UpdateGameObjects(time);
                LevelManager.Update(time);
                HealthBar.Value = MainCharacter.Health * 10;
                Snowflakelabel.Text = MainCharacter.Score.ToString();
                Livesremaining.Text = MainCharacter.LivesRemaining.ToString();
                if (MainCharacter.Dead)
                {
                    if (MainCharacter.LivesRemaining > 0)
                    {
                        if (MainCharacter.CanRevive)
                        {
                            MainCharacter.Revive();
                            Flash();
                            LevelManager.ReloadLevel();
                        }

                    }
                    else
                    {
                        _soundManager.StopSounds();
                        _soundManager.DisposeOfSounds();
                        SwitchState(new GameOverState());
                    }
                }
                if (LevelManager.Talker != null && LevelManager.Text != "")
                {
                    if (LevelManager.TalkSFXPlay)
                    {
                        NotifyEvent(new GameplayEvents.PlayerTalk());
                        LevelManager.TalkSFXPlay = false;
                    }
                    else if (LevelManager.TalkSFXPlayNPC)
                    {
                        NotifyEvent(new GameplayEvents.NPCTalk());
                        LevelManager.TalkSFXPlayNPC = false;
                    }
                }
            }
           
        }

        protected override void SetupInputManager()
        {
            InputManager = new Input.InputManager(new GamePlayInputMapper());
        }

        public override void Render(SpriteBatch spriteBatch)
        {
        
            
            TileMap.Draw(spriteBatch);
            MainCharacter.Draw(spriteBatch);
            LevelManager.Draw(spriteBatch);
            spriteBatch.Draw(Background, Camera.WorldToScreen(Camera.ViewPort), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
            base.Render(spriteBatch);
            
            
        }

        private void Flash()
        {
            AddGameObject(new GameObjects.SplashImage(WhiteSpace, 0.0f, true, true));
        }
    }
}
