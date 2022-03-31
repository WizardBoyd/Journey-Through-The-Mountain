using System;
using System.Collections.Generic;
using System.Text;
using JourneyThroughTheMountain.Input.Maps;
using JourneyThroughTheMountain.Input.Commands;
using Microsoft.Xna.Framework.Input;

namespace JourneyThroughTheMountain.Input
{
    public class InputManager
    {
        private readonly BaseInputMapper _inputMapper;

        public InputManager(BaseInputMapper inputMapper)
        {
            _inputMapper = inputMapper;
        }

        public void GetCommands(Action<BaseInputCommand> ActOnState)
        {
            var KeyboardState = Keyboard.GetState();
            foreach (var state in _inputMapper.GetKeyBoardState(KeyboardState))
            {
                ActOnState(state);
            }

            var mouseState = Mouse.GetState();
            foreach (var state in _inputMapper.GetMouseState(mouseState))
            {
                ActOnState(state);
            }

            // we're going to assume only 1 gamepad is being used
            var gamePadState = GamePad.GetState(0);
            foreach (var state in _inputMapper.GetGamePadState(gamePadState))
            {
                ActOnState(state);
            }
        }
    }
}
