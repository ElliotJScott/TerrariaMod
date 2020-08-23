using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Dusts
{
    class OrbOfVitalityAmbient : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.velocity *= 0.999f;
            dust.alpha = (int)(dust.alpha *  0.999f);
            if (dust.velocity.Length() < 0.1f) dust.active = false;
            return false;
        }
    }
}
