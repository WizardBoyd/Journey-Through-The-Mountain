using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class GameplayEvents: BaseGameStateEvent
    {

        public class PlayerJumps : GameplayEvents { }
        public class PlayerFallDamage: GameplayEvents
        {
            public int Damage { get; private set; }

            public PlayerFallDamage(int damage)
            {
                Damage = damage;
            }

        }
        public class PlayerDies: GameplayEvents { }

        public class PlayerRunsIntoEvent : GameplayEvents
        {
            public CollidedEvents TypeOfEvent { get; set; }

            public PlayerRunsIntoEvent(CollidedEvents Event)
            {
                TypeOfEvent = Event;
            }
        }

    }
}
