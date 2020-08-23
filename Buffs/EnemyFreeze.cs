using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Buffs
{
    class EnemyFreeze: ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Freeze");
            Description.SetDefault("I am freeze");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            //longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            //Main.NewText("Weh");
            npc.GetGlobalNPC<NPCBuffs>().freeze = true;
        }
    }
}
