using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.GUI;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor
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
            //ModContent.GetInstance<StarSailorMod>().speechBubbles.Add(new SpeechBubble("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", (int)pos.X, (int)pos.Y - 250, 700, 300));
        }
    }
    class CommandHelpTest : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "helptest"; }
        }

        public override string Description
        {
            get { return "Create a help"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            
            SpeechBubble.HelpText("This is an example of a piece of help text to guide the player. I am making this text longer just to see how it appears with a bit more to it but we will see what it looks like i don't really know.");
        }
    }
}
