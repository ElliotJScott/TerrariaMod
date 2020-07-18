using Terraria;
using Terraria.ModLoader;
using StarSailor.Mounts;
using Microsoft.Xna.Framework;

namespace StarSailor.Buffs
{
	public class BoatMount : ModBuff
	{
        public static Vector2 cameraPosition = new Vector2(-1000, -1000);
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Boat");
			Description.SetDefault("Cool boat bruh");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Boat>(), player);
            player.buffTime[buffIndex] = 10;
            //player.direction = 1;
            //player.AddBuff(23, 10);
            
		}

	}
}
