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
    class SpawnRateModifier : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (ModContent.GetInstance<DimensionManager>().currentDimension == Dimensions.Dimensions.Travel) maxSpawns = 0;
            //if (player.GetModPlayer<PlayerFixer>().InSpace)
            //{
            //    maxSpawns = 0;
            //}
        }
        //public override 
    }
}
