using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using StarSailor.Tiles;

namespace StarSailor.GUI
{
    public class GravDisplay
    {
        public static bool dispGui = false;
        public static void Draw(SpriteBatch sb)
        {
            if (dispGui)
            {
                GravitySource src = ModContent.GetInstance<GravitySource>();
                if (Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity)
                {
                    src.DrawGUIComps(sb);
                    Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().DrawGravGui(sb);
                }
            }
        }

    }

}
