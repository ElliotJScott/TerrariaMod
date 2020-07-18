using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using Terraria;
using Terraria.ModLoader;

namespace starsailor.Backgrounds
{
    class Level0TreeCave : ModSurfaceBgStyle
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
            return true;
        }
        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertCaveBack");
        }
        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertTreeCaveMid");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertTreeCaveFront");
        }
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            BgHooks.DrawBGs(spriteBatch, ((StarSailorMod)mod).desTreeCaveMid, ((StarSailorMod)mod).desTreeCaveFront);
            return base.PreDrawCloseBackground(spriteBatch);
        }
    }
}
