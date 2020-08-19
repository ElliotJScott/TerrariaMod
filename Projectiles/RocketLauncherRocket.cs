using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Buffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Projectiles
{
    public abstract class RocketLauncherRocket : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rocket Launcher Rocket");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.RocketI);
            aiType = ProjectileID.RocketI;
            projectile.damage = 1;
        }
        public override bool PreKill(int timeLeft)
        {
            projectile.type = ProjectileID.RocketI;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.type = ProjectileID.RocketI;
            return true;
            //return base.OnTileCollide(oldVelocity);
        }

    }
    public class RocketLauncherV1V2Rocket : RocketLauncherRocket
    {

    }
    public class RocketLauncherV3Rocket : RocketLauncherRocket
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 100);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
    public class RocketLauncherVMaxRocket : RocketLauncherRocket
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 100);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            base.AI();
            Main.NewText(projectile.timeLeft);
            int scale = Math.Min(3600 - projectile.timeLeft, 300);
            Vector2 closee = new Vector2(100000);
            bool haveFound = false;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].townNPC && !Main.npc[i].friendly)
                {
                    Vector2 trial = Main.npc[i].position - projectile.position;
                    if (trial.Length() < closee.Length()) closee = trial;
                    haveFound = true;
                }
            }
            if (haveFound)
            {
                Vector2 newVel = projectile.velocity + (closee * 0.00005f * scale);
                projectile.velocity = newVel * (projectile.velocity.Length() / newVel.Length());

            }

        }
    }
}
