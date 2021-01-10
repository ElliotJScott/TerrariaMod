using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace StarSailor.Items.Placeable
{
	public class LaunchPad : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a launch pad tile.");

		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType("LaunchPad");
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }

	}
}
