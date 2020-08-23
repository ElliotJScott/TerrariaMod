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
    abstract class Shotgun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 11;
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
            item.shootSpeed = 8f;
            item.useAmmo = AmmoID.Bullet;
        }

    }
    class ShotgunV1 : Shotgun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 Shotgun");
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
            int numberProjectiles = 3 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShotgunV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }

    class ShotgunV2 : Shotgun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 17;
            item.useTime = 80;
            item.useAnimation = 80;       
            item.rare = ItemRarityID.LightPurple;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(25));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShotgunV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }

    class ShotgunV3 : Shotgun
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 23;
            item.useTime = 80;
            item.useAnimation = 80;
            item.rare = ItemRarityID.LightRed;
            item.crit = 12;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShotgunV1V2V3Bullet>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
    class ShotgunVMax : Shotgun
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nimbus Shotgun");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 32;
            item.useTime = 70;
            item.useAnimation = 70;
            item.rare = ItemRarityID.Red;
            item.scale = 0.8f;
            item.crit = 12;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ShotgunVMaxBullet>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
