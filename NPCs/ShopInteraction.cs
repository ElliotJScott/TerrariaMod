using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace StarSailor.NPCs
{
    class ShopInteraction : IInteraction
    {
        public bool Enabled { get; set; }
        ITalkable source;
        Item[] items;
        public ShopInteraction(ITalkable src, params Item[] i)
        {
            source = src;
            items = i;
        }
        public void Execute()
        {
            Main.playerInventory = false;
            Enabled = true;
        }

        public void Update()
        {
            if (Main.playerInventory || !WithinDistance())
            {
                Enabled = false;
            }
            else
            {

            }

        }
        public bool WithinDistance()
        {
            Vector2 disp = Main.LocalPlayer.position - source.GetPosition();
            return Math.Abs(disp.X) < 3200 && Math.Abs(disp.Y) < 800;
            //return Vector2.Distance(source.GetPosition(), Main.LocalPlayer.position);
        }
    }
}
