using Microsoft.Xna.Framework;
using StarSailor.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Weapons
{
    abstract class OrbOfVitality : ModItem
    {
        public override void SetStaticDefaults()
        {

            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 6f;
            item.width = 16;
            item.height = 16;
            item.maxStack = 1;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 40;
            item.useTime = 40;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.rare = ItemRarityID.Blue;
        }
    }
    class OrbOfVitalityV1 : OrbOfVitality
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<OrbOfVitalityV1Projectile>();
        }
    }

    class OrbOfVitalityV2 : OrbOfVitality
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.shoot = ModContent.ProjectileType<OrbOfVitalityV2Projectile>(); 
            item.rare = ItemRarityID.LightPurple;
        }
    }

    class OrbOfVitalityV3 : OrbOfVitality
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<OrbOfVitalityV3Projectile>();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class OrbOfVitalityVMax : OrbOfVitality
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orb Of Ruin");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<OrbOfVitalityVMaxProjectile>();
            item.rare = ItemRarityID.Red;
        }
    }
}
