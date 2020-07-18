using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace starsailor.Backgrounds
{
    class Level0TopBg : ModSurfaceBgStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
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
        }
        public override int ChooseFarTexture()
        {
            return base.ChooseFarTexture();
        }
        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertAboveMid");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertAboveFront");
        }
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            return base.PreDrawCloseBackground(spriteBatch);
        }
    }
}
