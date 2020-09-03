using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace StarSailor.NPCs
{
    class SpeechInteraction : IInteraction
    {
        ITalkable talker;
        List<WorldSpeechItem> items; 

        public SpeechInteraction(ITalkable src, List<WorldSpeechItem> i)
        {
            talker = src;
            items = i;
            Enabled = false;
        }
        public SpeechInteraction(ITalkable src, params WorldSpeechItem[] i)
        {
            talker = src;
            items = i.ToList();
            Enabled = false;
        }
        public bool Enabled { get; set; }

        public void Execute()
        {
            Enabled = true;
            if (items.Count > 0)
            {
                items[0].Execute();
            }
            else Enabled = false;
        }

        public void Update()
        {
            if (items.Count > 0)
            {
                items[0].Update();
                if (!items[0].Enabled)
                {
                    ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(items[0]);
                    items.RemoveAt(0);
                    Execute();
                }
            }
            else Enabled = false;
        }
    }
}
