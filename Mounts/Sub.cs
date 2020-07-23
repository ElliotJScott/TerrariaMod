using Microsoft.Xna.Framework;
using starsailor.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace starsailor.Mounts
{
    class Sub : ModMountData
    {
        public override void SetDefaults()
        {

            mountData.buff = mod.BuffType("SubMount");
            mountData.heightBoost = 20;
            mountData.fallDamage = 0.5f;
            mountData.runSpeed = 11f;
            mountData.dashSpeed = 8f;
            mountData.flightTimeMax = 0;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 5;
            mountData.acceleration = 0.19f;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = false;
            mountData.totalFrames = 4;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 20;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = -13;
            mountData.bodyFrame = 3;
            mountData.yOffset = -30;
            mountData.playerHeadOffset = 22;
            mountData.standingFrameCount = 4;
            mountData.standingFrameDelay = 4;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 4;
            mountData.runningFrameDelay = 4;
            mountData.runningFrameStart = 0;
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 4;
            mountData.inAirFrameDelay = 4;
            mountData.inAirFrameStart = 0;
            mountData.idleFrameCount = 4;
            mountData.idleFrameDelay = 4;
            mountData.idleFrameStart = 0;
            mountData.idleFrameLoop = true;
            mountData.swimFrameCount = mountData.inAirFrameCount;
            mountData.swimFrameDelay = mountData.inAirFrameDelay;
            mountData.swimFrameStart = mountData.inAirFrameStart;
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            mountData.textureWidth = mountData.backTexture.Width + 20;
            mountData.textureHeight = mountData.backTexture.Height;
        }
        /*
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            ++mountedPlayer.mount._frame;
            mountedPlayer.mount._frame%=4;
            
            return false;
        }
        */
        public override void SetMount(Player player, ref bool skipDust)
        {
            skipDust = true;
        }
    }
}
