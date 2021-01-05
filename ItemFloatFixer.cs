using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Dimensions;
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
            //StarSailorMod sm = (StarSailorMod)mod;
            if (ModContent.GetInstance<DimensionManager>().currentDimension == Dimensions.Dimensions.Asteroid)
            {
                item.velocity.Y = 0;
                return;
            }
            else base.Update(item, ref gravity, ref maxFallSpeed);

        }
    }
}
