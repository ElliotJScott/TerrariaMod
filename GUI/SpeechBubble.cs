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
using StarSailor.NPCs;
using StarSailor.Sequencing;
using Microsoft.Xna.Framework.Input;

namespace StarSailor.GUI
{
    public class SpeechBubble : IDisposable
    {
        Texture2D corner;
        Texture2D spike;
        public int xPos;
        public int yPos;
        int width;
        int initDuration;
        int height;
        string text;
        public const float scale = 0.37f;
        const int numCharsPerFrame = 3;
        public int totChars = 0;
        int duration;
        public string[] lines;
        public Vector2 dims;
        Color border = new Color(12, 11, 36, 221);
        Color intColor = new Color(58, 56, 136, 221);
        public DynamicSpriteFont font = Main.fontDeathText;
        public int GetDuration() => duration;
        public static void HelpText(string text)
        {

            //ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(new SpeechBubble(text, Main.screenWidth / 4, Main.screenWidth / 10, Main.screenWidth / 2, 600));
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
            int measWidth = (int)(Main.fontDeathText.MeasureString(lines[0]).X * scale);
            width = Math.Min(measWidth + 30, width);
            height = 2 * corner.Height;
            foreach (string l in lines)
            {
                height += (int)(scale * font.MeasureString(l).Y);
            }
        }
        public string GetText() => text;
        public Vector2 GetPos() => new Vector2(xPos, yPos);
        public int GetInitDuration() => initDuration;
        public int GetWidth() => width;

        public virtual void Update()
        {
            totChars += numCharsPerFrame;
            duration--;
            if (duration <= 0)
            {
                Dispose();
            }
        }
        public void Update(Vector2 newPos)
        {
            xPos = (int)newPos.X;
            yPos = (int)newPos.Y;
            Update();
        }
        public virtual void Draw(SpriteBatch sb)
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
        public void DrawSpike(SpriteBatch sb, ITalkable target, int side)
        {
            int size = 20;
            Vector2 dest = target.GetScreenPosition() + new Vector2(30, 30);
            if (side >= 0)
            {
                for (int i = 1; i < size; i++)
                {
                    GuiHelpers.DrawLine(sb, new Vector2(xPos + corner.Width + i, yPos + height), dest, 1, intColor);
                }
                GuiHelpers.DrawLine(sb, new Vector2(xPos + corner.Width, yPos + height), dest, 2, border);
                GuiHelpers.DrawLine(sb, new Vector2(xPos + corner.Width + size, yPos + height), dest, 2, border);
            }
            else
            {
                for (int i = 1; i < size; i++)
                {
                    GuiHelpers.DrawLine(sb, new Vector2(xPos + width - (corner.Width + i), yPos + height), dest, 1, intColor);
                }
                GuiHelpers.DrawLine(sb, new Vector2(xPos + width - corner.Width, yPos + height), dest, 2, border);
                GuiHelpers.DrawLine(sb, new Vector2(xPos + width - (corner.Width + size), yPos + height), dest, 2, border);
            }
        }
        public void Dispose()
        {
            //ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(this);
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

            return buffer.ToArray();
        }
        public int GetNumChars()
        {
            int i = 0;
            foreach (string s in lines) i += s.Length;
            return i;
        }
    }
    public class InteractableSpeechBubble : SpeechBubble
    {
        SpeechOption[] options;
        public InteractableSpeechBubble(string t, int x, int y, int w, int d, params SpeechOption[] o) : base(t, x, y, w, d)
        {
            options = o;
        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            if (totChars > GetNumChars())
            {
                int numLines = 0;
                float height = 3 + (font.MeasureString(lines[0]).Y * scale);
                Vector2 init = new Vector2(xPos, yPos + dims.Y);
                for (int i = 0; i < options.Length; i++)
                {
                    SpeechOption current = options[i];
                    current.Draw(sb, init + new Vector2(40, (numLines * height) + i));
                    numLines += current.lines.Length;
                }
            }
        }
        public override void Update()
        {
            base.Update();
            if (totChars > GetNumChars())
            {
                int numLines = 0;
                float height = 3 + (font.MeasureString(lines[0]).Y * scale);
                Vector2 init = new Vector2(xPos, yPos + dims.Y);
                for (int i = 0; i < options.Length; i++)
                {
                    SpeechOption current = options[i];
                    current.Update(init + new Vector2(40, (numLines * height) + (i * 5)));
                    numLines += current.lines.Length;
                }
            }
        }

    }
    public class SpeechOption
    {
        public string text;
        public string[] lines;
        SequenceQueue result;
        const float scale = 0.37f;
        float hoverScale = scale;
        DynamicSpriteFont font = Main.fontDeathText;
        public SpeechOption(string t, SequenceQueue r)
        {
            text = t;
            result = r;
            lines = GetLines(text, new List<string>(), 600);
        }
        public void Update(Vector2 position)
        {
            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            float height = 0f;
            float maxWidth = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                Vector2 mea = font.MeasureString(lines[i]) * hoverScale;
                maxWidth = Math.Max(mea.X, maxWidth);
                height += mea.Y + 3;
            }
            Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
            Rectangle boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)maxWidth, (int)height);
            if (mouseRect.Intersects(boundingBox))
            {
                HoverUpdate(true);
                if (sm.newMouseState.LeftButton == ButtonState.Pressed && sm.oldMouseState.LeftButton == ButtonState.Released)
                {
                    
                    sm.speechBubbles.Clear();
                    result.Execute();
                }
            }
            else HoverUpdate(false);
        }
        public string[] GetLines(string input, List<string> buffer, int width)
        {
            Texture2D corner = ModContent.GetInstance<StarSailorMod>().GetTexture("GUI/spBubble");
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
                        return GetLines(remainingInput, buffer, width);
                    }
                    else
                    {
                        line = tempLine;
                    }
                }

            }
            buffer.Add(input);

            return buffer.ToArray();
        }
        void HoverUpdate(bool hovered)
        {
            if (hovered) hoverScale = (float)Math.Min(0.45f, hoverScale + 0.2);
            else hoverScale = (float)Math.Max(scale, hoverScale - 0.2);
        }
        public void Draw(SpriteBatch sb, Vector2 position)
        {
            for (int i = 0; i < lines.Length; i++) {
                Vector2 mea = font.MeasureString(lines[i]) * scale;
                ChatManager.DrawColorCodedStringWithShadow(sb, font, lines[i], position, Color.White, 0f, mea / (2f), new Vector2(hoverScale), -1f, 1.5f);
            }
        }
    }
}
