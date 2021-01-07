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
            for (int i = 0; i < tex.Length; i++)
            {
                widths[i] = tex[i].Width * (int)(scales[i] * Main.screenHeight / tex[i].Height);
                if (widths[i] == 0) widths[i] = tex[i].Width;
            }
          
            int[] kekw = CalcDisplacement(widths);
            for (int i = tex.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    int q = tex[i].Width * (int)(scales[i] * Main.screenHeight / tex[i].Height);
                    if (q == 0) q = tex[i].Width;
                    Rectangle rect = new Rectangle(-kekw[i] + (q * j), offsets[i], q, (int)(scales[i] * Main.screenHeight));
                   
                    sb.Draw(tex[i], rect, new Color(darkens[i] * 1, darkens[i] * 1, darkens[i] * 1));
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
                int adj = 3 + (2 * i);
                ret[i] = (xPos % (widths[i] * adj)) / adj;

            }
            return ret;
        }
        public static void DrawAnimatedBGs(SpriteBatch sb, Texture2D[] tex, int[] offsets, float[] darkens, float[] scales, int[] frameCounts, int[] frames)
        {
            int[] heights = new int[tex.Length];
            int[] widths = new int[tex.Length];
            for (int i = 0; i < tex.Length; i++)
            {
                heights[i] = tex[i].Height / frameCounts[i];
                widths[i] = tex[i].Width * (int)(scales[i] * Main.screenHeight / heights[i]);
            }
            int[] kekw = CalcDisplacement(widths);
            for (int i = tex.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    sb.Draw(tex[i], new Rectangle(-kekw[i] + (tex[i].Width * (int)(scales[i] * Main.screenHeight / heights[i]) * j), offsets[i], tex[i].Width * (int)(scales[i] * Main.screenHeight / heights[i]), (int)(scales[i] * Main.screenHeight)), new Rectangle(0, frames[i] * heights[i], tex[i].Width, heights[i]), new Color(darkens[i] * 1, darkens[i] * 1, darkens[i] * 1));
                }
            }
        }
    }
}
