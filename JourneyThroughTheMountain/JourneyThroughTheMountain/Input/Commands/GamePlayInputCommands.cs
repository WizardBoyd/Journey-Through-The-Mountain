using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.Input.Commands
{
    class GamePlayInputCommands : BaseInputCommand
    {
        public class GamePause: GamePlayInputCommands { }

        public class PlayerMoveLeft: GamePlayInputCommands { }

        public class PlayerMoveRight: GamePlayInputCommands { }

        public class PlayerJump: GamePlayInputCommands { }

        public class PlayerAttacks: GamePlayInputCommands { }

        public class PlayerInteractsWithNPC: GamePlayInputCommands { }

        public class PlayerClimbsUp: GamePlayInputCommands { }

        public class PlayerClimbsDown: GamePlayInputCommands { }
    }
}
