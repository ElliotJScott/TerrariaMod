using Microsoft.Xna.Framework;
using StarSailor.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    public class AmbientSpeechItem : IInteraction
    {
        SpeechBubble bubble;
        ITalkable target;
        Vector2 offset;

        public AmbientSpeechItem(SpeechBubble b, ITalkable t, Vector2 o)
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
    }
}
