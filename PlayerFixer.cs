using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teo.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace teo
{
    class PlayerFixer : ModPlayer
    {
        public bool amRocket;
        public bool InSpace;
        public override void Initialize()
        {
            InSpace = false;
            amRocket = false;
            base.Initialize();
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            if (player.mount.Type == ModContent.GetInstance<Rocket>().Type)
            {
                Color invis = new Color(0, 0, 0, 0);
                drawInfo.bodyColor = invis;
                drawInfo.hairColor = invis;
                drawInfo.headGlowMaskColor = invis;
                drawInfo.armGlowMaskColor = invis;
                drawInfo.bodyGlowMaskColor = invis;
                drawInfo.eyeColor = invis;
                drawInfo.eyeWhiteColor = invis;
                drawInfo.faceColor = invis;
                drawInfo.legColor = invis;
                drawInfo.legGlowMaskColor = invis;
                drawInfo.lowerArmorColor = invis;
                drawInfo.middleArmorColor = invis;
                drawInfo.pantsColor = invis;
                drawInfo.shirtColor = invis;
                drawInfo.shoeColor = invis;
                drawInfo.underShirtColor = invis;
                drawInfo.upperArmorColor = invis;
            }
            else
            {
                amRocket = false;
            }
            base.ModifyDrawInfo(ref drawInfo);
        }

        public override void UpdateBiomeVisuals()
        {
            bool condition = InSpace;
            player.ManageSpecialBiomeVisuals("TEO:SkySpace", condition);
                
        }
    }
}
