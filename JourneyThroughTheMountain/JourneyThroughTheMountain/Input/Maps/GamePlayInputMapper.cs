using JourneyThroughTheMountain.Input.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using JourneyThroughTheMountain.Input.Commands;

namespace JourneyThroughTheMountain.Input.Maps
{
    class GamePlayInputMapper: BaseInputMapper
    {
      
        KeyboardState keyboardCurrent;
        KeyboardState keyboardPrevious;

        public bool IsKeyPressed(Keys key, bool Oneshot)
        {
            if (!Oneshot) return keyboardCurrent.IsKeyDown(key);
            return keyboardCurrent.IsKeyDown(key) && !keyboardPrevious.IsKeyDown(key);
        }

        public override IEnumerable<BaseInputCommand> GetKeyBoardState(KeyboardState state)
        {
            var commands = new List<GamePlayInputCommands>();
            keyboardCurrent = state;

            if (IsKeyPressed(Keys.Escape,true))
            {
                commands.Add(new GamePlayInputCommands.GamePause());
            }

            if (keyboardCurrent.IsKeyDown(Keys.Left))
            {
                commands.Add(new GamePlayInputCommands.PlayerMoveLeft());
            }
            if (keyboardCurrent.IsKeyDown(Keys.Right))
            {
                commands.Add(new GamePlayInputCommands.PlayerMoveRight());
            }
            if (keyboardCurrent.IsKeyDown(Keys.Space))
            {
                commands.Add(new GamePlayInputCommands.PlayerJump());
            }

            if (IsKeyPressed(Keys.E, true))
            {
                commands.Add(new GamePlayInputCommands.PlayerAttacks());
            }

            if (IsKeyPressed(Keys.F, true))
            {
                commands.Add(new GamePlayInputCommands.PlayerInteractsWithNPC());
            }

            if (keyboardCurrent.IsKeyDown(Keys.Down))
            {
                commands.Add(new GamePlayInputCommands.PlayerClimbsDown());
            }else if(keyboardCurrent.IsKeyDown(Keys.Up))
            {
                commands.Add(new GamePlayInputCommands.PlayerClimbsUp());
            }

            keyboardPrevious = keyboardCurrent;

            return commands;


        }

    }
}
