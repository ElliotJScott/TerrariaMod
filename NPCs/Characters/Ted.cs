using Microsoft.Xna.Framework;
using StarSailor.GUI;
using StarSailor.Items.Upgrades;
using StarSailor.Items.Weapons;
using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor.NPCs.Characters
{
    class Ted : Character
    {
        public override string InternalName => "Ted";

        public override IInteraction Interaction
        {
            get
            {
                Vector2 offs = new Vector2(40, -40);
                SequenceQueue queue = new SequenceQueue(Sequence.NPCtalk);

                SequenceQueue queue2 = new SequenceQueue(Sequence.NPCtalk);
                queue2.Append(new CancellableSpeechItem("Free uyghur", this, offs, 400, 100));
                SequenceQueue queue3 = new SequenceQueue(Sequence.NPCtalk);
                queue3.Append(new CancellableSpeechItem("Free hong kong", this, offs, 400, 100));
                SequenceQueue queue4 = new SequenceQueue(Sequence.NPCtalk);
                queue4.Append(new CancellableSpeechItem("Trans rights", this, offs, 400, 100));


                SpeechOption[] options = new SpeechOption[3];
                options[0] = new SpeechOption("Also free uyghur tho", queue2);
                options[1] = new SpeechOption("Yeah free hong kong", queue3);
                options[2] = new SpeechOption("Trans rights", queue4);

                queue.Append(new SelectionSpeechItem("Free hong kong", this, offs, 400, options));
                return new SequenceInteraction(queue);
            }

        }



        //public override
    }
}
