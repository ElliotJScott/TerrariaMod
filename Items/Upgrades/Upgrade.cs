using StarSailor.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarSailor.Items.Upgrades
{
    abstract class Upgrade : ModItem
    {
        public abstract Item InputItem { get; }
        public abstract Item OutputItem { get; }
        public abstract string UpgradeTooltip { get; }
        public override void SetStaticDefaults()
        {
            string ttip = UpgradeTooltip + "\nRight click to use.";
            Tooltip.SetDefault(ttip);
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.maxStack = 1;
            item.value = 100;
        }
        public override bool CanRightClick()
        {
            return IndexOfItem(InputItem, Main.LocalPlayer) != -1;
        }
        public override void RightClick(Player player)
        {
            int ind = IndexOfItem(InputItem, player);
            if (ind != -1) 
            {
                player.inventory[ind].type = ItemID.None;
                player.inventory[ind].SetDefaults();
                Item.NewItem(player.position, OutputItem.type);

                //player.inventory[ind].SetDefaults();
                //int j = IndexOfItem(item, player);
                //player.inventory[j] = null;
            }
            base.RightClick(player);
        }
        public int IndexOfItem(Item i, Player p)
        {
            Item[] list = p.inventory;
            for (int q = 0; q < list.Length; q++)
            {
                if (i.Name == list[q].Name) return q;
            }
            return -1;
        }
    }
    #region Pistol
    class PistolV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV1>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolV2>().item;

        public override string UpgradeTooltip => "Increases shooting speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class PistolV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV2>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolV3>().item;

        public override string UpgradeTooltip => "Increases critical strike chance.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class PistolVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<PistolV3>().item;

        public override Item OutputItem => ModContent.GetInstance<PistolVMax>().item;

        public override string UpgradeTooltip => "Bullets cause bleeding damage over time.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
    #region Assault Rifle
    class AssaultRifleV2Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV1>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleV2>().item;

        public override string UpgradeTooltip => "Increases shooting speed.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightPurple;
        }
    }
    class AssaultRifleV3Upgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV2>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleV3>().item;

        public override string UpgradeTooltip => "Increases damage.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.LightRed;
        }
    }
    class AssaultRifleVMaxUpgrade : Upgrade
    {
        public override Item InputItem => ModContent.GetInstance<AssaultRifleV3>().item;

        public override Item OutputItem => ModContent.GetInstance<AssaultRifleVMax>().item;

        public override string UpgradeTooltip => "Bullets pierce 3 enemies.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            item.rare = ItemRarityID.Red;
        }
    }
    #endregion
}
