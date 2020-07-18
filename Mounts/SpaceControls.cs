using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarSailor.Mounts
{
    class SpaceControls : ModMountData
    {
        public override void SetDefaults()
        {
            mountData.spawnDust = DustID.Vortex;
            mountData.buff = mod.BuffType("SpaceControlsMount");
            mountData.heightBoost = 0;
            mountData.fallDamage = 0;
            //mountData.runSpeed = 0f;
            //mountData.dashSpeed = 0f;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 0;
            //mountData.acceleration = 0f;
            mountData.jumpSpeed = 0f;
            
            mountData.blockExtraJumps = true;
            mountData.totalFrames = 1;
            int[] ar = {0};
            mountData.playerYOffsets = ar;
            mountData.xOffset = 0;
            mountData.yOffset = 0;
            
            if (Main.netMode != NetmodeID.Server)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
            
        }
    }
}
