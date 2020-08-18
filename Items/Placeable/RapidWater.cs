using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace StarSailor.Items.Placeable
{
	public class RapidWater : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is rapid water");

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
			item.createTile = mod.TileType("RapidWater");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.SetResult(this, 10);
			recipe.AddRecipe();
		}

	}
}
