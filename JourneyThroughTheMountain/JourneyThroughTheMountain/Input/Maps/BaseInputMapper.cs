using System;
using System.Collections.Generic;
using System.Text;
using JourneyThroughTheMountain.Input.Commands;
using Microsoft.Xna.Framework.Input;

namespace JourneyThroughTheMountain.Input.Maps
{
    public class BaseInputMapper
    {
       public virtual IEnumerable<BaseInputCommand> GetKeyBoardState(KeyboardState state)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState state)
        {
            return new List<BaseInputCommand>();
        }

        public virtual IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state)
        {
            return new List<BaseInputCommand>();
        }
    }
}
