using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace teo.Walls
{

	public class SpaceWall : ModWall
	{
		public override void SetDefaults()
		{
			drop = mod.ItemType("SpaceWall");
			AddMapEntry(new Color(0, 0, 0));
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 0.9f;
        }

    }

}
