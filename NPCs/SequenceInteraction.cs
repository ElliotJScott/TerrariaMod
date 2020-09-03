using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.NPCs
{
    class SequenceInteraction : IInteraction
    {
        public bool Enabled { get; set; }
        SequenceQueue queue;
        public SequenceInteraction(SequenceQueue q)
        {
            queue = q;
        }

        public void Execute()
        {
            Main.NewText("wewewexgdgsg");
            Enabled = true;
            StarSailorMod mod = ModContent.GetInstance<StarSailorMod>();
            queue.Execute();
        }

        public void Update()
        {
            //Don't update here bcos it will be updated by the mod
            StarSailorMod mod = ModContent.GetInstance<StarSailorMod>();
            if (!mod.sequence.GetActive()) Enabled = false;
        }
    }
}
