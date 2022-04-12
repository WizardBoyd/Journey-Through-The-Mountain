using JourneyThroughTheMountain.GameObjects;
using JourneyThroughTheMountain.Input.Commands;
using JourneyThroughTheMountain.Input.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.GameStates
{
    public class SplashState : BaseGameState
    {

        public SplashState()
        {
            _desktop = new Myra.Graphics2D.UI.Desktop();
            GameGUI = new Myra.Graphics2D.UI.Desktop();
        }

        public override void HandleInput(GameTime time)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is SplashInputCommands.GameSelect)
                {
                    //SwitchState(new GameplayState(800,720));
                    NotifyEvent(new BaseGameStateEvent.MenuUI());
                    SwitchState(new TransitionState(new MenuGameState()));
                }
            });
        }

        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture(@"Splash screen")));
        }

        public override void UpdateGameState(GameTime time)
        {
        }

        protected override void SetupInputManager()
        {
            InputManager = new Input.InputManager(new SplashInputMapper());
        }
    }
}
