using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using StarSailor.GUI;
using StarSailor.Mounts;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarSailor.Tiles
{
    public class RopedPole : ModTile
    {
        int bobCounter = 0;
        int inc = 1;
        int disp = 0;
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
            AddMapEntry(new Color(40, 40, 40), name);
            disableSmartCursor = true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 32, mod.ItemType("RopedPole"));
        }
        public override bool NewRightClick(int i, int j)
        {
            //Add the mounting code in here
            Player p = Main.player[Main.myPlayer];
            if (p.selectedItem == 58)
            {
                Main.NewText("Throwing your items at the pole does nothing, you dingus!");
            }
            else if (p.mount.Type != ModContent.GetInstance<Mech>().Type)
            {
                p.mount.SetMount(ModContent.GetInstance<Mech>().Type, p);
            }
            return base.NewRightClick(i, j);
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            bobCounter = ++bobCounter;
            if (bobCounter >= 90)
            {
                bobCounter = 0;
                inc = -inc;
            }
            if (bobCounter % 45 == 0)
            {
                disp += inc;
            }
            if (Math.Abs(disp) > 4)
            {
                disp -= Math.Sign(bobCounter);
            }
            base.AnimateTile(ref frame, ref frameCounter);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {

            if (Framing.GetTileSafely(i, j-1).type != Type && Main.player[Main.myPlayer].mount.Type != ModContent.GetInstance<Boat>().Type)
            {
                Vector2 pixelPos = 16f * new Vector2(i, j);
                Vector2 screenPos = pixelPos - Main.screenPosition;
                Vector2 boatPos = new Vector2((int)screenPos.X - 140, (int)screenPos.Y + 46 + disp);
                StarSailorMod t = ModContent.GetInstance<StarSailorMod>();
                GuiHelpers.DrawLine(spriteBatch, screenPos + new Vector2(10, 10), boatPos + new Vector2(t.boatTex.Width - 10, 20), t.ropeTex.Height, Color.White, t.ropeTex);
                spriteBatch.Draw(t.boatTex, new Rectangle((int)screenPos.X - 140, (int)screenPos.Y + 46 + disp, t.boatTex.Width, t.boatTex.Height), Color.White);
            }
            return base.PreDraw(i, j, spriteBatch);
        }
    }
}