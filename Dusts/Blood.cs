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
    class Blood : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity.Y = -0.8f;
            dust.velocity.X = 0f;
        }

        public override bool MidUpdate(Dust dust)
        {
            dust.velocity.Y += 0.05f;

            return false;
        }
    }
}
