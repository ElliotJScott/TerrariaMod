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
    abstract class ShieldCharger : ModItem
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
            //item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 8f;
            //item.useAmmo = AmmoID.Arrow;
            item.useAnimation = 1;
            item.useTime = 1;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 newPosition = position + (5f * new Vector2(speedX, speedY)) - new Vector2(6, 20);
            Projectile.NewProjectile(newPosition.X, newPosition.Y, speedX * 0.01f, speedY * 0.01f, ModContent.ProjectileType<ShieldChargerV1V2V3Barrier>(), 0, 0, player.whoAmI);
            return false;
        }
    }
    class ShieldChargerV1 : ShieldCharger
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 ShieldCharger");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class ShieldChargerV2 : ShieldCharger
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.rare = ItemRarityID.LightPurple;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(BuffID.Ironskin, 10);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }

    class ShieldChargerV3 : ShieldCharger
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(BuffID.Ironskin, 10);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
    class ShieldChargerVMax : ShieldCharger
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spunk Weapon");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(BuffID.Ironskin, 10);
            Vector2 newPosition = position + (5f * new Vector2(speedX, speedY)) - new Vector2(6, 20);
            Projectile.NewProjectile(newPosition.X, newPosition.Y, speedX * 0.01f, speedY * 0.01f, ModContent.ProjectileType<ShieldChargerVMaxBarrier>(), 0, 0, player.whoAmI);
            return false;
        }
    }
}
