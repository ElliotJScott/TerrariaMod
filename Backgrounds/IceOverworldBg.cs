using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using StarSailor.Dimensions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    class IceOverworldBg : ModSurfaceBgStyle
    {
        public const int numStars = 150;
        
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.targetStarNum = numStars;
            sm.currentDistribution = Distribution.Atan;
            sm.forbiddenStarRegions = new (Vector2, int)[1];
            sm.forbiddenStarRegions[0] = (new Vector2(Main.screenWidth * 7 / 16, 85) + new Vector2(80, 80), (200 * 160) / sm.sun1.Width);
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
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();

                return !Main.gameMenu && ModContent.GetInstance<DimensionManager>().currentDimension == Dimensions.Dimensions.Ice;
            }
            catch { return false; }
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
            DrawData d = new DrawData(sm.iceSky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * 0.8f);
            
            d.Draw(spriteBatch);

            Texture2D[] texs = { sm.iceOverworldNear, sm.iceOverworldMid, sm.iceOverworldFar};
            float[] darkens = { 1f, 1f, 1f };
            int[] offs = { 300, 100, 500};
            float[] scales = { 0.850f, 0.85f, 1f};
            //spriteBatch.Draw(texs[0], new Rectangle(0, 0, 1024, 1024), Color.White);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(sm.sun1, new Rectangle(Main.screenWidth * 7 / 16, 85, 160, 160), Color.White);
            //spriteBatch.Draw(sm.sun0, new Rectangle(Main.screenWidth * 5 / 16, 165, 160, 160), Color.White);
            sm.DrawStars(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);


            BgHooks.DrawBGs(spriteBatch, texs, offs, darkens, scales);


            return false;
        }
    }
}
