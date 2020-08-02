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
    class RodOfTransmutation : ModItem
    {
        const int maxRadius = 30;
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
            item.rare = ItemRarityID.Blue;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
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
            if (player.altFunctionUse == 2)
            {
                /*
                Item i = player.inventory[9];
                if (i.createWall != -1)
                {
                    SpreadClear((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), (int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), true, Math.Min(i.stack, maxRadius) - 2, new List<Point>());
                }
                if (i.createTile != -1)
                {
                    SpreadClear((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), (int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), false, Math.Min(i.stack, maxRadius) - 2, new List<Point>());
                }
                */
            }
            else
            {
                Item i = player.inventory[9];
                if (i.createWall != -1)
                {
                    SpreadPlacer((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), (int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), true, (ushort)i.createWall, 30);
                }
                if (i.createTile != -1)
                {
                    SpreadPlacer((int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), (int)(Main.MouseWorld.X / 16), (int)(Main.MouseWorld.Y / 16), false, (ushort)i.createTile, 30);
                }
            }
            return true;
        }
        public void SpreadPlacer(int i, int j, int initI, int initJ, bool wall, ushort id, int max, int IDtoReplace = -1)
        {
            Tile t = Framing.GetTileSafely(i, j);
            if (IDtoReplace == -1) IDtoReplace = t.type;
            if (IDtoReplace == id) return;
            if (wall)
            {
                if (t.wall == IDtoReplace)
                {
                    t.wall = id;
                    WorldGen.SquareWallFrame(i, j, true);
                    if (Vector2.Distance(new Vector2(i, j), new Vector2(initI, initJ)) <= max)
                    {
                        SpreadPlacer(i - 1, j, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i + 1, j, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i, j - 1, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i, j + 1, initI, initJ, wall, id, max, IDtoReplace);

                    }

                }
            }
            else
            {
                //Tile t = Main.tile[i, j];
                Tile r = GetTileInstance(id);
                ushort w = t.wall;

                if (t.type == IDtoReplace && t.active())
                {
                    t.CopyFrom(r);
                    t.frameX = 0;
                    t.frameY = 0;
                    t.wall = w;
                    WorldGen.SquareTileFrame(i, j, true);
                    if (Vector2.Distance(new Vector2(i, j), new Vector2(initI, initJ)) <= max)
                    {
                        SpreadPlacer(i - 1, j, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i + 1, j, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i, j - 1, initI, initJ, wall, id, max, IDtoReplace);
                        SpreadPlacer(i, j + 1, initI, initJ, wall, id, max, IDtoReplace);

                    }

                }



            }
        
        }
        private Tile GetTileInstance(ushort id, int i = 0, int j = 0)
        {
            Tile r = Framing.GetTileSafely(i, j);
            if (r.type == 0) return GetTileInstance(id, i, j + 1);
            else
            {
                Tile k = new Tile();
                k.CopyFrom(r);
                k.type = id;
                k.frameX = 0;
                k.frameY = 0;
                return k;
            }
        }
    }
}

