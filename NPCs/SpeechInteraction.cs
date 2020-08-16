using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    class SpeechInteraction : IInteraction
    {
        ITalkable talker;
        List<AmbientSpeechItem> items; 

        public SpeechInteraction(ITalkable src, List<AmbientSpeechItem> i)
        {
            talker = src;
            items = i;
            Enabled = false;
        }
        public SpeechInteraction(ITalkable src, params AmbientSpeechItem[] i)
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
                    items.RemoveAt(0);
                    Execute();
                }
            }
            else Enabled = false;
        }
    }
}
