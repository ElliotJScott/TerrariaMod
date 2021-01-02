using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor
{
    class TileFixer : GlobalTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[TileID.Coralstone] = true;
            base.SetDefaults();
        }
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            return true;
            float b = Lighting.Brightness(i, j);
            if (b <= 0.05 || (type == TileID.Dirt && !Main.tile[i,j].active()))
            {
                Vector2 screenLoc = new Vector2(192, 192) + (16f * new Vector2(i, j)) - Main.screenPosition;
                spriteBatch.Draw(ModContent.GetInstance<StarSailorMod>().pixel, new Rectangle((int)screenLoc.X, (int)screenLoc.Y, 16, 16), Color.Black);
            }
            return base.PreDraw(i, j, type, spriteBatch);
        }
        public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
        {

            if (type == TileID.Coralstone)
            {
                float q = i % 30 >= 15 ? 1f : 0f;
                r = q;
                g = (1 - q) == 0 ? 0.3f : 1f;
                b = 0.1f;
            }
            
            base.ModifyLight(i, j, type, ref r, ref g, ref b);
        }
    }
}
