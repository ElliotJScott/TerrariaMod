using Microsoft.Xna.Framework;
using StarSailor.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Commands
{
    class CommandAsteroids : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "fixAsteroids"; }
        }

        public override string Description
        {
            get { return "Fixes all the asteroids"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int tlx = int.Parse(args[0]);
            int tly = int.Parse(args[1]);
            int brx = int.Parse(args[2]);
            int bry = int.Parse(args[3]);

            for (int i = tlx; i <= brx; i++)
            {
                for (int j = tly; j <= bry; j++)
                {
                    if (Framing.GetTileSafely(i, j).type == TileID.Titanium)
                    {
                    
                        List<Vector2> positions = new List<Vector2>();
                        SpreadPlacer(i, j, i, j, ModContent.TileType<AsteroidRock>(), positions);
                        Vector2 sum = Vector2.Zero;
                        foreach (Vector2 v in positions) sum += v;
                        sum /= positions.Count;
                        PlaceGravSource((int)Math.Round(sum.X), (int)Math.Round(sum.Y));
                    }
                }
            }
        }
        public void SpreadPlacer(int i, int j, int initI, int initJ, int id, List<Vector2> positions)
        {
            Tile t = Framing.GetTileSafely(i, j);
            Tile r = GetTileInstance(id);
            ushort w = t.wall;
            int IDtoReplace = TileID.Titanium;
            int max = 80;

            if (t.type == IDtoReplace && t.active())
            {
                positions.Add(new Vector2(i, j));
                t.CopyFrom(r);
                t.frameX = 0;
                t.frameY = 0;
                t.wall = w;
                WorldGen.SquareTileFrame(i, j, true);
                FrameCorrectly(i, j);
                if (Vector2.Distance(new Vector2(i, j), new Vector2(initI, initJ)) <= max)
                {
                    SpreadPlacer(i - 1, j, initI, initJ, id, positions);
                    SpreadPlacer(i + 1, j, initI, initJ, id, positions);
                    SpreadPlacer(i, j - 1, initI, initJ, id, positions);
                    SpreadPlacer(i, j + 1, initI, initJ, id, positions);

                }

            }





        }
        public void PlaceGravSource(int i, int j)
        {
            Tile t = Framing.GetTileSafely(i, j);
            Tile r = GetTileInstance(ModContent.TileType<GravitySource>());
            ushort w = t.wall;
            t.CopyFrom(r);
            t.frameX = 0;
            t.frameY = 0;
            t.wall = w;
            WorldGen.SquareTileFrame(i, j, true);

        }


        public void FrameCorrectly(int i, int j)
        {
            Vector2 thisPos = new Vector2(i, j);
            List<Tile> checkTiles = new List<Tile>();
            List<Vector2> positions = new List<Vector2>();
            for (int p = -1; p <= 1; p += 2)
            {
                checkTiles.Add(Framing.GetTileSafely(i, j + p));
                positions.Add(new Vector2(i, j + p));
                checkTiles.Add(Framing.GetTileSafely(i + p, j));
                positions.Add(new Vector2(i + p, j));
            }
            bool[] vals = new bool[4];
            for (int q = 0; q < checkTiles.Count; q++)
            {
                vals[q] = checkTiles[q].type == 0;
            }
            int slopeid = 0;
            if (vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 1;
            else if (vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 2;
            else if (!vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 3;
            else if (!vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 5;
            WorldGen.SlopeTile(i, j, slopeid);
        }

        private Tile GetTileInstance(int id, int i = 0, int j = 0)
        {
            Tile r = Framing.GetTileSafely(i, j);
            if (r.type == 0) return GetTileInstance(id, i, j + 1);
            else
            {
                Tile k = new Tile();
                k.CopyFrom(r);
                k.type = (ushort)id;
                k.frameX = 0;
                k.frameY = 0;
                return k;
            }
        }
    }
    public class CommandFixFixedAsteroids : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "fixAstProper"; }
        }

        public override string Description
        {
            get { return "Fixes all the asteroids"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            int tlx = int.Parse(args[0]);
            int tly = int.Parse(args[1]);
            int brx = int.Parse(args[2]);
            int bry = int.Parse(args[3]);

            for (int i = tlx; i <= brx; i++)
            {
                for (int j = tly; j <= bry; j++)
                {
                    if (Framing.GetTileSafely(i, j).type == ModContent.TileType<AsteroidRock>())
                    {
                        FrameCorrectly(i, j);
                    }
                }
            }
        }
        public void FrameCorrectly(int i, int j)
        {
            Vector2 thisPos = new Vector2(i, j);
            List<Tile> checkTiles = new List<Tile>();
            List<Vector2> positions = new List<Vector2>();
            for (int p = -1; p <= 1; p += 2)
            {
                checkTiles.Add(Framing.GetTileSafely(i, j + p));
                positions.Add(new Vector2(i, j + p));
                checkTiles.Add(Framing.GetTileSafely(i + p, j));
                positions.Add(new Vector2(i + p, j));
            }
            bool[] vals = new bool[4];
            for (int q = 0; q < checkTiles.Count; q++)
            {
                vals[q] = checkTiles[q].type == 0;
            }
            int slopeid = 0;
            if (vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 1;
            else if (vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && !vals[(int)Direction.Down]) slopeid = 2;
            else if (!vals[(int)Direction.Up] && vals[(int)Direction.Right] && !vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 3;
            else if (!vals[(int)Direction.Up] && !vals[(int)Direction.Right] && vals[(int)Direction.Left] && vals[(int)Direction.Down]) slopeid = 5;
            WorldGen.SlopeTile(i, j, slopeid);
        }
    }
}
