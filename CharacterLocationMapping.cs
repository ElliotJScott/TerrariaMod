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
            npcLocations["Boris"] = new Vector2(1550, 1769) * 16f;
            npcLocations["Hilda"] = new Vector2(1066, 2027) * 16f;
            npcLocations["Justin"] = new Vector2(842, 2027) * 16f;
            npcLocations["Erin"] = new Vector2(839, 2020) * 16f;
            npcLocations["Daisy"] = new Vector2(991, 2025) * 16f;
            npcLocations["Helen"] = new Vector2(836, 2020) * 16f;
            npcLocations["Sienna"] = new Vector2(994, 2025) * 16f;
            npcLocations["George"] = new Vector2(1028, 2026) * 16f;
            npcLocations["Bert"] = new Vector2(1031, 2019) * 16f;
            npcLocations["Ted"] = new Vector2(1034, 2019) * 16f;
            npcLocations["Alton"] = new Vector2(965, 1153) * 16f;
            npcLocations["Lydia"] = new Vector2(221, 1419) * 16f;
            npcLocations["Clint"] = new Vector2(1994, 1162) * 16f;
            npcLocations["Tanya"] = new Vector2(1522, 562) * 16f;
            npcLocations["Leonard"] = new Vector2(383, 571) * 16f;
            npcLocations["Eleanor"] = new Vector2(5442, 270) * 16f;
            npcLocations["Daniel"] = new Vector2(5467, 284) * 16f;
            npcLocations["Gurean"] = new Vector2(5482, 270) * 16f;
            npcLocations["Orixis"] = new Vector2(5506, 270) * 16f;
            npcLocations["Brax"] = new Vector2(5442, 284) * 16f;
            npcLocations["Klerit"] = new Vector2(5467, 270) * 16f;
            npcLocations["Xem"] = new Vector2(5482, 284) * 16f;
            npcLocations["Yuri"] = new Vector2(5506, 284) * 16f;
        }
    }
}
