using Microsoft.Xna.Framework;
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
}
