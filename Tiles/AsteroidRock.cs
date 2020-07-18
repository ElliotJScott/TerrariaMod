using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Tiles
{
	public class AsteroidRock : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("GravitySource")] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			drop = mod.ItemType("AsteroidRock");
			AddMapEntry(new Color(20, 20, 20));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            ModContent.GetInstance<GravitySource>().boundingTiles.Clear();
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }

    }
}