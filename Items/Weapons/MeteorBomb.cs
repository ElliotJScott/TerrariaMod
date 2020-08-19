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
    abstract class MeteorBomb : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 40;
            item.useAnimation = 40;
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
    class MeteorBombV1 : MeteorBomb
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 MeteorBomb");
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
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<MeteorBombV1V2Bomb>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class MeteorBombV2 : MeteorBomb
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 14;
            item.useTime = 25;
            item.useAnimation = 25;       
            item.rare = ItemRarityID.LightPurple;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<MeteorBombV1V2Bomb>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class MeteorBombV3 : MeteorBomb
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 18;
            item.useTime = 25;
            item.useAnimation = 25;
            item.rare = ItemRarityID.LightRed;
            item.crit = 12;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<MeteorBombV3Bomb>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
    class MeteorBombVMax : MeteorBomb
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wee wee");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 26;
            item.useTime = 25;
            item.useAnimation = 25;
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
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<MeteorBombVMaxBomb>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
