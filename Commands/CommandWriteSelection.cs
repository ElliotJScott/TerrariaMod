using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.GUI;
using StarSailor.Items;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarSailor
{
    class CommandWriteSelection : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "writeSel"; }
        }

        public override string Description
        {
            get { return "Writes the current selection to a new file"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            string fileName = args[0].Trim();
            if (fileName.Length == 0)
            {
                Main.NewText("Must supply a file name");
                return;
            }
            Vector2[] bounds = EnvironmentDevTool.corners;
            Main.NewText("top left " + bounds[0]);
            Main.NewText("bottom right " + bounds[1]);
            if (bounds[0].X < bounds[1].X && bounds[0].Y < bounds[1].Y)
            {
                Rectangle area = new Rectangle((int)bounds[0].X, (int)bounds[0].Y, (int)(bounds[1].X - bounds[0].X), (int)(bounds[1].Y - bounds[0].Y));
                for (int i = area.X; i <= area.X + area.Width; i++)
                {
                    for (int j = area.Y; j <= area.Y + area.Height; j++)
                    {
                        Tile t = Framing.GetTileSafely(i, j);
                        Main.NewText(t.ToString());
                    }
                }
            }
            else
            {
                Main.NewText("ERROR: corners are not set up correctly");
            }
        }
    }
}
