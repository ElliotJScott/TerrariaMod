using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using teo.Mounts;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace teo.Skies
{
    class SpaceSky : CustomSky
    {
        bool _isActive = false;
        float bgStarVelocity = 0f;
        float bgStarPos = 0f;
        float planetPos = 0f;
        float planetVelocity = 0.5f;
        int counter = 0;
        int mode = 0;
        Texture2D tex;
        Texture2D level0Above;
        const int numPlanets = 3;
        const int sideMaskNum = 6;
        Texture2D[] planetSides = new Texture2D[numPlanets];
        Texture2D[] planetSideMasks = new Texture2D[sideMaskNum];
        int sideMaskXOffset = 10;
        int sideMaskYOffset = 12;
        int planetToDraw = 2;
        //Color[] tempColors = { Color.Red, Color.Green, Color.Red, Color.Green, Color.Red, Color.Green, Color.Red, Color.Green };
        float[] sideMaskAlphas = { 0.8f, 0.7f, 0.4f, 0.2f, 0.05f, 0.01f }; 
        public override void OnLoad()
        {
            tex = ModContent.GetInstance<TEO>().GetTexture("Skies/InSpace");
            level0Above = ModContent.GetInstance<TEO>().GetTexture("Skies/Level0AboveEnlarged");
            for (int i = 0; i < numPlanets; i++)
            {
                planetSides[i] = ModContent.GetInstance<TEO>().GetTexture("Skies/planet" + i  + "Side");
            }
            for (int i = 0; i < sideMaskNum; i++)
            {
                planetSideMasks[i] = ModContent.GetInstance<TEO>().GetTexture("Skies/planetAtmMask" + i);
                //sideMaskAlphas[i] = (float)Math.Pow(i+1, -2);
            }

        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            _isActive = false;
        }

        public override Color OnTileColor(Color inColor)
        {
            return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f));
        }
        public override void Reset()
        {
            _isActive = false;
        }

        public override bool IsActive()
        {
            return _isActive;
        }

        public override void Update(GameTime gameTime)
        {
            if (ModContent.GetInstance<Rocket>().spaceAnim)
            {
                bgStarPos = (bgStarPos - bgStarVelocity) % tex.Width;
                planetPos -= planetVelocity;
                counter++;
                if (mode == 0)
                {
                    planetVelocity += 0.015f;
                    bgStarVelocity += 0.002f;
                    if (counter == 200)
                    {
                        mode++;
                        planetToDraw = -1;
                        planetVelocity = 0;
                        counter = 0;
                    }
                }
                else if (mode == 1)
                {
                    if (bgStarVelocity < 30)
                        bgStarVelocity += 1.5f;
                    if (counter == 200)
                        mode++;
                }
                else if (mode == 2)
                {
                    if (bgStarVelocity > 1)
                        bgStarVelocity -= 1.5f;
                    else
                        mode++;
                    planetToDraw = 2;
                    planetPos = planetSides[0].Width;
                    planetVelocity = 2f;
                }
                else if (mode == 3)
                {
                    if (planetPos == 0)
                    {
                        planetVelocity = 0;
                        mode++;
                        counter = 0;
                    }
                    if (bgStarVelocity > 0)
                        bgStarVelocity -= 0.04f;
                    else bgStarVelocity = 0;
                }
                else if (mode == 4 && counter == 30)
                {
                    mode = 0;
                    counter = 0;
                    planetToDraw = 2;
                    planetVelocity = 0.5f;
                    ModContent.GetInstance<Rocket>().bgland = true;
                }
            }
        }
        public override float GetCloudAlpha()
        {
            return 0f;
        }
        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0f && minDepth < 0f)
            {
                spriteBatch.Draw(tex, new Rectangle((int)bgStarPos, 0, tex.Width, tex.Height), Color.White);
                spriteBatch.Draw(tex, new Rectangle(tex.Width + (int)bgStarPos, 0, tex.Width, tex.Height), Color.White);
                spriteBatch.Draw(tex, new Rectangle((2 * tex.Width) + (int)bgStarPos, 0, tex.Width, tex.Height), Color.White);
                spriteBatch.Draw(tex, new Rectangle((int)bgStarPos, tex.Height, tex.Width, tex.Height), Color.White);
                spriteBatch.Draw(tex, new Rectangle(tex.Width + (int)bgStarPos, tex.Height, tex.Width, tex.Height), Color.White);
                spriteBatch.Draw(tex, new Rectangle((2 * tex.Width) + (int)bgStarPos, tex.Height, tex.Width, tex.Height), Color.White);
                //spriteBatch.Draw(level0Above, new Rectangle(-(level0Above.Width - Main.screenWidth) / 2, Main.screenHeight - level0Above.Height, level0Above.Width, level0Above.Height), new Color(125, 125, 125, 125));
                if (mode == 0)
                {
                    DrawPlanet(spriteBatch, planetToDraw, 0, 2 + Main.screenHeight - planetSides[0].Height, false);
                }
                else if (mode >= 3)
                {
                    DrawPlanet(spriteBatch, planetToDraw, Main.screenWidth - planetSides[0].Width, 2 + Main.screenHeight - planetSides[0].Height, true);
                }
            }

        }
        public void DrawPlanet(SpriteBatch spriteBatch, int planetID, int x, int y, bool flip)
        {
            if (planetID != -1)
            {
                spriteBatch.Draw(planetSides[planetID], new Rectangle(x + (int)planetPos, y, planetSides[planetID].Width, planetSides[planetID].Height), null, Color.White, 0, new Vector2(0, 0), flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                for (int i = 0; i < sideMaskNum; i++)
                {
                    spriteBatch.Draw(planetSideMasks[i], new Rectangle(x + (-1 * (flip ? 1:0) * sideMaskXOffset) + (int)planetPos, y - sideMaskYOffset, planetSideMasks[i].Width, planetSideMasks[i].Height), null, Color.White * sideMaskAlphas[i], 0, new Vector2(0, 0), flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                }
            }
        }
    }
}
