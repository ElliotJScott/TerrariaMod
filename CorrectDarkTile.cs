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
    class CorrectDarkTile : GlobalTile
    {
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            
            float b = Lighting.Brightness(i, j);
            if (b <= 0.05 || type == TileID.Dirt)
            {
                Vector2 screenLoc = new Vector2(192, 192) + (16f * new Vector2(i, j)) - Main.screenPosition;
                spriteBatch.Draw(ModContent.GetInstance<StarSailorMod>().pixel, new Rectangle((int)screenLoc.X, (int)screenLoc.Y, 16, 16), Color.Black);
            }
            return base.PreDraw(i, j, type, spriteBatch);
        }
    }
}
