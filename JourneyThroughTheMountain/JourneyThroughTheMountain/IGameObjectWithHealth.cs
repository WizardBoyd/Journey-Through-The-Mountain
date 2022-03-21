using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain
{
    public interface IGameObjectWithHealth
    {
        void TakeDamage(IGameObjectWithDamage o);

        void TakeDamage(int Amount);
    }
}
