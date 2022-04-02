using JourneyThroughTheMountain.Input.Commands;
using JourneyThroughTheMountain.Input.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TileEngine;

namespace JourneyThroughTheMountain.GameStates
{
    class GameplayState : BaseGameState
    {
        private const string BackGroundTexture = @"Backgrounds/Cavern";
        private const string TileSet = @"Tiles/Tileset";

        private Vector2 ScorePosition = new Vector2(20, 580);
        Vector2 livesPosition = new Vector2(600, 580);

        private SpriteFont pericles8;

        private Texture2D Background;

        private Player MainCharacter;

        public GameplayState(int Height, int width)
        {
            _viewportHeight = Height;
            _viewportWidth = width;
        }

        public override void HandleInput(GameTime time)
        {
            InputManager.GetCommands(cmd =>
            {
               
            });
        }

        public override void LoadContent()
        {
            TileMap.Initialize(LoadTexture(TileSet));

            Background = LoadTexture(BackGroundTexture);

            Camera.WorldRectangle = new Rectangle(0, 0, 160 * 48, 12 * 48);
            Camera.Position = Vector2.Zero;
            Camera.ViewPortWidth = 800;
            Camera.ViewPortHeight = 720;
            Camera.Gameplay = true;

            pericles8 = _contentManager.Load<SpriteFont>(@"Pericles7");
            
            MainCharacter = new Player(_contentManager);
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
            MainCharacter.Update(time);
            LevelManager.Update(time);
            if (MainCharacter.Dead)
            {
                if (MainCharacter.LivesRemaining > 0)
                {
                    LevelManager.ReloadLevel();
                }
                else
                {
                    //SwitchState
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
            spriteBatch.DrawString(pericles8,
                "Score" + MainCharacter.Score.ToString(),
                ScorePosition,
                Color.White);
            spriteBatch.DrawString(pericles8,
                "Health Remaining" + MainCharacter.Health.ToString(),
                livesPosition,
                Color.White);
            spriteBatch.Draw(Background, Camera.WorldToScreen(Camera.ViewPort), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1.0f);
        }
    }
}
