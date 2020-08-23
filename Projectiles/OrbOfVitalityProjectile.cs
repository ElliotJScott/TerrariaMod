using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Buffs;
using StarSailor.Dusts;
using StarSailor.GUI;
using StarSailor.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Projectiles
{
    public abstract class OrbOfVitalityProjectile : ModProjectile
    {
        public abstract int Remain { get; }
        public abstract int Damage { get; }
        public abstract int buffID { get; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb of Vitality");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode

        }
        float damageDealt = 0;
        int ticker = 0;
        public override void SetDefaults()
        {
            projectile.width = 15;
            projectile.height = 15;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;

            // These 2 help the projectile hitbox be centered on the projectile sprite.
            drawOffsetX = 5;
            drawOriginOffsetY = 5;
            //projectile.CloneDefaults(ProjectileID.Bomb);
            projectile.damage = 0;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.timeLeft = Math.Min(projectile.timeLeft, Remain);
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 texPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            if (projectile.timeLeft < Remain)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

                // Retrieve reference to shader
                var orbShader = GameShaders.Misc["StarSailor:OrbEffect"];
                
                orbShader.UseOpacity(1f);
                orbShader.Apply(null);
                Texture2D glowTex = ((StarSailorMod)mod).orbGlow;
                Vector2 glowPosition = texPos + (0.5f * (new Vector2(texture.Width, texture.Height) - new Vector2(glowTex.Width, glowTex.Height)));
                Main.spriteBatch.Draw(glowTex, glowPosition, Color.White);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                DrawLinesForNPCs(glowTex.Width / 2);
            }
            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, texPos, drawColor);
            
            return false;
        }
        public override void AI()
        {

            /*
            if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
            {
                projectile.tileCollide = false;
                projectile.alpha = 255;
                projectile.position = projectile.Center;
                projectile.width = 125;
                projectile.height = 125;
                projectile.Center = projectile.position;
                projectile.damage = 20;
                projectile.knockBack = 10f;
            }
            */
            if (projectile.timeLeft > Remain)
            {
                projectile.ai[0] += 1f;
                if (projectile.ai[0] > 5f)
                {
                    projectile.ai[0] = 10f;
                    projectile.velocity.Y = projectile.velocity.Y + 0.2f;
                }
            }
            else
            {
                projectile.velocity = ReduceVelocity(projectile.velocity);
                Texture2D glowTex = ((StarSailorMod)mod).orbGlow;
                ticker++;
                if (ticker >= 10)
                {
                    ticker -= 10;
                    HurtNPCsInRange(glowTex.Width / 2);
                }

                for (int i = 0; i < 10; i++)
                {
                    Vector2 rand = new Vector2(1, 0);
                    rand = rand.RotatedByRandom(2 * Math.PI);
                    int size = Main.rand.Next(16, 32);
                    Vector2 offs = new Vector2(0.5f * Main.rand.NextFloat(-glowTex.Width, glowTex.Width), 0.5f * Main.rand.NextFloat(-glowTex.Height, glowTex.Height));
                    if (offs.Length() < glowTex.Width * 0.5f) {
                        int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y) + offs, size, size, ModContent.DustType<OrbOfVitalityAmbient>(), rand.X, rand.Y, 100, default(Color), 1f);
                        Main.dust[dustIndex].noGravity = true;
                    }
                }
            }
            //projectile.rotation += projectile.velocity.X * 0.1f;
            return;
        }
        private void HurtNPCsInRange(float dist)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].townNPC && !Main.npc[i].friendly)
                {
                    Vector2 trial = Main.npc[i].Center - projectile.Center;
                    if (trial.Length() < dist)
                    {

              
                        damageDealt += Damage;

                        ApplyDebuffs(Main.npc[i]);
                    }
                }
            }
        }
        public virtual void ApplyDebuffs(NPC npc)
        {
            npc.AddBuff(buffID, 1);
        }
        public void DrawLinesForNPCs(float dist)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            // Retrieve reference to shader
            var orbShader = GameShaders.Misc["StarSailor:OrbConnectorEffect"];

            orbShader.UseOpacity(1f);
            orbShader.Apply(null);
            GuiHelpers.DrawLineExactPositions(Main.spriteBatch, Main.player[projectile.owner].Center, projectile.Center + new Vector2(texture.Width / 2, texture.Height / 2), 8, Color.DarkBlue, ((StarSailorMod)mod).orbConnector);
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].townNPC && !Main.npc[i].friendly)
                {
                    Vector2 trial = Main.npc[i].Center - projectile.Center;
                    if (trial.Length() < dist)
                    {
                        GuiHelpers.DrawLineExactPositions(Main.spriteBatch, Main.npc[i].Center, projectile.Center + new Vector2(texture.Width / 2, texture.Height / 2), 3, Color.Pink, ((StarSailorMod)mod).orbConnector);
                    }
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

        }
        private Vector2 ReduceVelocity(Vector2 vel)
        {
            Vector2 newVel = vel * 0.95f;
            if (newVel.Length() < 0.5f) return Vector2.Zero;
            else return newVel;
        }
        public override void Kill(int timeLeft)
        {
            if (damageDealt >= 5)
            {
                Main.player[projectile.owner].HealEffect((int)(damageDealt / 5));
            }
            base.Kill(timeLeft);
        }
    }
    public class OrbOfVitalityV1Projectile : OrbOfVitalityProjectile
    {
        public override int Remain => 150;

        public override int Damage => 1;

        public override int buffID => ModContent.BuffType<OrbV1Buff>();

        public override void Kill(int timeLeft)
        {
            Item.NewItem(projectile.position, new Vector2(0, 0), ModContent.ItemType<OrbOfVitalityV1>());
            base.Kill(timeLeft);
        }
    }

    public class OrbOfVitalityV2Projectile : OrbOfVitalityProjectile
    {
        public override int Remain => 250;
        public override int Damage => 5;
        public override int buffID => ModContent.BuffType<OrbV2Buff>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.timeLeft = 300;
        }
        public override void Kill(int timeLeft)
        {
            Item.NewItem(projectile.position, new Vector2(0, 0), ModContent.ItemType<OrbOfVitalityV2>());
            base.Kill(timeLeft);
        }
    }
    public class OrbOfVitalityV3Projectile : OrbOfVitalityProjectile
    {
        public override int Remain => 250;
        public override int Damage => 10;
        public override int buffID => ModContent.BuffType<OrbV3Buff>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.timeLeft = 300;
        }
        public override void ApplyDebuffs(NPC npc)
        {
            base.ApplyDebuffs(npc);
            npc.AddBuff(ModContent.BuffType<EnemySlow>(), 1);
        }
        public override void Kill(int timeLeft)
        {
            Item.NewItem(projectile.position, new Vector2(0, 0), ModContent.ItemType<OrbOfVitalityV3>());
            base.Kill(timeLeft);
        }
    }
    public class OrbOfVitalityVMaxProjectile : OrbOfVitalityProjectile
    {
        public override int Remain => 310;
        public override int Damage => 15;
        public override int buffID => ModContent.BuffType<OrbVMaxBuff>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            projectile.timeLeft = 360;
        }
        public override void ApplyDebuffs(NPC npc)
        {
            base.ApplyDebuffs(npc);
            npc.AddBuff(ModContent.BuffType<EnemySlow>(), 1);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 texPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            if (projectile.timeLeft < Remain)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

                // Retrieve reference to shader
                var orbShader = GameShaders.Misc["StarSailor:OrbVMaxEffect"];

                orbShader.UseOpacity(1f);
                orbShader.Apply(null);
                Texture2D glowTex = ((StarSailorMod)mod).orbGlow;
                Vector2 glowPosition = texPos + (0.5f * (new Vector2(texture.Width, texture.Height) - new Vector2(glowTex.Width, glowTex.Height)));
                Main.spriteBatch.Draw(glowTex, glowPosition, Color.White);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
                DrawLinesForNPCs(glowTex.Width / 2);
            }
            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture, texPos, drawColor);


            return false;
        }
        public override void Kill(int timeLeft)
        {
            Item.NewItem(projectile.position, new Vector2(0, 0), ModContent.ItemType<OrbOfVitalityVMax>());
            base.Kill(timeLeft);
        }
    }
}
