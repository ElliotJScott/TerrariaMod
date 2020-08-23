using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.Buffs;
using StarSailor.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Items.Weapons
{
    abstract class PowerGlove : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 6;
            item.melee = true;
            item.width = 38;
            item.height = 26;
            item.reuseDelay = 120;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            scale = 20;
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override void UseStyle(Player player)
        {
            
            base.UseStyle(player);
            player.itemRotation = (float)Math.PI * (1f - (player.direction / 2f));
            player.itemLocation -= new Vector2(4, 16);
            Main.playerDrawData.Add(new DrawData(Main.itemTexture[item.type], player.itemLocation, null, Color.White, player.itemRotation, Vector2.Zero, 1f, SpriteEffects.None, 0));
            
        }
        public override bool UseItem(Player player)
        {
            return base.UseItem(player);
        }
    }
    class PowerGloveV1 : PowerGlove
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Level 1 PowerGlove");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }

    class PowerGloveV2 : PowerGlove
    {
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("Has increased shooting speed.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 12;
            item.autoReuse = false;
            item.rare = ItemRarityID.LightPurple;
        }
    }

    class PowerGloveV3 : PowerGlove
    {
        public override void SetStaticDefaults()
        {
            
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 25;
            item.autoReuse = false;
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
    class PowerGloveVMax : PowerGlove
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Fist");
            //Tooltip.SetDefault("Has increased shooting speed and critical strike chance. Causes bleeding on hit - multiple bleeding wounds do bonus damage.");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.damage = 41;
            item.autoReuse = false;
            item.rare = ItemRarityID.Red;
            item.crit = 12;
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            damage += player.GetModPlayer<PlayerFixer>().ampedCounter/6;
            base.ModifyHitNPC(player, target, ref damage, ref knockBack, ref crit);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 120);
            if (crit)
            {
                player.AddBuff(ModContent.BuffType<Amped>(), 360);
            }
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
