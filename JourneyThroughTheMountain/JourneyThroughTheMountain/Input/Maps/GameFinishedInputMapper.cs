using JourneyThroughTheMountain.Input.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.Input.Maps
{
    class GameFinishedInputMapper: BaseInputMapper
    {

        public override IEnumerable<BaseInputCommand> GetKeyBoardState(KeyboardState state)
        {
            var commands = new List<GameFinishedInputCommands>();

            if (state.IsKeyDown(Keys.Enter))
            {
                commands.Add(new GameFinishedInputCommands.BackToMainScreen());
            }
            return commands;
        }

    }
}
