using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarSailor.Mounts;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarSailor.Tiles
{
    class RapidWater : ModTile
    {
        public bool accelMyPlayer = false;
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = false;
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = false;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            drop = mod.ItemType("RapidWater");
            AddMapEntry(new Color(20, 20, 200));
            
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 0f;
            b = 0f;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 playerPos = Main.player[Main.myPlayer].position;
            Rectangle tileRect = new Rectangle(i * 16, j * 16, 16, 16);
            Rectangle playerRect = new Rectangle((int)playerPos.X - 38, (int)playerPos.Y + 10, 90, 36);
            if (tileRect.Intersects(playerRect))
            {
                accelMyPlayer = true;
            }
            ModContent.GetInstance<StarSailorMod>().rapidWaterRedraws.Add(new Vector2(i, j));
            //spriteBatch.Draw(ModContent.GetInstance<TEO>().pixel, playerRect, Color.Red * 0.5f);
            return false;
        }

        public void CorrectDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int uniqueAnimationFrame = Main.tileFrame[Type] + i;
            if (i % 2 == 0)
            {
                uniqueAnimationFrame += 3;
            }
            if (i % 3 == 0)
            {
                uniqueAnimationFrame += 3;
            }
            uniqueAnimationFrame = uniqueAnimationFrame % 4;
            int flowDir = -1; //-1 for from right, 1 for  from left, 0 for neither (though I can't think when one would use neither)
            int frameYOffset = uniqueAnimationFrame * 18;
            Texture2D texture = Main.tileTexture[Type];
            SpriteEffects ef = SpriteEffects.None; //change this in the future
            int frameXOffset = CalcCorrectTile(i, j, flowDir);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            Tile tile = Framing.GetTileSafely(i, j);
            Main.spriteBatch.Draw(
                        texture,
                        new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + Vector2.Zero,
                        new Rectangle(tile.frameX + frameXOffset, tile.frameY + frameYOffset, 16, 16),
                        Lighting.GetColor(i, j), 0f, default, 1f, ef, 0f);

        }
        public int CalcCorrectTile(int i, int j, int flowDir)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            Tile tileAbove = Framing.GetTileSafely(i, j - 1);
            Tile tileTwoAbove = Framing.GetTileSafely(i, j - 2);
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);
            Tile tileBefore = Framing.GetTileSafely(i - flowDir, j);
            Tile tileAfter = Framing.GetTileSafely(i + flowDir, j);
            Tile tileTwoAfter = Framing.GetTileSafely(i + (flowDir * 2), j);
            int frameXOffset = 0;
            if (tileBefore.type == Type && tileAfter.type != Type && tileBelow.type == Type && tileAbove.type != Type)
            {
                frameXOffset = 54;
            }
            else if ((tileBefore.type == Type && tileAfter.type == Type && tileAbove.type != Type) || (tileAbove.type != Type && tileBefore.type != Type))
            {
                frameXOffset = 0;
            }
            else if (tileAbove.type == Type && tileBelow.type == Type && !(tileBefore.type == Type && tileAfter.type == Type))
            {
                frameXOffset = 36;
            }
            else if (CalcCorrectTile(i, j - 1, flowDir) == 54 || CalcCorrectTile(i, j - 1, flowDir) == 36)
            {
                frameXOffset = 72;
            }
            /*
            else if (tileBefore.type != Type && tileAfter.type == Type && tileAbove.type == Type)
            {
                frameXOffset = 72;
            }
            */
            else if (tileAbove.type == Type && tileBelow.type != Type)
            {
                frameXOffset = 18;
            }
            else
            {
                frameXOffset = 18;
            }
            return frameXOffset;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {

            if (++frameCounter >= Main.rand.Next(6, 9))
            {
                frameCounter = 0;
                frame = ++frame % 4;
            }
        }

    }
}

