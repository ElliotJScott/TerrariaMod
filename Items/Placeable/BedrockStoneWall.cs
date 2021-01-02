using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Placeable
{
	public class BedrockStoneWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This is bedrock stone wall.");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 7;
			item.useStyle = 1;
			item.consumable = true;
			item.createWall = mod.WallType("BedrockStoneWall");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<BedrockStone>(), 1);
			recipe.SetResult(this, 4);
			recipe.AddRecipe();
		}
	}
}