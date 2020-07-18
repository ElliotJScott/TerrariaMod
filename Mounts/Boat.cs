using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarSailor.Mounts
{
    class Boat : ModMountData
    {
        public const float ROT_VELOCITY = 0.2f;
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("BoatMount");
            mountData.heightBoost = 20;
            mountData.fallDamage = 0.5f;
            mountData.runSpeed = 1f;
            mountData.dashSpeed = 8f;
            mountData.flightTimeMax = 0;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 5;
            mountData.acceleration = 0.19f;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = false;
            mountData.totalFrames = 1;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 24;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = 13;
            mountData.bodyFrame = 3;
            mountData.yOffset = -34;
            mountData.playerHeadOffset = 22;
            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 12;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 1;
            mountData.runningFrameDelay = 12;
            mountData.runningFrameStart = 0;
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 12;
            mountData.inAirFrameStart = 0;
            mountData.idleFrameCount = 1;
            mountData.idleFrameDelay = 12;
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
            /*

            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 26;
            }
            mountData.playerYOffsets = array;
            mountData.playerHeadOffset = 0;
            mountData.xOffset = 0;
            //.bodyFrame = 3;
            if (Main.netMode != NetmodeID.Server)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
            */
        }
        public override void UpdateEffects(Player player)
        {
            
            base.UpdateEffects(player);
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            //Sort rotation here
            spriteEffects = SpriteEffects.FlipHorizontally;
            //rotation = 1f;
            UpdateRotation(drawPlayer);
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);

        }
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            bool b = true;
            if (mountedPlayer.wet || velocity == Vector2.Zero)
            {
                mountedPlayer.QuickMount();
            }
            mountData.xOffset = -14 * mountedPlayer.direction;
            /*
            mountData.xOffset = 0;
            mountData.yOffset = -45;
            mountedPlayer.mount._frame = 0;
            
            */
            return false;
        }
        public void UpdateRotation(Player p)
        {
            float idealRotation;
            if (p.velocity.X == 0)
            {
                if (p.velocity.Y == 0)
                    idealRotation = 0;
                else idealRotation = -(float)Math.PI/2;
            }
            else
            {
                idealRotation = (float)Math.Atan(p.velocity.Y / p.velocity.X);
            }
            if (Math.Sign(p.velocity.X) == 1) idealRotation += (float)Math.PI;
            //Main.NewText(idealRotation + " " + p.fullRotation);
            float diff = idealRotation - p.fullRotation;
            //Main.NewText(diff);
            diff %= (float)(Math.PI * 2);
            if (Math.Abs(diff) > Math.PI) diff = -diff;
            if (Math.Abs(diff) < Math.PI * ROT_VELOCITY) p.fullRotation = idealRotation;
            else p.fullRotation += (float)(Math.Sign(diff) * Math.PI * ROT_VELOCITY);
            p.fullRotation %= 2 * (float)Math.PI;
        }
    }
}
