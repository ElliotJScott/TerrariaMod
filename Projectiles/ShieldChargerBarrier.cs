using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Projectiles
{
    public abstract class ShieldChargerBarrier : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield Charger Barrier");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }
        public override void SetDefaults()
        {
            ((StarSailorMod)mod).barriers.Add(this);
            projectile.frame = Main.rand.Next(3);
            projectile.width = 54;   
            projectile.height = 24;
            projectile.aiStyle = 1;            
            projectile.friendly = true;         
            projectile.hostile = false;         
            projectile.ranged = true;           
            projectile.penetrate = -1;
            projectile.timeLeft = 5;
            projectile.alpha = 0;
            projectile.light = 0.1f;   
            projectile.ignoreWater = false;
            projectile.tileCollide = false;  
            projectile.extraUpdates = 1;
            projectile.damage = 0;
            aiType = ProjectileID.Bullet;        
        }

        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            // Retrieve reference to shader
            var noiseShader = GameShaders.Misc["StarSailor:NoiseEffect"];

            noiseShader.UseOpacity(1f);
            noiseShader.Apply(null);
            Texture2D tex = Main.projectileTexture[projectile.type];
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, Main.projectileTexture[projectile.type].Width * 0.5f);

            Vector2 drawPos = projectile.position - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
            float factor = projectile.timeLeft < 1 ? 0f : 1f;
            Color color = projectile.GetAlpha(lightColor) * factor;
            Main.spriteBatch.Draw(tex, drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void AI()
        {
            base.AI();
        }
        public Rectangle GetHitbox()
        {
            Rectangle hb = projectile.Hitbox;
            int newWidth = (int)((hb.Width * Math.Abs(Math.Cos(projectile.rotation))) + (hb.Height * Math.Abs(Math.Sin(projectile.rotation))));
            int newHeight = (int)((hb.Width * Math.Abs(Math.Sin(projectile.rotation))) + (hb.Height * Math.Abs(Math.Cos(projectile.rotation))));
            int newX = hb.X - (int)(20f * Math.Abs(Math.Cos(projectile.rotation))) + 20 + (int)(20f * Math.Sin(projectile.rotation));
            int newY = hb.Y - (int)(20f*Math.Abs(Math.Sin(projectile.rotation))) + 20 - (int)(20f * Math.Cos(projectile.rotation));
            Rectangle newRect = new Rectangle(newX, newY, newWidth, newHeight);
            return newRect;
        }
        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
            ((StarSailorMod)mod).barriers.Remove(this);
        }


    }
    public class ShieldChargerV1V2V3Barrier : ShieldChargerBarrier
    {

    }

    public class ShieldChargerVMaxBarrier : ShieldChargerBarrier
    {
    }
}
