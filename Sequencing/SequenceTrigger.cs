using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sequencing
{
    class SequenceTrigger
    {
        SequenceQueue sequence;
        Rectangle triggerRegion;
        public void Trigger()
        {
            ModContent.GetInstance<StarSailorMod>().sequence = sequence;
            //sequence.Execute(); This is not necessary as it doesn't exist - all execution is done in the update method
        }
    }
}
