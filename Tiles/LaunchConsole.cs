using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using teo.Mounts;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace teo.Tiles
{
    public class LaunchConsole : ModTile
    {
        public string reference = "Default Launch Console";
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault(reference);
            AddMapEntry(new Color(40, 40, 40), name);
            disableSmartCursor = true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("LaunchConsole"));
        }
        public override bool NewRightClick(int i, int j)
        {
            Tile below = Framing.GetTileSafely(i, j - 1);
            Tile above = Framing.GetTileSafely(i, j + 1);
            int aboveCheck = above.type == Type ? 2 : 0;
            int belowCheck = below.type == Type ? 1 : 0;
            int newJ = j + aboveCheck + belowCheck;
            LaunchPoint l = new LaunchPoint(new Vector2(i, newJ), ((TEO)mod).GenerateName(), mod);
            string error = l.CheckValidity();
            if (error == "")
            {
                Player p = Main.player[Main.myPlayer];
                if (p.selectedItem == 58)
                {
                    Main.NewText("Throwing your items at the launch pad does nothing, you dingus!");
                }
                else
                {
                    l = ModContent.GetInstance<LaunchPointManager>().GetCorrectLaunchPoint(l);

                    ModContent.GetInstance<LaunchPointManager>().AddLaunchPoint(l);
                    ModContent.GetInstance<LaunchPointManager>().currentLaunchPoint = l;

                    p.mount.SetMount(ModContent.GetInstance<Rocket>().Type, p);
                    ((TEO)mod).inLaunchGui = true;
                    ((TEO)mod).updateButtonsFlag = true;
                }
            }
            else
            {
                Main.NewText(error);
            }
            return base.NewRightClick(i, j);
        }

    }
}