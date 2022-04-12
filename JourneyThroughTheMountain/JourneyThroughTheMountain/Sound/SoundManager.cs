using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyThroughTheMountain.Sound
{
    public class SoundManager
    {
        private int _soundtrackindex = -1;
        private List<SoundEffectInstance> _SoundTracks = new List<SoundEffectInstance>();
        private Dictionary<Type, SoundBankItem> _soundBank = new Dictionary<Type, SoundBankItem>();

        public void SetSoundTrack(List<SoundEffectInstance> tracks)
        {
            _SoundTracks = tracks;
            _soundtrackindex = _SoundTracks.Count - 1;
        }

        public void OnNotify(BaseGameStateEvent gameevent)
        {
            if (_soundBank.ContainsKey(gameevent.GetType()))
            {
                var sound = _soundBank[gameevent.GetType()];
                sound.Sound.Play(sound.Attributes.Volume, sound.Attributes.Pitch, sound.Attributes.Pan);
            }
        }

        public void PlaySoundTrack()
        {
            var nbTracks = _SoundTracks.Count;

            if (nbTracks <= 0)
            {
                return;
            }

            var CurrentTrack = _SoundTracks[_soundtrackindex];
            var nextTrack = _SoundTracks[(_soundtrackindex + 1) % nbTracks];

            if (CurrentTrack.State == SoundState.Stopped)
            {
                nextTrack.Play();
                _soundtrackindex++;

                if (_soundtrackindex >= _SoundTracks.Count)
                {
                    _soundtrackindex = 0;
                }
            }
        }

        public void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound)
        {
            RegisterSound(gameEvent, sound, 1.0f, 0.0f, 0.0f);
        }

        internal void RegisterSound(BaseGameStateEvent gameEvent, SoundEffect sound, float Volume, float Pitch, float Pan)
        {
            if (!_soundBank.ContainsKey(gameEvent.GetType()))
            {
                _soundBank.Add(gameEvent.GetType(), new SoundBankItem(sound, new SoundAttributes(Volume, Pitch, Pan)));
            }
          
        }
      

    }
}
