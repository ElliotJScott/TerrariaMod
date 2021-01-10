using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.Dimensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items
{
    abstract class StarMap : ModItem
    {
        public abstract Dimensions.Dimensions Destination { get; }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shootSpeed = 6f;
            item.width = 16;
            item.height = 16;
            item.maxStack = 1;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 40;
            item.useTime = 40;
            item.noUseGraphic = false;
            item.noMelee = true;
            item.rare = ItemRarityID.Purple;
            //base.SetDefaults();
        }
        public override bool UseItem(Player player)
        {
            ModContent.GetInstance<DimensionManager>().DiscoverDimension(Destination);
            return base.UseItem(player);
        }
    }
    class IceStarMap : StarMap
    {
        public override Dimensions.Dimensions Destination => Dimensions.Dimensions.Ice;
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
    class AstStarMap : StarMap
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asteroid Star Map");           
        }
        public override Dimensions.Dimensions Destination => Dimensions.Dimensions.Asteroid;
    }
    class DesertStarMap : StarMap
    {
        public override Dimensions.Dimensions Destination => Dimensions.Dimensions.Jungle;
    }
}
