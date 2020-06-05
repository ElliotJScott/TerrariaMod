using Terraria;
using Terraria.ModLoader;
using teo.Mounts;
using Microsoft.Xna.Framework;

namespace teo.Buffs
{
	public class RocketMount : ModBuff
	{
        public static Vector2 cameraPosition = new Vector2(-1000, -1000);
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rocket");
			Description.SetDefault("Cool rocket bruh");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Rocket>(), player);
            player.buffTime[buffIndex] = 10;
            player.direction = 1;
            player.AddBuff(23, 10);
            
		}
	}
}
