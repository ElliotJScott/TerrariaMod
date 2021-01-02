using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarSailor.Tiles
{
    class GlowingCoral : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileWaterDeath[Type] = false;
            Main.tileLavaDeath[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoFail[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newTile.WaterDeath = false;
            TileObjectData.newTile.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.WaterDeath = false;
            TileObjectData.newAlternate.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newAlternate.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.addAlternate(1);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.WaterDeath = false;
            TileObjectData.newAlternate.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newAlternate.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.addAlternate(2);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.StyleTorch);
            TileObjectData.newAlternate.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.AlternateTile, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.WaterDeath = false;
            TileObjectData.newAlternate.WaterPlacement = LiquidPlacement.Allowed;
            TileObjectData.newAlternate.LavaPlacement = LiquidPlacement.NotAllowed;
            TileObjectData.addAlternate(0);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(253, 221, 3));
            base.SetDefaults();
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, ModContent.ItemType<Items.Placeable.GlowingCoral>());
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            tile.frameX = (short)(26 * ((i+j) % 6));
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            float rotation = 0;
            int offsetX = 0;
            int offsetY = 0;
            if (i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1)
            {
                if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active() && Main.tileSolid[Main.tile[i, j - 1].type])
                {
                    offsetX = 26;
                    offsetY = 26;
                    rotation = (float)Math.PI;
                }
                if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active() && Main.tileSolid[Main.tile[i - 1, j].type])
                {
                    offsetX = 26;
                    offsetY = 0;
                    rotation = (float)Math.PI / 2f;
                }
                if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active() && Main.tileSolid[Main.tile[i + 1, j].type])
                {
                    offsetX = 0;
                    offsetY = 26;
                    rotation = (float)(3f * Math.PI / 2);
                }
                if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active() && Main.tileSolid[Main.tile[i, j + 1].type])
                {
                    offsetX = 0;
                    offsetY = 0;
                    rotation = 0f;
                }
            }
            //int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Tiles/GlowingCoral"), new Vector2(i * 16 - (int)Main.screenPosition.X - 5 + offsetX, j * 16 - (int)Main.screenPosition.Y - 5 + offsetY) + zero, new Rectangle(tile.frameX, tile.frameY, 26, 26), Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            return false;
        }
        //public override 
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            switch (tile.frameX/26)
            {
                case 0:
                    r = 1f;
                    g = 0.2f;
                    b = 0.2f;
                    break;
                case 1:
                    r = 1f;
                    g = 0.1f;
                    b = 0.4f;
                    break;
                case 2:
                case 5:
                    r = 1f;
                    g = 0.4f;
                    b = 0.1f;
                    break;
                case 3:
                    r = 0.2f;
                    g = 1f;
                    b = 0.2f;
                    break;
                case 4:
                    r = 0.1f;
                    g = 0.3f;
                    b = 1f;
                    break;
                default:
                    r = 1f;
                    g = 1f;
                    b = 1f;
                    break;
            }
        }
    }
}
