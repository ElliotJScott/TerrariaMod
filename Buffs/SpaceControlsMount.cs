using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Buffs
{
    class SpaceControlsMount : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Space Controls");
            Description.SetDefault("This exists so the controls work properly in space");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<SpaceControls>(), player);
            player.buffTime[buffIndex] = 10;
            //player.AddBuff(23, 10);

        }
    }
}
