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
        public Dictionary<Vector2, List<Vector2>> boundingTiles = new Dictionary<Vector2, List<Vector2>>();
        //public List<Vector2> boundingPositions = new List<Vector2>();
        bool clearBoundPos = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            drop = mod.ItemType("GravitySource");
            AddMapEntry(new Color(20, 20, 20));
        }
        public void CreateSurfaceMap(int i, int j, List<Vector2> appList)
        {
            
            CheckBound(i, j, new Vector2(i, j), appList);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //.Clear();
            List<Vector2> appList;
            if (boundingTiles.TryGetValue(new Vector2(i, j), out appList))
            {
                appList.Clear();
                CreateSurfaceMap(i, j, appList);
            }
            else
            {
                boundingTiles.Add(new Vector2(i, j), new List<Vector2>());
            }
            return base.PreDraw(i, j, spriteBatch);
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            boundingTiles.Remove(new Vector2(i, j));
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }
        public void CheckBound(int i, int j, Vector2 home, List<Vector2> appList)
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
                else if (Vector2.Distance(positions[q], home) > Vector2.Distance(thisPos, home) && checkTiles[q].type == ModContent.GetInstance<AsteroidRock>().Type)
                    CheckBound((int)positions[q].X, (int)positions[q].Y, home, appList);
            }
            if (nextToAir && !appList.Contains(thisPos)) appList.Add(thisPos);
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
        public void DrawGUIComps(SpriteBatch sb)
        {
            foreach (List<Vector2> l in boundingTiles.Values)
            {
                foreach (Vector2 v in l)
                {
                    Vector2 tileTLC = (16f * v) - Main.screenPosition;
                    //Main.NewText(v.X + " " + v.Y);
                    sb.Draw(ModContent.GetInstance<TEO>().pixel, new Rectangle((int)tileTLC.X, (int)tileTLC.Y, 16, 16), Color.Red * 0.5f);
                }
            }
        }

    }
}