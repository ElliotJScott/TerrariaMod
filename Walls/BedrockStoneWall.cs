using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace StarSailor.Walls
{

	public class BedrockStoneWall : ModWall
	{
		public override void SetDefaults()
		{
			drop = mod.ItemType("BedrockStoneWall");
			AddMapEntry(new Color(5, 5, 5));
		}

    }

}
