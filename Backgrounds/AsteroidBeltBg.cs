using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Dimensions;
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
    class AsteroidBeltBg : ModSurfaceBgStyle
    {
        public const int numStars = 700;
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
            sm.forbiddenStarRegions[0] = (new Vector2(Main.screenWidth * 3 / 16, Main.screenHeight * 2 / 19) + new Vector2(200, 200), (200 * 400) / sm.sun1.Width);
            sm.forbiddenStarRegions[1] = (new Vector2(Main.screenWidth * 10 / 16, Main.screenHeight * 7 / 19) + new Vector2(sm.asteroidBeltPlanet.Width/2), 250);
            sm.forbiddenStarRegions[2] = (new Vector2(Main.screenWidth * 9 / 16, Main.screenHeight * 6 / 19) + new Vector2(sm.asteroidBeltMoon.Width/2), sm.asteroidBeltMoon.Width / 2);
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

                return !Main.gameMenu && ModContent.GetInstance<DimensionManager>().currentDimension == Dimensions.Dimensions.Asteroid;
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
            spriteBatch.Draw(sm.sun1, new Rectangle(Main.screenWidth * 3 / 16, Main.screenHeight * 2 / 19, 400, 400), Color.White);
            spriteBatch.Draw(sm.asteroidBeltPlanet, new Rectangle(Main.screenWidth * 10 / 16, Main.screenHeight * 7/19, sm.asteroidBeltPlanet.Width, sm.asteroidBeltPlanet.Height), Color.White);
            spriteBatch.Draw(sm.asteroidBeltMoon, new Rectangle(Main.screenWidth * 9 / 16, Main.screenHeight * 6 / 19, sm.asteroidBeltMoon.Width, sm.asteroidBeltMoon.Height), Color.White);
            //Texture2D planTex = sm.planet0Above;
            //spriteBatch.Draw(planTex, new Rectangle((Main.screenWidth - planTex.Width) / 2, Main.screenHeight + yOffset, planTex.Width, planTex.Height), Color.White);


            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);


            return false;
        }
    }
}
