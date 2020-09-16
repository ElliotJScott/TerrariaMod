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
    class JungleValleyBg : ModSurfaceBgStyle
    {
        public const int numStars = 0;
        
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
            try
            {
                //PlayerFixer pl = ModContent.GetInstance<PlayerFixer>();
                
                return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome == Biomes.JungleRiver;
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
            //DrawData d = new DrawData(sm.overworldSky, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * 0.8f);
            
            //GameShaders.Misc["StarShader"].Apply(d);
            //d.Draw(spriteBatch);

            Texture2D[] texs = { sm.jungleValleyNear, sm.jungleValleyMid, sm.jungleValleyFar };
            float[] darkens = { 1f, 1f, 1f};
            int[] offs = { 200, 0, -100};
            float[] scales = { 1f , 1f, 1f};
            //spriteBatch.Draw(texs[0], new Rectangle(0, 0, 1024, 1024), Color.White);
            BgHooks.DrawBGs(spriteBatch, texs, offs, darkens, scales);


            return false;
        }
    }
}
