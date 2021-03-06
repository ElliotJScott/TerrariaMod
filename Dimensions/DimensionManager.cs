﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace StarSailor.Dimensions
{
    class DimensionManager : ModWorld
    {
        public Dimension[] dimensions = new Dimension[5];
        public Dimensions currentDimension;
        public int dimsLoaded = 0;
        public override void Load(TagCompound tag)
        {
            mod.Logger.Info("Beginning dimension load");
            dimensions = new Dimension[5];
            currentDimension = (Dimensions)tag.GetInt("currDim");
            foreach (Dimensions q in typeof(Dimensions).GetEnumValues())
            {

                TagCompound dimTag = tag.GetCompound((int)q + "DimData");
                TagCompound extraDat = tag.GetCompound((int)q + "ExData");
                if (q != currentDimension)
                    LoadDimension(q, dimTag, extraDat);
                else dimensions[(int)q] = new Dimension(q, extraDat);
            }
            base.Load(tag);
        }
        public float GetGravity()
        {
            return dimensions[(int)currentDimension].GetGravity();
        }
        public Color GetSunlight() => dimensions[(int)currentDimension].GetSunlight();
        public void LoadDimension(Dimensions d, TagCompound tag, TagCompound extraDat)
        {
            dimsLoaded = 0;
            mod.Logger.Info("Attempting to load dimension " + d + "with data of size " + tag.Count);
            dimsLoaded++;
            dimensions[(int)d] = new Dimension(d, tag, extraDat);
                //dimensions[(int)d].data = tag;
            //}
        }
        public TagCompound SaveDimension(Dimensions d)
        {

            Dimension dim = dimensions[(int)d];
            //if (d == Dimensions.Asteroid) mod.Logger.Info(dim.data);
            return dim.data;
        }
        public TagCompound SaveExtraData(Dimensions d)
        {

            Dimension dim = dimensions[(int)d];
            //if (d == Dimensions.Asteroid) mod.Logger.Info(dim.data);
            return dim.GetExtraData();
        }
        public override TagCompound Save()
        {
            TagCompound output = new TagCompound();
            output.Add("currDim", (int)currentDimension);
            foreach (Dimensions q in typeof(Dimensions).GetEnumValues())
            {
                if (q != currentDimension)
                {
                    output.Add((int)q + "DimData", SaveDimension(q));
                }
                output.Add((int)q + "ExData", SaveExtraData(q));

            }
            //mod.Logger.Info("Data to save is : " + output);
            return output;
        }
        public void DiscoverDimension(Dimensions d)
        {
            Dimension dim = dimensions[(int)d];
            dim.Discover();
        }
        public override void Initialize()
        {
            dimensions = new Dimension[5];
            for (int i = 0; i < 5; i++)
            {
                dimensions[i] = new Dimension((Dimensions)i);
            }
            currentDimension = Dimensions.Overworld;
            base.Initialize();
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            tasks.Add(new PassLegacy("Generating Dimensions", GenerateDimensions));
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }
        public void GenerateDimensions(GenerationProgress progress)
        {
            progress.Message = "Generating alternate dimensions";
            dimensions = new Dimension[6];
            dimensions[(int)Dimensions.Overworld] = new Dimension(Dimensions.Overworld, progress);
            dimensions[(int)Dimensions.Travel] = new Dimension(Dimensions.Travel, progress);
            dimensions[(int)Dimensions.Asteroid] = new Dimension(Dimensions.Asteroid, progress);
            dimensions[(int)Dimensions.Ice] = new Dimension(Dimensions.Ice, progress);
            dimensions[(int)Dimensions.Jungle] = new Dimension(Dimensions.Jungle, progress);

        }

        public void SwitchDimension(Dimensions dim)
        {
            mod.Logger.Info("Switching dimension from " + currentDimension + " to " + dim);
            Dimension dimo = dimensions[(int)currentDimension];
            //dimo.basicTileData = Dimension.GetCompressedBasicTileData(Main.tile);
            //dimo.extraTileData = Dimension.GetExtraTileData(Main.tile);
            dimo.data = Dimension.GetCompressedTileData(Main.tile);
            dimo.chest = Main.chest;
            dimo.sign = Main.sign;
            
            Dimension nextDim = dimensions[(int)dim];
            Tile[,] tileData = Dimension.DecompressTileData(nextDim.data, Main.tile.GetLength(0), Main.tile.GetLength(1));
         
            //Tile[,] allTileData = Dimension.LoadExtraTileData(nextDim.extraTileData, basicTileData);
            Main.tile = tileData;
            Main.chest = nextDim.chest;
            Main.sign = nextDim.sign;
            currentDimension = dim;
            nextDim = new Dimension(currentDimension, nextDim.GetExtraData());
            mod.Logger.Info("Done switching dimension from " + currentDimension + " to " + dim);
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
        }
        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
        }
    }
}
