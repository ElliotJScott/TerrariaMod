using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace StarSailor.Items
{
	public class RopedPole : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a roped pole tile.");

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
			item.createTile = mod.TileType("RopedPole");
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
