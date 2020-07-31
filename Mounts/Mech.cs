using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using StarSailor.Buffs;
using StarSailor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Mounts
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
        
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            if (mountedPlayer.mount._frame <= 1) return true;
            else
            {
                if (velocity.Y != 0 || velocity == Vector2.Zero) return true;
                mountedPlayer.mount._frameCounter += 2.4f;
                if (mountedPlayer.mount._frameCounter >= mountData.runningFrameDelay)
                {
                    mountedPlayer.mount._frameCounter = 0;
                    mountedPlayer.mount._frame += Math.Sign(velocity.X);
                }
                if (mountedPlayer.mount._frame < 2) mountedPlayer.mount._frame = 9;
                if (mountedPlayer.mount._frame > 9) mountedPlayer.mount._frame = 2;
                //mountedPlayer.mount._frame = 2;
            }
            return false;
        }
        
        public override void UpdateEffects(Player player)
        {
            MechData dat = (MechData)player.mount._mountSpecificData;
            //Main.NewText(Main.MouseWorld);
            player.ChangeDir(1);
            //Main.NewText();
            Vector2 dir = Main.MouseWorld - (player.position + new Vector2(0, -165));
            //Main.NewText(dir);
            float f = (float)Math.Atan(dir.Y / dir.X);
            dat.gunRotation = MathHelper.Clamp(f, MathHelper.ToRadians(-30), MathHelper.ToRadians(30));
            if (Main.mouseLeft && dat.charge > 0.1f)
            {
                Vector2 line = new Vector2(70f * (float)Math.Cos(dat.gunRotation), 70f * (float)Math.Sin(dat.gunRotation));
                float spray = (Main.rand.NextFloat() - 0.5f) * 0.4f;
                Vector2 bullDir = new Vector2(6f * (float)Math.Cos(dat.gunRotation + spray), 6f * (float)Math.Sin(dat.gunRotation + spray));
                dat.UpdateFrame(player.position + new Vector2(0, -175) + line, bullDir);

                //Main.NewText(dat.shootCounter);
            }
            else
            {
                dat.ResetFrame();
            }
            if (Main.mouseRight)
            {
                Main.NewText("Electroboom");
            }
            base.UpdateEffects(player);
        }
        public override void SetMount(Player player, ref bool skipDust)
        {
            player.mount._mountSpecificData = new MechData();
            skipDust = true;
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            //drawPlayer.direction = 1;
            
            if (drawType == 0)
            {
                MechData dat = (MechData)drawPlayer.mount._mountSpecificData;
                Vector2 dir = -Main.screenPosition + drawPlayer.position + new Vector2(0, -156);
                //Main.NewText(dir + " " + drawPlayer.position);
                Vector2 initPos = dir + new Vector2(60, 22);
                Texture2D gunTex = mod.GetTexture("Mounts/MechGatlingGun");
                Texture2D mountTex = mod.GetTexture("Mounts/Mech_Back");
                //Main.NewText(new Rectangle(0, dat.getFrame() * (tex.Height / 4), tex.Width, tex.Height / 4));
                playerDrawData.Add(new DrawData(mountTex, dir - new Vector2(170, 200), new Rectangle(0, drawPlayer.mount._frame * (mountTex.Height / mountData.totalFrames), mountTex.Width, mountTex.Height / mountData.totalFrames), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0));
                playerDrawData.Add(new DrawData(gunTex, initPos - new Vector2(60, 48), new Rectangle(0, dat.GetFrame() * (gunTex.Height / 4), gunTex.Width, gunTex.Height / 4), Color.White, dat.gunRotation, new Vector2(10 + (gunTex.Height / 8), gunTex.Height / 8), 1f, SpriteEffects.None, 0));
                Main.spriteBatch.DrawString(Main.fontDeathText, "Charge = " + Math.Truncate(dat.charge) + "%", new Vector2((Main.screenWidth - Main.fontDeathText.MeasureString("Charge = " + Math.Truncate(dat.charge) + "%").X) / 2, 50), Color.Blue);
            }
            return false;
        }
        internal class MechData
        {
            public float charge;
            public int shootCounter;
            public float gunRotation;
            int frame;
            public int lastLegFrame;
            int ticksForFrame;
            int frameCounter = 0;
            public MechData()
            {
                shootCounter = 0;
                charge = 100f;
                gunRotation = 0;
                ticksForFrame = 15;
                lastLegFrame = 2;
            }
            public void ResetFrame()
            {
                charge = Math.Min(charge + 0.05f, 100);
                ticksForFrame = 15;
                shootCounter = 0;
            }
            
            public void UpdateFrame(Vector2 pos, Vector2 dir)
            {
                charge -= 0.1f;
                shootCounter++;
                if (ticksForFrame <= ++shootCounter)
                {
                    Projectile.NewProjectile(pos, dir, ProjectileID.Bullet, 40, 0.2f, Main.myPlayer);
                    shootCounter = 0;
                    frame++;
                    frame %= 4;
                    ticksForFrame = Math.Max(ticksForFrame - 1, 3);
                }
            }
            public int GetFrame()
            {
                return frame;
            }
        }
    }
}
