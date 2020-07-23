using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using starsailor.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace starsailor.Mounts
{
    class Mech : ModMountData
    {
        public override void SetDefaults()
        {

            mountData.buff = mod.BuffType("MechMount");
            mountData.heightBoost = 20;
            mountData.fallDamage = 0.5f;
            mountData.runSpeed = 3f;
            mountData.heightBoost = 0;
            mountData.dashSpeed = 0f;
            mountData.flightTimeMax = 0;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 15;
            mountData.acceleration = 0.19f;
            mountData.jumpSpeed = 4f;
            mountData.blockExtraJumps = true;
            mountData.totalFrames = 10;
            mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 250;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = -13;
            mountData.bodyFrame = 0;
            mountData.yOffset = -176;
            Main.blockInput = false;
            mountData.playerHeadOffset = 22;
            mountData.standingFrameCount = 1;
            mountData.standingFrameDelay = 4;
            mountData.standingFrameStart = 0;
            mountData.runningFrameCount = 8;
            mountData.runningFrameDelay = 24;
            mountData.runningFrameStart = 2;
            mountData.flyingFrameCount = 0;
            mountData.flyingFrameDelay = 0;
            mountData.flyingFrameStart = 0;
            mountData.inAirFrameCount = 1;
            mountData.inAirFrameDelay = 4;
            mountData.inAirFrameStart = 1;
            mountData.idleFrameCount = 1;
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
        public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
        {
            Main.NewText(Main.MouseWorld);
            Main.NewText(Main.MouseWorld - (player.position + new Vector2(0, -150)));
            base.UseAbility(player, mousePosition, toggleOn);
        }
        public override void SetMount(Player player, ref bool skipDust)
        {
            skipDust = true;
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            //playerDrawData.Add(new DrawData());
            //spriteEffects = SpriteEffects.None;
            
            if (playerDrawData.Count > 0)
            {
                for (int i = 0; i < playerDrawData.Count; i++)
                {
                    Main.spriteBatch.Draw(playerDrawData[i].texture, new Rectangle(150 * i, 100, playerDrawData[i].texture.Width, playerDrawData[i].texture.Height), Color.White);
                    Main.spriteBatch.DrawString(Main.fontDeathText, "" + playerDrawData[i].texture.Height, new Vector2(150 * i, 50), Color.Green);

                }
            }
            //Main.LocalPlayer.controlTorch = false;
            //drawPlayer.direction = -1;
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
        }
    }
}
