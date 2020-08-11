using Microsoft.Xna.Framework;
using StarSailor.GUI;
using StarSailor.Mounts;
using StarSailor.Sounds;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarSailor.Sequencing
{
    static class SequenceBuilder
    {
        static List<SequenceQueue> sequences = new List<SequenceQueue>();
        static readonly Vector2 spaceLocation = 16 * new Vector2(4556, 249);
        public static void InitialiseSequences(Player player)
        {
            sequences.Clear();
            sequences.Add(ConstructIntroSequence(player));
        }

        //If origin == destination don't do the space sequence
        //Change spawn, takeoff, teleport, inflight, teleport, landing, changemount, mobilise
        static SequenceQueue ConstructSpaceSequence(Planet origin, Planet destination, Player player, Vector2 destLoc)
        {
            SequenceQueue queue = new SequenceQueue(Sequence.InSpace);
            queue.Append(new SpawnChangeItem(destLoc, player));
            queue.Append(new ShipTakeOffItem(player));
            if (origin != destination)
            {

                queue.Append(new TeleportItem(spaceLocation, player));
                queue.Append(new ShipTravelItem((origin, destination), player));

            }

            queue.Append(new TeleportItem(destLoc, player));
            queue.Append(new ShipLandItem(player));
            queue.Append(new ChangeMountItem(player));
            queue.Append(new MobiliseItem());
            return queue;

        }
        //(probably teleport) changemount, change spawn, speech, speech, play sound, play sound, speech, animate ship + sky, speech, speech, teleport, animate ship, changemount, mobilise
        static SequenceQueue ConstructIntroSequence(Player player)
        {
            Vector2 destination = 16 * new Vector2(259, 1726);         
            SequenceQueue queue = new SequenceQueue(Sequence.IntroCutscene);
            PlayerFixer pf = player.GetModPlayer<PlayerFixer>();
            queue.Append(new ImmobiliseItem());
            queue.Append(new PlayerHolderItem(player, new Vector2(4756 * 16, 249 * 16)));
            queue.Append(new ChangeMountItem(player, ModContent.GetInstance<StartingShip>().Type));
            queue.Append(new SpawnChangeItem(destination, player));
            queue.Append(new PauseItem(300));         
            queue.Append(new SpeechItem(new SpeechBubble("Hmmm, this doesn't look anything like my starmap...so it was a left at the Crab Nebula, right at the supermassive black hole, right at the...", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 480), pf, new Vector2(100, -100)));
            queue.Append(new SpeechItem(new SpeechBubble("Oh my mistake that should have been a left at the Crab Nebula, which means on the map I should be...", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 300), pf, new Vector2(100, -100)));
            queue.Append(new SpeechItem(new SpeechBubble("...off the edge of it. Which makes me lost.", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 300), pf, new Vector2(100, -100)));
            queue.Append(new PauseItem(240));
            queue.Append(new SpeechItem(new SpeechBubble("I wonder what's on the radio in these parts...", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 300), pf, new Vector2(100, -100)));
            queue.Append(new SoundEffectItem(665, "Sounds/RadioSound"));
            queue.Append(new SpeechItem(new SpeechBubble("Gosh they've got some weird music around here.", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 180), pf, new Vector2(100, -100)));
            queue.Append(new SpeechItem(new SpeechBubble("ERROR: ENGINE FAILURE!", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 180), pf, new Vector2(-400, -100)));
            queue.Append(new SpaceShipCrashItem(player));
            queue.Append(new SpeechItem(new SpeechBubble("This is not at all good...", (int)pf.GetScreenPosition().X, (int)pf.GetScreenPosition().Y, 400, 180), pf, new Vector2(100, -100)));
            queue.Append(new SpaceShipCrashItem(player));
            queue.Append(new PlayerHolderItem(player, destination));
            queue.Append(new PauseItem(240));
            queue.Append(new SpaceShipCrashItem(player));
            queue.Append(new ChangeMountItem(player));
            queue.Append(new PlayerHolderItem(player));
            queue.Append(new MobiliseItem());
            
            return queue;
        }
        public static SequenceQueue CloneSequence(Sequence seq)
        {
            foreach (SequenceQueue s in sequences) if (s.GetSequence() == seq) return (SequenceQueue)s.Clone();
            throw new ArgumentException("Sequence not yet constructed!");
        }
    }
}
