using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Items.Upgrades
{
    abstract class Upgrade : ModItem
    {
        public abstract Item InputItem { get; }
        public abstract Item OutputItem { get; }
        public override void RightClick(Player player)
        {
            if (player.inventory.Contains(InputItem))
            {
                int i = player.inventory.ToList().IndexOf(InputItem);
                player.inventory[i] = OutputItem;
            }
            base.RightClick(player);
        }
    }
}
