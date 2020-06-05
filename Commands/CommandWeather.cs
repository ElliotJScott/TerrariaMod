using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace teo
{
    class CommandDay : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "day"; }
        }

        public override string Description
        {
            get { return "Set Day"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.time = 29000;
            Main.dayTime = true;
            Main.NewText("Time Updated");
        }
    }
    class CommandNight : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "night"; }
        }

        public override string Description
        {
            get { return "Set Night"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.time = 29000;
            Main.dayTime = false;
            Main.NewText("Time Updated");
        }
    }
    class CommandSandStorm : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "sandstorm"; }
        }

        public override string Description
        {
            get { return "Set weather"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            //Main.maxRaining = 5.0f;
            //Main.raining = true; 
            //Main.rainTime = int.MaxValue;
            Sandstorm.Happening = !Sandstorm.Happening;
        }
    }
    class CommandRain : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "rain"; }
        }

        public override string Description
        {
            get { return "Toggle rain"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (Main.raining)
            {
                Main.maxRaining = 0f;
                Main.raining = false;
                Main.rainTime = 0;
            }
            else
            {
                Main.maxRaining = 5.0f;
                Main.raining = true;
                Main.rainTime = int.MaxValue;
            }
        }
    }
    class CommandClouds : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "clouds"; }
        }

        public override string Description
        {
            get { return "Adjust clouds"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.numClouds = int.Parse(args[0]);
            Main.weatherCounter = int.MaxValue;
        }
    }
}
