using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    class iceTownBg : ModSurfaceBgStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.targetStarNum = 0;
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
#warning this will need to be used for the ice planet cave
            return false;
            /*
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome == Biomes.IceCaveTown;
            }
            catch { return false; }
            */
        }
        public override int ChooseFarTexture()
        {
            return -1;
        }
        public override int ChooseMiddleTexture()
        {
            return -1;
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return -1;
        }
        public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            sm.stars.Clear();
            Texture2D[] texs = { sm.iceCaveFront, sm.iceCaveMid1, sm.iceCaveMid3, sm.iceCaveMid2, sm.iceCaveBack };
            float[] darkens = { 1, 1, 0.8f, 0.5f, 0.3f };
            int[] offs = { -300, -250, -150, -250, 0 };
            float[] scales = { 1.5f, 1.5f, 1.3f, 1.5f, 1.3f };
            BgHooks.DrawBGs(spriteBatch, texs, offs, darkens, scales);
            return false;
        }
    }
}
