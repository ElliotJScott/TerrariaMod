using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarSailor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace starsailor.Skies
{
    class OverworldSky : CustomSky
    {

        private bool _isActive;

        public override void OnLoad()
        {
            try
            {
                //Main.sun
                //Main.sunTexture = ModContent.GetInstance<StarSailorMod>().pixel;
            }
            catch { }
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        private float GetIntensity()
        {
            return 1f - Utils.SmoothStep(3000f, 6000f, 200f);
        }


        public override float GetCloudAlpha()
        {
            return 0f;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            _isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            _isActive = false;
        }

        public override void Reset()
        {
            _isActive = false;
        }

        public override bool IsActive()
        {
            return _isActive;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            //Main.NewText(Main.sunModY + " " + Main.sunCircle);
            if (maxDepth >= 0f && minDepth < 0f)
            {
           
                float intensity = GetIntensity();
                //MiscShaderData test = new MiscShaderData(null, "strign");

            }


        }
    }

}
