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
    abstract class Cryogun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 1;
            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 4;
            item.useAnimation = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 1f;
            item.useAmmo = AmmoID.None;
        }
    }
    class CryogunV1 : Cryogun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 Cryogun");
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
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            position += muzzleOffset;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CryogunV1V2Cloud>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class CryogunV2 : Cryogun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 5;
            item.shootSpeed = 2f;
            item.useTime = 3;
            item.useAnimation = 3;       
            item.rare = ItemRarityID.LightPurple;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            position += muzzleOffset;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CryogunV1V2Cloud>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class CryogunV3 : Cryogun
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 9;
            item.shootSpeed = 2f;
            item.useTime = 3;
            item.useAnimation = 3;
            item.rare = ItemRarityID.LightRed;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            position += muzzleOffset;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CryogunV3Cloud>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
    class CryogunVMax : Cryogun
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blizzard Gun");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 19;
            item.shootSpeed = 2.5f;
            item.useTime = 2;
            item.useAnimation = 2;
            item.rare = ItemRarityID.Red;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
            position += muzzleOffset;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<CryogunVMaxCloud>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
