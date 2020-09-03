using StarSailor.GUI;
using StarSailor.Items.Upgrades;
using StarSailor.Items.Weapons;
using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor.NPCs.Characters
{
    class Boris : Character
    {
        public override string InternalName => "Boris";

        public override IInteraction Interaction{ get
            {
                string pistolUpgTT = "Increases shooting speed. Right click this in your\ninventory to apply it to a Pistol V1.";
                string lasRifUpgTT = "Increases damage and critical strike chance. Right click\nthis in your inventory to apply it to a Laser Rifle V2.";
                string assRifTT = string.Format("Fires three bullets in a short burst.\nDeals {0} damage per bullet. Uses 5.5mm ammunition.", ModContent.GetInstance<AssaultRifleV1>().item.damage);

                SequenceQueue queue = SequenceBuilder.ConstructBasicShopSequence(this, "Want to buy anything?", 
                    new ShopItem(ModContent.ItemType<PistolV2Upgrade>(), 31000, Resource.Money, pistolUpgTT), 
                    new ShopItem(ModContent.ItemType<AssaultRifleV1>(), 7800, Resource.Money, assRifTT),
                    new ShopItem(ModContent.ItemType<LaserRifleV3Upgrade>(), 213000, Resource.Money, lasRifUpgTT)
                    );
                return new SequenceInteraction(queue);
            }

        }



        //public override
    }
}
