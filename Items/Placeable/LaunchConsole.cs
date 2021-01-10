using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Placeable
{
	public class LaunchConsole : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is a console.");
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
			item.createTile = mod.TileType("LaunchConsole");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Ruby, 5);
            recipe.AddIngredient(ItemID.Emerald, 5);
            recipe.AddIngredient(ItemID.Sapphire, 5);
            recipe.AddIngredient(ModContent.ItemType<LaunchPad>(), 20);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}