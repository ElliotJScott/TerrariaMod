﻿using Microsoft.Xna.Framework;
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
    abstract class JuiceJacker : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.CopperShortsword);
            item.useTime = 20;
            item.useAnimation = 20;
            item.scale = 1f;
            item.damage = 1;
            item.crit = 0;
            item.melee = true;
            item.width = 32;
            item.height = 32;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = false;
            
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
    class JuiceJackerV1 : JuiceJacker
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 JuiceJacker");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class JuiceJackerV2 : JuiceJacker
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
    }

    class JuiceJackerV3 : JuiceJacker
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
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 120);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
    class JuiceJackerVMax : JuiceJacker
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cum Weapon");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }
        bool canShoot = false;
        int useTimer = 0;
        public override void SetDefaults()
        {
            base.SetDefaults();
            useTimer = 0;
            item.rare = ItemRarityID.Red;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            useTimer = Math.Max(useTimer - 1, 0);
            base.UpdateInventory(player);
        }
        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && useTimer == 0)
            {
                Vector2 position = player.position + new Vector2(14 + player.direction * 36, 24);
                Projectile.NewProjectile(position.X,position.Y, player.direction * 3, 0, ModContent.ProjectileType<JuiceJackerProjectile>(), 1, 0, player.whoAmI);
                useTimer = 30;
            }
            return base.UseItem(player);
        }
    }
}
