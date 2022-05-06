using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public class GameplayEvents: BaseGameStateEvent
    {

        public class PlayerEndGame: GameplayEvents { }
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

        public class PlayerCoinPickupEvent : GameplayEvents
        {

        }

        public class PlayerKilledEnemyEvent : GameplayEvents { }

        public class PlayerClimbs: GameplayEvents { }

        public class DamageDealt : GameplayEvents
        {
            public int Damage;
            public DamageDealt(int damage)
            {
                Damage = damage;
            }
        }

        public class PlayerDealtDamage : GameplayEvents { }

        public class PlayerMoves: GameplayEvents { }

        public class NPCTalk : GameplayEvents { }

        public class PlayerTalk : GameplayEvents { }
        public class NPCLeaveTalk : GameplayEvents { }

        public class PlayerAttacks : GameplayEvents { }

        public class RangedEnemyAttack: GameplayEvents { }

        public class EnemyDealtDamage: GameplayEvents { }

        public class EnemyDeath : GameplayEvents { }

    }
}
