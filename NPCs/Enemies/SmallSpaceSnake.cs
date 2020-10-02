using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.NPCs.Enemies
{
    class SmallSpaceSnake : ModNPC
    {
        int frameTimer = 0;
        int numFrame = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Small space snake");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.CaveBat);
            npc.aiStyle = 14;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = numFrame * frameHeight;
            if (++frameTimer >= 6)
            {
                frameTimer = 0;
                if (++numFrame >= Main.npcFrameCount[npc.type]) numFrame = 0;
            }
            npc.rotation = npc.velocity.ToRotation() - (float)Math.PI;
        }
    }
}
