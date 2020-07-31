using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using ReLogic.Graphics;

namespace StarSailor.GUI
{
    class SpeechBubble
    {
        Texture2D corner;
        Texture2D spike;
        int xPos;
        int yPos;
        int width;
        int initDuration;
        int height;
        string text;
        const float scale = 0.37f;
        const int numCharsPerFrame = 3;
        int totChars = 0;
        int duration;
        string[] lines;
        Vector2 dims;
        DynamicSpriteFont font = Main.fontDeathText;
        public static void HelpText(string text)
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(new SpeechBubble(text, Main.screenWidth / 4, Main.screenWidth / 10, Main.screenWidth / 2, 600));
        }
        public SpeechBubble(string t, int x, int y, int w, int d)
        {
            text = t;
            corner = ModContent.GetInstance<StarSailorMod>().GetTexture("GUI/spBubble");
            spike = ModContent.GetInstance<StarSailorMod>().GetTexture("GUI/spSpike");
            xPos = x;
            yPos = y;
            duration = d;
            initDuration = d;
            width = w;
            dims = Main.fontDeathText.MeasureString(t);
            lines = GetLines(t, new List<string>());
            height = 2 * corner.Height;
            foreach (string l in lines)
            {
                height += (int)(scale * font.MeasureString(l).Y);
            }
        }

        public int GetInitDuration() => initDuration;

        public void Update()
        {
            totChars += numCharsPerFrame;
            duration--;
            if (duration <= 0)
            {
                Dispose();
            }
        }
        public void UpdatePosition(Vector2 newPos)
        {
            xPos = (int)newPos.X;
            yPos = (int)newPos.Y;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(corner, new Rectangle(xPos, yPos, corner.Width, corner.Height), Color.White * 0.8f);
            sb.Draw(corner, new Rectangle(xPos + width, yPos, corner.Width, corner.Height), null, Color.White * 0.8f, (float)Math.PI / 2f, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos + width, yPos + height, corner.Width, corner.Height), null, Color.White * 0.8f, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos, yPos + height, corner.Width, corner.Height), null, Color.White * 0.8f, 3 * (float)Math.PI / 2f, new Vector2(0, 0), SpriteEffects.None, 0);
            sb.Draw(corner, new Rectangle(xPos + corner.Width, yPos + corner.Height, width - (2 * corner.Width), height - (2 * corner.Height)), new Rectangle(corner.Width - 1, corner.Height - 1, 1, 1), Color.White * 0.8f);
            for (int i = 0; i < width - (2 * corner.Width); i++)
            {
                sb.Draw(corner, new Rectangle(xPos + corner.Width + i, yPos, 1, corner.Height), new Rectangle(corner.Width - 1, 0, 1, corner.Height), Color.White * 0.8f);
                sb.Draw(corner, new Rectangle(1 + xPos + corner.Width + i, yPos + height, 1, corner.Height), new Rectangle(corner.Width - 1, 0, 1, corner.Height), Color.White * 0.8f, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            }

            for (int i = 0; i < height - (2 * corner.Height); i++)
            {
                sb.Draw(corner, new Rectangle(xPos, yPos + corner.Height + i, corner.Width, 1), new Rectangle(0, corner.Height - 1, corner.Width, 1), Color.White * 0.8f);
                sb.Draw(corner, new Rectangle(xPos + width, 1 + yPos + corner.Height + i, corner.Width, 1), new Rectangle(0, corner.Height - 1, corner.Width, 1), Color.White * 0.8f, (float)Math.PI, new Vector2(0, 0), SpriteEffects.None, 0);
            }
            int charsLeft = totChars;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int numChars = line.Length;
                if (numChars < charsLeft)
                {
                    Vector2 mea = font.MeasureString(lines[i]) * scale;
                    ChatManager.DrawColorCodedStringWithShadow(sb, font, line, new Vector2((scale * mea.X * 0.5f) + xPos + corner.Width, (scale * mea.Y * 0.5f) + yPos + corner.Height + ((mea.Y) * i)), Color.White, 0f, mea / (2f), new Vector2(scale), -1f, 1.5f);
                    charsLeft -= numChars;
                }
                else
                {
                    line = line.Substring(0, charsLeft);
                    Vector2 mea = font.MeasureString(lines[i]) * scale;
                    ChatManager.DrawColorCodedStringWithShadow(sb, font, line, new Vector2((scale * mea.X * 0.5f) + xPos + corner.Width, (scale * mea.Y * 0.5f) + yPos + corner.Height + ((mea.Y) * i)), Color.White, 0f, mea / (2f), new Vector2(scale), -1f, 1.5f);
                    charsLeft = 0;
                    break;
                }
            }
        }
        public void Dispose()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(this);
        }
        public string[] GetLines(string input, List<string> buffer)
        {
            Vector2 measure = font.MeasureString(input);
            if (measure.X > width - (2 * corner.Width))
            {
                string[] splitto = input.Split(' ');
                string line = "";
                for (int i = 0; i < splitto.Length; i++)
                {
                    string tempLine = (line + " " + splitto[i]).Trim();
                    Vector2 m = font.MeasureString(tempLine) * scale;
                    if (m.X > width - (2 * corner.Width))
                    {
                        buffer.Add(line);
                        string remainingInput = "";
                        for (int j = i; j < splitto.Length; j++)
                        {
                            remainingInput += " " + splitto[j];
                            remainingInput = remainingInput.Trim();
                        }
                        return GetLines(remainingInput, buffer);
                    }
                    else
                    {
                        line = tempLine;
                    }
                }

            }
            buffer.Add(input);
            Main.NewText(buffer.Count);
            return buffer.ToArray();
        }
    }
}
