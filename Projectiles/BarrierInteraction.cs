using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Projectiles
{
    class BarrierInteraction : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        int reflectionTimer = 0;
        public override void AI(Projectile projectile)
        {
            if (projectile.hostile)
            {
                reflectionTimer = Math.Max(reflectionTimer - 1, 0);
                StarSailorMod sm = (StarSailorMod)mod;
                for (int i = 2; i < sm.barriers.Count; i++)
                {
                    if (sm.barriers[i].GetHitbox().Intersects(projectile.Hitbox))
                    {
                        if (sm.barriers[i].projectile.type == ModContent.ProjectileType<ShieldChargerV1V2V3Barrier>())
                        {
                            projectile.timeLeft = 0;
                        }
                        else if (sm.barriers[i].projectile.type == ModContent.ProjectileType<ShieldChargerVMaxBarrier>() && reflectionTimer == 0 && Math.Sign(Vector2.Dot(projectile.velocity, sm.barriers[i].projectile.velocity)) == -1)
                        {
                            projectile.velocity = -projectile.velocity;
                            reflectionTimer = 5;
                            projectile.hostile = false;
                            projectile.friendly = true;
                        }
                    }
                }
            }
            base.AI(projectile);
        }
    }
}
