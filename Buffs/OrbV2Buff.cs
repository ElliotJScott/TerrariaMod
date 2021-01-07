using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Buffs
{
    class OrbV2Buff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Distorted");
            Description.SetDefault("Losing life");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            //longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            
            npc.GetGlobalNPC<NPCBuffs>().distortion = Math.Max(npc.GetGlobalNPC<NPCBuffs>().distortion, 2);
        }
    }
}
