using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
        public bool MatchBubble(SpeechBubble b) => bubble == b;
    }
}
