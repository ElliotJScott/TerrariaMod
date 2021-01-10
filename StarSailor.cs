using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using StarSailor.GUI;
using StarSailor.Mounts;
using StarSailor.Skies;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using StarSailor.Backgrounds;
using StarSailor.Sequencing;
using StarSailor.NPCs;
using StarSailor.Projectiles;
using StarSailor.Tiles;
using Terraria.ID;
using StarSailor.Dimensions;

namespace StarSailor
{
    class StarSailorMod : Mod
    {

        private string[] words = {"area","book","business","case","child","company","country","day","eye","fact","family","government","group","hand","home","job","life","lot","man","money",
"month","mother","mister","night","number","part","people","place","point","problem","program","question","right","room","school","state","story","student","study","system","thing","time","water",
"way","week","woman","word","work","world","year"};
        public static int[,] QRCode = new int[,]

        {
            {1,1,1,1,1,1,1,0,1,1,0,0,1,0,1,1,1,1,1,1,1 },
            {1,0,0,0,0,0,1,0,0,0,0,1,1,0,1,0,0,0,0,0,1 },
            {1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,0,1,0,1,1,0,1,1,0,1,0,1,1,1,0,1 },
            {1,0,1,1,1,0,1,0,1,1,1,1,0,0,1,0,1,1,1,0,1 },
            {1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,0,1,0,1,0,1,0,1,1,1,1,1,1,1 },
            {0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0 },
            {1,1,1,1,0,0,1,0,1,0,1,0,0,1,0,0,1,1,1,0,1 },
            {0,1,0,1,0,1,0,0,0,1,0,1,1,1,0,1,1,1,1,0,1 },
            {1,1,0,1,1,0,1,0,1,0,1,0,0,1,0,0,0,1,0,1,0 },
            {1,0,0,0,0,1,0,1,0,0,1,0,1,1,1,0,0,1,1,1,0 },
            {0,0,1,0,1,1,1,1,0,0,1,1,1,1,0,0,0,1,1,0,0 },
            {0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,0,0,0,0,0,1 },
            {1,1,1,1,1,1,1,0,0,0,1,0,0,1,1,0,1,0,1,0,0 },
            {1,0,0,0,0,0,1,0,0,0,0,1,0,0,0,1,1,0,1,0,1 },
            {1,0,1,1,1,0,1,0,0,1,0,0,0,0,1,0,0,0,0,0,0 },
            {1,0,1,1,1,0,1,0,1,1,1,1,1,0,0,1,1,1,0,0,0 },
            {1,0,1,1,1,0,1,0,1,0,0,1,0,1,0,0,0,0,0,0,0 },
            {1,0,0,0,0,0,1,0,1,0,1,1,1,0,1,0,1,0,0,1,0 },
            {1,1,1,1,1,1,1,0,1,1,0,1,0,1,0,1,1,0,0,0,0 },
        };
        public bool inputEnabled = true;
        public List<ShieldChargerBarrier> barriers = new List<ShieldChargerBarrier>();
        public Texture2D pixel;
        public Texture2D box;
        public Texture2D boxInside;
        public Texture2D mainAbove, iceAbove, astAbove, jungAbove;
        public Texture2D asteroidBeltPlanet, asteroidBeltMoon;
        public Texture2D boatTex;
        public Texture2D ropeTex;
        public Texture2D overworldSky, lavaSky, iceSky;
        public Texture2D smallStar;
        public Texture2D sun0a, sun0b, sun1, sun2;
        public Texture2D corner;
        public Texture2D sun0Glow;
        public Texture2D mechGatlingGun;
        public Texture2D friendlyTentacle;
        public Texture2D cryoCloud;
        public Texture2D orbGlow;
        public Texture2D orbVmaxGlow;
        public Texture2D orbConnector;
        public bool haveInitSequences = false;
        public SequenceQueue sequence = new SequenceQueue(Sequence.None);
        public List<SequenceTrigger> triggers = new List<SequenceTrigger>();
        public List<CustomStar> stars = new List<CustomStar>();
        public (Vector2, int)[] forbiddenStarRegions = new (Vector2, int)[0];
        public int targetStarNum = 0;
        public int currentStarNum = 0;
        public Distribution currentDistribution;
        public Shop currentShop;
        #region bgTexs
        public Texture2D desTreeCaveMid, desTreeCaveFront, desTreeCaveMid2, desTreeCaveMid3, desTreeCaveBack;
        public Texture2D iceCaveFront, iceCaveMid1, iceCaveMid2, iceCaveMid3, iceCaveBack;
        public Texture2D desOverMid, desOverFront;
        public Texture2D oceanFront;
        public Texture2D desertTownFront, desertTownMid, desertTownFar;
        public Texture2D jungleNear, jungleMid, jungleFar;
        public Texture2D denseJungleNear, denseJungleMid, denseJungleFar;
        public Texture2D jungleValleyNear, jungleValleyMid, jungleValleyFar;
        public Texture2D iceOverworldNear, iceOverworldMid, iceOverworldFar;
        public Texture2D lavaMid, lavaMid2, lavaBack;
        #endregion
        public List<WorldSpeechItem> speechBubbles = new List<WorldSpeechItem>();
        public int rocketGuiPageNum = 0;
        public LaunchGuiButton exitButton;
        public LaunchGuiButton launchButton;
        public LaunchGuiButton nameButton;
        //public Dictionary<Biomes, int> biomeSunlightStrength = new Dictionary<Biomes, int>();
        public List<Vector2> rapidWaterRedraws = new List<Vector2>();
        public LaunchGuiButton[] locationButtons = new LaunchGuiButton[11];
        string oldText = "wew";
        public KeyboardState oldKeyState;
        public KeyboardState currentKeyState;
        public MouseState oldMouseState;
        public MouseState newMouseState;
        public bool updateButtonsFlag = false;
        public bool inLaunchGui;
        //Dictionary<Biomes, Color> grassColorMappings = new Dictionary<Biomes, Color>();
        //public List<BiomeLocationMapping> biomeLocMappings = new List<BiomeLocationMapping>();

        public StarSailorMod()
        {
            //CharacterLocationMapping.Initialise();
            //PopulateSunlightStrength();
            //SetUpBiomeMappings();
            //SetUpColorMappings();
            currentKeyState = Keyboard.GetState();
            newMouseState = Mouse.GetState();
            //Main.tileMerge[TileID.Marble][TileID.Mud] = false;
            //Main.tileMerge[TileID.MarbleBlock][TileID.Mud] = false;
            //Main.tileMerge[TileID.Mud][TileID.Marble] = false;
            //Main.tileMerge[TileID.Mud][TileID.MarbleBlock] = false;
        }
        #region commented out shit
        /*
        void SetUpColorMappings()
        {
            grassColorMappings[Biomes.FloatingU0] = Color.PaleVioletRed;
            grassColorMappings[Biomes.FloatingU1] = Color.MintCream;
            grassColorMappings[Biomes.FloatingU2] = Color.Aquamarine;
            grassColorMappings[Biomes.FloatingU3] = Color.DeepPink;
            grassColorMappings[Biomes.FloatingU4] = Color.SandyBrown;
            grassColorMappings[Biomes.FloatingU5] = Color.Crimson;
            grassColorMappings[Biomes.FloatingU6] = Color.PapayaWhip;
            grassColorMappings[Biomes.FloatingU7] = Color.LightCoral;
            grassColorMappings[Biomes.FloatingU8] = Color.LemonChiffon;
            grassColorMappings[Biomes.FloatingU9] = Color.BlueViolet;
            grassColorMappings[Biomes.FloatingUTunnel] = Color.Coral;
        }
        public Color GetGrassColor(Biomes b)
        {
            if (grassColorMappings.ContainsKey(b)) return grassColorMappings[b];
            else return Color.White;
        }
        public void SetUpBiomeMappings()
        {
            biomeLocMappings.Clear();

            //Desert
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(201, 1572), new Vector2(1741, 1812), Biomes.DesertOverworld, Planet.Desert, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(313, 1843), new Vector2(1294, 2047), Biomes.DesertTown, Planet.Desert, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1899, 1640), new Vector2(2157, 1784), Biomes.DesertTreeCave, Planet.Desert, 2));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1317, 1881), new Vector2(1668, 2038), Biomes.DesertMoleCave, Planet.Desert, 2));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(192, 1554), new Vector2(2187, 2069), Biomes.DesertCaves, Planet.Desert, 0));

            //Jungle
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(150, 1051), new Vector2(2187, 1551), Biomes.JungleOverworld, Planet.Jungle, 0));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1797, 1098), new Vector2(2187, 1551), Biomes.JungleDeep, Planet.Jungle, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(150, 1051), new Vector2(548, 1465), Biomes.JungleRiver, Planet.Jungle, 1));

            //Ocean
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1340, 771), new Vector2(1620, 979), Biomes.OceanKrakenCave, Planet.Ocean, 2));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1460, 542), new Vector2(2159, 1038), Biomes.OceanEast, Planet.Ocean, 0));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(228, 502), new Vector2(1509, 1038), Biomes.OceanWest, Planet.Ocean, 0));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(1608, 774), new Vector2(2126, 1038), Biomes.OceanCaves, Planet.Ocean, 1));

            //Ice
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2261, 599), new Vector2(3390, 967), Biomes.IceOverworld, Planet.Ice, 0));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3382, 771), new Vector2(3752, 963), Biomes.IceCaveEntry, Planet.Ice, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3560, 642), new Vector2(3887, 755), Biomes.IceCaveTown, Planet.Ice, 3));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3749, 740), new Vector2(3998, 996), Biomes.IceCaveDeep, Planet.Ice, 2));

            //Floating
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2190, 1554), new Vector2(4174, 2070), Biomes.FloatingOverworld, Planet.Floating, 0));

            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2277, 1954), new Vector2(2382, 2015), Biomes.FloatingU0, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2461, 1937), new Vector2(2561, 1982), Biomes.FloatingU1, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2594, 1900), new Vector2(2752, 1965), Biomes.FloatingU2, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2810, 1872), new Vector2(2914, 1930), Biomes.FloatingU3, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2930, 1815), new Vector2(3038, 1907), Biomes.FloatingU4, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3045, 1773), new Vector2(3195, 1860), Biomes.FloatingU5, Planet.Floating, 1));

            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3045, 1760), new Vector2(3599, 1842), Biomes.FloatingU6, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3637, 1815), new Vector2(3757, 1880), Biomes.FloatingU7, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3786, 1836), new Vector2(3973, 1909), Biomes.FloatingU8, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3993, 1855), new Vector2(4089, 1897), Biomes.FloatingU9, Planet.Floating, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3550, 1867), new Vector2(3779, 2048), Biomes.FloatingUTunnel, Planet.Floating, 2));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(3467, 1933), new Vector2(3568, 2005), Biomes.FloatingUTown, Planet.Floating, 3));

            //Lava
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(2195, 1019), new Vector2(4181, 1533), Biomes.LavaOverworld, Planet.Lava, 1));

            //AsteroidBelt
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(4100, 497), new Vector2(6056, 1015), Biomes.AsteroidBelt, Planet.AsteroidBelt, 1));
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(6032, 249), new Vector2(8117, 1557), Biomes.AsteroidField, Planet.AsteroidBelt, 0));

            //Spaceport
            biomeLocMappings.Add(new BiomeLocationMapping(new Vector2(4188, 1035), new Vector2(6028, 1555), Biomes.SpacePort, Planet.SpacePort, 0));

            //Misc
            biomeLocMappings.Add(new BiomeLocationMapping(new Rectangle(4656, 200, 200, 100), Biomes.Intro, Planet.Intro, 0));
        }
        public (Biomes, Planet) GetBiomePlanet(Vector2 loc)
        {
            List<BiomeLocationMapping> valids = new List<BiomeLocationMapping>();
            foreach (BiomeLocationMapping b in biomeLocMappings)
                if (b.CheckPlayerSatisfies(loc))
                    valids.Add(b);
            valids.Sort();

            if (valids.Count > 0)
            {
                return (valids[0].biome, valids[0].planet);
            }
            else return (Biomes.InFlight, Planet.InFlight);
        }
        public void PopulateSunlightStrength()
        {
            biomeSunlightStrength.Add(Biomes.DesertCaves, 210);
            biomeSunlightStrength.Add(Biomes.DesertOverworld, 150);
            biomeSunlightStrength.Add(Biomes.DesertTown, 150);
            biomeSunlightStrength.Add(Biomes.DesertTreeCave, 63);
            biomeSunlightStrength.Add(Biomes.DesertMoleCave, 63);
            int underwDarkness = 63;
            biomeSunlightStrength.Add(Biomes.FloatingU0, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU1, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU2, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU3, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU4, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU5, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU6, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU7, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU8, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingU9, underwDarkness);
            biomeSunlightStrength.Add(Biomes.FloatingUTown, underwDarkness);
            biomeSunlightStrength.Add(Biomes.IceCaveTown, 63);
            biomeSunlightStrength.Add(Biomes.AsteroidBelt, 255);
            biomeSunlightStrength.Add(Biomes.AsteroidField, 255);

            biomeSunlightStrength.Add(Biomes.LavaOverworld, 255);
        }
        */
        #endregion
        public void RemoveSpeechBubble(SpeechBubble bubble)
        {
            for (int i = 0; i < speechBubbles.Count; i++)
            {
                if (speechBubbles[i].MatchBubble(bubble))
                {
                    speechBubbles.RemoveAt(i);
                    return;
                }
            }
        }
        public void GenerateStars(int numStars, Distribution dist)
        {
            stars.Clear();
            for (int i = 0; i < numStars; i++)
            {
                stars.Add(CustomStar.CreateNewStar(Main.screenHeight, dist, forbiddenStarRegions));
            }
        }
        public void DrawStars(SpriteBatch sb, params (Vector2, int)[] forbiddenRegions)
        {
            foreach (CustomStar s in stars) s.Update();
            foreach (CustomStar s in stars) s.Draw(sb);
        }
        public void DrawStars(SpriteBatch sb, int vel, params (Vector2, int)[] forbiddenRegions)
        {
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].Update();
                stars[i].position.X -= vel;
                if (stars[i].position.X < 20)
                {
                    CustomStar p = CustomStar.CreateNewStar(Main.screenHeight, currentDistribution, forbiddenRegions);
                    p.position.X = stars[i].position.X + Main.screenWidth + 10;
                    stars[i] = p;
                }
            }

            foreach (CustomStar s in stars) s.DrawIfNotForbidden(sb, forbiddenRegions);
        }

        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            int str = 255;
            /*
            try
            {
                Biomes b = Main.LocalPlayer.GetModPlayer<PlayerFixer>().biome;

                if (biomeSunlightStrength.TryGetValue(b, out str))
                {
                    ;
                }
                else str = 255;
            }
            catch { }
            */

            tileColor = ModContent.GetInstance<DimensionManager>().GetSunlight();
            backgroundColor = new Color(str, str, str);
            base.ModifySunLightColor(ref tileColor, ref backgroundColor);
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {

                pixel = GetTexture("GUI/pixel");
                //Main.backgroundTexture[0] = GetTexture("Skies/OverworldSky");
                overworldSky = GetTexture("Skies/OverworldSky");
                lavaSky = GetTexture("Skies/LavaSky");
                iceSky = GetTexture("Skies/IceSky");
                boatTex = GetTexture("Tiles/DisplayBoat");
                ropeTex = GetTexture("Tiles/BoatRope");
                sun0a = GetTexture("Skies/Star_0a");
                sun1 = GetTexture("Skies/Star_1");
                sun2 = GetTexture("Skies/Star_2");
                box = GetTexture("GUI/Box");
                boxInside = GetTexture("GUI/BoxInside");
                sun0Glow = GetTexture("Skies/Star_0Glow");
                mainAbove = GetTexture("Skies/Homeworld");
                iceAbove = GetTexture("Skies/IcePlanet");
                astAbove = GetTexture("Skies/AsteroidBeltSpace");
                jungAbove = GetTexture("Skies/DesertPlanet");
                smallStar = GetTexture("Skies/Star");
                mechGatlingGun = GetTexture("Mounts/MechGatlingGun");
                corner = GetTexture("GUI/spBubble");
                friendlyTentacle = GetTexture("Projectiles/Tentacle");
                cryoCloud = GetTexture("Projectiles/CryogunCloud");
                orbGlow = GetTexture("Projectiles/OrbOfVitalityEffect");
                orbVmaxGlow = GetTexture("Projectiles/OrbOfVitalityVMaxEffect");
                orbConnector = GetTexture("Projectiles/OrbConnector");
                asteroidBeltPlanet = GetTexture("Skies/Planet2");
                asteroidBeltMoon = GetTexture("Skies/AsteroidBeltMoon");
                #region bgTexs
                desTreeCaveMid = GetTexture("Backgrounds/DesertTreeCaveMid");
                desTreeCaveFront = GetTexture("Backgrounds/DesertTreeCaveFront");
                desTreeCaveMid2 = GetTexture("Backgrounds/DesertTreeCaveMid2");
                desTreeCaveMid3 = GetTexture("Backgrounds/DesertTreeCaveMid3");
                desTreeCaveBack = GetTexture("Backgrounds/DesertCaveBack");
                desOverFront = GetTexture("Backgrounds/DesertAboveFront");
                desOverMid = GetTexture("Backgrounds/DesertAboveMid");
                desertTownFar = GetTexture("Backgrounds/DesertTownFar");
                desertTownMid = GetTexture("Backgrounds/DesertTownMid");
                desertTownFront = GetTexture("Backgrounds/DesertTownNear");
                jungleFar = GetTexture("Backgrounds/JungleFar");
                jungleMid = GetTexture("Backgrounds/JungleMid");
                jungleNear = GetTexture("Backgrounds/JungleNear");
                jungleValleyFar = GetTexture("Backgrounds/JungleValleyFar");
                jungleValleyMid = GetTexture("Backgrounds/JungleValleyMid");
                jungleValleyNear = GetTexture("Backgrounds/JungleValleyNear");
                denseJungleFar = GetTexture("Backgrounds/DenseJungleFar");
                denseJungleMid = GetTexture("Backgrounds/DenseJungleMid");
                denseJungleNear = GetTexture("Backgrounds/DenseJungleNear");
                oceanFront = GetTexture("Backgrounds/Ocean");
                lavaMid = GetTexture("Backgrounds/LavaMid");
                lavaMid2 = GetTexture("Backgrounds/Lavamid2");
                lavaBack = GetTexture("Backgrounds/LavaBack");
                iceOverworldNear = GetTexture("Backgrounds/IceNear");
                iceOverworldMid = GetTexture("Backgrounds/IceMid");
                iceOverworldFar = GetTexture("Backgrounds/IceFar");
                iceCaveFront = GetTexture("Backgrounds/IceCaveFront");
                iceCaveMid1 = GetTexture("Backgrounds/IceCaveMid1");
                iceCaveMid2 = GetTexture("Backgrounds/IceCaveMid2");
                iceCaveMid3 = GetTexture("Backgrounds/IceCaveMid3");
                iceCaveBack = GetTexture("Backgrounds/IceCaveBack");
                #endregion
                Filters.Scene["StarSailorMod:SkySpace"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium); //write an empty effect to put in here
                SkyManager.Instance["StarSailorMod:SkySpace"] = new SpaceSky();
                Filters.Scene["StarSailorMod:SkyOverworld"] = Filters.Scene["WaterDistortion"];
                SkyManager.Instance["StarSailorMod:SkyOverworld"] = new OverworldSky();
                GameShaders.Misc["StarSailor:OrbEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/OrbEffect")), "OrbEffect");
                GameShaders.Misc["StarSailor:FloatingUnderTownEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/FloatingUnderTownEffect")), "FloatingUnderTownEffect");
                GameShaders.Misc["StarSailor:FloatingUnderTreeEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/FloatingUnderTreeEffect")), "FloatingUnderTreeEffect");
                GameShaders.Misc["StarSailor:NoiseEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/NoiseEffect")), "NoiseEffect");
                GameShaders.Misc["StarSailor:OrbVMaxEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/OrbVMaxEffect")), "OrbVMaxEffect");
                GameShaders.Misc["StarSailor:OrbConnectorEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/OrbConnectorEffect")), "OrbConnectorEffect");
                GameShaders.Misc["StarSailor:StarNoiseEffect"] = new MiscShaderData(new Ref<Effect>(GetEffect("Effects/StarNoiseEffect")), "StarNoiseEffect");
                Ref<Effect> starglowRef = new Ref<Effect>(GetEffect("Effects/StarGlow"));
                GameShaders.Misc["StarShader"] = new MiscShaderData(starglowRef, "StarShader");
                Filters.Scene["AmpedEffect"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/AmpedEffect")), "AmpedEffect"), EffectPriority.Medium);
                Filters.Scene["StarSailorMod:SkyLava"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/FireEffect")), "FireEffect"), EffectPriority.Medium);
                //Filters.Scene["StarSailorMod:SkyLava"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium); //write an empty effect to put in here

                SkyManager.Instance["StarSailorMod:SkyLava"] = new LavaSky();

            }
            base.Load();
        }
        public void ExitRocketGui()
        {
            inLaunchGui = false;
            Main.player[Main.myPlayer].mount.Dismount(Main.player[Main.myPlayer]);
            Main.blockInput = false;
            rocketGuiPageNum = 0;
        }
        public string InputText(string oldString)
        {
            Main.NewText(oldString);
            Main.inputTextEnter = false;
            Main.inputTextEscape = false;
            string text = oldString;
            string text2 = "";
            if (text == null)
            {
                text = "";
            }
            bool flag = false;
            if (Main.inputText.IsKeyDown(Keys.LeftControl) || Main.inputText.IsKeyDown(Keys.RightControl))
            {

                if (Main.inputText.IsKeyDown(Keys.Z) && !Main.oldInputText.IsKeyDown(Keys.Z))
                {
                    text = "";
                }
                else if (Main.inputText.IsKeyDown(Keys.X) && !Main.oldInputText.IsKeyDown(Keys.X))
                {
                    Platform.Current.Clipboard = oldString;

                    text = "";
                }
                else if ((Main.inputText.IsKeyDown(Keys.C) && !Main.oldInputText.IsKeyDown(Keys.C)) || (Main.inputText.IsKeyDown(Keys.Insert) && !Main.oldInputText.IsKeyDown(Keys.Insert)))
                {
                    Platform.Current.Clipboard = oldString;
                }
                else if (Main.inputText.IsKeyDown(Keys.V) && !Main.oldInputText.IsKeyDown(Keys.V))
                {
                    text2 += Platform.Current.Clipboard;
                }
            }
            else
            {
                if (Main.inputText.PressingShift())
                {
                    if (Main.inputText.IsKeyDown(Keys.Delete) && !Main.oldInputText.IsKeyDown(Keys.Delete))
                    {
                        Platform.Current.Clipboard = oldString;
                        text = "";
                    }
                    if (Main.inputText.IsKeyDown(Keys.Insert) && !Main.oldInputText.IsKeyDown(Keys.Insert))
                    {
                        string text3 = Platform.Current.Clipboard;
                        for (int i = 0; i < text3.Length; i++)
                        {
                            if (text3[i] < ' ' || text3[i] == '\u007f')
                            {
                                text3 = text3.Replace(string.Concat(text3[i--]), "");
                            }
                        }
                        text2 += text3;
                    }
                }
                for (int j = 0; j < Main.keyCount; j++)
                {

                    int num2 = Main.keyInt[j];
                    string str = Main.keyString[j];
                    if (num2 == 13)
                    {
                        Main.inputTextEnter = true;
                    }
                    else if (num2 == 27)
                    {
                        Main.inputTextEscape = true;
                    }
                    else if (num2 >= 32 && num2 != 127)
                    {
                        text2 += str;
                    }
                }
            }
            Main.keyCount = 0;
            text += text2;
            Main.oldInputText = Main.inputText;
            Main.inputText = Keyboard.GetState();
            Keys[] pressedKeys = Main.inputText.GetPressedKeys();
            Keys[] pressedKeys2 = Main.oldInputText.GetPressedKeys();
            if (Main.inputText.IsKeyDown(Keys.Back) && Main.oldInputText.IsKeyDown(Keys.Back))
            {
                //
            }
            else
            {
                //
            }
            for (int k = 0; k < pressedKeys.Length; k++)
            {
                bool flag2 = true;
                for (int l = 0; l < pressedKeys2.Length; l++)
                {
                    if (pressedKeys[k] == pressedKeys2[l])
                    {
                        flag2 = false;
                    }
                }
                string a = string.Concat(pressedKeys[k]);
                if (a == "Back" && (flag2 | flag) && text.Length > 0)
                {
                    TextSnippet[] array = ChatManager.ParseMessage(text, Color.White).ToArray();
                    text = ((!array[array.Length - 1].DeleteWhole) ? text.Substring(0, text.Length - 1) : text.Substring(0, text.Length - array[array.Length - 1].TextOriginal.Length));
                }
            }
            
            return text;
        }
        public void UpdateFireEffect()
        {
            return;
            /*
            if (Main.netMode != NetmodeID.Server) // This all needs to happen client-side!
            {
                PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
                if (Filters.Scene["FireEffect"].IsActive() && pf.GetCurrentBiomePlanet().Item1 != Biomes.LavaOverworld)
                {
                    Filters.Scene.Deactivate("FireEffect");
                }
                else if (!Filters.Scene["FireEffect"].IsActive() && pf.GetCurrentBiomePlanet().Item1 == Biomes.LavaOverworld)
                {
                    Filters.Scene.Activate("FireEffect");
                }


            }
            */
        }
        public override void PostUpdateEverything()
        {
            if (currentStarNum != targetStarNum)
            {
                GenerateStars(targetStarNum, currentDistribution);
                currentStarNum = targetStarNum;
            }
            UpdateFireEffect();
            base.PostUpdateEverything();
        }
        public override void UpdateUI(GameTime gameTime)
        {
            newMouseState = Mouse.GetState();
            foreach (SequenceTrigger t in triggers)
            {
                t.Update(Main.LocalPlayer);
            }
            sequence.Update();
            foreach (WorldSpeechItem s in speechBubbles) s.Update();
            if (currentShop != null) currentShop.Update();
            oldMouseState = newMouseState;
            /*
            int prevNumSpeechBubbles = speechBubbles.Count;
            for (int i = 0; i < speechBubbles.Count; i++)
            {
                speechBubbles[i].Update();
                if (speechBubbles.Count != prevNumSpeechBubbles)
                {
                    prevNumSpeechBubbles = speechBubbles.Count;
                    i--;
                }
            }
            */

            base.UpdateUI(gameTime);
        }
        public void InitialiseSequences()
        {
            if (!haveInitSequences)
            {
                try
                {
                    SequenceBuilder.InitialiseSequences(Main.LocalPlayer);
                    haveInitSequences = true;
                }
                catch
                {
                    haveInitSequences = false;
                }
            }
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {

            DrawRapidWater(spriteBatch);
            GravDisplay.Draw(spriteBatch);
            if (currentShop != null)  currentShop.Draw(spriteBatch); 
            Main.blockInput = !inputEnabled;
            foreach (WorldSpeechItem s in speechBubbles) s.Draw(spriteBatch);
            if (inLaunchGui)
            {
                Main.blockInput = true;
                if (Main.player[Main.myPlayer].mount.Type != ModContent.GetInstance<Rocket>().Type)
                {
                    ExitRocketGui();
                    return;
                }

                Texture2D menuTex = GetTexture("GUI/LaunchPointMenu");
                Vector2 menuTexPos = new Vector2(-400 + (Main.screenWidth - menuTex.Width) / 2, (Main.screenHeight - menuTex.Height) / 2);
                spriteBatch.Draw(menuTex, new Rectangle((int)menuTexPos.X, (int)menuTexPos.Y, menuTex.Width, menuTex.Height), Color.White);
                Vector2 exitTextLocation = new Vector2(menuTexPos.X + menuTex.Width - 75, menuTexPos.Y + menuTex.Height - 35);

                if (locationButtons.Length == 11 || updateButtonsFlag)
                {
                    updateButtonsFlag = false;
                    List<LaunchPoint> list = ModContent.GetInstance<LaunchPointManager>().GetLaunchPoints(ModContent.GetInstance<LaunchPointManager>().currentLaunchPoint);
                    LaunchPoint[] points = list.GetRange(rocketGuiPageNum * 10, Math.Min(10, list.Count - (rocketGuiPageNum * 10))).ToArray();
                    locationButtons = new LaunchGuiButton[points.Length];
                    exitButton = new LaunchGuiButton("Exit", (int)exitTextLocation.X, (int)exitTextLocation.Y, Function.Exit, this);
                    launchButton = new LaunchGuiButton("Launch", 90 + (int)menuTexPos.X, (int)exitTextLocation.Y, Function.Launch, this);
                    nameButton = new LaunchGuiButton(ModContent.GetInstance<LaunchPointManager>().currentLaunchPoint.name, 225 + (int)menuTexPos.X, 45 + (int)menuTexPos.Y, Function.Name, this);
                    for (int i = 0; i < points.Length; i++)
                    {
                        locationButtons[i] = new LaunchGuiButton(points[i].name, 225 + (int)menuTexPos.X, 125 + (i * 55) + (int)menuTexPos.Y, Function.SelectNewLocation, this, i);
                    }
                }

                bool anActive = false;
                foreach (LaunchGuiButton g in locationButtons)
                {
                    g.Update();
                    g.Draw(spriteBatch);
                    if (g.active) anActive = true;
                }
                exitButton.Update();
                exitButton.Draw(spriteBatch);
                nameButton.Update();
                nameButton.Draw(spriteBatch);
                if (anActive)
                {
                    launchButton.Update();
                    launchButton.Draw(spriteBatch);
                }
                //spriteBatch.DrawString(Main.fontMouseText, "Exit", exitTextLocation, textCol);
                //oldText = InputText(oldText);
                //spriteBatch.End();
                
            }

            
            base.PostDrawInterface(spriteBatch);
        }
        public void DrawRapidWater(SpriteBatch sb)
        {
            foreach (Vector2 v in rapidWaterRedraws)
            {
                Rectangle testRect = new Rectangle((int)v.X, (int)v.Y, 16, 16);
                Rectangle screenrect = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
                //if (testRect.Intersects(screenrect))
                    ModContent.GetInstance<RapidWater>().CorrectDraw((int)v.X, (int)v.Y, sb);
            }
            //rapidWaterRedraws.Clear();
        }
        public string GenerateName()
        {
            Random rnd = new Random();
            string s = words[rnd.Next(words.Length)] + " " + words[rnd.Next(words.Length)] + " " + rnd.Next(1000);
            foreach (LaunchPoint l in ModContent.GetInstance<LaunchPointManager>().GetLaunchPoints())
            {
                if (l.name == s) return GenerateName();
            }
            return s;
        }

    }
    /*
    public struct BiomeLocationMapping : IComparable<BiomeLocationMapping>
    {
        public int priority;
        public Rectangle location;
        public Biomes biome;
        public Planet planet;

        public BiomeLocationMapping(Rectangle loc, Biomes b, Planet pl, int pr)
        {
            location = loc;
            biome = b;
            planet = pl;
            priority = pr;
        }
        public BiomeLocationMapping(Vector2 tl, Vector2 br, Biomes b, Planet pl, int pr)
        {
            location = new Rectangle((int)tl.X, (int)tl.Y, (int)(br.X - tl.X), (int)(br.Y - tl.Y));
            biome = b;
            planet = pl;
            priority = pr;
        }
        public bool CheckPlayerSatisfies(Vector2 pos)
        {
            Rectangle testRect = new Rectangle((int)pos.X, (int)pos.Y, 1, 1);
            return testRect.Intersects(location);
        }

        public int CompareTo(BiomeLocationMapping other)
        {
            return other.priority - priority;
        }
    }
    */

}
