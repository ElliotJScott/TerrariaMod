using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor
{
    class ItemFloatFixer : GlobalItem
    {

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            StarSailorMod sm = (StarSailorMod)mod;
            if (sm.GetBiomePlanet(item.position/16f).Item2 == Planet.AsteroidBelt)
            {
                item.velocity.Y = 0;
                return;
            }
            else base.Update(item, ref gravity, ref maxFallSpeed);

        }
    }
}
