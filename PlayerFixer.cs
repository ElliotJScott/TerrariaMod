using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teo.Mounts;
using teo.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace teo
{
    class PlayerFixer : ModPlayer
    {
        public bool amRocket;
        public const float ROT_VELOCITY = 1f/15f;
        public const float GRAV_ACCEL = 1f/5f;
        public const float MAX_RUNSPEED = 3f;
        public const float RUN_ACCEL = 1f / 5f;
        public bool InSpace;
        public List<Vector2> gravSources = new List<Vector2>();
        public Vector2 dirToGrav = Vector2.Zero;
        public bool custGravity = true;
        public bool canJump = true;
        public override void Initialize()
        { 
            //player.rotati
            InSpace = false;
            amRocket = false;
            base.Initialize();
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.drawPlayer.fullRotationOrigin = 0.5f * new Vector2(drawInfo.drawPlayer.width, drawInfo.drawPlayer.height); ;
            if (player.mount.Type == ModContent.GetInstance<Rocket>().Type)
            {
                Color invis = new Color(0, 0, 0, 0);
                drawInfo.bodyColor = invis;
                drawInfo.hairColor = invis;
                drawInfo.headGlowMaskColor = invis;
                drawInfo.armGlowMaskColor = invis;
                drawInfo.bodyGlowMaskColor = invis;
                drawInfo.eyeColor = invis;
                drawInfo.eyeWhiteColor = invis;
                drawInfo.faceColor = invis;
                drawInfo.legColor = invis;
                drawInfo.legGlowMaskColor = invis;
                drawInfo.lowerArmorColor = invis;
                drawInfo.middleArmorColor = invis;
                drawInfo.pantsColor = invis;
                drawInfo.shirtColor = invis;
                drawInfo.shoeColor = invis;
                drawInfo.underShirtColor = invis;
                drawInfo.upperArmorColor = invis;
            }
            else
            {
                amRocket = false;
            }
            if (custGravity)
            {
                drawInfo.drawPlayer.mount.SetMount(ModContent.GetInstance<SpaceControls>().Type, drawInfo.drawPlayer);
                //Main.blockInput = true;
                drawInfo.drawPlayer.maxFallSpeed = 0;
                drawInfo.drawPlayer.vortexDebuff = true;
                if (gravSources.Count > 0)
                {
                    Vector2 displ = new Vector2(20020020);
                    foreach (Vector2 v in gravSources)
                    {
                        float currDist = Vector2.Distance(v, drawInfo.position / 16f);
                        if (currDist < displ.Length()) displ = v - (drawInfo.position / 16f);
                    }
                    dirToGrav = displ;
                    //drawInfo.drawPlayer.velocity += (displ / 100);
                    //Main.NewText(displ.X + " " + displ.Y);
                    float idealRotation = (float)Math.Atan(displ.Y / displ.X) + (float)Math.PI / 2f;
                    if (Math.Sign(displ.X) == 1) idealRotation += (float)Math.PI;
                    float diff = idealRotation - drawInfo.drawPlayer.fullRotation;
                    diff %= (float)(Math.PI * 2);
                    if (Math.Abs(diff) > Math.PI) diff = -diff;
                    if (Math.Abs(diff) < Math.PI * ROT_VELOCITY) drawInfo.drawPlayer.fullRotation = idealRotation;
                    else drawInfo.drawPlayer.fullRotation += (float)(Math.Sign(diff) * Math.PI * ROT_VELOCITY);
                    drawInfo.drawPlayer.fullRotation %= 2 * (float)Math.PI;
                }
                else
                {
                    Main.blockInput = false;
                    drawInfo.drawPlayer.QuickMount();
                    drawInfo.drawPlayer.fullRotation = 0;
                }
                
                if (drawInfo.drawPlayer.controlRight && !drawInfo.drawPlayer.controlLeft)
                    drawInfo.spriteEffects = SpriteEffects.None;
                else if (player.controlLeft && !player.controlRight)
                    drawInfo.spriteEffects = SpriteEffects.FlipHorizontally;
                base.ModifyDrawInfo(ref drawInfo);
            }
            else
            {
                drawInfo.drawPlayer.fullRotation = 0;
            }
        }
        public override void UpdateBiomeVisuals()
        {
            bool condition = InSpace;
            player.ManageSpecialBiomeVisuals("TEO:SkySpace", condition);

        }
        
        public override void PostUpdateRunSpeeds()
        {
            if (custGravity && dirToGrav != Vector2.Zero)
            {
                Vector2 direction = dirToGrav / dirToGrav.Length();
                Vector2 perpDir = new Vector2(-direction.Y, direction.X);
                player.velocity += direction * GRAV_ACCEL;
                if (player.controlRight && !player.controlLeft)
                {
                    player.velocity += -perpDir * RUN_ACCEL;
                }
                if (player.controlLeft && !player.controlRight) player.velocity += perpDir * RUN_ACCEL;
                float tangVel = Vector2.Dot(player.velocity, perpDir);
                float radVel = Vector2.Dot(player.velocity, direction);
                if (Math.Abs(tangVel) > MAX_RUNSPEED)
                {
                    player.velocity = (Math.Sign(tangVel) * MAX_RUNSPEED * perpDir) + (radVel * direction);
                }
                if (player.controlJump && radVel == 0)
                {
                    Main.NewText("Weh");
                    //canJump = false;
                    player.velocity += -direction * 70f;
                }
            }
            base.PostUpdateRunSpeeds();
        }
    }
}
