using Microsoft.Xna.Framework;
using StarSailor.Buffs;
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
    abstract class RodOfDistortion : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 20;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = false;
        }
    }
    class RodOfDistortionV1 : RodOfDistortion
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 RodOfDistortion");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class RodOfDistortionV2 : RodOfDistortion
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 30;
            item.knockBack = 30;
            item.rare = ItemRarityID.LightPurple;
        }
    }

    class RodOfDistortionV3 : RodOfDistortion
    {
        public override void SetStaticDefaults()
        {

            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 50;
            item.knockBack = 30;
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
    class RodOfDistortionVMax : RodOfDistortion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Rod Weapon");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 70;
            item.rare = ItemRarityID.Red;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {


            if (player.altFunctionUse == 2)
            {
                item.damage = 90;
                item.knockBack = 0;
            }
            else
            {
                item.damage = 70;
                item.knockBack = 50;
            }
            return base.CanUseItem(player);

        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.altFunctionUse == 2)
            {
                target.AddBuff(ModContent.BuffType<EnemyFreeze>(), 30);
            }
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
