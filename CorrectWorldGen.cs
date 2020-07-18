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

namespace StarSailor
{
    class CorrectWorldGen : ModWorld
    {
        public static List<string> tsk = new List<string>();
        static bool preInit = true;
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        
        {
            
            foreach (GenPass t in tasks)
            {
                tsk.Add(t.Name);
            }
            int SpawnIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Spawn Point"));
            GenPass spawnTask = tasks[SpawnIndex];
            tasks.Clear();
            tasks.Add(spawnTask);
            tasks.Add(new PassLegacy("Insert Correct World", InsertWorld));
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
            base.PreUpdate();
        }
        public override void Initialize()
        {
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
