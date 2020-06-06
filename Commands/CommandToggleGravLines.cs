using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teo.GUI;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace teo
{
    class CommandToggleGravLines : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "togglegravdisplay"; }
        }

        public override string Description
        {
            get { return "Toggles the debugging info display for custom gravity"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            GravDisplay.dispGui = !GravDisplay.dispGui;
        }
    }
}
