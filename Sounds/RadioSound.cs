using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sounds
{
    class RadioSound : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan, SoundType type)
        {
            // By checking if the input soundInstance is playing, we can prevent the sound from firing while the sound is still playing, allowing the sound to play out completely. Non-ModSound behavior is to restart the sound, only permitting 1 instance.
            if (soundInstance.State == SoundState.Playing)
            {
                return null;
            }

            soundInstance.Volume = volume;
            soundInstance.Pan = pan;
            soundInstance.Pitch = 0f;
            return soundInstance;
        }
    }
}
