using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Tiles
{
    class SwampGrass : CustomGrass
    {
        public override void SetDefaults()
        {
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileMerge[Type][ModContent.TileType<SwampDirt>()] = true;
            Main.tileMerge[ModContent.TileType<SwampDirt>()][Type] = true;
            //Main.tileLighted[Type] = true;]
            AddMapEntry(new Color(150, 20, 40));
            drop = ModContent.ItemType<Items.Placeable.SwampGrass>(); //put this in the subclass
            base.SetDefaults();
        }
    }
}
