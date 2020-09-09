using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    public interface ITalkable
    {
        string GetName();
        Vector2 GetPosition();
        Vector2 GetScreenPosition();
        bool WithinDistance();
        void DrawHeadSpeech(SpriteBatch sb, Rectangle rect);
    }
}
