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
    abstract class KrakenGun : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 90;
            item.useAnimation = 90;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item11;
            item.autoReuse = false;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 5f;
            item.useAmmo = AmmoID.None;
            item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
            Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Tentacle>(), damage, knockBack, player.whoAmI);
            return false;
        }

    }

    class KrakenGunV1 : KrakenGun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 KrakenGun");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }


    }

    class KrakenGunV2 : KrakenGun
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 14;
            item.useTime = 45;
            item.useAnimation = 45;       
            item.rare = ItemRarityID.LightPurple;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Tentacle>(), damage, knockBack, player.whoAmI);
            return false;
        }
    }

    class KrakenGunV3 : KrakenGun
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
    }
    class KrakenGunVMax : KrakenGun
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Octocannon");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 26;
            item.useTime = 15;
            item.useAnimation = 15;
            item.rare = ItemRarityID.Red;
            item.scale = 0.8f;
            item.crit = 12;

        }
    }
}
