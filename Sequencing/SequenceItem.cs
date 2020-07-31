using Microsoft.Xna.Framework;
using StarSailor.NPCs;
using StarSailor;
using StarSailor.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sequencing
{
    public interface ISequenceItem
    {
        int Duration { get; }
        bool Execute();
        void Update();
    }
    public class ChangeMountItem : ISequenceItem
    {
        public int Duration { get => 0;}
        int mountID;
        Player player;
        public ChangeMountItem(Player pl, int id = -1)
        {
            player = pl;
            mountID = id;
        }
        public bool Execute()
        {
            if (mountID == -1) return false;
            if (player.mount.Type != mountID)
            {
                player.mount.SetMount(mountID, player);
                return true;
            }
            return false;
        }
        public void Update() { }
    }
    public class SpeechItem : ISequenceItem
    {
        SpeechBubble bubble;
        public int Duration { get => bubble.GetInitDuration();}
        ITalkable source;
        Vector2 offset;

        public bool Execute()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(bubble);
            return true;
        }

        //The actual bubble.update method gets called by the main mod from the list of all active speech bubbles currently
        //I might change that later though, we shall see
        //If I do I'll have to make all help text stuff go through sequenced events but that might be a good idea - easy way to make it play only once
        public void Update()
        {
            Vector2 newPos = source.GetScreenPosition() + offset;
            bubble.UpdatePosition(newPos);
        }
    }
    public class ImmobiliseItem : ISequenceItem
    {
        public int Duration { get => 0; }

        public bool Execute()
        {
            //Put the code in here
            return true;
        }

        public void Update() {  }
    }
    public class MobiliseItem : ISequenceItem
    {
        public int Duration { get => 0; }

        public bool Execute()
        {
            //Put the inverse code in here
            return true;
        }

        public void Update() { }
    }
}
