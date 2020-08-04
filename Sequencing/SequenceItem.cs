using Microsoft.Xna.Framework;
using StarSailor.NPCs;
using StarSailor.GUI;
using System;
using Terraria;
using Terraria.ModLoader;
using StarSailor.Mounts;
using Microsoft.Xna.Framework.Audio;
using Terraria.ID;
using StarSailor.Backgrounds;

namespace StarSailor.Sequencing
{
    public interface ISequenceItem : ICloneable, IDisposable
    {
        int Duration { get; }
        bool Execute();
        void Update();
    }
    public class PauseItem : ISequenceItem
    {
        public int Duration { get; } = 0;

        public PauseItem(int d)
        {
            Duration = d;
        }
        public object Clone()
        {
            return new PauseItem(Duration);
        }

        public void Dispose()
        {

        }

        public bool Execute()
        {
            return true;
        }

        public void Update()
        {

        }
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
            if (mountID == -1 && player.mount.Type != mountID)
            {
                player.mount.SetMount(mountID, player);
                player.mount.Dismount(player);
                Main.NewText(player.mount.Type);
                return true;
            }
            if (player.mount.Type != mountID)
            {
                player.mount.SetMount(mountID, player);
                return true;
            }
            return false;
        }
        public void Update() { }
        public override string ToString()
        {
            return "ChangeMountItem : target " + mountID + ", current " + player.mount.Type;
        }
        public object Clone()
        {
            return new ChangeMountItem(player, mountID);
        }

        public void Dispose() { }
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
            Update();
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
            Main.blockInput = true;
            return true;
        }

        public void Update() { }
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
            Main.blockInput = false;
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
            player.position = destination;
            //player.Teleport(destination);
            return true;
        }

        public void Update() { }
        public void Dispose() { }
    }
    public class PlayerHolderItem : ISequenceItem
    {
        bool engage;
        Player player;
        Vector2 loc;

        public int Duration => 0;

        public PlayerHolderItem(Player pl, Vector2 l)
        {
            engage = true;
            player = pl;
            loc = l;
        }
        public PlayerHolderItem(Player pl)
        {
            engage = false;
            player = pl;
            loc = Vector2.Zero;
        }


        public bool Execute()
        {
            PlayerFixer pf = player.GetModPlayer<PlayerFixer>();
            if (engage) pf.playerHolder.Engage(loc, player);
            else pf.playerHolder.Disengage();
            return true;
        }

        public void Update()
        {

        }

        public object Clone()
        {
            return new PlayerHolderItem(player, loc);
        }

        public void Dispose()
        {

        }
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

        public void Update() { }
        public void Dispose() { }
    }
    public class SoundEffectItem : ISequenceItem
    {
        public int Duration { get; } = 0;
        string soundSrc;
        public SoundEffectItem(int duration, string src)
        {
            Duration = duration;
            soundSrc = src;
        }
        public object Clone()
        {
            return new SoundEffectItem(Duration, soundSrc);
        }

        public void Dispose()
        {
        }

        public bool Execute()
        {
            try
            {
                Main.PlaySound(SoundLoader.customSoundType, -1, -1, ModContent.GetInstance<StarSailorMod>().GetSoundSlot(SoundType.Custom, soundSrc));
                return true;
            }
            catch (Exception e)
            {
                Main.NewText(e);
                return false;
            }
        }

        public void Update()
        {
        }
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
        Player player;
        public ShipTakeOffItem(Player pl)
        {
            player = pl;
        }
        public object Clone()
        {
            return new ShipTakeOffItem(player);
        }

        public void Dispose()
        {
        }

        public bool Execute()
        {
            if (player.mount.Type != ModContent.GetInstance<Rocket>().Type)
            {
                Main.NewText("The player is not in the correct mount for the current sequence! This is very concerning");
                return false;
            }
            else
            {
                ModContent.GetInstance<Rocket>().ExecuteTakeOffAnim(player);
                return true;
            }
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
        Player player;
        public ShipTravelItem(Planet or, Planet dest, Player pl)
        {
            originDest = (or, dest);
            player = pl;
        }
        public ShipTravelItem((Planet, Planet) p, Player pl)
        {
            originDest = p;
            player = pl;
        }
        public object Clone()
        {
            return new ShipTravelItem(originDest, player);
        }

        public bool Execute()
        {
            if (player.mount.Type != ModContent.GetInstance<Rocket>().Type)
            {
                Main.NewText("The player is not in the correct mount for the current sequence! This is very concerning");
                return false;
            }
            else
            {
                ModContent.GetInstance<Rocket>().ExecuteLandingAnim(player);
                return true;
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
    public class ShipLandItem : ISequenceItem
    {
        public int Duration => throw new NotImplementedException();
        Player player;
        public ShipLandItem(Player pl)
        {
            player = pl;
        }

        public object Clone()
        {
            return new ShipLandItem(player);
        }

        public void Dispose()
        {
            if (player.mount.Type != ModContent.GetInstance<Rocket>().Type)
            {
                Main.NewText("The player is not in the correct mount for the current sequence! This is very concerning");
                return;
            }
            else
            {
                ModContent.GetInstance<Rocket>().ExecuteLand(player);
            }
        }

        public bool Execute()
        {
            if (player.mount.Type != ModContent.GetInstance<Rocket>().Type)
            {
                Main.NewText("The player is not in the correct mount for the current sequence! This is very concerning");
                return false;
            }
            else
            {
                ModContent.GetInstance<Rocket>().ExecuteLandingAnim(player);
                return true;
            }
        }

        public void Update()
        {

        }
    }
    #region single use items
    public class SpaceShipCrashItem : ISequenceItem
    {
        public int Duration => 180; // This needs to be corrected when I decide how long the animation takes
        Player player;
        public SpaceShipCrashItem(Player p)
        {
            player = p;
        }
        public object Clone()
        {
            return new SpaceShipCrashItem(player);
        }

        public void Dispose()
        {
            StartingShip ship = ModContent.GetInstance<StartingShip>();
            ship.IncrementState(player);

        }

        public bool Execute()
        {
            return true;
        }

        public void Update()
        {
            StartingShip ship = ModContent.GetInstance<StartingShip>();
            ship.Update(player);
            if (ship.GetState(player) == 0)
            {
                ModContent.GetInstance<IntroBg>().UpdateOffset();
            }
        }
    }

    #endregion
}
