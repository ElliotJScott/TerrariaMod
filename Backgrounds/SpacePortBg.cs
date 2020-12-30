using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    class SpacePortBg : ModSurfaceBgStyle
    {
        public const int numStars = 702;
        public int yOffset = 0;
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.targetStarNum = numStars;
            sm.currentDistribution = Distribution.Flat;
            //spriteBatch.Draw(sm.sun1, new Rectangle(Main.screenWidth * 3 / 16, Main.screenHeight * 2 / 19, 400, 400), Color.White);
            //spriteBatch.Draw(sm.asteroidBeltPlanet, new Rectangle(Main.screenWidth * 10 / 16, Main.screenHeight * 7 / 19, sm.asteroidBeltPlanet.Width, sm.asteroidBeltPlanet.Height), Color.White);
            //spriteBatch.Draw(sm.asteroidBeltMoon, new Rectangle(Main.screenWidth * 9 / 16, Main.screenHeight * 5 / 19, sm.asteroidBeltMoon.Width, sm.asteroidBeltMoon.Height), Color.White);

            sm.forbiddenStarRegions = new (Vector2, int)[3];
            sm.forbiddenStarRegions[0] = (new Vector2(Main.screenWidth * 3 / 16, Main.screenHeight * 2 / 19) + new Vector2(400, 400), (400 * 400) / sm.sun1.Width);
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
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().planet == Planet.SpacePort;
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
            sm.DrawStars(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            var starShader = GameShaders.Misc["StarSailor:StarNoiseEffect"];

            starShader.Apply(null);
            spriteBatch.Draw(sm.sun1, new Rectangle(Main.screenWidth * 3 / 16, Main.screenHeight * 2 / 19, 800, 800), Color.White);
            //Texture2D planTex = sm.planet0Above;
            //spriteBatch.Draw(planTex, new Rectangle((Main.screenWidth - planTex.Width) / 2, Main.screenHeight + yOffset, planTex.Width, planTex.Height), Color.White);


            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);


            return false;
        }
    }
}
