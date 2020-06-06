using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System.Collections.Generic;
using teo.Mounts;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace teo.Tiles
{
    public class GravitySource : ModTile
    {
        public List<Vector2> boundingPositions = new List<Vector2>();
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("GravitySource");
            AddMapEntry(new Color(20, 20, 20));
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            CreateSurfaceMap(i, j);
            base.PlaceInWorld(i, j, item);
        }
        public override void RandomUpdate(int i, int j)
        {
            CreateSurfaceMap(i, j);
            base.RandomUpdate(i, j);
        }
        /*
         string error = "";
            int width = 11;
            for (int k = -16; k <= 0; k++)
            {
                for (int l = -width; l <= width; l++)
                {
                    Tile tile = Framing.GetTileSafely((int)position.X + l, (int)position.Y + k);
                    if (k == 0)
                    {

                        if (tile.type != ModContent.GetInstance<LaunchPad>().Type)
                        {
                            error = "Launch pad is not wide enough";
                        }
                    }
                    else if ((k == -1 || k == -2) && l == 0)
                    {
                        if (tile.type != ModContent.GetInstance<LaunchConsole>().Type)
                        {
                            error = "Something is very wrong here...and yet, a little bit right";
                        }
                    }
                    else
                    {
                        if (tile.type != 0)
                        {
                            error = "Launch pad is not uncovered";
                        }
                    }

                }
            } 
         */

        public void CreateSurfaceMap(int i, int j)
        {
            boundingPositions.Clear();
            CheckBound(i, j, new Vector2(i, j));
        }
        public void CheckBound(int i, int j, Vector2 home)
        {
            Vector2 thisPos = new Vector2(i, j);
            List<Tile> checkTiles = new List<Tile>();
            List<Vector2> positions = new List<Vector2>();
            for (int p = -1; p <= 1; p += 2)
            {
                checkTiles.Add(Framing.GetTileSafely(i, j + p));
                positions.Add(new Vector2(i, j + p));
                checkTiles.Add(Framing.GetTileSafely(i + p, j));
                positions.Add(new Vector2(i + p, j));
            }
            bool nextToAir = false;
            for (int q = 0; q < checkTiles.Count; q++)
            {
                if (checkTiles[q].type == 0)
                    nextToAir = true;
                else if (Vector2.Distance(positions[q], home) > Vector2.Distance(thisPos, home) && checkTiles[q].type != ModContent.GetInstance<AsteroidRock>().Type)
                    CheckBound((int)positions[q].X, (int)positions[q].Y, home);
            }
            if (nextToAir && !boundingPositions.Contains(thisPos)) boundingPositions.Add(thisPos);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            //Main.NewText(i + " " + j);
            //Main.player[Main.myPlayer].gravControl = true;
            //Main.player[Main.myPlayer].gravDir = -1f;
            //Main.player[Main.myPlayer].AddBuff(BuffID.Featherfall, 5, false);
            if (closer && !Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().gravSources.Contains(new Vector2(i, j)))
                Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().gravSources.Add(new Vector2(i, j));
            Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity = closer;
            base.NearbyEffects(i, j, closer);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 blocTLCorner = (16f * new Vector2(i, j)) - Main.screenPosition;
            base.PostDraw(i, j, spriteBatch);
        }

    }
}