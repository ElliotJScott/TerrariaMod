using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.NPCs
{
    public abstract class Character : ModNPC, ITalkable, IInteractable
    {
        public static NPC FindNPC(int npcType) => Main.npc.FirstOrDefault(npc => npc.type == npcType && npc.active);
        public abstract string InternalName { get; }
        public bool doMotion = true;
        Texture2D speechTexture;
        public float WalkSpeed { get
            {
                return 1;
            }
        }
        public int MaxDistance { get
            {
                return 96;
            }
        }

        public abstract IInteraction Interaction { get; }

        int walkTimer = 0;
        int walkMode = 0;
        public int frameNum = 0;

        public void RightClick()
        {
            Interact();
        }

        public void Interact()
        {
            Main.playerInventory = false;
            Interaction.Execute();
        }
        
        public void SpawnCharacter(Vector2 position, int type)
        {

            int newChar = NPC.NewNPC((int)position.X, (int)position.Y, type, 1);
            NPC ne = Main.npc[newChar];
            ne.homeless = false;
            ne.direction = -1;
            ne.netUpdate = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(InternalName);
            Main.npcFrameCount[npc.type] = 15;

        }


        public override void SetDefaults()
        {
            npc.townNPC = false; // This will be changed once the NPC is spawned
            npc.friendly = true;
            npc.width = 18;
            npc.height = 56;
            npc.aiStyle = 7;
            npc.damage = 0;
            npc.defense = 9999;
            npc.lifeMax = 9999;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            Texture2D texture = ModContent.GetTexture(Texture);
            speechTexture = TextureHooks.GetShrunkTexture(texture, 15);
            //speechTexture = texture;
        }
        public bool CanRightClick()
        {
            StarSailorMod sm = (StarSailorMod)mod;
            Rectangle mouseRect = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1);
            Rectangle npcRect = npc.getRect();
            Rectangle clicktangle = new Rectangle(npcRect.X - 6, npcRect.Y - 6, npcRect.Width + 12, npcRect.Height + 12);
            return mouseRect.Intersects(clicktangle) && WithinDistance() && !Main.ingameOptionsWindow && !sm.sequence.GetActive();
        }
        public bool WithinDistance()
        {
            StarSailorMod sm = (StarSailorMod)mod;
            Vector2 disp = npc.Center - Main.LocalPlayer.Center;
            return Math.Abs(disp.X) < 320 && Math.Abs(disp.Y) < 80;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (Interaction == null) return base.PreDraw(spriteBatch, drawColor); 
            if (Interaction.Enabled) Interaction.Update();
            if (CanRightClick())
            {
                Vector2 screenPos = GetScreenPosition();
                spriteBatch.Draw(Main.chat2Texture, new Rectangle((int)(screenPos.X + 18), (int)(screenPos.Y - 14), Main.chat2Texture.Width, Main.chat2Texture.Height), Color.White);
            }
            return base.PreDraw(spriteBatch, drawColor);
        }

        public override void FindFrame(int frameHeight)
        {
            if (!doMotion)
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
                frameNum = 0;
                return;
            }
            if (CanRightClick()) 
            {
                
                if (Main.mouseRight && Main.mouseRightRelease)
                {
                    RightClick();               
                }
            }
            npc.life = npc.lifeMax;
            npc.frame.Y = 56 * frameNum;
            int speed = 10;
            npc.spriteDirection = -Math.Sign(npc.velocity.X);
            if (npc.velocity.X != 0)
            {
                
                npc.frameCounter++;
                if (npc.frameCounter >= speed)
                {
                    npc.frameCounter %= speed;
                    frameNum++;
                    if (frameNum >= Main.npcFrameCount[npc.type]) frameNum = 1 + (frameNum % Main.npcFrameCount[npc.type]);
                }
            }
            else
            {
                npc.frameCounter = 0;
                frameNum = 0;
            }
            //npc.frameCounter = 0;
            //base.FindFrame(frameHeight);

        }
        /*
        public static bool NeedToSpawn(string name, int type)
        {
            NPC nipple = FindNPC(type);
            return nipple == null && IsNpcOnscreen(CharacterLocationMapping.npcLocations[name]);
        }
        */
        //ty examplemod for this code that i stole
        private static bool IsNpcOnscreen(Vector2 center)
        {
            return true;
            int w = NPC.sWidth + NPC.safeRangeX * 2;
            int h = NPC.sHeight + NPC.safeRangeY * 2;
            Main.NewText(w + " " + h);
            Rectangle npcScreenRect = new Rectangle((int)center.X - w / 2, (int)center.Y - h / 2, w, h);
            foreach (Player player in Main.player)
            {
                if (player.active && player.getRect().Intersects(npcScreenRect)) return true;
            }
            return false;
        }
        public override void AI()
        {
            if (!doMotion)
            {
                npc.velocity = Vector2.Zero;
                return;
            }
            switch (walkMode)
            {
                case 0:
                    npc.velocity = Vector2.Zero;
                    if (walkTimer <= 0) walkMode++;
                    else walkTimer--;
                    break;
                case 1:
                    npc.velocity = new Vector2(-WalkSpeed, 0);
                    if (Math.Abs(GetDistFromHome()) > MaxDistance)
                    {
                        walkMode++;
                    }
                    break;
                case 2:
                    npc.velocity = new Vector2(WalkSpeed, 0);
                    if (GetDistFromHome() > MaxDistance)
                    {
                        npc.velocity = -npc.velocity;
                        walkMode++;
                    }
                    break;
                case 3:
                    npc.velocity = new Vector2(-WalkSpeed, 0);
                    if (Math.Abs(GetDistFromHome()) < WalkSpeed)
                    {

                        float tryAgain = Main.rand.NextFloat();
                        if (tryAgain < 0.3) walkMode = 1;
                        else
                        {
                            walkMode = 0;
                            npc.velocity = Vector2.Zero;
                            walkTimer = 120 + (int)(Main.rand.NextFloat() * 180f);
                        }
                    }
                    break;

            }
            if (walkTimer == 0 && walkMode == 0)
            {
                walkMode++;
            }
            else if (walkMode == 1)
            {
            }
            //base.AI();
        }

        public float GetDistFromHome()
        {
            return 0;
            //return GetPosition().X - CharacterLocationMapping.npcLocations[InternalName].X;
        }
        public Vector2 GetPosition()
        {
            return npc.position;
        }

        public Vector2 GetScreenPosition()
        {
            return GetPosition() - Main.screenPosition + npc.visualOffset;
        }

        public void DrawHeadSpeech(SpriteBatch sb, Rectangle rect)
        {
            //sb = Main.spriteBatch;
            int w = speechTexture.Width;
            //int factor = rect.Width / w;
            int factor = 2;
            int newWidth = factor * w;
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            sb.Draw(speechTexture, new Rectangle(rect.X + (rect.Width / 2) - (newWidth / 2), rect.Y + rect.Height - newWidth, newWidth, newWidth), new Rectangle(0, 0, speechTexture.Width, speechTexture.Width), Color.White);
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public string GetName() => InternalName;
    }
}
