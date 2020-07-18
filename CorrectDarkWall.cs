using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor
{
    class CorrectDarkWall : GlobalWall
    {
        public override void SetDefaults()
        {
            
            base.SetDefaults();
        }

        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {
            if (Framing.GetTileSafely(i, j).type == TileID.Dirt && r == 0 && g == 0 && b == 0)
            {
                r = 0.01f;
                g = 0.01f;
                b = 0.01f;
            }

            //base.ModifyLight(i, j, type, ref r, ref g, ref b);
        }
    }
}
