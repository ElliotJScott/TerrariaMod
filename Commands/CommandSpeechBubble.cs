using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teo.GUI;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace teo
{
    class CommandSpeechBubble : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "speech"; }
        }

        public override string Description
        {
            get { return "Create a speech bubble"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Vector2 pos = Main.playerDrawData[Main.myPlayer].position;
            Main.NewText(pos);
            ModContent.GetInstance<TEO>().speechBubbles.Add(new SpeechBubble("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", (int)pos.X, (int)pos.Y - 250, 700, 350, 300));
        }
    }
}
