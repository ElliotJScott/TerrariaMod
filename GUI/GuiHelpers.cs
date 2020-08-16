using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace StarSailor.GUI
{
    static class GuiHelpers
    {
        public static void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2, int thickness, Color color)
        {
            Line line = new Line(p1, p2, thickness, color);
            line.Draw(sb);
        }
        public static void DrawLine(SpriteBatch sb, Vector2 p1, Vector2 p2, int thickness, Color color, Texture2D tex)
        {
            Line line = new Line(p1, p2, thickness, color);
            line.pixel = tex;
            line.Draw(sb);
        }
        public static void DrawBox(SpriteBatch sb, Rectangle rect, Color col, float alpha)
        {
            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            Texture2D corner = sm.corner;
            int xPos = rect.X;
            int yPos = rect.Y;
            int width = rect.Width;
            int height = rect.Height;
            sb.Draw(corner, new Rectangle(xPos, yPos, corner.Width, corner.Height), col * alpha);
            sb.Draw(corner, new Rectangle(xPos + width, yPos, corner.Width, corner.Height), null, col * alpha, (float)Math.PI / 2f, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos + width, yPos + height, corner.Width, corner.Height), null, col * alpha, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos, yPos + height, corner.Width, corner.Height), null, col * alpha, 3 * (float)Math.PI / 2f, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos + corner.Width, yPos + corner.Height, width - (2 * corner.Width), height - (2 * corner.Height)), new Rectangle(corner.Width - 1, corner.Height - 1, 1, 1), col * alpha);
            for (int i = 0; i < width - (2 * corner.Width); i++)
            {
                sb.Draw(corner, new Rectangle(xPos + corner.Width + i, yPos, 1, corner.Height), new Rectangle(corner.Width - 1, 0, 1, corner.Height), col * alpha);
                sb.Draw(corner, new Rectangle(1 + xPos + corner.Width + i, yPos + height, 1, corner.Height), new Rectangle(corner.Width - 1, 0, 1, corner.Height), col * alpha, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            for (int i = 0; i < height - (2 * corner.Height); i++)
            {
                sb.Draw(corner, new Rectangle(xPos, yPos + corner.Height + i, corner.Width, 1), new Rectangle(0, corner.Height - 1, corner.Width, 1), col * alpha);
                sb.Draw(corner, new Rectangle(xPos + width, 1 + yPos + corner.Height + i, corner.Width, 1), new Rectangle(0, corner.Height - 1, corner.Width, 1), col * alpha, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            }
        }
    }
    class Line
    {
        public Texture2D pixel;
        Vector2 p1, p2; //this will be the position in the center of the line
        int length, thickness; //length and thickness of the line, or width and height of rectangle
        Rectangle rect; //where the line will be drawn
        float rotation; // rotation of the line, with axis at the center of the line
        Color color;


        //p1 and p2 are the two end points of the line
        public Line(Vector2 p1, Vector2 p2, int thickness, Color color)
        {
            pixel = ModContent.GetInstance<StarSailorMod>().pixel;
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
