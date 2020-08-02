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
            sequence.Execute();
        }
    }
}
