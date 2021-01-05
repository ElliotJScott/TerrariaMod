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
    class Level0TreeCave : ModSurfaceBgStyle
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
            return false;
            /*
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome == Biomes.DesertTreeCave;
            }
            catch { return false; }
            */
        }
        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/DesertCaveBack");
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
            Texture2D[] texs = { sm.desTreeCaveMid3, sm.desTreeCaveMid2, sm.desTreeCaveFront, sm.desTreeCaveMid, sm.desTreeCaveBack };
            float[] darkens = { 1, 1, 0.8f, 0.5f, 0.3f };
            int[] offs = { -300, -250, -150, -250, 0 };
            float[] scales = { 1.5f, 1.5f, 1.3f, 1.5f, 1.3f };
            BgHooks.DrawBGs(spriteBatch, texs, offs, darkens, scales);
            return false;
        }
    }
}
