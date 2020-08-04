using Terraria;
using Terraria.ModLoader;
using StarSailor.Mounts;
using Microsoft.Xna.Framework;

namespace StarSailor.Buffs
{
	public class StartingShipMount : ModBuff
	{
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
            player.mount.SetMount(ModContent.MountType<StartingShip>(), player);
            player.buffTime[buffIndex] = 10;
            player.direction = 1;
            player.AddBuff(23, 10);
            
		}

	}
}
