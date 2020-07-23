using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using starsailor.Mounts;
using Terraria.ID;

namespace starsailor.Buffs
{
    class SubMount : ModBuff
    {
        public static Vector2 cameraPosition = new Vector2(-1000, -1000);
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Sub");
            Description.SetDefault("Cool sub bruh");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Sub>(), player);
            player.buffTime[buffIndex] = 10;
            //player.direction = 1;
            player.AddBuff(BuffID.Gills, 10);

        }
    }
}
