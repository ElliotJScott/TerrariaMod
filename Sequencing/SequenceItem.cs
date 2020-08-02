using Microsoft.Xna.Framework;
using StarSailor.NPCs;
using StarSailor.GUI;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sequencing
{
    public interface ISequenceItem : ICloneable, IDisposable
    {
        int Duration { get; }
        bool Execute();
        void Update();
    }

    public class ChangeMountItem : ISequenceItem
    {
        public int Duration => 0;
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

        public object Clone()
        {
            return new ChangeMountItem(player, mountID);
        }

        public void Dispose(){ }
    }
    public class HelpTextItem : ISequenceItem
    {
        SpeechBubble bubble;
        public int Duration => bubble.GetInitDuration();

        public HelpTextItem(SpeechBubble b)
        {
            bubble = b;
        }
        public bool Execute()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(bubble);
            return true;
        }

        public void Update()
        {
            bubble.Update();
        }

        public object Clone()
        {
            SpeechBubble sb = new SpeechBubble(bubble.GetText(), (int)bubble.GetPos().X, (int)bubble.GetPos().Y, bubble.GetWidth(), bubble.GetInitDuration());
            return new HelpTextItem(sb);
        }

        public void Dispose()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(bubble);
        }
    }
    public class SpeechItem : ISequenceItem
    {
        SpeechBubble bubble;
        public int Duration => bubble.GetInitDuration();
        ITalkable source;
        Vector2 offset;
        public SpeechItem(SpeechBubble b, ITalkable src, Vector2 o)
        {
            bubble = b;
            source = src;
            offset = o;
        }
        public bool Execute()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(bubble);
            return true;
        }

        public void Update()
        {
            Vector2 newPos = source.GetScreenPosition() + offset;
            bubble.Update(newPos);
        }

        public object Clone()
        {
            SpeechBubble sb = new SpeechBubble(bubble.GetText(), (int)bubble.GetPos().X, (int)bubble.GetPos().Y, bubble.GetWidth(), bubble.GetInitDuration());
            return new SpeechItem(sb, source, offset);
        }
        public void Dispose()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(bubble);
        }
    }

    public class ImmobiliseItem : ISequenceItem
    {
        public int Duration => 0; 

        public object Clone()
        {
            return new ImmobiliseItem();
        }

        public bool Execute()
        {
            //Put the code in here
            return true;
        }

        public void Update() {  }
        public void Dispose() { }
    }

    public class MobiliseItem : ISequenceItem
    {
        public int Duration => 0; 

        public object Clone()
        {
            return new MobiliseItem();
        }

        public bool Execute()
        {
            //Put the inverse code in here
            return true;
        }

        public void Update() { }
        public void Dispose() { }
    }

    public class TeleportItem : ISequenceItem
    {
        public int Duration => 5;
        public Vector2 destination;
        public Player player;
        public TeleportItem(Vector2 d, Player p)
        {
            player = p;
            destination = d;
        }
        public object Clone()
        {
            return new TeleportItem(destination, player);
        }

        public bool Execute()
        {
            player.Teleport(destination);
            return true;
        }

        public void Update() { }
        public void Dispose() { }
    }
    public class SpawnChangeItem : ISequenceItem
    {
        public int Duration => 0;
        Vector2 loc;
        Player player;
        public SpawnChangeItem(Vector2 v, Player pl)
        {
            loc = v;
            player = pl;
        }
        public object Clone()
        {
            return new SpawnChangeItem(loc, player);
        }

        public bool Execute()
        {
            player.ChangeSpawn((int)loc.X, (int)loc.Y);
            return true;
        }

        public void Update() {}
        public void Dispose() { }
    }
    public class MusicChangeItem : ISequenceItem
    {
        public int Duration => 0;

        public int musicID;
        public MusicChangeItem(int id)
        {
            musicID = id;
        }
        public object Clone()
        {
            return new MusicChangeItem(musicID);
        }

        public bool Execute()
        {
            if (Main.curMusic != musicID)
            {
                Main.curMusic = musicID;
                return true;
            }
            else return false;
        }

        public void Update() { }
        public void Dispose() { }
    }
    public class ShipTakeOffItem : ISequenceItem
    {
        public int Duration => throw new NotImplementedException();

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Execute()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    public class ShipTravelItem : ISequenceItem
    {
        public int Duration => throw new NotImplementedException();
        public (Planet, Planet) originDest;
        public ShipTravelItem(Planet or, Planet dest)
        {
            originDest = (or, dest);
        }
        public ShipTravelItem((Planet, Planet) p)
        {
            originDest = p;
        }
        public object Clone()
        {
            return new ShipTravelItem(originDest);
        }

        public bool Execute()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
    public class ShipLandItem : ISequenceItem
    {
        public int Duration => throw new NotImplementedException();

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Execute()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    #region single use items
    public class SpaceShipCrash : ISequenceItem
    {
        public int Duration => 100; // This needs to be corrected when I decide how long the animation takes

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Execute()
        {
#warning implement this
            return true;
        }

        public void Update()
        {
#warning implement this
        }
    }
    public class PlanetShipCrash : ISequenceItem
    {
        public int Duration => 100; //Fix this too

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Execute()
        {
            return true;
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
