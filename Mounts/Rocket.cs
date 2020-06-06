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

namespace teo.Mounts
{
    class Rocket : ModMountData
    {
        int offsetCounter = 0;
        int animCounter = 0;
        int mode = 0;
        int initOffset = -18;
        int xOffsetBuffer = 0;
        int yOffsetBuffer = 0;
        float rot = 0;
        public bool bgland = false;
        public bool takeOffAnimate = false;
        public bool landingAnimate = false;
        public bool spaceAnim = false;
        public bool crashing = false;
        int framedelay = 0;
        float drScale = 1f;
        public Vector2 destination;
        public Texture2D[] burnTexs = new Texture2D[3];
        public Color drColor = new Color(55, 55, 55, 55);
        public override void SetDefaults()
        {
            //mountData.spawnDust = mod.DustType("Smoke");
            for (int i = 0; i < 3; i++)
            {
                burnTexs[i] = mod.GetTexture("Mounts/BurnOverlay" + i);
            }
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
        public override void UpdateEffects(Player player)
        {
            base.UpdateEffects(player);
        }
        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            rotation = rot;
            drawScale = drScale;
            if (crashing)
            {
                glowTexture = burnTexs[Main.rand.Next(3)];
                glowColor = drColor;
                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(drawPlayer.position + new Vector2(mountData.xOffset - (glowTexture.Width / 2), mountData.yOffset - (glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 6, 0f, 0f, 150, default(Color), 1.5f);
                    Dust.NewDust(drawPlayer.position + new Vector2(mountData.xOffset - (glowTexture.Width / 2), mountData.yOffset - (glowTexture.Height / 2)), glowTexture.Width, glowTexture.Height, 174, 0f, 0f, 150, default(Color), 1.5f);
                }
            }
            return base.Draw(playerDrawData, drawType, drawPlayer, ref texture, ref glowTexture, ref drawPosition, ref frame, ref drawColor, ref glowColor, ref rotation, ref spriteEffects, ref drawOrigin, ref drawScale, shadow);
        }
        public override bool UpdateFrame(Player mountedPlayer, int state, Vector2 velocity)
        {
            PlayerFixer modPlayer = mountedPlayer.GetModPlayer<PlayerFixer>();
            if (bgland)
            {
                bgland = false;
                BeginLandingAnim(mountedPlayer);
            }
            if (modPlayer.amRocket)
            {
                mode = 0;
                offsetCounter = 0;
                animCounter = 0;
                mountData.xOffset = 0;
                mountData.yOffset = initOffset;
                mountedPlayer.mount._frame = 0;
            }
            else if (takeOffAnimate) DoTakeOffAnimation(mountedPlayer);
            else if (spaceAnim) DoSpaceAnim(mountedPlayer);
            else if (landingAnimate) DoLandingAnimation(mountedPlayer);
            return false;
        }
/// <summary>
/// Do takeoff animation for rocket ship
/// </summary>
/// <param name="p">The player in the ship</param>
/// <param name="newLocation">The new place to send the player to</param>
        public void DoTakeOffAnimation(Player p)
        {
            offsetCounter++;
            if (offsetCounter == 30 && mode == 0)
            {
                mode++;
                offsetCounter = 0;
                p.mount._frame++;
            }
            else if (mode == 1)
            {
                animCounter++;
                if (animCounter == 20)
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
                    animCounter = 0;
                }
                if (offsetCounter == 80)
                {
                    offsetCounter = 0;
                    mode++;
                    animCounter = 0;
                    p.mount._frame = 2;
                }
            }
            else if (mode == 2)
            {
                animCounter++;
                mountData.yOffset--;
                if (animCounter == 20)
                {
                    p.mount._frame++;
                    animCounter = 0;
                }
                if (offsetCounter == 60)
                {
                    offsetCounter = 0;
                    mode++;
                    animCounter = 0;
                }
            }
            else if (mode == 3)
            {
                animCounter++;
                if (offsetCounter < 160)
                    mountData.yOffset--;
                if (animCounter == 20)
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
                    animCounter = 0;
                }
                if (offsetCounter == 180)
                {
                    offsetCounter = 0;
                    mode++;
                    animCounter = 0;
                    p.mount._frame = 6;
                }
            }
            else if (mode == 4)
            {
                animCounter++;
                if (offsetCounter > 60)
                {
                    mountData.yOffset -= 1;
                    mountData.xOffset += 10 + (offsetCounter / 10);
                }
                if (animCounter == 20)
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
                    animCounter = 0;
                }
                if (offsetCounter == 150)
                {
                    xOffsetBuffer = -mountData.xOffset;
                    mountData.xOffset = 0;
                    yOffsetBuffer = mountData.yOffset + 90;
                    mountData.yOffset = initOffset + 100;
                    mode = 0;
                    p.mount._frame = 6;
                    animCounter = 0;
                    offsetCounter = 0;
                    p.Teleport(new Vector2(150 * 16, 150 * 16), 5);
                    //Main.NewText("player pos " + p.position);
                    landingAnimate = false;
                    takeOffAnimate = false;
                    spaceAnim = true;

                    p.GetModPlayer<PlayerFixer>().InSpace = true;
                }
            }
           
        }
        public void BeginLandingAnim(Player p)
        {
            PlayerFixer modp = p.GetModPlayer<PlayerFixer>();
            mountData.xOffset = xOffsetBuffer;
            mountData.yOffset = yOffsetBuffer;
            p.Teleport(16 * destination, 5);
            spaceAnim = false;
            modp.InSpace = false;
            mode = 0;
            p.mount._frame = 4;
            animCounter = 0;
            offsetCounter = 0;
            landingAnimate = true;
            takeOffAnimate = false;
        }
        public void DoSpaceAnim(Player p)
        {
            //p.Teleport(new Vector2(150 * 16, 150 * 16), 5);
            p.velocity = Vector2.Zero;
            //Main.NewText(p.position);
            animCounter++;
            if (animCounter == 20)
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
                animCounter = 0;
            }
        }
        public void DoLandingAnimation(Player p)
        {
            offsetCounter++;
            if (mode == 0)
            {
                animCounter++;
                if (offsetCounter < 186)
                {
                    if (mountData.yOffset < initOffset && offsetCounter % 2 == 0) mountData.yOffset++;
                    mountData.xOffset += 5 + ((150 - (offsetCounter / 2)) / 20);
                    if (animCounter == 20)
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
                        animCounter = 0;
                    }
                }
                else if (p.mount._frame > 2 && animCounter == 20)
                {
                    if (p.mount._frame == 5) p.mount._frame = 3;
                    else p.mount._frame--;
                }

                if (offsetCounter == 240)
                {
                    mode++;
                    animCounter = 0;
                    offsetCounter = 0;
                    p.mount._frame = 1;
                }
            }
            else if (mode == 1)
            {
                animCounter++;
                if (mountData.yOffset < initOffset) mountData.yOffset++;
                if (animCounter == 20)
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
                    animCounter = 0;
                }
                if (mountData.yOffset == initOffset)
                {
                    offsetCounter = 0;
                    mode++;
                    animCounter = 0;
                }
            }
            else if (mode == 2)
            {
                animCounter++;
                if (animCounter == 20)
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
                    animCounter = 0;
                }
                if (offsetCounter == 80)
                {
                    offsetCounter = 0;
                    mode++;
                    animCounter = 0;
                    p.mount._frame = 0;
                }
            }
            else if (offsetCounter == 30 && mode == 3)
            {
                PlayerFixer modp = p.GetModPlayer<PlayerFixer>();
                Main.blockInput = false;
                p.QuickMount();
                mode = 0;
                offsetCounter = 0;
                animCounter = 0;
                p.mount._frame = 0;
                takeOffAnimate = false;
                landingAnimate = false;
                spaceAnim = false;
                modp.InSpace = false;
            }
        }
    }
}
