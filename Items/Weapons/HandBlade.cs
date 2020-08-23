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
    abstract class HandBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 6;
            item.melee = true;
            item.width = 38;
            item.height = 26;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = false;
        }
    }
    class HandBladeV1 : HandBlade
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 HandBlade");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class HandBladeV2 : HandBlade
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 12;
            item.useTime = 12;
            item.useAnimation = 12;
            item.autoReuse = true;
            item.rare = ItemRarityID.LightPurple;
        }
    }

    class HandBladeV3 : HandBlade
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 25;
            item.useTime = 12;
            item.useAnimation = 12;
            item.autoReuse = true;
            item.rare = ItemRarityID.LightRed;
            item.crit = 8;
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
    class HandBladeVMax : HandBlade
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Assassin's Dagger");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 41;
            item.useTime = 12;
            item.useAnimation = 12;
            item.autoReuse = true;
            item.rare = ItemRarityID.Red;
            item.crit = 12;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (target.life * 3 < target.lifeMax)
            {
                crit = true;
                damage =(int)(damage *  1.75f);
                player.AddBuff(BuffID.Swiftness, 120);
            }
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 120);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
