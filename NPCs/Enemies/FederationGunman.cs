using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.NPCs.Enemies
{
    class FederationGunman : ModNPC
    {
        const int timeToShoot = 80;
        int shoot = 0;
        int shootingFixer = timeToShoot;
        int frameTimer = 0;
        int numFrame = 7;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Federation Gunman");
            Main.npcFrameCount[npc.type] = 21;
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.SkeletonSniper);
            npc.aiStyle = 3;
        }

        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
            if (npc.velocity == Vector2.Zero)
            {
                if (npc.target == 255) npc.frame.Y = 2 * frameHeight;
                else
                {
                    Vector2 disp = npc.Center - Main.player[npc.target].Center;

                    float angle = MathHelper.ToDegrees((float)Math.Atan(disp.Y / disp.X));
                    if (angle < 0) angle += 180f;
                    if (angle < 30f || angle > 150f)
                    {
                        npc.frame.Y = 2 * frameHeight;
                    }
                    else if ((angle >= 30f && angle < 60f) || (angle >= 120f && angle < 150f))
                    {
                        if (disp.Y < 0) npc.frame.Y = 1 * frameHeight;
                        else npc.frame.Y = 3 * frameHeight;
                    }
                    else
                    {
                        if (disp.Y < 0) npc.frame.Y = 0;
                        else npc.frame.Y = 4 * frameHeight;
                    }
                    //Main.NewText(angle + " " + disp);
                }
            }
            else if (npc.velocity.Y != 0)
            {
                npc.frame.Y = 6 * frameHeight;
            }
            else
            {
                npc.frame.Y = numFrame * frameHeight;
                if (++frameTimer >= 6)
                {
                    frameTimer = 0;
                    if (++numFrame >= Main.npcFrameCount[npc.type]) numFrame = 7;
                }
            }
        }
        public override void AI()
        {
            base.AI();
            npc.TargetClosest();
            npc.spriteDirection = -Math.Sign(npc.Center.X - Main.player[npc.target].Center.X);
            shoot = Math.Max(shoot - 1, 0);
            if (shootingFixer == 0 && shoot == 0)
            {
                if (npc.velocity.Y == 0)
                {
                    shootingFixer += timeToShoot;
                }

            }
            else
            {
                if (shoot == 0)
                {
                    shootingFixer = Math.Max(shootingFixer - 1, 0);
                    npc.velocity = Vector2.Zero;
                    if (shootingFixer == timeToShoot / 4)
                    {
                        Main.NewText("Bang!");
                    }
                    else if (shootingFixer == 0)
                    {
                        shoot += Main.rand.Next(180, 480);
                    }
                }
            }
            //npc.velocity = Vector2.Zero;
            //Main.NewText(npc.target);
        }


    }
}
