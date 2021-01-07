using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Tiles
{
    enum MergeState
    {
        SameGrass = 'g', //g for grass
        Merge = 'm', //m for merge/mud
        Either = 'e', //o for or (as in grass or merge)
        DontMerge = 'a', //a for air
        Ignore = 'n', //n for null
        This = 't'
    }
    struct TileMask
    {
        string mask;
        (int, int) loc;
        Direction dir;
        byte spacing;
        public TileMask(string m, int x, int y, Direction d, byte s)
        {
            loc = (x, y);
            mask = m;
            dir = d;
            spacing = s;
        }
        public (int, int) GetFrameLoc(int choice)
        {
            (int, int) ret = loc;
            switch (dir)
            {
                case Direction.Right:
                    ret.Item1 += spacing * choice;
                    break;
                case Direction.Down:
                    ret.Item2 += spacing * choice;
                    break;
                default:
                    
                    break;
            }
            return ret;
        }
        public bool CheckString(string s)
        {
            if (s.Length != mask.Length)
            {
                
                return false;
            }
            for (int i = 0; i < s.Length; i++)
            {

                if (mask[i] != (char)MergeState.Ignore || mask[i] != (char)MergeState.Either)
                {
                    if (mask[i] != s[i]) return false;
                }
                else if (mask[i] == (char)MergeState.Either)
                {
                    if (s[i] != (char)MergeState.SameGrass && s[i] != (char)MergeState.Merge) return false;
                }
            }
            return true;
        }
    }
    public abstract class CustomGrass : ModTile
    {
        
        List<TileMask> tileMasks = new List<TileMask>();
        void SetupTileMasks()
        {
            //top three rows
            tileMasks.Add(new TileMask("ngeatenge", 0, 0, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nangtgeee", 1, 0, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("eeeeteeee", 1, 1, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("eeegtgnan", 1, 2, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("egnetaegn", 4, 0, Direction.Down, 0)); //

            tileMasks.Add(new TileMask("ngnatangn", 5, 0, Direction.Down, 0)); //

            tileMasks.Add(new TileMask("nanatangn", 6, 0, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("agagtgeee", 6, 1, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("eeegtgaga", 6, 2, Direction.Right, 0)); //

            tileMasks.Add(new TileMask("nanatgnan", 9, 0, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("agegteage", 10, 0, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("egaetgega", 11, 0, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nangtanan", 12, 0, Direction.Down, 0)); //

            //Next two rows
            tileMasks.Add(new TileMask("nanatenee", 0, 3, Direction.Right, 1)); //
            tileMasks.Add(new TileMask("neeatenan", 0, 4, Direction.Right, 1)); //
            tileMasks.Add(new TileMask("nanetaeen", 1, 3, Direction.Right, 1)); //
            tileMasks.Add(new TileMask("eenetanan", 1, 4, Direction.Right, 1)); //

            tileMasks.Add(new TileMask("ngnatanan", 6, 3, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nangtgnan", 6, 4, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nanatanan", 9, 3, Direction.Right, 0)); //

            //Middle Block
            tileMasks.Add(new TileMask("eeeetgega", 2, 5, Direction.Down, 1)); //
            tileMasks.Add(new TileMask("eeegteage", 3, 5, Direction.Down, 1)); //
            tileMasks.Add(new TileMask("egaetgeee", 2, 6, Direction.Down, 1)); //
            tileMasks.Add(new TileMask("agegteeee", 3, 6, Direction.Down, 1)); //

            tileMasks.Add(new TileMask("ngeatenme", 4, 5, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("egnetaemn", 5, 5, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nmeatenge", 4, 8, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("emnetaegn", 5, 8, Direction.Down, 0)); //

            tileMasks.Add(new TileMask("nanatanmn", 6, 5, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("ngnatanmn", 7, 5, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nmnatanan", 6, 8, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nmnatangn", 7, 8, Direction.Down, 0)); //
            //There's some here I don't know the function of - they don't seem to actually get used

            //Next few bits
            tileMasks.Add(new TileMask("nanmtgnen", 0, 11, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nangtmnen", 3, 11, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nenmtgnan", 0, 12, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nengtmnan", 3, 12, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nanmtmnan", 9, 11, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nmnatanmn", 6, 12, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nanmtanan", 0, 13, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nanatmnan", 3, 13, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nanmtgnan", 0, 14, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nangtmnan", 3, 14, Direction.Right, 0)); //

            //First Green Block - some of these are unused
            //tileMasks.Add(new TileMask("nanatgnge", 7, 12, Direction.Right, 2));
            tileMasks.Add(new TileMask("nangtgaga", 8, 12, Direction.Right, 2)); //
            //tileMasks.Add(new TileMask("nangtaegn", 7, 12, Direction.Right, 2));
            tileMasks.Add(new TileMask("ngaatgnga", 7, 13, Direction.Right, 2)); //
            tileMasks.Add(new TileMask("agagtgaga", 8, 13, Direction.Right, 2)); //
            tileMasks.Add(new TileMask("agngtaagn", 9, 13, Direction.Right, 2)); //
            tileMasks.Add(new TileMask("agagtgnan", 8, 14, Direction.Right, 2)); //

            //Singles
            tileMasks.Add(new TileMask("nmnatenmn", 0, 15, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nmnetanmn", 1, 15, Direction.Down, 0)); //
            tileMasks.Add(new TileMask("nanmtmnen", 2, 15, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nenmtmnan", 2, 16, Direction.Right, 0)); //

            //Second Green Block
            tileMasks.Add(new TileMask("nanatgnga", 5, 15, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nangtaagn", 5, 16, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("ngaatgnan", 8, 15, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("agngtanan", 8, 16, Direction.Right, 0)); //

            //Peculiars
            tileMasks.Add(new TileMask("ggagtgagg", 2, 17, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("agggtggga", 5, 17, Direction.Right, 0)); //

            //Spurs
            tileMasks.Add(new TileMask("nangtgega", 0, 18, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("nangtgage", 3, 18, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("egagtgnan", 0, 19, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("agegtgnan", 3, 19, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("ngaatgnge", 0, 20, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("ngeatgnga", 3, 20, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("egngtaagn", 0, 21, Direction.Right, 0)); //
            tileMasks.Add(new TileMask("agngtaegn", 3, 21, Direction.Right, 0)); //

            //Bonus extra safety mask
            tileMasks.Add(new TileMask("nnnntnnnn", 1, 1, Direction.Right, 0));
        }
        public override void SetDefaults()
        {
            SetupTileMasks();
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            //Main.tileLighted[Type] = true;
            drop = ModContent.ItemType<Items.Placeable.FloatingUnderGrass>(); //put this in the subclass
            base.SetDefaults();
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            int tileSize = 18;
            int choice = Main.rand.Next(3);

            //I think I'm going to do this in the most jank way imaginable for shits and giggles
            // this goes 1 2 3
            //           4 - 5
            //           6 7 8
            /*
            string statusString = "";
            for (int q = -1; q <= 1; q++)
            {
                for (int p = -1; p <= 1; i++)
                {

                    if (p == 0 && q == 0)
                    {
                        statusString += (char)MergeState.This;
                    }
                    else
                    {
                        Tile tile = Framing.GetTileSafely(i + p, j + q);
                        if (tile.active())
                            statusString += (char)GetMergeState(tile.type);
                        else statusString += (char)MergeState.DontMerge;
                    }
                }
            }
            /*
            foreach (TileMask t in tileMasks)
            {
                if (t.CheckString(statusString))
                {
                    (int, int) pos = t.GetFrameLoc(choice);
                    Main.tile[i, j].frameX = (short)(pos.Item1 * tileSize);
                    Main.tile[i, j].frameY = (short)(pos.Item2 * tileSize);
                    break;
                }
            }
            
            /*
            if (top == grassID && left == grassID && right == dirtID && bot == dirtID)
            {
                Main.tile[i, j].frameX = (short)(3 * tileSize);
                Main.tile[i, j].frameY = (short)((6 + (2 * choice)) * tileSize);
                return false;
            }
            if (top == grassID && right == grassID && left == dirtID && bot == dirtID)
            {
                Main.tile[i, j].frameX = (short)(2 * tileSize);
                Main.tile[i, j].frameY = (short)((6 + (2 * choice)) * tileSize);
                return false;
            }
            */
            /* Ok so at some point i should finish this , this would be a definitive full correct tile frame selector but cba to do it now
(int, int) loc = (0, 0);

if (!Merge(grassID, dirtID, left) && right == dirtID && top == grassID && bot == grassID) loc = (0, choice);
if (!Merge(grassID, dirtID, right) && left == dirtID && top == grassID && bot == grassID) loc = (4, choice);
if (!Merge(grassID, dirtID, top) && bot == dirtID && left == grassID && right == grassID) loc = (1 + choice, 0);
if (!Merge(grassID, dirtID, bot) && top == dirtID && left == grassID && right == grassID) loc = (1 + choice, 2);
*/
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
        MergeState GetMergeState(int type)
        {
           
            if (type == Type) return MergeState.SameGrass;
            else return Main.tileMerge[Type][type] ? MergeState.Merge : MergeState.DontMerge;
        }
        //bool Merge(int did, int gid, int t) => t == did || t == gid;

    }
}
