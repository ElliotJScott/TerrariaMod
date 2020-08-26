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
    abstract class ChainWhip : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(silver: 5);
            item.rare = ItemRarityID.Blue;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 4f;
            item.damage = 9;
            item.noUseGraphic = true;
            item.shoot = ModContent.ProjectileType<ChainWhipV1V2V3Projectile>();
            item.shootSpeed = 15.1f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.crit = 9;
            item.channel = true;
        }
    }
    class ChainWhipV1 : ChainWhip
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 ChainWhip");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class ChainWhipV2 : ChainWhip
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 25;
            item.rare = ItemRarityID.LightPurple;
        }
    }

    class ChainWhipV3 : ChainWhip
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 31;
            item.rare = ItemRarityID.LightRed;
        }
    }
    class ChainWhipVMax : ChainWhip
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion's Tail");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 48;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<ChainWhipVMaxProjectile>();
        }
        public override bool AltFunctionUse(Player player)
        {
            if (GetProjectile(player) != null)
            {
                   
                ChainWhipVMaxProjectile proj = (ChainWhipVMaxProjectile)GetProjectile(player).modProjectile;
                if (proj.CanDrag())
                {
                    proj.RightClick();
                }
                
            }
            return false;
        }
        public Projectile GetProjectile(Player player)
        {
            foreach (Projectile p in Main.projectile)
            {
                if (p.type == ModContent.ProjectileType<ChainWhipVMaxProjectile>() && p.owner == player.whoAmI)
                {
                    return p;
                }
            }
            return null;
        }
    }
}
