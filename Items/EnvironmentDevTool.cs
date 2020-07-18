using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarSailor.Walls;
using System.Threading;

namespace StarSailor.Items
{
    class EnvironmentDevTool : ModItem
    {
        public static Vector2[] corners = new Vector2[2];
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is a modded item.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.value = 10000;
            item.rare = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.autoReuse = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
        public override bool UseItem(Player player)
        {
            Vector2 clickedTile = new Vector2((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16));
            string printText = "";
            if (player.altFunctionUse == 2) {
                corners[1] = clickedTile;
                printText = "Bottom right";
            }
            else {
                corners[0] = clickedTile;
                printText = "Top left";
            }
            Main.NewText(printText + " corner set to (" + clickedTile.X + ", " + clickedTile.Y +")");
            return true;
        }

        
    }
}

