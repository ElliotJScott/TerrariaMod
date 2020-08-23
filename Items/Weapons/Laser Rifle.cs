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
    abstract class LaserRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 80;
            item.useAnimation = 80;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item11;
            item.autoReuse = false;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.None;
        }
    }
    class LaserRifleV1 : LaserRifle
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 LaserRifle");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<LaserRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class LaserRifleV2 : LaserRifle
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 26;    
            item.rare = ItemRarityID.LightPurple;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<LaserRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class LaserRifleV3 : LaserRifle
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 36;
            item.rare = ItemRarityID.LightRed;
            item.crit = 8;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<LaserRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
    class LaserRifleVMax : LaserRifle
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Bloodthirster");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 50;
            item.rare = ItemRarityID.Red;
            item.crit = 25;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<LaserRifleVMaxBullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
