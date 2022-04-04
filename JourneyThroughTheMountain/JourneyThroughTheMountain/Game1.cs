using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TileEngine;
using JourneyThroughTheMountain.Dialogue;
using System.IO;
using System;
using CommonClasses;
using JourneyThroughTheMountain.GameStates;
using Myra;
using Myra.Graphics2D.UI;

namespace JourneyThroughTheMountain
{
    public class Game1 : Game
    {

        //
        private BaseGameState _CurrentGameState;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       

        private RenderTarget2D _RenderTarget;
        private Rectangle _renderScaleRectangle;

        private int _DesignedResoloutionWidth;
        private int _DesignedResoloutionHeight;
        private float _DesignedResolutionAspectRatio;

        private BaseGameState _FirstGameState;

        #region Globals

        public static float MasterVolume;
        public static float PitchVolume;
        public static float PanVolume;

        #endregion
        //


        //Player player;
        //public static SpriteFont pericles8;
        //Vector2 scorePosition = new Vector2(20, 580);
        //public static Texture2D BoundingBox;
        //public static Texture2D TileBox;
        //enum GameState { TitleScreen, Playing, PlayerDead, GameOver };
        //GameState gameState = GameState.TitleScreen;

        //Vector2 gameOverPosition = new Vector2(350, 300);
        //Vector2 livesPosition = new Vector2(600, 580);

        //Texture2D titleScreen;
        //Texture2D GameBackground;

        //float deathTimer = 0.0f;
        //float deathDelay = 5.0f;

        public Game1(int Width, int Height, BaseGameState FirstGameState)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _FirstGameState = FirstGameState;
            _DesignedResoloutionHeight = Height;
            _DesignedResoloutionWidth = Width;
            _DesignedResolutionAspectRatio = Width / (float)Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.graphics.PreferredBackBufferWidth = _DesignedResoloutionWidth;
            this.graphics.PreferredBackBufferHeight = _DesignedResoloutionHeight;
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            this.graphics.ApplyChanges();

            _RenderTarget = new RenderTarget2D(GraphicsDevice, _DesignedResoloutionWidth, _DesignedResoloutionHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

            _renderScaleRectangle = GetScaleRectangle();
            
            base.Initialize();
        }

        private Rectangle GetScaleRectangle()
        {
            var variance = 0.5f;

            var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

            Rectangle ScaleRectangle;

            if (actualAspectRatio <= _DesignedResolutionAspectRatio)
            {
                var PresentHeight = (int)(Window.ClientBounds.Width / _DesignedResolutionAspectRatio + variance);
                var BarHeight = (Window.ClientBounds.Height - PresentHeight) / 2;

                ScaleRectangle = new Rectangle(0, BarHeight, Window.ClientBounds.Width, PresentHeight);
            }
            else
            {
                var PresentWidth = (int)(Window.ClientBounds.Height * _DesignedResolutionAspectRatio + variance);
                var BarHeight = (Window.ClientBounds.Width - PresentWidth) / 2;

                ScaleRectangle = new Rectangle(0, BarHeight, PresentWidth, Window.ClientBounds.Height);
            }
            return ScaleRectangle;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            MyraEnvironment.Game = this;



            spriteBatch = new SpriteBatch(MyraEnvironment.GraphicsDevice);

            SwitchGameState(_FirstGameState);

            //spriteBatch = new SpriteBatch(GraphicsDevice);

            //TileMap.Initialize(
            //    Content.Load<Texture2D>(@"Tiles/Tileset"));
            //TileMap.spriteFont =
            //    Content.Load<SpriteFont>(@"Pericles7");

            //pericles8 = Content.Load<SpriteFont>(@"Pericles7");

            //titleScreen = Content.Load<Texture2D>(@"Splash Screen");

            //GameBackground = Content.Load<Texture2D>(@"Backgrounds/Cavern");

            //Camera.WorldRectangle = new Rectangle(0, 0, 160 * 48, 12 * 48);
            //Camera.Position = Vector2.Zero;
            //Camera.ViewPortWidth = 800;
            //Camera.ViewPortHeight = 600;
            //Camera.Gameplay = true;

            //BoundingBox = new Texture2D(GraphicsDevice, 1, 1);
            //Color[] colordata = new Color[1];
            //colordata[0] = Color.Red;
            //BoundingBox.SetData<Color>(colordata);
            ////colordata[0] = Color.Blue;
            ////TileBox = BoundingBox;
            ////TileBox.SetData<Color>(colordata);

            //player = new Player(Content);
            //LevelManager.Initialize(Content, player);
           
        }

        private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e)
        {
            SwitchGameState(e);
        }

        private void SwitchGameState(BaseGameState gameState)
        {
            if (_CurrentGameState != null)
            {
                _CurrentGameState.OnStateSwitched -= CurrentGameState_OnStateSwitched;
                _CurrentGameState.OnEventNotification -= _currentGameState_OnEventNotifaction;
                _CurrentGameState.UnloadContent();
            }
            _CurrentGameState = gameState;
            _CurrentGameState.Initialize(Content, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            _CurrentGameState.LoadContent();

            _CurrentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
            _CurrentGameState.OnEventNotification += _currentGameState_OnEventNotifaction;
        }

        private void _currentGameState_OnEventNotifaction(object sender, BaseGameStateEvent e)
        {
            switch (e)
            {
                case BaseGameStateEvent.GameQuit _:
                    Exit();
                    break;
                case BaseGameStateEvent.MenuUI _:
                    IsMouseVisible = true;
                    break;
                case BaseGameStateEvent.GamePlay _:
                    IsMouseVisible = false;
                    break;
                default:
                    break;
            }
        }

        //private void StartNewGame()
        //{
        //    player.Revive();
        //    player.LivesRemaining = 3;
        //    player.WorldLocation = Vector2.Zero;
        //    LevelManager.LoadLevel(0);
        //}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            _CurrentGameState?.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            _CurrentGameState.HandleInput(gameTime);
            _CurrentGameState.Update(gameTime);
           

            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
            //    ButtonState.Pressed)
            //    this.Exit();

            //KeyboardState keyState = Keyboard.GetState();
            //GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            //float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (gameState == GameState.TitleScreen)
            //{
            //    if (keyState.IsKeyDown(Keys.Space) ||
            //        gamepadState.Buttons.A == ButtonState.Pressed)
            //    {
            //        StartNewGame();
            //        gameState = GameState.Playing;
            //    }
            //}

            //if (gameState == GameState.Playing)
            //{
            //    player.Update(gameTime);
            //    LevelManager.Update(gameTime);
            //    if (player.Dead)
            //    {
            //        if (player.LivesRemaining > 0)
            //        {
            //            gameState = GameState.PlayerDead;
            //            deathTimer = 0.0f;
            //        }
            //        else
            //        {
            //            gameState = GameState.GameOver;
            //            deathTimer = 0.0f;
            //        }
            //    }
            //}

            //if (gameState == GameState.PlayerDead)
            //{
            //    player.Update(gameTime);
            //    LevelManager.Update(gameTime);
            //    deathTimer += elapsed;
            //    if (deathTimer > deathDelay)
            //    {
            //        player.WorldLocation = Vector2.Zero;
            //        LevelManager.ReloadLevel();
            //        player.Revive();
            //        gameState = GameState.Playing;
            //    }
            //}

            //if (gameState == GameState.GameOver)
            //{
            //    deathTimer += elapsed;
            //    if (deathTimer > deathDelay)
            //    {
            //        gameState = GameState.TitleScreen;
            //    }
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Black);

            //spriteBatch.Begin(
            //    SpriteSortMode.BackToFront,
            //    BlendState.AlphaBlend);

            //if (gameState == GameState.TitleScreen)
            //{
            //    spriteBatch.Draw(titleScreen, Vector2.Zero, Color.White);
            //}

            //if ((gameState == GameState.Playing) ||
            //    (gameState == GameState.PlayerDead) ||
            //    (gameState == GameState.GameOver))
            //{

            //    TileMap.Draw(spriteBatch);
            //    player.Draw(spriteBatch);
            //    LevelManager.Draw(spriteBatch);
            //    spriteBatch.DrawString(
            //        pericles8,
            //        "Score: " + player.Score.ToString(),
            //        scorePosition,
            //        Color.White);
            //    spriteBatch.DrawString(
            //        pericles8,
            //        "Lives Remaining: " + player.Health.ToString(),
            //        livesPosition,
            //        Color.White);
            //}
            //spriteBatch.Draw(GameBackground, Camera.WorldToScreen(Camera.ViewPort), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);//Edited

            //if (gameState == GameState.PlayerDead)
            //{
            //}

            //if (gameState == GameState.GameOver)
            //{
            //    spriteBatch.DrawString(
            //        pericles8,
            //        "G A M E  O V E R !",
            //        gameOverPosition,
            //        Color.White);
            //}

            //spriteBatch.End();

            GraphicsDevice.SetRenderTarget(_RenderTarget);
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            _CurrentGameState.Render(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque);

            spriteBatch.Draw(_RenderTarget, _renderScaleRectangle, Color.White);

            spriteBatch.End();
            _CurrentGameState._desktop.Render();

            base.Draw(gameTime);
        }

        public void DrawTree()
        {

        }

    }
}
