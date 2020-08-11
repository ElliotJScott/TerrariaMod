using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    class StartingShip : ModMountData
    {
        int initOffset = -18;
        public const int crashTime = 40;
        Texture2D[] burnTexs = new Texture2D[3];
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("StartingShipMount");
            for (int i = 0; i < burnTexs.Length; i++) burnTexs[i] = mod.GetTexture("Mounts/BurnOverlay" + i);
            mountData.heightBoost = 0;
            mountData.fallDamage = 0;
            mountData.runSpeed = 0f;
            mountData.dashSpeed = 0f;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 0;
            mountData.acceleration = 0f;
            mountData.jumpSpeed = 0f;
            mountData.blockExtraJumps = true;
            mountData.totalFrames = 3;
            //mountData.constantJump = true;
            int[] array = new int[mountData.totalFrames];
            for (int l = 0; l < array.Length; l++)
            {
                array[l] = 20;
            }
            mountData.playerYOffsets = array;
            mountData.xOffset = 0;
            //.bodyFrame = 3;
            mountData.yOffset = initOffset;
            if (Main.netMode != NetmodeID.Server)
            {
                mountData.textureWidth = mountData.backTexture.Width;
                mountData.textureHeight = mountData.backTexture.Height;
            }
        }
        public override void SetMount(Player player, ref bool skipDust)
        {
            player.mount._mountSpecificData = new StartingShipData();
            skipDust = true;
        }
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            Main.blockInput = true;
            Main.blockMouse = true;
            return false;
        }
        public void Update(Player player)
        {
            
            StartingShipData data = (StartingShipData)player.mount._mountSpecificData;
            switch (data.mode)
            {
                case 0:
                    data.rot = Math.Max(MathHelper.ToRadians(-90), data.rot - 0.005f);
                    break;
                case 1:
                    data.offset += new Vector2(0, 2 + (data.offset.Y / 100));
                    break;
                case 2:
                    data.offset += new Vector2(-6, 1 + (Main.screenHeight/(2f * crashTime)));
                    break;
                default:
                    throw new InvalidOperationException("I done fucked up");
            }

        }
        public void IncrementState(Player player)
        {
            StartingShipData data = (StartingShipData)player.mount._mountSpecificData;
            data.mode++;
            data.mode %= 3;
            if (data.mode == 2)
            {
                
                data.offset = new Vector2(6 * crashTime, -crashTime - (Main.screenHeight / 2));
                
            }
        }
        public int GetState(Player player)
        {
            StartingShipData data = (StartingShipData)player.mount._mountSpecificData;
            return data.mode;
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            spriteEffects = SpriteEffects.FlipHorizontally;
            StartingShipData data = (StartingShipData)drawPlayer.mount._mountSpecificData;
            rotation = data.rot;
            
            if (data.mode == 2 && data.offset != new Vector2(6 * crashTime, -crashTime - (Main.screenHeight / 2)))
            {
                glowTexture = burnTexs[Main.rand.Next(3)];

                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(drawPlayer.position + data.offset + new Vector2(-(glowTexture.Width / 2), -(glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 6, 0f, 0f, 150, default, 1.5f);
                    Dust.NewDust(drawPlayer.position + data.offset + new Vector2(-(glowTexture.Width / 2), -(glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 174, 0f, 0f, 150, default, 1.5f);
                }
            }
            
            drawPosition += data.offset;
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
        }
    }
    class StartingShipData
    {
        public Vector2 offset;
        public int mode;
        public float rot;
        public StartingShipData()
        {
            offset = Vector2.Zero;
            rot = 0f;
            mode = 0;
        }
    }
}
