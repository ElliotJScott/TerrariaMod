using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Placeable
{
	public class GravitySource : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a source of gravity.");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 30;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 150;
			item.createTile = mod.TileType("GravitySource");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}