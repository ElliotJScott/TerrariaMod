using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace StarSailor.Sequencing
{
    class SequenceQueue
    {
        private List<SequenceQueueElement> elements = new List<SequenceQueueElement>();
        int ticker = 0;
        public void Append(SequenceQueueElement el)
        {
            elements.Add(el);
        }
        public bool Update()
        {
            ticker++;
            if (elements.Count > 0)
            {
                elements[0].Update();
                int? dur = elements[0].GetDuration();
                if (dur.HasValue)
                {
                    int t = dur.Value;
                    if (ticker >= t)
                    {
                        elements.RemoveAt(0);
                        if (elements.Count > 0)
                        {
                            elements[0].Execute();
                            return false;
                        }
                        else return true;
                    }
                    else return false;
                }
                else
                {
                    elements.RemoveAt(0);
                    if (elements.Count > 0)
                    {
                        elements[0].Execute();
                        return false;
                    }
                    else return true;
                }
            }
            else return true;
        }
    }
    class SequenceQueueElement
    {
        List<ISequenceItem> items = new List<ISequenceItem>();
        public int? GetDuration()
        {
            int? i = null;
            foreach (ISequenceItem s in items)
            {
                if (!i.HasValue) i = s.Duration;
                else if (i.Value < s.Duration) i = s.Duration;
            }
            return i;
        }
        public void Update()
        {
            foreach (ISequenceItem s in items)
                s.Update();       
        }
        public void Execute()
        {
            foreach (ISequenceItem s in items)
            {
                bool b = s.Execute();
                if (!b) Main.NewText("Something went wrong in a cutscene: " + s.ToString(), Color.Red);
            }
        }
    }

}
