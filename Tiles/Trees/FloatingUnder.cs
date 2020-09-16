using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Tiles.Trees
{
    class FloatingUnder : ModTree
    {
        private Mod mod => ModLoader.GetMod("StarSailor");

        public override int CreateDust()
        {
            return DustID.Vortex;
        }

        public override int GrowthFXGore()
        {
            return GoreID.TreeLeaf_Mushroom;
        }

        public override int DropWood()
        {
            return ItemID.Wood;
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Trees/FloatingUnder");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return mod.GetTexture("Tiles/Trees/FloatingUnder_Tops");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Trees/FloatingUnder_Branches");
        }
    }
}
