using Microsoft.Xna.Framework;
using StarSailor.Backgrounds;
using StarSailor.Dimensions;
using StarSailor.Sequencing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            foreach (string s in WorldFixer.tsk)
            {
                if (s.Contains("spawn") || s.Contains("Spawn"))
                {
                    Main.NewText(WorldFixer.tsk.IndexOf(s) + ": " + s);
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
            sm.GenerateStars(250, Distribution.Atan);
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
            SequenceBuilder.CloneSequence(Sequence.IntroCutscene).Execute();
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
    class CommandNPCs : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "loadnpcs"; }
        }

        public override string Description
        {
            get { return "Load all the characters"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {

            //ModContent.GetInstance<WorldFixer>().UpdateNPCSpawns();
        }
    }
    class CommandCoins : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "dimdat"; }
        }

        public override string Description
        {
            get { return "Does the coins"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            /*
            Dimension[] dims = ModContent.GetInstance<DimensionManager>().dimensions;
            foreach (Dimension d in dims)
            {
                Main.NewText("Dimension: " + d.dimension + " with total TileIDs: " + d.basicTileData.tileIDs.Count);
            }
            */
            Main.NewText(ModContent.GetInstance<DimensionManager>().currentDimension +" " + ModContent.GetInstance<DimensionManager>().dimsLoaded);
            //Tile t = new Tile();
            //t.type = 1;
            //t.active(true);
            //Main.NewText(t.active());
            //Thread tr = new Thread(CommandMethod);
            //tr.Start();
        }
        private void CommandMethod()
        {
            for (int i = 0; i < Main.tile.GetLength(0); i++)
                for (int j = 0; j < Main.tile.GetLength(1); j++)
                    if (Main.tile[i, j] != null)
                        Main.tile[i, j].active(true);
        }
    }

    class CommandTP : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "fr"; }
        }

        public override string Description
        {
            get { return "Does the coins"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText("Beginning bricking");
            Thread t = new Thread(new ParameterizedThreadStart(CommandMethod));
            t.Start(int.Parse(args[0]));
            //CommandMethod(args[0]);
            
        }
        private void CommandMethod(object argo)
        {
            ModContent.GetInstance<DimensionManager>().SwitchDimension((Dimensions.Dimensions)argo);
            Main.NewText("Finished Bricking");
            //Main.wor
            //Main.tile = new Tile[4200, 1200];
            //Main.LocalPlayer.Teleport(new Vector2(300, 300));
            /*
            Tile[,] backup = new Tile[Main.tile.GetLength(0), Main.tile.GetLength(1)];
            for (int i = 0; i < Main.tile.GetLength(0); i++)
            {
                for (int j = 0; j < Main.tile.GetLength(1); j++)
                {
                    if (i == j) Main.NewText("in da loop at " + i);
                    //Main.NewText("in da loop");
                    try
                    {
                        backup[i, j] = (Tile)Main.tile[i, j].Clone();
                    }
                    catch
                    {
                        //Main.NewText("Error at " + i + "," + j);
                    }
                }
            }
            Main.NewText("Cloning done");
            foreach (Tile t in Main.tile)
            {
                t.ClearEverything();
            }
            Main.NewText("World cleared");
            for (int i = 0; i < backup.GetLength(0); i++)
            {
                for (int j = 0; j < backup.GetLength(1); j++)
                {
                    Main.tile[i, j] = (Tile)backup[i, j].Clone();
                }
            }
            Main.NewText("Done!");
            */
        }

    }
    
}
