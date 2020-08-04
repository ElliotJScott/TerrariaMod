using Microsoft.Xna.Framework;
using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor
{
    class Command22 : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "gettask"; }
        }

        public override string Description
        {
            get { return "Get num of worldgen tasks"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            foreach (string s in CorrectWorldGen.tsk)
            {
                if (s.Contains("spawn") || s.Contains("Spawn"))
                {
                    Main.NewText(CorrectWorldGen.tsk.IndexOf(s) + ": " + s);
                }
            }

            Main.NewText("Done!");
        }
    }
    class CommandStars : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "regenStars"; }
        }

        public override string Description
        {
            get { return "Re-generate stars"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            sm.GenerateStars(250);
        }
    }
    class CommandRun : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "run"; }
        }

        public override string Description
        {
            get { return "Runs the programmed sequence"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            sm.sequence = SequenceBuilder.CloneSequence(Sequence.IntroCutscene);
        }
    }
    class CommandConstruct : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "construct"; }
        }

        public override string Description
        {
            get { return "Runs the programmed sequence"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            SequenceBuilder.InitialiseSequences(Main.LocalPlayer);
        }
    }
}
