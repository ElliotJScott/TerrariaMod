using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor
{
    static class CharacterLocationMapping
    {
        public static Dictionary<string, Vector2> npcLocations = new Dictionary<string, Vector2>();
        public static void Initialise()
        {
            npcLocations["Boris"] = new Vector2(1546, 1767) * 16f;
        }
    }
}
