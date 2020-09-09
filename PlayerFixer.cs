using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.GUI;
using StarSailor.Mounts;
using StarSailor.Tiles;
using Terraria;
using Terraria.ModLoader;
using StarSailor;
using StarSailor.NPCs;
using StarSailor.Sequencing;
using Terraria.DataStructures;
using StarSailor.Items.Weapons;
using Terraria.ID;

namespace StarSailor
{
    class PlayerFixer : ModPlayer, ITalkable
    {

        public const float ROT_VELOCITY = 1f / 15f;
        public const float GRAV_ACCEL = 1f / 7f;
        public const float MAX_RUNSPEED = 3f;
        public const float RUN_ACCEL = 1f / 5f;

        public List<Vector2> gravSources = new List<Vector2>();
        public Vector2 dirToGrav = Vector2.Zero;
        public bool custGravity = false;
        public bool canJump = true;
        public int jumpTicker = 0;
        public Vector2 gravVelocity = Vector2.Zero;
        public PlayerHolder playerHolder;

        public int ampedCounter;

        public Planet planet;
        public Biomes biome;
        public List<BiomeLocationMapping> mappings = new List<BiomeLocationMapping>();
        public Dictionary<Planet, float> gravityValues = new Dictionary<Planet, float>();
        public Dictionary<Biomes, int> starCounts = new Dictionary<Biomes, int>();

        public float legFrameCounter = 0;
        public float bodyFrameCounter = 0;
        public int legFrameY = 0;
        public int bodyFrameY = 0;
        public List<DrawData> drawDataBuffer = new List<DrawData>();
        public Biomes[] overworldBiomes = { Biomes.DesertOverworld, Biomes.DesertTown };
        public string GetName() => player.name;
        public override void Initialize()
        {

            playerHolder = new PlayerHolder();
            //player.rotati
            biome = Biomes.DesertOverworld;
            planet = Planet.Desert;
            SetUpBiomeMappings();
            SetUpGravityValues();
            SetUpStarCounts();
            //Texture2D tempTex = TextureHooks.StackTextures(20, Main.playerHairTexture[player.hair], )
            base.Initialize();
        }
        public void DrawHeadSpeech(SpriteBatch sb, Rectangle rect)
        {
            Rectangle src = new Rectangle(0, 0, 40, 40);
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            //Main.NewText(Main.playerDrawData.Count);
            //sb.Draw(Main.playerTextures);
            //sb.Draw(Main.playerTextures[0, 0], rect, src, player.skinColor);
            //sb.Draw(Main.playerTextures[0, 1], rect, src, Color.White);
            //sb.Draw(Main.playerTextures[0, 2], rect, src, player.eyeColor);
            //sb.Draw(Main.playerHairTexture[player.hair], rect, src, player.hairColor);
            for (int i = 0; i < drawDataBuffer.Count; i++)
            {
                //sb.Draw(Main.playerDrawData[i].texture, new Vector2(100 * i, 50*i), Main.playerDrawData[i].color);
                sb.Draw(drawDataBuffer[i].texture, rect, src, drawDataBuffer[i].color);
            }
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            drawDataBuffer.Clear(); //Might have to copy this back across
        }
        public override void ResetEffects()
        {
            ampedCounter = 0;
            base.ResetEffects();
        }
        public void SetUpBiomeMappings()
        {
            mappings.Clear();
            mappings.Add(new BiomeLocationMapping(new Vector2(201, 1572), new Vector2(1741, 1812), Biomes.DesertOverworld, Planet.Desert, 1));
            mappings.Add(new BiomeLocationMapping(new Vector2(313, 1843), new Vector2(1294, 2047), Biomes.DesertTown, Planet.Desert, 1));
            mappings.Add(new BiomeLocationMapping(new Vector2(1899, 1640), new Vector2(2157, 1784), Biomes.DesertTreeCave, Planet.Desert, 2));
            mappings.Add(new BiomeLocationMapping(new Vector2(1317, 1881), new Vector2(1668, 2038), Biomes.DesertMoleCave, Planet.Desert, 2));
            mappings.Add(new BiomeLocationMapping(new Vector2(192, 1554), new Vector2(2187, 2069), Biomes.DesertCaves, Planet.Desert, 0));
            mappings.Add(new BiomeLocationMapping(new Rectangle(4656, 200, 200, 100), Biomes.Intro, Planet.Intro, 0));
        }
        public bool CanPurchase(ShopItem i)
        {
            int cost = i.cost;
            Resource res = i.resource;
            return CanPurchase(cost, res);
        }
        public bool CanPurchase(int cost, Resource res)
        {
            int num = 0;
            switch (res)
            {
                case Resource.Money:
                    num = GetMoney();
                    break;
            }
            return num >= cost;
        }
        public int GetMoney()
        {
            int num = 0;
            foreach (Item it in player.inventory)
            {
                if (it.type == ItemID.PlatinumCoin) num += it.stack * 1000000;
                else if (it.type == ItemID.GoldCoin) num += it.stack * 10000;
                else if (it.type == ItemID.SilverCoin) num += it.stack * 100;
                else if (it.type == ItemID.CopperCoin) num += it.stack;
            }
            return num;
        }
        public void ChargeCoins(int c)
        {
            bool b = player.BuyItem(c);
            if (!b) Main.NewText("Uh oh stinky");
        }
        public void SetUpGravityValues()
        {
            gravityValues.Clear();
            gravityValues.Add(Planet.Desert, 0.25f);
        }
        public void SetUpStarCounts()
        {
            starCounts.Clear();
            starCounts.Add(Biomes.DesertOverworld, 250);
        }
        public (Biomes, Planet) GetCurrentBiomePlanet()
        {
            Vector2 playerPos = player.position / 16;
            List<BiomeLocationMapping> valids = new List<BiomeLocationMapping>();
            foreach (BiomeLocationMapping b in mappings)
                if (b.CheckPlayerSatisfies(playerPos))
                    valids.Add(b);
            valids.Sort();

            if (valids.Count > 0)
            {
                return (valids[0].biome, valids[0].planet);
            }
            else return (Biomes.InFlight, Planet.InFlight);
        }

        public override void PreUpdateMovement()
        {
            #region boat stuff
            if (player.mount.Type == ModContent.GetInstance<Boat>().Type)
            {
                if (ModContent.GetInstance<RapidWater>().accelMyPlayer)
                {
                    player.velocity.X -= 2f;
                    player.velocity.X = Math.Max(player.velocity.X, -10f);
                }
                ModContent.GetInstance<RapidWater>().accelMyPlayer = false;
            }
            #endregion
            if (((StarSailorMod)mod).currentShop != null) player.AddBuff(BuffID.Cursed, 10);
            (Biomes, Planet) loc = GetCurrentBiomePlanet();
            biome = loc.Item1;
            planet = loc.Item2;
            playerHolder.Update();
            base.PreUpdateMovement();
        }
        public override void ModifyDrawInfo(ref PlayerDrawInfo drawInfo)
        {
            drawInfo.drawPlayer.fullRotationOrigin = 0.5f * new Vector2(drawInfo.drawPlayer.width, drawInfo.drawPlayer.height);
            StarSailorMod sm = (StarSailorMod)mod;
            Color invis = new Color(0, 0, 0, 0);
            if (player.mount.Type == ModContent.GetInstance<Rocket>().Type)
            {
                
                
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
            if (drawInfo.drawPlayer.mount.Type == ModContent.GetInstance<Mech>().Type)
            {
                drawInfo.spriteEffects = SpriteEffects.None;
            }

            base.ModifyDrawInfo(ref drawInfo);
        }
        
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int[] valids = { ModContent.ItemType<PowerGloveV1>(), ModContent.ItemType<PowerGloveV2>(), ModContent.ItemType<PowerGloveV3>(), ModContent.ItemType<PowerGloveVMax>() };
            if (valids.Contains(player.HeldItem.type))
            {
                PlayerLayer layer = null;
                foreach (PlayerLayer l in layers)
                {
                    if (l.Name == "HeldItem")

                    {
                        layer = l;
                        break;
                    }
                }
                layers.Remove(layer);
                layers.Add(layer);
            }
            if (player.mount.Type == ModContent.GetInstance<StartingShip>().Type)
            {
                foreach (PlayerLayer l in layers)
                {
                    if (l.Name == "Arms")

                    {
                        layers.Remove(l);
                        break;
                    }
                }
            }
            //layers[0].
            //layers.Add(new PlayerLayer())
            base.ModifyDrawLayers(layers);
        }
        


        public override void UpdateBiomeVisuals()
        {
            //Main.numStars = Main.maxStars;
            bool conditionInSpace = false;
            bool conditionOverworld = overworldBiomes.Contains(biome);
            player.ManageSpecialBiomeVisuals("StarSailorMod:SkySpace", conditionInSpace);
            player.ManageSpecialBiomeVisuals("StarSailorMod:SkyOverworld", conditionOverworld);

        }
        public override void PostUpdateRunSpeeds()
        {
            //player.mapFullScreen = false;
            //Main.mapFullscreen = false;
            Main.mapEnabled = true;
            if (gravityValues.TryGetValue(planet, out float grav))
            {
            }
            else grav = 0.4f;
            player.gravity = grav;
            base.PostUpdateRunSpeeds();

            #region gravity stuff
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
            #endregion
            else
            {
                player.maxRunSpeed = 3 + (ampedCounter / 60f);
                player.runAcceleration = 0.08f + (ampedCounter / 1000f);
            }
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {

            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);

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
            sb.Draw(ModContent.GetInstance<StarSailorMod>().pixel, new Rectangle((int)(playerRect.X - Main.screenPosition.X), (int)(playerRect.Y - Main.screenPosition.Y), playerRect.Width, playerRect.Height), Color.Pink * 0.3f);
            GuiHelpers.DrawLine(sb, centre, centre + (tangVel * perpDir * 30f), 5, Color.Red);
            GuiHelpers.DrawLine(sb, centre, centre + (radVel * direction * 30f), 5, Color.Blue);
            GuiHelpers.DrawLine(sb, centre, centre + (16f * dirToGrav), 2, Color.Green);
        }

        public Vector2 GetPosition()
        {
            return player.position;
        }

        public Vector2 GetScreenPosition()
        {
            return GetPosition() - Main.screenPosition;
        }

        public bool WithinDistance() => false;
    }
    public struct BiomeLocationMapping : IComparable<BiomeLocationMapping>
    {
        public int priority;
        public Rectangle location;
        public Biomes biome;
        public Planet planet;

        public BiomeLocationMapping(Rectangle loc, Biomes b, Planet pl, int pr)
        {
            location = loc;
            biome = b;
            planet = pl;
            priority = pr;
        }
        public BiomeLocationMapping(Vector2 tl, Vector2 br, Biomes b, Planet pl, int pr)
        {
            location = new Rectangle((int)tl.X, (int)tl.Y, (int)(br.X - tl.X), (int)(br.Y - tl.Y));
            biome = b;
            planet = pl;
            priority = pr;
        }
        public bool CheckPlayerSatisfies(Vector2 pos)
        {
            Rectangle testRect = new Rectangle((int)pos.X, (int)pos.Y, 1, 1);
            return testRect.Intersects(location);
        }

        public int CompareTo(BiomeLocationMapping other)
        {
            return other.priority - priority;
        }
    }
    public class PlayerHolder
    {
        bool engaged = false;
        Vector2 location;
        Player player;
        public void Engage(Vector2 loc, Player pl)
        {
            player = pl;
            engaged = true;
            location = loc;
        }
        public void Disengage()
        {
            engaged = false;
        }
        public void Update()
        {
            if (location == Vector2.Zero) engaged = false;
            if (engaged)
            {
                player.position = location;
            }
        }
    }
}
