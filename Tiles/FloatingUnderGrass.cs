using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Tiles
{
    /*
    class FloatingUnderGrass : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<Items.Placeable.FloatingUnderGrass>();
            SetModTree(new Trees.FloatingUnder());
            base.SetDefaults();
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            (Biomes, Planet) loc = sm.GetBiomePlanet(new Vector2(i, j));
            Color col = sm.GetGrassColor(loc.Item1);
            
            int k = col.R + col.G + col.B;
            float brightness = k / (255f * 3f);
            float factor = 0.05f / brightness;
            r = col.R * factor;
            g = col.G * factor;
            b = col.B * factor;
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            int tileSize = 18;
            int dirtID = TileID.Mud;
            int grassID = Type;
            int airID = 0;
            int top = Framing.GetTileSafely(i, j - 1).type;
            int tl = Framing.GetTileSafely(i - 1, j - 1).type;
            int tr = Framing.GetTileSafely(i + 1, j - 1).type;
            int left = Framing.GetTileSafely(i - 1, j).type;
            int right = Framing.GetTileSafely(i + 1, j).type;
            int bl = Framing.GetTileSafely(i - 1, j + 1).type;
            int bot = Framing.GetTileSafely(i, j + 1).type;
            int br = Framing.GetTileSafely(i + 1, j + 1).type;
            int choice = Main.rand.Next(3);


            if (top == grassID && left == grassID && right == dirtID && bot == dirtID)
            {
                Main.tile[i, j].frameX = (short)(3 * tileSize);
                Main.tile[i,j].frameY = (short)((6+ (2*choice)) * tileSize);
                return false;
            }
            if (top == grassID && right == grassID && left == dirtID && bot == dirtID)
            {
                Main.tile[i, j].frameX = (short)(2 * tileSize);
                Main.tile[i, j].frameY = (short)((6 + (2 * choice)) * tileSize);
                return false;
            }

            /* Ok so at some point i should finish this , this would be a definitive full correct tile frame selector but cba to do it now
(int, int) loc = (0, 0);

if (!Merge(grassID, dirtID, left) && right == dirtID && top == grassID && bot == grassID) loc = (0, choice);
if (!Merge(grassID, dirtID, right) && left == dirtID && top == grassID && bot == grassID) loc = (4, choice);
if (!Merge(grassID, dirtID, top) && bot == dirtID && left == grassID && right == grassID) loc = (1 + choice, 0);
if (!Merge(grassID, dirtID, bot) && top == dirtID && left == grassID && right == grassID) loc = (1 + choice, 2);
*r/
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }

        bool Merge(int did, int gid, int t) => t == did || t == gid;

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            //(Biomes, Planet) loc = sm.GetBiomePlanet(new Vector2(i, j));
            Color col = sm.GetGrassColor(loc.Item1);
           
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var grassShader = GameShaders.Misc["StarSailor:FloatingUnderTreeEffect"];

            grassShader.UseColor(col);
            grassShader.Apply(null);
      
            return base.PreDraw(i, j, Main.spriteBatch);
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            base.PostDraw(i, j, spriteBatch);
        }
    }
    */
}
