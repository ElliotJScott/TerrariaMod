using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teo.GUI;
using teo.Mounts;
using teo.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace teo
{
    class PlayerFixer : ModPlayer
    {
        public bool amRocket;
        public const float ROT_VELOCITY = 1f / 15f;
        public const float GRAV_ACCEL = 1f / 7f;
        public const float MAX_RUNSPEED = 3f;
        public const float RUN_ACCEL = 1f / 5f;
        public bool InSpace;
        public List<Vector2> gravSources = new List<Vector2>();
        public Vector2 dirToGrav = Vector2.Zero;
        public bool custGravity = true;
        public bool canJump = true;
        public int jumpTicker = 0;
        public Vector2 gravVelocity = Vector2.Zero;
        public float legFrameCounter = 0;
        public float bodyFrameCounter = 0;
        public int legFrameY = 0;
        public int bodyFrameY = 0;
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
                    float closestDist = 1000000f;
                    foreach (Vector2 v in gravSources)
                    {
                        if (ModContent.GetInstance<GravitySource>().boundingTiles.TryGetValue(v, out List<BoundingTile> boundTiles))
                        {
                            foreach (BoundingTile f in boundTiles)
                            {

                                float currDist = Vector2.Distance(f.pos, drawInfo.position / 16f);
                                if (currDist < closestDist)
                                {
                                    closestDist = currDist;
                                    displ = v - (drawInfo.position / 16f);
                                }
                            }
                        }
                    }
                    dirToGrav = displ;
                    //drawInfo.drawPlayer.velocity += (displ / 100);
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
                if (dirToGrav != Vector2.Zero)
                {
                    Vector2 direction = dirToGrav / dirToGrav.Length();
                    Vector2 perpDir = new Vector2(-direction.Y, direction.X);
                    float tangVel = Vector2.Dot(gravVelocity, perpDir);
                    float radVel = Vector2.Dot(gravVelocity, direction);
                    if (!ModContent.GetInstance<GravitySource>().CheckIntersectSurface(player))
                    {
                        bodyFrameY = player.bodyFrame.Height * 5;
                        bodyFrameCounter = 0.0f;
                        legFrameY = player.legFrame.Height * 5;
                        legFrameCounter = 0.0f;
                        player.legFrame.Y = legFrameY;
                        player.bodyFrame.Y = bodyFrameY;
                    }
                    else if (tangVel != 0f)
                    {
                        legFrameCounter += Math.Abs(tangVel) * 0.3f;
                        bodyFrameCounter += Math.Abs(tangVel) * 0.3f;
                        while (legFrameCounter > 8.0)
                        {
                            legFrameCounter -= 8.0f;
                            legFrameY += player.legFrame.Height;
                        }
                        if (legFrameY < player.legFrame.Height * 7)
                        {
                            legFrameY = player.legFrame.Height * 19;
                        }
                        else if (legFrameY > player.legFrame.Height * 19)
                        {
                            legFrameY = player.legFrame.Height * 7;
                        }
                        while (bodyFrameCounter > 8.0)
                        {
                            bodyFrameCounter -= 8.0f;
                            bodyFrameY += player.bodyFrame.Height;
                        }
                        if (bodyFrameY < player.bodyFrame.Height * 7)
                        {
                            bodyFrameY = player.bodyFrame.Height * 19;
                        }
                        else if (bodyFrameY > player.bodyFrame.Height * 19)
                        {
                            bodyFrameY = player.bodyFrame.Height * 7;
                        }
                        player.legFrame.Y = legFrameY;
                        player.bodyFrame.Y = bodyFrameY;
                    }

                }
            }
            else
            {
                drawInfo.drawPlayer.fullRotation = 0;
            }
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void UpdateBiomeVisuals()
        {
            bool condition = InSpace;
            player.ManageSpecialBiomeVisuals("TEO:SkySpace", condition);

        }
        public override void PostUpdateRunSpeeds()
        {
            base.PostUpdateRunSpeeds();
            if (custGravity && dirToGrav != Vector2.Zero)
            {
                Vector2 direction = dirToGrav / dirToGrav.Length();
                Vector2 perpDir = new Vector2(-direction.Y, direction.X);
                float tangVel = Vector2.Dot(gravVelocity, perpDir);
                float radVel = Vector2.Dot(gravVelocity, direction);
                float scale;
                float maxRunFactor = 0.5f;
                if (!ModContent.GetInstance<GravitySource>().CheckIntersectSurface(player))
                {
                    gravVelocity += direction * GRAV_ACCEL;
                    scale = 0.3f;
                }
                else
                {

                    scale = 1f;
                }
                
                if (player.controlRight && !player.controlLeft)
                {
                    maxRunFactor = 1f;
                    switch (Math.Sign(Vector2.Dot(perpDir, gravVelocity)))
                    {
                        case -1:
                        case 0:
                            gravVelocity += -perpDir * RUN_ACCEL * scale;
                            break;
                        case 1:
                            gravVelocity += -perpDir * RUN_ACCEL * scale * 2f;
                            break;
                    }
                }
                else if (player.controlLeft && !player.controlRight)
                {
                    maxRunFactor = 1f;
                    //player.velocity += dirToGrav * RUN_ACCEL * 0.05f;
                    switch (Math.Sign(Vector2.Dot(perpDir, gravVelocity)))
                    {
                        case 1:
                        case 0:
                            gravVelocity += perpDir * RUN_ACCEL * scale;
                            break;
                        case -1:
                            gravVelocity += perpDir * RUN_ACCEL * scale * 2f;
                            break;

                    }
                    //player.position += new Vector2((float)(Math.Pow(RUN_ACCEL, 2) / Math.Pow(dirToGrav.Length(), 3)), 2 * RUN_ACCEL / dirToGrav.Length());
                }
                gravVelocity -= perpDir * RUN_ACCEL * 0.1f * Math.Sign(Vector2.Dot(gravVelocity, perpDir));
                if (Math.Abs(tangVel) > MAX_RUNSPEED * maxRunFactor)
                {
                    gravVelocity = (Math.Sign(tangVel) * MAX_RUNSPEED * maxRunFactor * perpDir) + (radVel * direction);
                }
                if (player.controlJump && ModContent.GetInstance<GravitySource>().CheckIntersectSurface(player) && jumpTicker == 0)
                {
                    //canJump = false;
                    jumpTicker += 30;
                    gravVelocity += -direction * 7f;
                }
                
                if (jumpTicker > 0) jumpTicker--;
                player.velocity = gravVelocity;
            }
            
        }
        public void DrawGravGui(SpriteBatch sb)
        {
            Vector2 direction = dirToGrav / dirToGrav.Length();
            Vector2 perpDir = new Vector2(-direction.Y, direction.X);
            float tangVel = Vector2.Dot(player.velocity, perpDir);
            float radVel = Vector2.Dot(player.velocity, direction);
            Player p = Main.player[Main.myPlayer];
            Rectangle playerRect = p.getRect();
            float maxSize = playerRect.Height;
            float minSize = playerRect.Width;
            float diff = maxSize - minSize;
            playerRect.Width = (int)maxSize;
            //playerRect.Width = (int)(minSize + (0.5f * diff) + (-0.5f * diff * Math.Cos(2 * p.fullRotation)));
            //playerRect.Height = (int)(minSize + (0.5f * diff) +  (0.5f * diff * Math.Cos(2 * p.fullRotation)));
            playerRect.X -= (int)(0.5f * (playerRect.Width - minSize));
            playerRect.Y -= (int)(0.5f * (playerRect.Height - maxSize));
            Vector2 centre = new Vector2((playerRect.X - Main.screenPosition.X) + (playerRect.Width / 2), (playerRect.Y - Main.screenPosition.Y) + (playerRect.Height / 2));
            sb.Draw(ModContent.GetInstance<TEO>().pixel, new Rectangle((int)(playerRect.X - Main.screenPosition.X), (int)(playerRect.Y - Main.screenPosition.Y), playerRect.Width, playerRect.Height), Color.Pink * 0.3f);
            GravDisplay.DrawLine(sb, centre, centre + (tangVel * perpDir * 30f), 5, Color.Red);
            GravDisplay.DrawLine(sb, centre, centre + (radVel * direction * 30f), 5, Color.Blue);
            GravDisplay.DrawLine(sb, centre, centre + (16f * dirToGrav), 2, Color.Green);
        }
    }
}
