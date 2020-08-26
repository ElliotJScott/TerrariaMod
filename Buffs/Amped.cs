using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
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
            if (Main.netMode != NetmodeID.Server && Filters.Scene["AmpedEffect"].IsActive()) // This all needs to happen client-side!
            {
                int time = player.buffTime[buffIndex];
                float progress = 1f - (time / 360f);
                Filters.Scene["AmpedEffect"].GetShader().UseProgress(progress);
                if (player.buffTime[buffIndex] < 2)
                {
                    Filters.Scene.Deactivate("AmpedEffect");
                }
            }
            base.Update(player, ref buffIndex);
        }
    }
}
