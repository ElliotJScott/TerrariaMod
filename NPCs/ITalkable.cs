using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor.NPCs
{
    interface ITalkable
    {
        Vector2 GetPosition();
        Vector2 GetScreenPosition();
    }
}
