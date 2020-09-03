using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sequencing
{
    class SequenceQueue : ICloneable
    {
        readonly Sequence sequence;
        private List<SequenceQueueElement> elements = new List<SequenceQueueElement>();
        int ticker = 0;
        public SequenceQueue(Sequence seq)
        {
            sequence = seq;
        }
        public Sequence GetSequence() => sequence;
        public void Append(SequenceQueueElement el)
        {
            elements.Add(el);
        }
        public void Append(ISequenceItem item)
        {
            elements.Add(new SequenceQueueElement(item));
        }
        public void AddInParallel(ISequenceItem item)
        {
            ticker = 0;
            if (elements.Count == 0)
            {
                Append(item);
                return;
            }
            else if (elements[0].GetDuration().HasValue)
            {
                if (elements[0].GetDuration() == 0)
                {
                    elements.Insert(1, new SequenceQueueElement(item));
                    return;
                }
            }
            elements[0].Add(item);
            
        }
        public void Execute()
        {
            ModContent.GetInstance<StarSailorMod>().sequence = this;
            elements[0].Execute();
        }
        public object Clone()
        {
            SequenceQueue queue = new SequenceQueue(sequence);
            foreach (SequenceQueueElement s in elements) queue.Append((SequenceQueueElement)s.Clone());
            return queue;
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
                        return RemoveStart();
                    }
                    else return false;
                }
                else
                {
                    return RemoveStart();
                }
            }
            else return true;
            
        }
        public bool RemoveStart()
        {
            //Main.NewText("Removing item at start");
            elements[0].Dispose();
            ticker = 0;
            elements.RemoveAt(0);
            Main.NewText(elements.Count);
            if (elements.Count > 0)
            {
                elements[0].Execute();
                return false;
            }
            else return true;
        }
        public bool GetActive() => elements.Count > 0;
    }
    class SequenceQueueElement : ICloneable, IDisposable
    {
        List<ISequenceItem> items = new List<ISequenceItem>();
        public SequenceQueueElement(ISequenceItem item)
        {
            items.Clear();
            items.Add(item);
        }
        public SequenceQueueElement(List<ISequenceItem> it)
        {
            items = it;
        }
        public void Add(ISequenceItem item)
        {
            items.Add(item);
            bool b = item.Execute();
            if (!b) Main.NewText("Something went wrong in a cutscene: " + item.ToString(), Color.Red);

        }
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
        public void Dispose()
        {
            foreach (ISequenceItem i in items) i.Dispose();
        }
        public object Clone()
        {
            List<ISequenceItem> newList = new List<ISequenceItem>();
            foreach (ISequenceItem s in items) newList.Add((ISequenceItem)s.Clone());
            return new SequenceQueueElement(newList);
        }
    }

}
