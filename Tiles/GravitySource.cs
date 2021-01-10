using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using StarSailor.Mounts;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarSailor.Tiles
{
    public class GravitySource : ModTile
    {
        public Dictionary<Vector2, List<BoundingTile>> boundingTiles = new Dictionary<Vector2, List<BoundingTile>>();
        //public List<Vector2> boundingPositions = new List<Vector2>();
        List<int> unallowedValues = new List<int>();
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

        public void CreateSurfaceMap(int i, int j, List<BoundingTile> appList)
        {
            List<Vector2> checkedLocations = new List<Vector2>();
            CheckBound(i, j, new Vector2(i, j), appList, checkedLocations);
        }
        public override void RandomUpdate(int i, int j)
        {
            
           
            List<BoundingTile> appList;
            if (boundingTiles.TryGetValue(new Vector2(i, j), out appList))
            {
                appList.Clear();
                CreateSurfaceMap(i, j, appList);
            }
            else
            {
                boundingTiles.Add(new Vector2(i, j), new List<BoundingTile>());
            }
            
            base.RandomUpdate(i, j);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            
            
            //.Clear();
            if (!boundingTiles.ContainsKey(new Vector2(i, j)))
            {
                List<BoundingTile> appList = new List<BoundingTile>();
                boundingTiles.Add(new Vector2(i, j), appList);
                CreateSurfaceMap(i, j, appList);
            }
            

            return base.PreDraw(i, j, spriteBatch);
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            boundingTiles.Remove(new Vector2(i, j));
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }
        public void CheckBound(int i, int j, Vector2 home, List<BoundingTile> appList, List<Vector2> checkedLocations)
        {
            Vector2 thisPos = new Vector2(i, j);
            checkedLocations.Add(thisPos);
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
            bool[] vals = new bool[4];
            for (int q = 0; q < checkTiles.Count; q++)
            {
                vals[q] = checkTiles[q].type == 0;
                if (checkTiles[q].type == 0 && !checkTiles[q].active())
                    nextToAir = true;
                else if (/*Vector2.Distance(positions[q], home) > Vector2.Distance(thisPos, home)
                    && */!checkedLocations.Contains(positions[q])
                    && !unallowedValues.Contains(checkTiles[q].type)
                    && Main.tileSolid[checkTiles[q].type])
                    CheckBound((int)positions[q].X, (int)positions[q].Y, home, appList, checkedLocations);
                    
            }
            if (nextToAir && !CheckListForTile(appList, thisPos)) appList.Add(new BoundingTile(thisPos, vals));
        }
        public bool CheckListForTile(List<BoundingTile> list, Vector2 v)
        {
            foreach (BoundingTile b in list)
                if (b.pos == v)
                    return true;
            return false;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        public override void NearbyEffects(int i, int j, bool closer)
        {
            //Main.player[Main.myPlayer].gravControl = true;
            //Main.player[Main.myPlayer].gravDir = -1f;
            //Main.player[Main.myPlayer].AddBuff(BuffID.Featherfall, 5, false);
            if (closer && !Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().gravSources.Contains(new Vector2(i, j)))
                Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().gravSources.Add(new Vector2(i, j));
            //Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity = true;
            base.NearbyEffects(i, j, closer);
        }
        
        public bool CheckIntersectSurface(Player player)
        {
            foreach (List<BoundingTile> l in boundingTiles.Values)
            {
                foreach (BoundingTile v in l)
                {
                    Vector2 tileTLC = (16f * v.pos) - Main.screenPosition;
                    for (int i = 0; i < 4; i++)
                    {

                        if (v.getDirValue((Direction)i))
                        {
                            Vector2 loc = v.pos;
                            Vector2 screenLoc = (16f * loc) - Main.screenPosition;
                            Vector2 dims = new Vector2(2, 2);
                            switch ((Direction)i)
                            {
                                case Direction.Up:
                                    screenLoc.Y -= 2 + dims.Y;
                                    dims.X = 16;
                                    break;
                                case Direction.Left:
                                    screenLoc.X -= 2 + dims.Y;
                                    dims.Y = 16;
                                    break;
                                case Direction.Right:
                                    screenLoc.X += 17;
                                    dims.Y = 16;
                                    break;
                                case Direction.Down:
                                    screenLoc.Y += 17;
                                    dims.X = 16;
                                    break;
                            }
                            Color c = Color.Orange * 0.5f;
                            Rectangle rect = new Rectangle((int)screenLoc.X, (int)screenLoc.Y, (int)dims.X, (int)dims.Y);
                            Rectangle testRect = new Rectangle((int)(screenLoc.X + Main.screenPosition.X), (int)(screenLoc.Y + Main.screenPosition.Y), (int)dims.X, (int)dims.Y);
                            Player p = player;
                            Rectangle playerRect = p.getRect();
                            float maxSize = playerRect.Height;
                            float minSize = playerRect.Width;
                            float diff = maxSize - minSize;
                            playerRect.Width = (int)maxSize;
                            //playerRect.Width = (int)(minSize + (0.5f * diff) + (-0.5f * diff * Math.Cos(2 * player.fullRotation)));
                            //playerRect.Height = (int)(minSize + (0.5f * diff) + (0.5f * diff * Math.Cos(2 * player.fullRotation)));
                            playerRect.X -= (int)(0.5f * (playerRect.Width - minSize));
                            playerRect.Y -= (int)(0.5f * (playerRect.Height - maxSize));
                            if (testRect.Intersects(playerRect))
                            {
                                return true;
                            }
                        }

                    }

                    //sb.Draw(ModContent.GetInstance<TEO>().pixel, new Rectangle((int)tileTLC.X, (int)tileTLC.Y, 16, 16), Color.Red * 0.5f);
                }
            }
            return false;

        }
        public void DrawGUIComps(SpriteBatch sb)
        {
            foreach (List<BoundingTile> l in boundingTiles.Values)
            {
                foreach (BoundingTile v in l)
                {
                    Vector2 tileTLC = (16f * v.pos) - Main.screenPosition;
                    for (int i = 0; i < 4; i++)
                    {

                        if (v.getDirValue((Direction)i))
                        {
                            Vector2 loc = v.pos;
                            Vector2 screenLoc = (16f * loc) - Main.screenPosition;
                            Vector2 dims = new Vector2(3, 3);
                            switch ((Direction)i)
                            {
                                case Direction.Up:
                                    screenLoc.Y -= 2 + dims.Y;
                                    dims.X = 16;
                                    break;
                                case Direction.Left:
                                    screenLoc.X -= 2 + dims.Y;
                                    dims.Y = 16;
                                    break;
                                case Direction.Right:
                                    screenLoc.X += 17;
                                    dims.Y = 16;
                                    break;
                                case Direction.Down:
                                    screenLoc.Y += 17;
                                    dims.X = 16;
                                    break;
                            }
                            Color c = Color.Orange * 0.5f;
                            Rectangle rect = new Rectangle((int)screenLoc.X, (int)screenLoc.Y, (int)dims.X, (int)dims.Y);
                            Rectangle testRect = new Rectangle((int)(screenLoc.X + Main.screenPosition.X), (int)(screenLoc.Y + Main.screenPosition.Y), (int)dims.X, (int)dims.Y);
                            Player p = Main.player[Main.myPlayer];
                            Rectangle playerRect = p.getRect();
                            float maxSize = playerRect.Height;
                            float minSize = playerRect.Width;
                            float diff = maxSize - minSize;
                            playerRect.Width = (int)maxSize;
                            //playerRect.Width = (int)(minSize + (0.5f * diff) + (-0.5f * diff * Math.Cos(2 * p.fullRotation)));
                            //playerRect.Height = (int)(minSize + (0.5f * diff) +  (0.5f * diff * Math.Cos(2 * p.fullRotation)));
                            playerRect.X -= (int)(0.5f * (playerRect.Width - minSize));
                            playerRect.Y -= (int)(0.5f * (playerRect.Height - maxSize));
                            if (testRect.Intersects(playerRect))
                            {
                                c = Color.Green * 0.5f;
                            }
                            sb.Draw(ModContent.GetInstance<StarSailorMod>().pixel, rect, c);

                        }

                    }

                    //sb.Draw(ModContent.GetInstance<TEO>().pixel, new Rectangle((int)tileTLC.X, (int)tileTLC.Y, 16, 16), Color.Red * 0.5f);
                }
            }
        }

    }

    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }
    public class BoundingTile
    {

        public Vector2 pos;
        bool[] isAir = new bool[4];
        public BoundingTile(Vector2 p, bool[] vals)
        {
            pos = p;
            isAir = vals;
        }
        public void setDirValue(Direction dir, bool val)
        {
            isAir[(int)dir] = val;
        }
        public bool getDirValue(Direction dir)
        {
            return isAir[(int)dir];
        }

    }

}