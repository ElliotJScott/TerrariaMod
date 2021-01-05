using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    class LavaBg : ModSurfaceBgStyle
    {
        public const int numStars = 250;
        int ticker = 0;
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.targetStarNum = numStars;
            sm.currentDistribution = Distribution.Atan;
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
        public override bool ChooseBgStyle()
        {
            return false;
            /*
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();
                
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome == Biomes.LavaOverworld;
            }
            catch { return false; }
            */
        }
        public override int ChooseFarTexture()
        {
            return base.ChooseFarTexture();
        }
        public override int ChooseMiddleTexture()
        {
            return -1;
            //return mod.GetBackgroundSlot("Backgrounds/DesertAboveMid");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return -1;
            //return mod.GetBackgroundSlot("Backgrounds/DesertAboveFront");
        }
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            //return true;
            DrawData d = new DrawData(sm.lavaSky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
            
            //GameShaders.Misc["StarShader"].Apply(d);
            d.Draw(spriteBatch);
            if (++ticker > 12) ticker = 0;
            Texture2D[] texs = { sm.lavaMid, sm.lavaBack, sm.lavaMid2 };
            float[] darkens = { 0.8f, 0.6f, 0.6f};
            int[] offs = { 100, 0, 270};
            float[] scales = { 1f,0.8f, 1f };
            int[] frameCounts = { 2,1, 2 };
            int[] frames = { ticker>=6?1:0,0, ticker >= 6 ? 1 : 0};
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            sm.DrawStars(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            BgHooks.DrawAnimatedBGs(spriteBatch, texs, offs, darkens, scales, frameCounts, frames);


            return false;
        }
    }
}
