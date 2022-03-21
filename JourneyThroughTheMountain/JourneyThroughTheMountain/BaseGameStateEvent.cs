using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class BaseGameStateEvent
    {

        public class Nothing: BaseGameStateEvent { }
        public class GameQuit : BaseGameStateEvent { }

        public class GameTick: BaseGameStateEvent { }

    }
}
