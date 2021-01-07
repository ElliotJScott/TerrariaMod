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
using StarSailor.Dimensions;

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
    public class StopAIItem : ISequenceItem
    {
        public int Duration => 0;
        Character target;
        public StopAIItem(Character tr)
        {
            target = tr;
        }
        public object Clone()
        {
            return new StopAIItem(target);
        }
        public void Dispose()
        {

        }

        public bool Execute()
        {
            
            target.doMotion = false;
            return true;
        }

        public void Update()
        {

        }
    }
    public class StartAIItem : ISequenceItem
    {
        public int Duration => 0;
        Character target;
        public StartAIItem(Character tr)
        {
            target = tr;
        }
        public object Clone()
        {
            return new StartAIItem(target);
        }
        public void Dispose()
        {

        }

        public bool Execute()
        {
            
            target.doMotion = true;
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
            //ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(bubble);
            
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
            //ModContent.GetInstance<StarSailorMod>().speechBubbles.Remove(bubble);
        }
    }
    public class SpeechItem : ISequenceItem
    {
        public SpeechBubble bubble;
        public virtual int Duration => bubble.GetInitDuration();
        public ITalkable source;
        Vector2 offset;
        public SpeechItem(SpeechBubble b, ITalkable src, Vector2 o)
        {
            bubble = b;
            source = src;
            offset = o;
        }
        public SpeechItem(string s, ITalkable src, Vector2 offs, int w = 400, int d = 300)
        {
            Vector2 loc = src.GetScreenPosition();
            bubble = new SpeechBubble(s, (int)loc.X, (int)loc.Y, w, d);
            offset = offs;
            source = src;
        }
        public bool Execute()
        {
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(new WorldSpeechItem(bubble, source, offset));
            //Update();
            return true;
        }

        public virtual void Update()
        {
            Vector2 newPos = source.GetScreenPosition() + offset;
            bubble.Update(newPos);
        }

        public object Clone()
        {
            SpeechBubble sb = new SpeechBubble(bubble.GetText(), (int)bubble.GetPos().X, (int)bubble.GetPos().Y, bubble.GetWidth(), bubble.GetInitDuration());
            return new SpeechItem(sb, source, offset);
        }
        public virtual void Dispose()
        {
            ModContent.GetInstance<StarSailorMod>().RemoveSpeechBubble(bubble);
        }
    }
    public class CancellableSpeechItem : SpeechItem
    {
        public override int Duration => duration;
        int duration;
        public CancellableSpeechItem(string s, ITalkable src, Vector2 o, int w = 400, int d = 300) : base(s, src, o, w, d)
        {
            duration = bubble.GetInitDuration();
        }
        public override void Update()
        {
            if (Main.playerInventory || Main.ingameOptionsWindow || !source.WithinDistance()) Dispose();
            base.Update();
        }
        public override void Dispose()
        {
            base.Dispose();
            duration = 0;
        }
    }
    public class SelectionSpeechItem : CancellableSpeechItem
    {
        public SelectionSpeechItem(string s, ITalkable src, Vector2 o, int w, params SpeechOption[] options) : base(s, src, o, w, 99999999)
        {
            Vector2 loc = src.GetScreenPosition();
            bubble = new InteractableSpeechBubble(s, (int)loc.X, (int)loc.Y, w, 99999999, options); 
        }
        public override void Update()
        {
            base.Update();
            Main.LocalPlayer.AddBuff(BuffID.Cursed, 10);
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
            
            ModContent.GetInstance<StarSailorMod>().inputEnabled = false;
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
            ModContent.GetInstance<StarSailorMod>().inputEnabled = true;
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
        public int Duration => duration;
        int duration = int.MaxValue;
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
            ModContent.GetInstance<Rocket>().DisposeTakeOff(player);
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
            if (ModContent.GetInstance<Rocket>().UpdateTakeOffAnimation(player)) duration = 0;
        }
    }
    public class ShipTravelItem : ISequenceItem
    {
        public int Duration { get; private set; } = int.MaxValue;

        public (Dimensions.Dimensions, Dimensions.Dimensions) originDest;
        Player player;
        public ShipTravelItem(Dimensions.Dimensions or, Dimensions.Dimensions dest, Player pl)
        {
            originDest = (or, dest);
            player = pl;
        }
        public ShipTravelItem((Dimensions.Dimensions, Dimensions.Dimensions) p, Player pl)
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
                ModContent.GetInstance<Rocket>().ExecuteSpaceAnim(player);
                ModContent.GetInstance<InSpaceBg>().SetDefaults(originDest.Item1, originDest.Item2);
                return true;
            }
        }

        public void Update()
        {
            ModContent.GetInstance<Rocket>().UpdateSpaceAnim(player);
            bool f = ModContent.GetInstance<InSpaceBg>().UpdateBg();
            if (f) Duration = 0;
        }

        public void Dispose()
        {
        }
    }
    public class ShipLandItem : ISequenceItem
    {
        public int Duration => duration;
        int duration = int.MaxValue;
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
            if (ModContent.GetInstance<Rocket>().UpdateLandingAnim(player)) duration = 0;
        }
    }
    public class ClearAmbientTextItem : ISequenceItem
    {
        public int Duration => 0;

        public object Clone()
        {
            return new ClearAmbientTextItem();
        }

        public void Dispose()
        {
        }

        public bool Execute()
        {
            
            ModContent.GetInstance<StarSailorMod>().speechBubbles.Clear();
            return true;
        }

        public void Update()
        {
        }
    }
    public class ChangeDimensionItem : ISequenceItem
    {
        public int Duration => 0;
        Dimensions.Dimensions destination;
        public ChangeDimensionItem(Dimensions.Dimensions dim)
        {
            destination = dim;
        }
        public object Clone()
        {
            return new ChangeDimensionItem(destination);
        }

        public void Dispose()
        {
            
        }

        public bool Execute()
        {
            ModContent.GetInstance<DimensionManager>().SwitchDimension(destination);
            return true;
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
    public class AddPlatformItem : ISequenceItem
    {
        public int Duration => 0;
        Dimensions.Dimensions destination;
        Vector2 position;

        public AddPlatformItem(Vector2 p, Dimensions.Dimensions dim)
        {
            position = p;
            destination = dim;
        }
        public bool Execute()
        {
            Main.NewText("Adding landing platform at " + position + " in " + destination);
            Tile[,] tiles = DimensionBuilder.GetDimensionSpawn(destination == Dimensions.Dimensions.Asteroid);
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Main.tile[(int)position.X + i - LaunchPoint.width, (int)position.Y + j - 16] = new Tile(tiles[i, j]);
                }
            }
            return true;
        }

        public void Update()
        {
            
        }

        public object Clone()
        {
            return new AddPlatformItem(position, destination);
        }

        public void Dispose()
        {
            
        }
    }


    public class ShopSeqItem : ISequenceItem
    {
        public int Duration => duration;

        private int duration = int.MaxValue;
        ShopItem[] items;
        ITalkable source;

        public ShopSeqItem(ITalkable src, params ShopItem[] i)
        {
            items = i;
            source = src;
        }
        public object Clone()
        {
            return new ShopSeqItem(source, items);
        }

        public void Dispose()
        {
            ModContent.GetInstance<StarSailorMod>().currentShop = null;
            duration = 0;
        }

        public bool Execute()
        {
            
            ModContent.GetInstance<StarSailorMod>().currentShop = new Shop(items);
            return true;
        }

        public void Update()
        {
            if (Main.playerInventory || Main.ingameOptionsWindow || !source.WithinDistance()) Dispose();
            //Updating is done in the mod class
        }
    }
    #region single use items
    public class SpaceShipCrashItem : ISequenceItem
    {
        public int Duration 
            {
            get
            {
                StartingShip ship = ModContent.GetInstance<StartingShip>();
                switch (ship.GetState(player))
                {
                    case 0:
                    case 1:
                        return 180;
                    case 2:
                    default:
                        return StartingShip.crashTime;
                }
            }      
        } 
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
            if (ship.GetState(player) == 2)
            {
                Main.PlaySound(SoundID.Item62, player.position);
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(new Vector2(player.position.X + (float)(player.width / 2) - 24f, player.position.Y + (float)(player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
            ship.IncrementState(player);

        }

        public bool Execute()
        {
            StartingShip ship = ModContent.GetInstance<StartingShip>();
            if (ship.GetState(player) == 2)
            {
                Main.PlaySound(SoundID.Item46, player.position + new Vector2(6 * StartingShip.crashTime, -StartingShip.crashTime - (Main.screenHeight / 2)));
            }
            return true;
        }

        public void Update()
        {
            StartingShip ship = ModContent.GetInstance<StartingShip>();
            ship.Update(player);
            if (ship.GetState(player) == 0)
            {
                //ModContent.GetInstance<InSpaceBg>().UpdateOffset();
            }
        }
    }

    #endregion
}
