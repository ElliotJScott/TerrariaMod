using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using teo.Tiles;

namespace teo.GUI
{
    public class GravDisplay
    {
        public static bool dispGui = false;
        public static void Draw(SpriteBatch sb)
        {
            GravitySource src = ModContent.GetInstance<GravitySource>();
            if (Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity)
                src.DrawGUIComps(sb);
        }
        public static void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2, int thickness, Color color)
        {
            Line line = new Line(p1, p2, thickness, color);
            line.Draw(sb);
        }
    }
    class Line
    {
        Texture2D pixel;
        Vector2 p1, p2; //this will be the position in the center of the line
        int length, thickness; //length and thickness of the line, or width and height of rectangle
        Rectangle rect; //where the line will be drawn
        float rotation; // rotation of the line, with axis at the center of the line
        Color color;


        //p1 and p2 are the two end points of the line
        public Line(Vector2 p1, Vector2 p2, int thickness, Color color)
        {
            pixel = ModContent.GetTexture("GUI/Pixel.png");
            this.p1 = p1;
            this.p2 = p2;
            this.thickness = thickness;
            this.color = color;
            length = (int)Vector2.Distance(p1, p2);
            rotation = getRotation(p1.X, p1.Y, p2.X, p2.Y);
            rect = new Rectangle((int)p1.X, (int)p1.Y, length, thickness);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, rect, null, color, rotation, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

        //this returns the angle between two points in radians 
        private float getRotation(float x, float y, float x2, float y2)
        {
            float adj = x - x2;
            float opp = y - y2;
            float tan = opp / adj;
            float res = (float)Math.Atan2(opp, adj);
            res = (res - (float)Math.PI) % (2f * (float)Math.PI);
            if (res < 0) { res += (2f * (float)Math.PI); }
            return res;
        }

    }
}
