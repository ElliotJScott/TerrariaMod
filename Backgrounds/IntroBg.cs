using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    class IntroBg : ModSurfaceBgStyle
    {
        public const int numStars = 700;
        public int yOffset = 0;
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.targetStarNum = numStars;
            sm.currentDistribution = Distribution.Flat;
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
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome == Biomes.Intro;
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
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return -1;
        }
        public void UpdateOffset()
        {
            StarSailorMod sm = (StarSailorMod)mod;
            if (yOffset > -sm.planet0Above.Height)
            {
                yOffset -= 2;
            }
        }
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            //return true;
            DrawData d = new DrawData(sm.pixel, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black);

            d.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            //spriteBatch.Draw(sm.sun0, new Rectangle(Main.screenWidth * 11 / 16, 165, 160, 160), Color.White);
            Texture2D planTex = sm.planet0Above;
            spriteBatch.Draw(planTex, new Rectangle((Main.screenWidth - planTex.Width) / 2, Main.screenHeight + yOffset, planTex.Width, planTex.Height), Color.White);
            sm.DrawStars(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);


            return false;
        }
    }
}
