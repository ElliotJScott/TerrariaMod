using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.GUI;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor
{
    class CommandToggleGravLines : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "gravDisp"; }
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
    class CommandToggleGrav : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "grav"; }
        }

        public override string Description
        {
            get { return "Toggles the custom gravity"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity = !Main.player[Main.myPlayer].GetModPlayer<PlayerFixer>().custGravity;
            Main.player[Main.myPlayer].QuickMount();
        }
    }
}
