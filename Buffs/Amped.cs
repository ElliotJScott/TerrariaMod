using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Buffs
{
    class Amped : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Amped");
            Description.SetDefault("Critical strikes with the plasma fist give massive mobility boosts");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            //longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerFixer>().ampedCounter = player.buffTime[buffIndex];
            base.Update(player, ref buffIndex);
        }
    }
}
