using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.GameContent.Generation;
using Terraria.ID;
using StarSailor.Sequencing;
using StarSailor.NPCs.Characters;
using System.Reflection;
using Terraria.GameContent.Events;
using StarSailor.NPCs;
using Terraria.ModLoader.IO;
using StarSailor.Dimensions;

namespace StarSailor
{
    
    class WorldFixer : ModWorld
    {
        public static List<string> tsk = new List<string>();
        static bool preInit = true;
        bool haveLoadedNPCs = false;
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            foreach (GenPass t in tasks)
            {
                
                tsk.Add(t.Name);
                Main.NewText(t.Name);
            }
            //int SpawnIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Spawn Point"));
            //GenPass spawnTask = tasks[SpawnIndex];
            //tasks.Clear();
            //tasks.Add(spawnTask);
            //tasks.Add(new PassLegacy("Insert Correct World", InsertWorld));
            base.ModifyWorldGenTasks(tasks, ref totalWeight);
        }
        public void ModifyWorld(GenerationProgress progress)
        {
            progress.Message = "Breaking game";
            Main.tile = new Tile[8400, 2400];
        }
        public void InsertWorld(GenerationProgress progress)
        {
            progress.Message = "Loading map";


            int spawnX = Main.spawnTileX;
            int spawnY = Main.spawnTileY;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {

                    Tile tile = Framing.GetTileSafely(i + spawnX - 25, j + spawnY - 25);
                    tile.type = StarSailorMod.QRCode[j, i] == 1 ? TileID.ObsidianBrick : TileID.Glass;
                    tile.active(true);
                    tile.slope(0);
                    /*
                    Tile tile2 = Framing.GetTileSafely(i, j);
                    tile2.type = teo.QRCode[j, i] == 1 ? TileID.ObsidianBrick : TileID.Glass;
                    tile2.active(true);
                    tile2.slope(0);
                    */
                }
            }
            tsk.Add("(" + spawnX + "," + spawnY + ")");

        }
        public override void PreUpdate()
        {
            if (ModContent.GetInstance<DimensionManager>().currentDimension != Dimensions.Dimensions.Overworld)
            {
                Main.slimeRain = false;
                Main.raining = false;
                Main.UseStormEffects = false;
                Sandstorm.Happening = false;
            }
            //UpdateNPCSpawns();
            base.PreUpdate();
        }
        /*
        public void UpdateNPCSpawns()
        {
            //All of this is from stack overflow I have no idea how it works
            //Well like I get pretty much what it's doing but, yeah...
            List<Type> subclassTypes = Assembly
                .GetAssembly(typeof(Character))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Character)))
                .ToList();
            foreach (Type t in subclassTypes)
            {
                // God this is so jank it's actually crazy
                MethodInfo method = typeof(ModContent).GetMethod("GetInstance",
                                                BindingFlags.Public | BindingFlags.Static);
                method = method.MakeGenericMethod(t);
                Character v = (Character)method.Invoke(null, null);
                MethodInfo method2 = typeof(ModContent).GetMethod("NPCType",
                    BindingFlags.Public | BindingFlags.Static);
                method2 = method2.MakeGenericMethod(t);
                int type = (int)method2.Invoke(null, null);
                if (Character.NeedToSpawn(v.Name, type))
                {
                    v.SpawnCharacter(CharacterLocationMapping.npcLocations[v.InternalName], type);
                }
                
            }
        }
        */
        public override void Initialize()
        {
            SequenceBuilder.InitialiseSequences(Main.LocalPlayer);
            base.Initialize();
            return;
            tsk.Clear();
            if (Main.worldName != "TEO") throw new InvalidOperationException("Wrong map dingus");
            int spawnX = Main.spawnTileX;
            int spawnY = Main.spawnTileY;
            int r = 0;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    int x = i + spawnX - 25;
                    int y = j + spawnY - 25;
                    Tile tile = Framing.GetTileSafely(x, y);


                    int tileID = StarSailorMod.QRCode[j, i] == 1 ? TileID.ObsidianBrick : TileID.Glass;

                    if (tile.type != tileID && preInit == false) throw new InvalidOperationException("Wrong map dingus");

                }
            }
            preInit = !preInit;

        }

    }
    
}
