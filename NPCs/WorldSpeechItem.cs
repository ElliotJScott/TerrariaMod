using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.UI.Chat;

namespace StarSailor.NPCs
{
    public class WorldSpeechItem : IInteraction
    {
        SpeechBubble bubble;
        ITalkable target;
        Vector2 offset;

        public WorldSpeechItem(SpeechBubble b, ITalkable t, Vector2 o)
        {
            bubble = b;
            target = t;
            offset = o;
            Enabled = false;
        }

        public bool Enabled { get; set; }

        public void Execute()
        {
            Enabled = true;
            Update();

        }

        public void Update()
        {
            Vector2 newPos = target.GetScreenPosition() + offset;
            bubble.Update(newPos);
            if (bubble.GetDuration() <= 0) Enabled = false;
        }
        public void Draw(SpriteBatch sb)
        {
            bubble.Draw(sb);
            Vector2 bubPos = bubble.GetPos();
            target.DrawHeadSpeech(sb, new Rectangle((int)bubPos.X, (int)bubPos.Y - 80, 80, 80));
            string text = target.GetName();
            Vector2 vector = Main.fontDeathText.MeasureString(text);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text, new Vector2(bubPos.X + 90, bubPos.Y - 30), Color.White, 0f, Vector2.Zero, new Vector2(0.6f), -1f, 1.5f);

            //bubble.DrawSpike(sb, target, Math.Sign(offset.X));
        }
        public bool MatchBubble(SpeechBubble b) => bubble == b;
    }
}
