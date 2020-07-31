using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace StarSailor.Backgrounds
{
    static class BgHooks
    {
        public static void DrawBGs(SpriteBatch sb, Texture2D[] tex, int[] offsets, float[] darkens, float[] scales)
        {
            int[] widths = new int[tex.Length];
            for (int i = 0; i < tex.Length; i++) widths[i] = tex[i].Width * (int)(scales[i] * Main.screenHeight / tex[i].Height);
            int[] kekw = CalcDisplacement(widths);
            for (int i = tex.Length - 1; i >= 0 ; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Draw(tex[i], new Rectangle(-kekw[i] + (tex[i].Width * (int)(scales[i] * Main.screenHeight / tex[i].Height) * j), offsets[i], tex[i].Width * (int)(scales[i] * Main.screenHeight / tex[i].Height), (int)(scales[i] * Main.screenHeight)), new Color(darkens[i] * 1, darkens[i] * 1, darkens[i] * 1));
                }
            }
        }
        static int[] CalcDisplacement(int[] widths)
        {
            int xPos = (int)Main.player[Main.myPlayer].position.X;
            int count = widths.Length;
            int[] ret = new int[count];
            for (int i = 0; i < count; i++)
            {
                int adj = 3 + ( 2 *i);
                ret[i] = (xPos % (widths[i] * adj)) / adj;
            }
            return ret;
        }
    }
}
