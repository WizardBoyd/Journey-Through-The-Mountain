using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.Sound
{
    class SoundAttributes
    {
        public float Volume { get; set; }
        public float Pitch { get; set; }
        public float Pan { get; set; }

        public SoundAttributes(float _Volume, float _Pitch, float _pan)
        {
            Volume = _Volume;
            Pitch = _Pitch;
            Pan = _pan;
        }
    }
}
