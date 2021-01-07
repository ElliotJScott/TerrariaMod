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
    class InSpaceBg : ModSurfaceBgStyle
    {
        public const int numStars = 700;
        Dimensions.Dimensions destination, origin;
        float velocity;
        int travCounter;
        int mode;
        Vector2 originLoc, destinationLoc;
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
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();

                return !Main.gameMenu && ModContent.GetInstance<DimensionManager>().currentDimension == Dimensions.Dimensions.Travel;
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
        public void SetDefaults(Dimensions.Dimensions og, Dimensions.Dimensions dest)
        {
            origin = og;
            destination = dest;
            mode = 0;
            travCounter = 0;
            velocity = 1f;
            originLoc = new Vector2(Main.screenWidth / 4, 3 * Main.screenHeight / 5);
            destinationLoc = new Vector2(5 * Main.screenWidth / 4, 3 * Main.screenHeight / 5);
            UpdateForbiddenRegions();
        }
        public bool UpdateBg()
        {
            UpdateForbiddenRegions();
            switch (mode)
            {
                case 0:
                    velocity += 0.3f;
                    if (velocity >= 15f) mode++;
                    originLoc.X -= velocity * 2;
                    break;
                case 1:
                    travCounter++;
                    if (travCounter > 90)
                    {
                        travCounter = 0;
                        mode++;
                    }
                    break;
                case 2:
                    velocity -= 0.3f;
                    if (velocity <= 0) mode++;
                    destinationLoc.X = Math.Max(originLoc.X - (velocity * 2), 3 * Main.screenWidth / 4);
                    break;
                case 3:
                    return true;

            }
            return false;
        }
        public void UpdateForbiddenRegions()
        {
            StarSailorMod sm = (StarSailorMod)mod;
            List<(Vector2, int)> frs = new List<(Vector2, int)>();
            switch (origin)
            {
                case Dimensions.Dimensions.Overworld:
                    frs.Add((originLoc + new Vector2(sm.mainAbove.Width / 2, sm.mainAbove.Height / 2), 243));
                    break;
                case Dimensions.Dimensions.Ice:
                    frs.Add((originLoc + new Vector2(sm.iceAbove.Width / 2, sm.iceAbove.Height / 2), 318));
                    break;
                case Dimensions.Dimensions.Asteroid:
                    frs.Add((originLoc + new Vector2(228,196), 124));
                    frs.Add((originLoc + new Vector2(70, 100), 40));
                    break;
                case Dimensions.Dimensions.Jungle:
                    frs.Add((originLoc + new Vector2(sm.jungAbove.Width / 2, sm.jungAbove.Height / 2), 286));
                    break;
            }
            switch (destination)
            {
                case Dimensions.Dimensions.Overworld:
                    frs.Add((destinationLoc + new Vector2(sm.mainAbove.Width / 2, sm.mainAbove.Height / 2), 243));
                    break;
                case Dimensions.Dimensions.Ice:
                    frs.Add((destinationLoc + new Vector2(sm.iceAbove.Width / 2, sm.iceAbove.Height / 2), 318));
                    break;
                case Dimensions.Dimensions.Asteroid:
                    frs.Add((destinationLoc + new Vector2(228, 196), 124));
                    frs.Add((destinationLoc + new Vector2(70, 100), 40));
                    break;
                case Dimensions.Dimensions.Jungle:
                    frs.Add((destinationLoc + new Vector2(sm.jungAbove.Width / 2, sm.jungAbove.Height / 2), 286));
                    break;
            }
            sm.forbiddenStarRegions = frs.ToArray();
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

            sm.DrawStars(spriteBatch, (int)velocity, ((StarSailorMod)mod).forbiddenStarRegions);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);


            return false;
        }
    }
}
