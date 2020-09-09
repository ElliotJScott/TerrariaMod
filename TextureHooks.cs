using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace StarSailor
{
    public static class TextureHooks
    {
        public static Texture2D GetShrunkTexture(Texture2D texture, int numFrames)
        {
            int x = 0;
            int y = 0;
            int width = texture.Width;
            int height = texture.Height / numFrames;
            Rectangle extractRegion = new Rectangle(x, y, width, height);
            Color[] rawData = new Color[width * height];
            texture.GetData(0, extractRegion, rawData, 0, width * height);
            //do rows first 
            List<Color> colorListData = rawData.ToList();
            for (int i = 0; i < height; i++)
            {
                bool emptyRow = true;
                for (int j = 0; j < width; j++)
                {
                    Color c = colorListData[i * width + j];
                    if (c.A != 0) emptyRow = false;
                }
                if (emptyRow)
                {
                    colorListData.RemoveRange(i * width, width);
                    height--;
                }
            }
            //then do columns
            for (int j = 0; j < width; j++)
            {
                bool emptyColumn = true;
                for (int i = 0; i < height; i++)
                {
                    Color c = colorListData[i * width + j];
                    if (c.A != 0) emptyColumn = false;
                }
                if (emptyColumn)
                {
                    for (int i = height - 1; i >= 0; i--)
                    {
                        colorListData.RemoveAt(i * width + j);

                    }
                    width--;
                }
            }
            Texture2D subtexture = new Texture2D(Main.graphics.GraphicsDevice, width, height);
            subtexture.SetData(colorListData.ToArray());
            return subtexture;
        }
        /// <summary>
        /// Returns a texture of the inputs overlayed on each other (not in an additive way). First elements are the prioritised ones
        /// </summary>
        /// <param name="textures"></param>
        /// <returns></returns>
        public static Texture2D StackTextures(int numFrames, params Texture2D[] textures)
        {
            List<Color[]> colorDataList = new List<Color[]>();
            foreach (Texture2D t in textures)
            {
                int x = 0;
                int y = 0;
                int width = t.Width;
                int height = t.Height / numFrames;
                Rectangle extractRegion = new Rectangle(x, y, width, height);
                Color[] rawData = new Color[width * height];
                t.GetData(0, extractRegion, rawData, 0, width * height);
                colorDataList.Add(rawData);
            }
            Color[] final = new Color[colorDataList[0].Length];
            for (int k = colorDataList.Count - 1; k >= 0; k--)
            {
                Color[] data = colorDataList[k];
                for (int i = 0; i < final.Length; i++)
                {
                    if (data[i].A != 0) final[i] = data[i];
                }
            }
            Texture2D finalTex = new Texture2D(Main.graphics.GraphicsDevice, textures[0].Width, textures[0].Height);
            finalTex.SetData(final);
            return finalTex;
        }
    }
}
