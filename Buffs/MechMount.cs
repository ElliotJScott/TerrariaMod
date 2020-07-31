using Terraria;
using Terraria.ModLoader;
using StarSailor.Mounts;
using Terraria.ID;

namespace StarSailor.Buffs
{
    class MechMount : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mech");
            Description.SetDefault("Cool mech bruh");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Mech>(), player);
            player.buffTime[buffIndex] = 10;
            player.AddBuff(BuffID.Cursed, 10);
            //player.direction = 1;

        }
    }
}
