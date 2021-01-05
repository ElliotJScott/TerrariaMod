﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace StarSailor.Dimensions
{

    public enum Dimensions
    {
        Overworld,
        Travel,
        Ice,
        Asteroid,
        Jungle,
    }
    internal struct TileData
    {
        public static TileData NULLDATA = new TileData();
        public ushort id; //This should only go up to 2048
        public ushort wall; //This should only go up to 2048
        public bool active; //This is 0 or 1
        public byte liq;
        public byte ltype; //If i need more space for tiles i can tidy this up since liquids is 2 bits
        public byte slope;
        public short frameX, frameY;

        public TileData(ushort i, ushort w, bool a, byte l, byte lt, byte s, short fx, short fy)
        {
            id = i;
            wall = w;
            active = a;
            liq = l;
            ltype = lt;
            slope = s;
            frameX = fx;
            frameY = fy;

        }
        public TileData(TagCompound tag)
        {
            id = tag.Get<ushort>("id");
            wall = tag.Get<ushort>("wall");
            active = tag.GetBool("active");
            liq = tag.GetByte("liq");
            ltype = tag.GetByte("ltype");
            slope = tag.GetByte("slope");
            frameX = tag.GetShort("frameX");
            frameY = tag.GetShort("frameY");
        }
        public override bool Equals(object obj)
        {
            TileData other = (TileData)obj;
            return other.id == id && other.wall == wall && other.active == active && other.liq == liq && other.ltype == ltype && other.slope == slope && other.frameX == frameX && other.frameY == frameY;
        }
        public TagCompound ToTagCompound()
        {
            return new TagCompound
            {
                ["id"] = id,
                ["wall"] = wall,
                ["active"] = active,
                ["liq"] = liq,
                ["ltype"] = ltype,
                ["slope"] = slope,
                ["frameX"] = frameX,
                ["frameY"] = frameY
            };
        }

        //autogenerated this so vs would shut up
        public override int GetHashCode()
        {
            var hashCode = 376460140;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + wall.GetHashCode();
            hashCode = hashCode * -1521134295 + active.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(TileData a, TileData b) => a.Equals(b);
        public static bool operator !=(TileData a, TileData b) => !(a == b);
    }
    class Dimension
    {
        float[] gravityVals = { 0.4f, 0f, 0.25f, 0f, 0.45f };
        Color[] sunlightColors = { new Color(255, 255, 255), new Color(255, 255, 255), new Color(125, 175, 255), new Color(210, 210, 210), new Color(150, 130, 130) };
        //public BasicTileData basicTileData;
        //public ExtraTileData extraTileData;
        public TagCompound data;
        public Dimensions dimension;
        //Tile[,] tile;
        public Chest[] chest;
        public Sign[] sign;
        public Dimension(Dimensions d, GenerationProgress progress)
        {
            dimension = d;
            //tile = new Tile[Main.tile.GetLength(0), Main.tile.GetLength(1)];
            //chest = new Chest[Main.maxChests];
            //sign = new Sign[Main.sign.Length];
            Generate(progress);
        }
        public Dimension(Dimensions d, TagCompound t)
        {
            dimension = d;
            data = t;
            chest = new Chest[Main.maxChests];
            sign = new Sign[Main.maxChests];
        }
        public Dimension(Dimensions d)
        {
            dimension = d;
            //ModContent.GetInstance<StarSailorMod>().Logger.Info(d + " generating");
            /*
            (BasicTileData, ExtraTileData) data2 = DimensionBuilder.GenerateCompressedEmptyDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
            basicTileData = data2.Item1;
            extraTileData = data2.Item2;
            */
            data = DimensionBuilder.GenerateCompressedEmptyDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
            chest = new Chest[Main.maxChests];
            sign = new Sign[Main.maxChests];
        }
        public Dimension(Dimensions d, Tile[,] t, Chest[] c, Sign[] s)
        {
            ModContent.GetInstance<StarSailorMod>().Logger.Info("Creating dimension from overworld");
            dimension = d;
            chest = c;
            sign = s;
            data = GetCompressedTileData(t);
        }
        public float GetGravity() => gravityVals[(int)dimension];
        public Color GetSunlight() => sunlightColors[(int)dimension];
        public void Generate()
        {
            ModContent.GetInstance<StarSailorMod>().Logger.Info("Generating " + dimension);
            Generate(new GenerationProgress());
        }
        public void Generate(GenerationProgress progress)
        {
            switch (dimension)
            {
                /*
                case Dimensions.Overworld:
                    progress.Message = "Finishing up overworld";
                    data = GetCompressedTileData(Main.tile);
                    //basicTileData = GetCompressedBasicTileData(Main.tile);
                    //extraTileData = GetExtraTileData(Main.tile);
                    chest = Main.chest;
                    sign = Main.sign;
                    break;
                    */
                case Dimensions.Jungle:
                    progress.Message = "Generating the Jungle Dimension";
                    Tile[,] tilej = DimensionBuilder.GenerateJungleDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
                    data = GetCompressedTileData(tilej);
                    chest = new Chest[Main.maxChests];
                    sign = new Sign[Main.maxChests];
                    break;
                case Dimensions.Ice:
                    progress.Message = "Generating the Ice Dimension";
                    Tile[,] tilei = DimensionBuilder.GenerateIceDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
                    data = GetCompressedTileData(tilei);
                    chest = new Chest[Main.maxChests];
                    sign = new Sign[Main.maxChests];
                    break;
                case Dimensions.Asteroid:

                    progress.Message = "Generating the Asteroid Dimension";
                    Tile[,] tilea = DimensionBuilder.GenerateAsteroidDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
                    data = GetCompressedTileData(tilea);
                    chest = new Chest[Main.maxChests];
                    sign = new Sign[Main.maxChests];
                    break;

                case Dimensions.Overworld:
                case Dimensions.Travel:
                default:
                    progress.Message = "Generating Empty Space";
                    //(BasicTileData, ExtraTileData) data2 = DimensionBuilder.GenerateCompressedEmptyDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
                    //basicTileData = data2.Item1;
                    //extraTileData = data2.Item2;
                    data = DimensionBuilder.GenerateCompressedEmptyDimension(Main.tile.GetLength(0), Main.tile.GetLength(1));
                    chest = new Chest[Main.maxChests];
                    sign = new Sign[Main.maxChests];
                    break;


            }

        }

        public static TagCompound GetCompressedTileData(Tile[,] tile) //Not compressed very well but ehhhh
        {
            TileData currentTile = TileData.NULLDATA;
            (int, int) currentCounter = (-1, 0);
            Dictionary<TileData, int> dictionary = new Dictionary<TileData, int>();
            List<(int, int)> tileList = new List<(int, int)>(); //First ushort is the dictionary id, second int is the quantity
                                                                //ModContent.GetInstance<StarSailorMod>().Logger.Info(dimension + " peep ");

            for (int j = 0; j < tile.GetLength(1); j++)
            {
                for (int i = 0; i < tile.GetLength(0); i++)
                {
                    Tile t = tile[i, j];
                    if (t == null)
                    {
                        currentTile = TileData.NULLDATA;
                        if (dictionary.ContainsValue(-1))
                        {
                            if (currentCounter.Item1 == -1) currentCounter.Item2 += 1;
                            else
                            {
                                if (currentCounter.Item2 >= 1)
                                    tileList.Add(currentCounter);
                                currentCounter = (-1, 1);
                            }
                        }
                        else
                        {
                            dictionary.Add(TileData.NULLDATA, -1);
                            if (currentCounter.Item2 >= 1)
                                tileList.Add(currentCounter);
                            currentCounter = (-1, 1);

                        }
                    }
                    else
                    {

                        TileData td = new TileData(t.type, t.wall, t.active(), t.liquid, t.liquidType(), t.slope(), t.frameX, t.frameY);
                        if (td == currentTile)
                        {
                            currentCounter.Item2 += 1;
                        }
                        else if (currentCounter.Item2 >= 1)
                        {
                            tileList.Add(currentCounter);
                            currentTile = td;
                            int val;
                            if (dictionary.TryGetValue(td, out val))
                            {
                                currentCounter = (val, 1);
                            }
                            else
                            {
                                int newID;
                                if (td != TileData.NULLDATA)
                                {
                                    newID = dictionary.Count;
                                }
                                else newID = -1;

                                dictionary.Add(td, newID);
                                currentCounter = (newID, 1);
                            }
                        }
                    }
                }
            }
            if (currentCounter.Item2 >= 1) tileList.Add(currentCounter);
            Main.NewText("Compressed tiles into a list of length " + tileList.Count);
            List<Vector2> pos = new List<Vector2>();
            List<int> data = new List<int>();
            for (int i = 0; i < tile.GetLength(0); i++)
            {
                for (int j = 0; j < tile.GetLength(1); j++)
                {
                    Tile t = tile[i, j];
                    if (t != null)
                    {
                        int num = 0;
                        num += t.actuator() ? 1 : 0;
                        num += t.wire() ? 2 : 0;
                        num += t.wire2() ? 4 : 0;
                        num += t.wire3() ? 8 : 0;
                        num += t.wire4() ? 16 : 0;
                        if (num != 0)
                        {
                            pos.Add(new Vector2(i, j));
                            data.Add(num);
                        }
                    }
                }
            }
            List<int> counterIDs = new List<int>();
            List<int> counterCounts = new List<int>();
            List<int> tileIDs = new List<int>();
            List<TagCompound> tileData = new List<TagCompound>();
            foreach ((int, int) k in tileList)
            {
                counterIDs.Add(k.Item1);
                counterCounts.Add(k.Item2);
            }
            foreach (KeyValuePair<TileData, int> k in dictionary)
            {
                tileIDs.Add(k.Value);
                tileData.Add(k.Key.ToTagCompound());
            }
            return new TagCompound
            {
                ["tileIDs"] = tileIDs,
                ["tileData"] = tileData,
                ["counterIDs"] = counterIDs,
                ["counterCounts"] = counterCounts,
                ["extraLocations"] = pos,
                ["extraData"] = data,
            };
            //return new BasicTileData(dictionary, tileList);
        }
        public static Tile[,] DecompressTileData(TagCompound tag, int w, int h)
        {
            Main.NewText("Beginning Decompression!!!!!");
            List<int> tileIDs = (List<int>)tag.GetList<int>("tileIDs");
            IList<TagCompound> tileData = tag.GetList<TagCompound>("tileData");
            List<int> counterIDs = (List<int>)tag.GetList<int>("counterIDs");
            List<int> counterCounts = (List<int>)tag.GetList<int>("counterCounts");
            List<Vector2> locations = (List<Vector2>)tag.GetList<Vector2>("extraLocations");
            List<int> extraData = (List<int>)tag.GetList<int>("extraData");
            Tile[,] tile = new Tile[w, h];
            int counter = 0;
            int x = 0;
            int y = 0;
            Dictionary<int, Tile> dictionary = new Dictionary<int, Tile>();
            List<(int, int)> tileList = new List<(int, int)>(); //First ushort is the dictionary id, second int is the quantity
            for (int i = 0; i < tileData.Count; i++)
            {

                //ModContent.GetInstance<StarSailorMod>().Logger.Info("tileID : " + data.tileIDs[i] + " | tileData : " + data.tileData[i]);
                TileData td = new TileData(tileData[i]);
                Tile t = new Tile(Main.tile[0, 0]);


                t.type = td.id;
                t.wall = td.wall;
                t.liquid = td.liq;
                t.liquidType(td.ltype);
                t.active(td.active);
                t.slope(td.slope);

                t.frameX = td.frameX;
                t.frameY = td.frameY;
                dictionary.Add(tileIDs[i], t);

            }
            Main.NewText("i have " + counterIDs.Count + "/" + counterCounts.Count + " tileList has " + tileList.Count);
            for (int i = 0; i < counterIDs.Count; i++)
            {
                tileList.Add((counterIDs[i], counterCounts[i]));
            }
            Main.NewText("world size is " + w + "," + h);
            //Main.NewText("Decompressing layer 0");
            Main.NewText("total items to deal with : " + tileList.Count);
            int k = 0;
            foreach ((int, int) tc in tileList)
            {
                k += tc.Item2;
            }
            Main.NewText("k is " + k);
            Main.NewText("adj k is " + (k - tileList.Count));
            foreach ((int, int) tc in tileList)
            {
                int count = tc.Item2;
                Tile t = dictionary[tc.Item1];
                for (int i = 0; i < count; i++)
                {

                    tile[x, y] = new Tile(t);

                    if (++x >= w)
                    {
                        x = 0;
                        y++;
                        //Main.NewText("Decompressing layer " + y);
                    }
                }
            }
            for (int i = 0; i < extraData.Count; i++)
            {
                Tile t = tile[(int)locations[i].X, (int)locations[i].Y];
                if (t == null)
                {
                    t = new Tile();
                    tile[(int)locations[i].X, (int)locations[i].Y] = t;
                }
                int data = extraData[i];
                bool act = (data & 1) == 1;
                bool w1 = (data & 2) == 2;
                bool w2 = (data & 4) == 4;
                bool w3 = (data & 8) == 8;
                bool w4 = (data & 16) == 16;
                t.actuator(act);
                t.wire(w1);
                t.wire2(w2);
                t.wire3(w3);
                t.wire4(w4);
            }
            #region old way
            /*
            for (int j = 0; j < tile.GetLength(1); j++)
            {
                
                for (int i = 0; i < tile.GetLength(0); i++)
                {
                    

                    if (tileList.Count == 0) return tile;
                    tile[i, j] = new Tile(Main.tile[0, 0]);
                    int id = tileList[0].Item1;
                    if (id == -1) tile[i, j] = null;
                    else
                    {
                        //Main.NewText(id);
                        TileData td = dictionary[id];
                        tile[i, j].type = (ushort)td.id;
                        tile[i, j].wall = (ushort)td.wall;
                        tile[i, j].active(td.active);
                        tile[i, j].liquid = td.liq;
                        tile[i, j].slope(td.slope);
                    }
                    if (++counter >= tileList[0].Item2)
                    {
                        tileList.RemoveAt(0);
                        counter = 0;
                    }
                }
            }
            */
            #endregion
            return tile;
        }

    }
}