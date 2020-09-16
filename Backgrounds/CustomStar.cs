using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Backgrounds
{
    enum Distribution
    {
        Flat,
        Atan,
    }
    class CustomStar
    {
        public Vector2 position;
        public Color color;
        public int size;
        public float scale;
        public float alpha;
        float initAlpha;
        float alphaDiff;
        public float rot;
        public int period;
        int ticker;
        public static Color[] colors = { Color.White, Color.White, Color.LightBlue, Color.LightBlue, Color.Orange, Color.White, Color.White, Color.White};
        public const int minSize = 3;
        public const int maxSize = 15;

        private CustomStar(Vector2 pos, Color c, int s, int p, float a = 0.6f)
        {
            position = pos;
            color = c;
            size = s;
            scale = 1f;
            alpha = a;
            initAlpha = a;
            alphaDiff = 0;
            rot = 0;
            period = p;
        }
        public static CustomStar CreateNewStar(int yMax, Distribution dist, (Vector2, int)[] forbiddenRegions)
        {
            Color c = colors[Main.rand.Next(colors.Length)];
            Vector2 pos = GeneratePosition(yMax, dist);
            if (IsForbidden(pos, forbiddenRegions)) return CreateNewStar(yMax, dist, forbiddenRegions);
            int s = GenSize();
            int p = Main.rand.Next(180, 300);
            float a = GenAlpha(dist, pos, yMax);
            return new CustomStar(pos, c, s, p, a);
        }
        static bool IsForbidden(Vector2 position, (Vector2, int)[] forbiddenRegions)
        {
            foreach ((Vector2, int) t in forbiddenRegions)
            {
                if (Vector2.Distance(t.Item1, position) <= t.Item2) return true;
            }
            return false;
        }
        public static int GenSize()
        {
            int s = Main.rand.Next(minSize, maxSize);
            float frac = ((float)(s - minSize)) / (maxSize - minSize);
            if (Main.rand.NextFloat() > frac) return s;
            else return GenSize();
        }
        public static float GenAlpha(Distribution dist, Vector2 pos, int yMax)
        {
            switch (dist)
            {
                case Distribution.Atan:
                    return 1f - ((pos.Y / yMax));
                case Distribution.Flat:
                default:
                    float fl = Main.rand.NextFloat() / 5;
                    return 0.8f + fl;
            }
        }
        public static Vector2 GeneratePosition(int yMax, Distribution dist)
        {
            int y = Main.rand.Next(yMax);
            float d;
            switch (dist)
            {
                case Distribution.Atan:
                    d = GetDistFunc(y, yMax);
                    break;
                case Distribution.Flat:
                default:
                    d = 1;
                    break;
                    
            }
           
            float trial = Main.rand.NextFloat();
            if (d > trial)
                return new Vector2(Main.rand.Next(-10, Main.screenWidth), y);
            else return GeneratePosition(yMax, dist);
        }
        public static float GetDistFunc(int y, int yMax)
        {
            float distConst = 3.2f;
            float yCor = ((float)y) / yMax;
            return -0.5f * (float)Math.Atan(distConst * (yCor - 1));
        }
        public void Update()
        {
            if (++ticker == period) ticker = 0;
            scale = 1f + (0.2f * (float)Math.Sin(2 * Math.PI * ((double)ticker / period)));
            rot = 0.1f * (float)Math.Sin(2 * Math.PI * ((double)(ticker + (period / 7)) / period));
            alphaDiff = 0.1f * (float)Math.Sin(2 * Math.PI * ((double)(ticker + (period / 13)) / period));
            alpha = initAlpha + alphaDiff; 
        }
        public void Draw(SpriteBatch sb)
        {

            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            sb.Draw(sm.smallStar, new Rectangle((int)position.X, (int)position.Y, (int)(size * scale), (int)(size * scale)), null, color * alpha, rot, new Vector2(size * scale/2f, size * scale / 2f), SpriteEffects.None, 0);

        }
    }
}
