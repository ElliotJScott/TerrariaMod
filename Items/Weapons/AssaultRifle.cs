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
    abstract class AssaultRifle : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 6;
            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 8f;
            item.useAmmo = AmmoID.Arrow;
            item.useAnimation = 24;
            item.useTime = 8;
            item.reuseDelay = 28;
        }
        public override bool ConsumeAmmo(Player player)
        {
            return !(player.itemAnimation < item.useAnimation - 2);
        }
    }
    class AssaultRifleV1 : AssaultRifle
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 AssaultRifle");
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
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<AssaultRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class AssaultRifleV2 : AssaultRifle
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 10; 
            item.rare = ItemRarityID.LightPurple;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<AssaultRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class AssaultRifleV3 : AssaultRifle
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 18;
            item.rare = ItemRarityID.LightRed;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<AssaultRifleV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
    class AssaultRifleVMax : AssaultRifle
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pee Pee Poo Poo Gun");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 23;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<AssaultRifleVMaxBullet>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
