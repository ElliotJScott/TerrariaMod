using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSailor
{
    static class BiomeNameMapping
    {
        public static Dictionary<Planet, string> planetNames = new Dictionary<Planet, string>();
        public static Dictionary<Biomes, string> biomeNames = new Dictionary<Biomes, string>();
        public static void Initialise()
        {
            CreatePlanetMappings();
            CreateBiomeMappings();
        }
        public static void LearnDesertName()
        {
            planetNames[Planet.Desert] = "DesertName";
            biomeNames[Biomes.DesertOverworld] = "DesertOverworld";
        }
        public static void CreatePlanetMappings()
        {
            planetNames[Planet.AsteroidBelt] = "AsteroidBeltName";
            planetNames[Planet.Desert] = "Unknown Planet"; //
            planetNames[Planet.End] = ""; //
            planetNames[Planet.Floating] = "FloatingName";
            planetNames[Planet.Ice] = "IcePlanetName";
            planetNames[Planet.IceMoon] = "IceMoonName";
            planetNames[Planet.InFlight] = ""; //
            planetNames[Planet.Intro] = ""; //
            planetNames[Planet.Jungle] = "JungleName";
            planetNames[Planet.Lava] = "LavaName";
            planetNames[Planet.Ocean] = "OceanName";
            planetNames[Planet.SpaceMarket] = "SpaceMarketName";
            planetNames[Planet.SpacePort] = "SpacePortName";
            planetNames[Planet.Swamp] = "SwampName";
        }
        public static void CreateBiomeMappings()
        {
            biomeNames[Biomes.AsteroidBelt] = "";
            biomeNames[Biomes.AsteroidField] = "AstFieldName";
            biomeNames[Biomes.DesertCaves] = "";
            biomeNames[Biomes.DesertOverworld] = "Unknown Location";
        }
    }
}
