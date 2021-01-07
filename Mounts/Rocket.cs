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
    class Rocket : ModMountData
    {

        int initOffset = -18;
        float rot = 0;
        public override void SetDefaults()
        {
            mountData.buff = mod.BuffType("RocketMount");
            mountData.heightBoost = 0;
            mountData.fallDamage = 0;
            mountData.runSpeed = 0f;
            mountData.dashSpeed = 0f;
            mountData.fatigueMax = 0;
            mountData.jumpHeight = 0;
            mountData.acceleration = 0f;
            mountData.jumpSpeed = 0f;
            mountData.blockExtraJumps = true;
            mountData.totalFrames = 8;
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
            player.mount._mountSpecificData = new RocketData();
            skipDust = true;
        }
        public override void UpdateEffects(Player player)
        {
            base.UpdateEffects(player);
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            rotation = rot;
            RocketData data = (RocketData)drawPlayer.mount._mountSpecificData;
            /*
            if (crashing)
            {
                glowTexture = burnTexs[Main.rand.Next(3)];
                glowColor = drColor;
                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(drawPlayer.position + new Vector2(mountData.xOffset - (glowTexture.Width / 2), mountData.yOffset - (glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 6, 0f, 0f, 150, default, 1.5f);
                    Dust.NewDust(drawPlayer.position + new Vector2(mountData.xOffset - (glowTexture.Width / 2), mountData.yOffset - (glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 174, 0f, 0f, 150, default, 1.5f);
                }
            }
            */
            drawPosition += data.offset;
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
        }
        
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            return false;
        }
        //public void 
        public bool UpdateTakeOffAnimation(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.offsetCounter++;
            if (data.offsetCounter == 30 && data.mode == 0)
            {
                data.mode++;
                data.offsetCounter = 0;
                p.mount._frame++;
            }
            else if (data.mode == 1)
            {
                data.animCounter++;
                if (data.animCounter == 20)
                {
                    switch (p.mount._frame)
                    {
                        case 1:
                            p.mount._frame = 2;
                            break;
                        case 2:
                            p.mount._frame = 1;
                            break;
                    }
                    data.animCounter = 0;
                }
                if (data.offsetCounter == 80)
                {
                    data.offsetCounter = 0;
                    data.mode++;
                    data.animCounter = 0;
                    p.mount._frame = 2;
                }
            }
            else if (data.mode == 2)
            {
                data.animCounter++;
                data.offset.Y--;
                if (data.animCounter == 20)
                {
                    p.mount._frame++;
                    data.animCounter = 0;
                }
                if (data.offsetCounter == 60)
                {
                    data.offsetCounter = 0;
                    data.mode++;
                    data.animCounter = 0;
                }
            }
            else if (data.mode == 3)
            {
                data.animCounter++;
                if (data.offsetCounter < 160)
                    data.offset.Y--;
                if (data.animCounter == 20)
                {
                    switch (p.mount._frame)
                    {
                        case 4:
                            p.mount._frame = 5;
                            break;
                        case 5:
                            p.mount._frame = 4;
                            break;
                    }
                    data.animCounter = 0;
                }
                if (data.offsetCounter == 180)
                {
                    data.offsetCounter = 0;
                    data.mode++;
                    data.animCounter = 0;
                    p.mount._frame = 6;
                }
            }
            else if (data.mode == 4)
            {
                data.animCounter++;
                if (data.offsetCounter > 60 && data.offsetCounter <= 150)
                {
                    data.offset.Y -= 1;
                    data.offset.X += 10 + (data.offsetCounter / 10);
                }
                if (data.animCounter == 20)
                {
                    switch (p.mount._frame)
                    {
                        case 6:
                            p.mount._frame = 7;
                            break;
                        case 7:
                            p.mount._frame = 6;
                            break;
                    }
                    data.animCounter = 0;
                }
                if (data.offsetCounter == 150)
                {
                    return true;
                }
            }
            return false;
        }
        public void ExecuteTakeOffAnim(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.mode = 0;
            data.offsetCounter = 0;
            data.animCounter = 0;
            mountData.xOffset = 0;
            mountData.yOffset = initOffset;
            p.mount._frame = 0;
        }
        public void ExecuteSpaceAnim(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            //data.offsetBuffer = new Vector2(-data.offset.X, data.offset.Y + 90);
            data.offset = new Vector2(0, initOffset + 100);
            data.mode = 0;
            p.mount._frame = 6;
            data.animCounter = 0;
            data.offsetCounter = 0;

        }
        public void ExecuteLandingAnim(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.offset = data.offsetBuffer;
            data.mode = 0;
            p.mount._frame = 4;
            data.animCounter = 0;
            data.offsetCounter = 0;
        }
        public void DisposeTakeOff(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.offsetBuffer = new Vector2(-data.offset.X, data.offset.Y + 90);
        }
        public void ExecuteLand(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.mode = 0;
            data.offsetCounter = 0;
            data.animCounter = 0;
            p.mount._frame = 0;
        }
        public bool UpdateSpaceAnim(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            p.velocity = Vector2.Zero;
            data.animCounter++;
            if (data.animCounter == 20)
            {
                
                switch (p.mount._frame)
                {
                    case 6:
                        p.mount._frame = 7;
                        break;
                    case 7:
                        p.mount._frame = 6;
                        break;
                }
                data.animCounter = 0;
            }
            return false;
        }
        public bool UpdateLandingAnim(Player p)
        {
            RocketData data = (RocketData)p.mount._mountSpecificData;
            data.offsetCounter++;
            if (data.mode == 0)
            {
                data.animCounter++;
                if (data.offsetCounter < 186)
                {
                    if (data.offset.Y < initOffset && data.offsetCounter % 2 == 0) data.offset.Y++;
                    data.offset.X += 5 + ((150 - (data.offsetCounter / 2)) / 20);
                    if (data.animCounter == 20)
                    {
                        switch (p.mount._frame)
                        {
                            case 4:
                                p.mount._frame = 5;
                                break;
                            case 5:
                                p.mount._frame = 4;
                                break;
                        }
                        data.animCounter = 0;
                    }
                }
                else if (p.mount._frame > 2 && data.animCounter == 20)
                {
                    if (p.mount._frame == 5) p.mount._frame = 3;
                    else p.mount._frame--;
                }

                if (data.offsetCounter == 240)
                {
                    data.mode++;
                    data.animCounter = 0;
                    data.offsetCounter = 0;
                    p.mount._frame = 1;
                }
            }
            else if (data.mode == 1)
            {
                data.animCounter++;
                if (data.offset.Y < initOffset) data.offset.Y++;
                if (data.animCounter == 20)
                {
                    switch (p.mount._frame)
                    {
                        case 1:
                            p.mount._frame = 2;
                            break;
                        case 2:
                            p.mount._frame = 1;
                            break;
                    }
                    data.animCounter = 0;
                }
                if (data.offset.Y == initOffset)
                {
                    data.offsetCounter = 0;
                    data.mode++;
                    data.animCounter = 0;
                }
            }
            else if (data.mode == 2)
            {
                data.animCounter++;
                if (data.animCounter == 20)
                {
                    switch (p.mount._frame)
                    {
                        case 1:
                            p.mount._frame = 2;
                            break;
                        case 2:
                            p.mount._frame = 1;
                            break;
                    }
                    data.animCounter = 0;
                }
                if (data.offsetCounter == 80)
                {
                    data.offsetCounter = 0;
                    data.mode++;
                    data.animCounter = 0;
                    p.mount._frame = 0;
                }
            }
            else if (data.offsetCounter == 30 && data.mode == 3)
            {
                return true;
            }
            return false;
        }
            
    }
    class RocketData
    {
        public int mode;
        public int animCounter;
        public int offsetCounter;
        public Vector2 offset;
        public Vector2 offsetBuffer;
        public RocketData()
        {
            mode = 0;
            animCounter = 0;
            offsetCounter = 0;
            offset = Vector2.Zero;
        }
    }
}
