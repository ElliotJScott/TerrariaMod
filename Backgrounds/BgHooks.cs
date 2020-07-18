using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace starsailor.Backgrounds
{
    static class BgHooks
    {
        public static void DrawBGs(SpriteBatch sb, Texture2D mid, Texture2D front)
        {
            (int, int) kekw = CalcDisplacement();
            
        }
        static (int, int) CalcDisplacement()
        {
            int xPos = (int)Main.player[Main.myPlayer].position.X;
            int num1 = (xPos % 2048) / 2;
            int num2 = (xPos / 2048) / 4;
            return (num1, num2);
        }
    }
}
