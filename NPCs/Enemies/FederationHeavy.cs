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
    class FederationHeavy : ModNPC
    {
        int frameTimer = 0;
        int numFrame = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Federation Heavy");
            Main.npcFrameCount[npc.type] = 16;
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.damage = 30;
            npc.defense = 6;
            npc.lifeMax = 400;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = 60f;
            npc.knockBackResist = 0.5f;
            npc.aiStyle = 3;
            aiType = NPCID.Zombie;

        }

        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
            if (npc.velocity == Vector2.Zero)
            {
                npc.frame.Y = 0;
            }
            else if (npc.velocity.Y != 0)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            else
            {
                npc.frame.Y = numFrame * frameHeight;
                if (++frameTimer >= 6)
                {
                    frameTimer = 0;
                    if (++numFrame >= Main.npcFrameCount[npc.type]) numFrame = 2;
                }
            }
        }
        public override void AI()
        {
            base.AI();
            npc.TargetClosest();
            npc.spriteDirection = Math.Sign(npc.Center.X - Main.player[npc.target].Center.X);

            //npc.velocity = Vector2.Zero;
            
        }
    }
}
