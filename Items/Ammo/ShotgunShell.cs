﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Ammo
{
    class ShotgunShell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Ammo for the Shotgun.");
            DisplayName.SetDefault("Shotgun Shell");
        }

        public override void SetDefaults()
        {
            item.damage = 6;
            item.ranged = true;
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            item.knockBack = 1.5f;
            item.value = 10;
            item.rare = ItemRarityID.White;  
            item.shootSpeed = 8f;                  //The speed of the projectile
            item.ammo = AmmoID.CandyCorn;              //The ammo class this ammo belongs to.
        }
    }
}
