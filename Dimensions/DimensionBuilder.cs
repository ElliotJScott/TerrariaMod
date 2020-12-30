﻿using Microsoft.Xna.Framework;
using StarSailor.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarSailor.Dimensions
{
    static class DimensionBuilder
    {
        public static Tile[,] GenerateIceDimension(int width, int height)
        {
            Tile[,] product = GenerateEmptyDimension(width, height);
            float scaleFactor = (Main.maxTilesX / 4200f);
            float areaScaleFactor = (float)Math.Pow(scaleFactor, 2f);
            int maxX = Main.maxTilesX;
            int maxY = Main.maxTilesY;
            Vector2 max = new Vector2(maxX, maxY);
            int[] topHeights = GenerateHeightsByLine(width, 60);
            int[] bottomHeights = GenerateHeightsByLine(width, 20);
            int[] floorHeights = GenerateHeightsByLine(width, 20);
            int[] caveDisps = GetCaveDisps(width / 2);
            int b1 = maxY / 8;
            int b2 = (int)(maxY * 0.38);
            int b3 = (int)(maxY * 0.66);
            Tile t = new Tile();
            t.type = TileID.IceBlock;
            t.active(true);
            Tile wat = new Tile();
            wat.liquid = 1;
            wat.liquidType(Tile.Liquid_Water);
            //wat.active(true);
            for (int i = 0; i < maxX; i++)
            {
                int p1 = b1 + topHeights[i];
                int p2 = b2 + bottomHeights[i] + caveDisps[i];
                int p3 = b3 + floorHeights[i] - caveDisps[i];
                for (int j = p1; j < maxY; j++)
                {
                    if (j < p2 || j > p3) product[i, j] = new Tile(t);
                    else if (j > b3 + floorHeights[30] - caveDisps[30]) product[i, j] = new Tile(wat);
                }
            }
            int caveCounter = 0;
            while (caveCounter < areaScaleFactor * 400)
            {
                int x = Main.rand.Next(0, maxX);
                int y = Main.rand.Next(b1, b3 + 10);
                Tile tes = product[x, y];
                if (tes != null)
                {
                    if (tes.type == TileID.IceBlock)
                    {
                        int[] walls = { -1, -1, WallID.IceUnsafe, WallID.SnowWallUnsafe };
                        //product = AddCave(x, y, product, Main.rand.Next(10, 35), maxX, maxY, walls[Main.rand.Next(0, walls.Length)]);
                        //product = TileRunner(product, 0, x, y, Main.rand.NextFloat(0.25f, 0.49f), Main.rand.Next(10, 40), new Vector2(0), true, false, walls[Main.rand.Next(0, walls.Length)]);
                        caveCounter++;
                        ModContent.GetInstance<StarSailorMod>().Logger.Info("cave number " + caveCounter);
                    }
                }

            }

            int roughingCounter = 0;
            while (roughingCounter < areaScaleFactor * 200)
            {
                int x = Main.rand.Next(1, maxX-1);
                int y = Main.rand.Next(b1, b3 + 10);
                Tile tes = product[x, y];
                if (tes != null)
                {
                    if (tes.type == TileID.IceBlock)
                    {
                        int free = 0;
                        for (int p = -1; p <= 1; p += 2)
                        {
                            if (product[x + p, y] == null || product[x + p, y].type == TileID.Dirt) free++;
                            if (product[x, y + p] == null || product[x, y + p].type == TileID.Dirt) free++;
                        }
                        if (Main.rand.Next(0, free + 1) != 0)
                        {
                            int[] walls = { -1, -1, WallID.IceUnsafe, WallID.SnowWallUnsafe };
                            product = TileRunner(product, 0, x, y, Main.rand.NextFloat(0.45f, 0.49f), Main.rand.Next(4, 12), new Vector2(0, 0), true, false, walls[Main.rand.Next(0, walls.Length)]);
                            roughingCounter++;
                            ModContent.GetInstance<StarSailorMod>().Logger.Info("roughing number " + roughingCounter);
                        }
                    }
                }
            }
            return product;
        }
        public static int[] GetCaveDisps(int width)
        {
            int[] ret = new int[width];
            for (int i = 0; i < width; i++)
            {
                ret[i] = (int)((200f / Math.Pow(width / 2f, 4f)) * Math.Pow(i - (width / 2f), 4f));
            }
            return ret;
        }
        #region Jungle Stuff
        public static Tile[,] GenerateJungleDimension(int width, int height)
        {
            Tile[,] product = GenerateEmptyDimension(width, height);
            float scaleFactor = (Main.maxTilesX / 4200f);
            int maxX = Main.maxTilesX;
            int maxY = Main.maxTilesY;
            Vector2 max = new Vector2(maxX, maxY);
            int[] rightHeights = GenerateHeightsByLine(maxX / 2, 40);
            int[] leftHeights = GenerateHeightsByNoise(maxX / 2, 4, 0.85f);
            for (int q = 0; q < rightHeights.Length; q++) rightHeights[q] += leftHeights.Last();
            Tile jgr = new Tile();
            Tile jd = new Tile();
            Tile sand = new Tile();
            jgr.type = (ushort)ModContent.TileType<SwampGrass>();
            jd.type = (ushort)ModContent.TileType<SwampDirt>();
            sand.type = TileID.Sand;
            jgr.active(true);
            jd.active(true);
            sand.active(true);
            int initPos = maxY / 4;
            for (int i = 0; i < maxX / 2; i++)
            {
                int k1 = initPos + leftHeights[i];
                product[i, k1] = new Tile(sand);
                k1++;
                while (k1 < maxY)
                {
                    product[i, k1] = new Tile(sand);
                    k1++;
                }
                int k2 = initPos + rightHeights[i];
                product[(maxX / 2) + i, k2] = new Tile(jgr);
                k2++;
                int diff;
                if (i == 0) diff = rightHeights[i] - rightHeights[i + 1];
                else if (i == (maxX / 2) - 1) diff = rightHeights[i] - rightHeights[i - 1];
                else diff = Math.Max(rightHeights[i] - rightHeights[i - 1], rightHeights[i] - rightHeights[i + 1]);
                diff = Math.Max(diff, 0);
                while (k2 < maxY)
                {
                    if (diff >= k2 - (1 + initPos + rightHeights[i])) product[(maxX / 2) + i, k2] = new Tile(jgr);
                    else product[(maxX / 2) + i, k2] = new Tile(jd);
                    if (k2 - (initPos + rightHeights[i]) > diff + 3) product[(maxX / 2) + i, k2].wall = WallID.TungstenBrick;
                    k2++;
                }

            }
            return product;
        }
        #endregion
        #region Asteroid Stuff
        public static Tile[,] GenerateAsteroidDimension(int width, int height)
        {
            Tile[,] product = GenerateEmptyDimension(width, height);
            float scaleFactor = (Main.maxTilesX / 4200f);
            int maxX = Main.maxTilesX;
            int maxY = Main.maxTilesY;
            Vector2 max = new Vector2(maxX, maxY);
            ModContent.GetInstance<StarSailorMod>().Logger.Info("Max vector = " + max);
            int originX = Main.spawnTileX;
            int originY = Main.spawnTileY;
            //Vector2 origin = new Vector2(originX, originY);
            Vector2 origin = new Vector2(maxX / 2, maxY / 2);
            List<Vector2> sources = new List<Vector2>();
            for (int i = 0; i < (8 * scaleFactor); i++)
            {
                sources.Add(GetSourceLoc(max, origin, sources));
            }
            int astCounter = 0;
            sources.Add(origin);
            while (astCounter < 1300f * (scaleFactor * scaleFactor))
            {
                Vector2 v = new Vector2(Main.rand.Next(50, (int)max.X - 50), Main.rand.Next(50, (int)max.Y - 50));
                if (TryAsteroid(v, max, origin, sources))
                {
                    astCounter++;
                    product = AddAsteroid((int)v.X, (int)v.Y, product, Main.rand.Next(5, 10));
                }
            }
            int templeCounter = 0;
            while (templeCounter < 2 * scaleFactor)
            {
                Vector2 v = new Vector2(Main.rand.Next(150, (int)max.X - 150), Main.rand.Next(150, (int)max.Y - 150));
                if (TryAsteroid(v, max, origin, sources))
                {
                    templeCounter++;
                    product = AddAsteroidTemple(product, (int)v.X, (int)v.Y);
                }
            }
            int spiralCounter = 0;
            while (spiralCounter < 6 * scaleFactor)
            {
                Vector2 v = new Vector2(Main.rand.Next(100, (int)max.X - 100), Main.rand.Next(100, (int)max.Y - 100));
                if (TryAsteroid(v, max, origin, sources))
                {
                    spiralCounter++;
                    product = AddSpiralLootAsteroid(product, (int)v.X, (int)v.Y);
                }
            }
            return product;
        }
        static Tile[,] AddAsteroidTemple(Tile[,] tile, int i, int j)
        {
            return AddAsteroid(i, j, tile, 45);
        }
        static Tile[,] AddSpiralLootAsteroid(Tile[,] tile, int i, int j)
        {
            int size = Main.rand.Next(15, 20);
            Tile[,] product = AddAsteroid(i, j, tile, size);
            Vector2 init = Main.rand.NextVector2Unit() * size;


            List<Vector2> points = new List<Vector2>();
            points.Add(init);
            bool reachedCentre = false;
            int ccw = (Main.rand.Next(0, 2) * 2) - 1;
            while (!reachedCentre)
            {
                Vector2 currPoint = points.Last();
                Vector2 tang = new Vector2(currPoint.Y, -currPoint.X) * ccw;
                tang.Normalize();
                Vector2 modify = currPoint;
                modify.Normalize();
                tang -= modify * 0.2f;
                Vector2 newPoint = currPoint + tang;
                if (newPoint.Length() <= 6) reachedCentre = true;
                else points.Add(newPoint);
            }
            foreach (Vector2 p in points)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if ((x ^ 2) + (y ^ 2) <= 1)
                        {
                            Tile t = new Tile();
                            t.type = 0;
                            t.active(false);
                            t.wall = 1;
                            product[i + x + (int)p.X, j + y + (int)p.Y] = t;
                        }
                    }
                }
            }
            return product;
        }
        static bool TryAsteroid(Vector2 pos, Vector2 mx, Vector2 src, List<Vector2> sources)
        {
            Vector2 c = new Vector2(100000);
            Vector2 sc = new Vector2(100000);
            foreach (Vector2 v in sources)
            {
                if ((v - pos).Length() < c.Length()) c = v;
                else if ((v - pos).Length() < sc.Length()) sc = v;
            }
            Vector3 c3 = new Vector3(c.X, c.Y, 0);
            Vector3 sc3 = new Vector3(sc.X, sc.Y, 0);
            Vector3 pos3 = new Vector3(pos.X, pos.Y, 0);
            Vector3 src3 = new Vector3(src.X, src.Y, 0);
            Vector3 diff = c3 - sc3;
            //Vector3 diff2 = c3 - src3;
            Vector3 top = Vector3.Cross(diff, pos3) + Vector3.Cross(sc3, c3);
            //Vector3 top2 = Vector3.Cross(diff2, pos3) + Vector3.Cross(src3, c3);
            float len = top.Length() / diff.Length();
            //float len2 = top2.Length() / diff2.Length();
            float flt = mx.Y / 4;
            float frac1 = (flt - len) / flt;
            //float frac2 = (flt - len2) / flt;
            float frac = Math.Max(0, frac1);
            return Main.rand.NextFloat(0, 1) < frac;
        }
        static Vector2 GetSourceLoc(Vector2 mx, Vector2 src, List<Vector2> currentLocs)
        {
            Vector2 v = new Vector2(Main.rand.Next(50, (int)mx.X - 50), Main.rand.Next(50, (int)mx.Y - 50));
            foreach (Vector2 f in currentLocs) if ((f - v).Length() < 100) return GetSourceLoc(mx, src, currentLocs);
            float len = (v - src).Length();
            float flt = mx.Y / 2;
            float frac = len / flt;
            if (Main.rand.NextFloat(0, 1) < frac) return v;
            else return GetSourceLoc(mx, src, currentLocs);

        }
        static Tile[,] AddAsteroid(int i, int j, Tile[,] tileData, int avSize)
        {

            float rotation = Main.rand.NextFloat(0, (float)Math.PI / 2);
            int[] distances = new int[4];
            for (int q = 0; q < 4; q++)
            {
                distances[q] = (int)(avSize * Main.rand.NextFloat(0.7f, 1.3f));
            }
            int max = distances.Max();
            //ModContent.GetInstance<StarSailorMod>().Logger.Info("Adding asteroid at (" + i + "," + j + ") Max is " + max + " Distances are " + distances[0] + "," + distances[1] + "," + distances[2] + "," + distances[3]);

            for (int x = -max; x <= max; x++)
            {
                for (int y = -max; y <= max; y++)
                {
                    //ModContent.GetInstance<StarSailorMod>().Logger.Info("(" + i + "," + j + ") : (" + x + "," + y + ")");
                    //float dist = CalcDistance(i, j, i + x, j + y);
                    float dist = (float)Math.Sqrt((x * x) + (y * y));
                    float angle = CalcAngle(i, j, i + x, j + y) - rotation;
                    if (angle < 0) angle += 2f * (float)Math.PI;
                    int k = (int)((2 * angle) / Math.PI) % 4;
                    int k2 = (k + 1) % 4;
                    float frac = (float)(Math.IEEERemainder(angle, Math.PI / 2) / (Math.PI / 2));
                    float maxDist = (frac * distances[k]) + ((1 - frac) * distances[k2]);
                    //float maxDist = max;
                    if (dist < maxDist)
                    {

                        Tile t = tileData[i + x, j + y];
                        if (t == null)
                        {
                            t = new Tile();
                            t.active(true);
                            t.type = (ushort)ModContent.TileType<AsteroidRock>();
                            tileData[i + x, j + y] = t;


                        }
                        else if (t.type != (ushort)ModContent.TileType<GravitySource>())
                        {
                            t.type = (ushort)ModContent.TileType<AsteroidRock>();
                            t.active(true);
                            tileData[i + x, j + y] = t;
                        }

                    }
                }
            }
            for (int x = -max; x <= max; x++)
            {
                for (int y = -max; y <= max; y++)
                {
                    if (tileData[i + x, j + y] != null)
                    {
                        if (tileData[i + x, j + y].type == ModContent.TileType<AsteroidRock>())
                        {
                            Vector2 thisPos = new Vector2(i + x, j + y);
                            List<Tile> checkTiles = new List<Tile>();
                            List<Vector2> positions = new List<Vector2>();
                            for (int p = -1; p <= 1; p += 2)
                            {
                                checkTiles.Add(tileData[i + x, j + y + p]);
                                positions.Add(new Vector2(i + x, j + y + p));
                                checkTiles.Add(tileData[i + x + p, j + y]);
                                positions.Add(new Vector2(i + x + p, j + y));
                            }
                            bool[] vals = new bool[4];
                            for (int q = 0; q < checkTiles.Count; q++)
                            {
                                try
                                {
                                    vals[q] = checkTiles[q].type == 0 && !checkTiles[q].active();
                                }
                                catch
                                {
                                    vals[q] = true;
                                }
                            }
                            byte slopeid = 0;
                            if (vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 1;
                            else if (vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 2;
                            else if (!vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 3;
                            else if (!vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 4;
                            tileData[i + x, j + y].slope(slopeid);
                        }
                    }
                }
            }
            if (tileData[i, j] == null) tileData[i, j] = new Tile();
            //tileData[i, j].type = 1;
            tileData[i, j].type = (ushort)ModContent.TileType<GravitySource>();
            tileData[i, j].active(true);
            return tileData;
        }
        #endregion
        public static Tile[,] GenerateEmptyDimension(int width, int height)
        {
            Tile[,] output = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    output[i, j] = null;
                }
            }
            return output;
        }
        public static TagCompound GenerateCompressedEmptyDimension(int width, int height)
        {
            List<int> counterIDs = new List<int>();
            List<int> counterCounts = new List<int>();
            List<int> tileIDs = new List<int>();
            List<TagCompound> tileData = new List<TagCompound>();
            List<Vector2> pos = new List<Vector2>();
            List<int> data = new List<int>();
            counterIDs.Add(-1);
            counterCounts.Add((width * height) - 1);
            counterIDs.Add(0);
            counterCounts.Add(1);
            tileIDs.Add(-1);
            tileIDs.Add(0);
            tileData.Add(new TileData().ToTagCompound());
            tileData.Add(new TileData(1, 0, true, 0, 0, 0, -1, -1).ToTagCompound());
            return new TagCompound
            {
                ["tileIDs"] = tileIDs,
                ["tileData"] = tileData,
                ["counterIDs"] = counterIDs,
                ["counterCounts"] = counterCounts,
                ["extraLocations"] = pos,
                ["extraData"] = data,
            };
            /*
            BasicTileData basicTileData = new BasicTileData(width, height);
            ExtraTileData extraTileData = new ExtraTileData(new List<Vector2>(), new List<int>());
            return (basicTileData, extraTileData);
            */
        }
        static int[] GenerateHeightsByLine(int width, int maxHeight)
        {
            int maxChange = 4;
            int[] output = new int[width];
            int direction = 1;
            int lastVal = 0;
            for (int index = 0; index < width; index++)
            {
                float frac = Math.Abs((float)lastVal / maxHeight);
                ModContent.GetInstance<StarSailorMod>().Logger.Info("index " + index + " direction " + direction + " lastVal " + lastVal + " frac " + frac);
                int sign = Math.Sign(lastVal);
                if (Main.rand.NextFloat(0, 1) < frac) direction -= sign;
                else if (Main.rand.NextFloat(0, 1) < (1 - frac)) direction += sign;
                else if (direction == 0 && Main.rand.Next(0, 2) == 0) direction += Main.rand.Next(-1, 2);
                if (Main.rand.Next(0, 5) == 0) direction /= 2;
                else if (Main.rand.Next(0, 5) == 0) direction += Main.rand.Next(-1, 2);
                else if (Main.rand.Next(0, 5) == 0) direction /= -2;
                else if (Main.rand.Next(0, 5) == 0) direction = 0;
                direction = Math.Min(Math.Max(direction, -maxChange), maxChange);
                output[index] = lastVal + direction;
                lastVal = output[index];
            }
            return output;
        }
        static int[] GenerateHeightsByNoise(int width, int range, float factor) //These are generated relative to the zero line
        {
            int[] output = new int[width];
            for (int i = 0; i < output.Length; i++) output[i] = 0;
            int counter = width / 2;
            int num = 0;
            while (counter >= 1)
            {
                int size = width / counter;
                int[] values = new int[size];
                for (int i = 0; i < size; i++) values[i] = Main.rand.Next(-range, range);
                for (int i = 0; i < width; i++)
                {
                    int index = i / counter;
                    try
                    {
                        output[i] += (int)(Math.Pow(factor, num) * values[index]);
                    }
                    catch
                    {
                        output[i] += (int)(Math.Pow(factor, num) * values[index - 1]);
                    }
                }
                num++;
                counter /= 2;
            }
            return output;

        }
        static Tile[,] TileRunner(Tile[,] tile, ushort type, int i, int j, float chance, int numSteps, Vector2 relPos, bool onlyReplaceTiles = true, bool active = true, int wall = -1)
        {
            if (onlyReplaceTiles && (tile[i, j] == null || !tile[i, j].active())) return tile;
            Tile t = new Tile();
            t.type = type;
            t.active(active);
            if (wall != -1) t.wall = (ushort)wall;
            tile[i, j] = new Tile(t);
            if (numSteps <= 0) return tile;
            for (int p = -1; p <= 1; p += 2)
            {
                if (Main.rand.NextFloat(0, 1) < chance && (relPos + new Vector2(p, 0)).Length() > relPos.Length()) tile = TileRunner(tile, type, i + p, j, chance, numSteps - 1, relPos, onlyReplaceTiles, active, wall);
                if (Main.rand.NextFloat(0, 1) < chance && (relPos + new Vector2(0, p)).Length() > relPos.Length()) tile = TileRunner(tile, type, i, j + p, chance, numSteps - 1, relPos, onlyReplaceTiles, active, wall);
            }
            return tile;
        }
        static Tile[,] AddCave(int i, int j, Tile[,] tileData, int avSize, int maxX, int maxY, int wall = -1)
        {
            float rotation = Main.rand.NextFloat(0, (float)Math.PI / 2);
            //rotation = 0;
            int[] distances = new int[4];
            for (int q = 0; q < 4; q++)
            {
                distances[q] = (int)(avSize * Main.rand.NextFloat(0.3f, 1.3f));
            }
            int max = distances.Max();
            //ModContent.GetInstance<StarSailorMod>().Logger.Info("Adding asteroid at (" + i + "," + j + ") Max is " + max + " Distances are " + distances[0] + "," + distances[1] + "," + distances[2] + "," + distances[3]);

            for (int x = -max; x <= max; x++)
            {
                for (int y = -max; y <= max; y++)
                {
                    //ModContent.GetInstance<StarSailorMod>().Logger.Info("(" + i + "," + j + ") : (" + x + "," + y + ")");
                    //float dist = CalcDistance(i, j, i + x, j + y);
                    float dist = (float)Math.Sqrt((x * x) + (y * y));
                    float angle = CalcAngleAlt(x, y) - rotation;
                    if (angle < 0) angle += 2f * (float)Math.PI;
                    int k = (int)(angle / (Math.PI / 2)) % 4;
                    int k2 = (k + 1) % 4;
                    float frac = (float)((angle % Math.PI / 2) / (Math.PI / 2));
                    float maxDist = ((1 - frac) * distances[k]) + (frac * distances[k2]);
                    //ModContent.GetInstance<StarSailorMod>().Logger.Info("frac "  + frac + " x,y = (" + x + "," + y+ ") k1,k2 = (" + k + "," + k2 + ") + angle " + angle + " maxdist " + maxDist);
                    //maxDist = 3 * angle;
                    if (dist < maxDist && i + x >= 0 && i + x < maxX && j + y >= 0 && j + y < maxY)
                    {

                        Tile t = tileData[i + x, j + y];
                        if (t != null && t.type == TileID.IceBlock)
                        {
                            t = new Tile();
                            t.active(false);
                            t.type = 0;
                            if (wall != -1) t.wall = (ushort)wall;
                            tileData[i + x, j + y] = t;


                        }

                    }
                }
            }
            return tileData;
        }
        static float CalcDistance(int x1, int y1, int x2, int y2)
        {
            return (float)Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
        }
        static float CalcAngleAlt(int x, int y)
        {
            Vector2 v = new Vector2(x, y);
            if (v.Length() == 0) return 0;
            else
            {
                float angle = (float)Math.Acos(x / v.Length());
                if (y > 0) angle = (float)(2 * Math.PI) - angle;
                return angle;
            }
        }
        static float CalcAngle(int x1, int y1, int x2, int y2) //if the two points are the same this gives 3 pi / 2
        {
            if (x2 == x1)
            {
                if (y2 > y1) return (float)Math.PI / 2;
                else return 3f * (float)Math.PI / 2;
            }
            else if (y2 == y1)
            {
                if (x2 < x1) return (float)Math.PI;
                else return 0;
            }
            else
            {
                float f = (float)Math.Atan((y2 - y1) / (x2 - x1));
                if (y2 < y1) f += (float)Math.PI;
                if (f < 0) f += 2f * (float)Math.PI;
                return f;
            }
        }
    }
}
