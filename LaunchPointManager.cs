using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarSailor.Mounts;
using StarSailor.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI.Chat;
using StarSailor.Sequencing;
using StarSailor.Dimensions;

namespace StarSailor
{
    class LaunchPointManager : ModWorld
    {
        private List<LaunchPoint> launchPoints = new List<LaunchPoint>();
        public LaunchPoint currentLaunchPoint;

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Load(TagCompound tag)
        {
            string[] names;
            if (tag.ContainsKey("Names"))
                names = tag.GetList<string>("Names").ToArray();
            else return;
            int[] xPos = tag.GetList<int>("XPos").ToArray();
            int[] yPos = tag.GetList<int>("YPos").ToArray();
            int[] dims = tag.GetList<int>("Dims").ToArray();
            if (names.Length != xPos.Length || names.Length != yPos.Length) return;
            for (int i = 0; i < names.Length; i++)
            {
                AddLaunchPoint(new LaunchPoint(new Vector2(xPos[i], yPos[i]), names[i], mod, (Dimensions.Dimensions)dims[i]));
            }
            base.Load(tag);
        }
        
        public override TagCompound Save()
        {
            //return base.Save();
            LaunchPoint[] points = GetLaunchPoints().ToArray();
            //if (points.Length > 0)
            {
                string[] names = new string[points.Length];
                int[] xPos = new int[points.Length];
                int[] yPos = new int[points.Length];
                int[] dims = new int[points.Length];

                for (int i = 0; i < points.Length; i++)
                {
                    names[i] = points[i].name;
                    xPos[i] = (int)points[i].position.X;
                    yPos[i] = (int)points[i].position.Y;
                    dims[i] = (int)points[i].dimension;
                }


                return new TagCompound
            {
                {"Names", names.ToList()},
                {"XPos", xPos.ToList()},
                {"YPos", yPos.ToList()},
                {"Dims", dims.ToList() }
                    
            };
            }
            //else return base.Save();
            
        }
        
        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);
        }
        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);
        }
        public string AddLaunchPoint(int i, int j, string name, Dimensions.Dimensions dim, bool originFound)
        {

            LaunchPoint l = new LaunchPoint(i, j, name, mod, originFound, dim);
            foreach (LaunchPoint r in launchPoints)
            {
                if (r == l)
                {
                    return r.name;
                }
            }
            return l.name;

        }
        public LaunchPoint SetDestination()
        {
            int index = 0;
            for (int i = 0; i < ((StarSailorMod)mod).locationButtons.Length; i++)
            {
                if (((StarSailorMod)mod).locationButtons[i].active)
                {
                    index = i;
                    break;
                }
            }
            List<LaunchPoint> launches = GetLaunchPoints(currentLaunchPoint);
            LaunchPoint destination = launches[(((StarSailorMod)mod).rocketGuiPageNum * 10) + index];
            return destination;
            //ModContent.GetInstance<Rocket>().destination = destination.position + new Vector2(0, -3);
        }
        public void AddLaunchPoint(LaunchPoint l)
        {
            if (!launchPoints.Contains(l)) launchPoints.Add(l);
        }
        public List<LaunchPoint> GetLaunchPoints()
        {
            List<LaunchPoint> returns = new List<LaunchPoint>();
            for (int i = 0; i < launchPoints.Count; i++)
            {
                switch (launchPoints[i].CheckValidity())
                {
                    case "":
                        returns.Add(launchPoints[i]);
                        break;
                    default:
                        launchPoints.RemoveAt(i);
                        i--;
                        break;
                }
            }
            return returns;
        }
        public List<LaunchPoint> GetLaunchPoints(LaunchPoint current)
        {
            List<LaunchPoint> returns = new List<LaunchPoint>();
            DimensionManager dm = ModContent.GetInstance<DimensionManager>();

            for (int i = 0; i < launchPoints.Count; i++)
            {
                switch (launchPoints[i].CheckValidity())
                {
                    case "":
                        if (launchPoints[i] != current)
                            returns.Add(launchPoints[i]);
                        break;
                    default:
                        launchPoints.RemoveAt(i);
                        i--;
                        break;
                }

            }
            foreach (Dimensions.Dimensions dim in typeof(Dimensions.Dimensions).GetEnumValues())
            {
                if (dm.dimensions[(int)dim].haveDiscovered && dim != dm.currentDimension)
                {
                    
                    bool havePoint = false;
                    foreach (LaunchPoint l in returns)
                    {
                        if (l.dimension == dim) havePoint = true;
                        break;
                    }
                    if (!havePoint)
                    {
                        
                        returns.Add(new LaunchPoint(dm.dimensions[(int)dim].GetDestination(), dm.dimensions[(int)dim].name, mod, dim, true));
                    }
                }
            }
            
            return returns;
        }
        public LaunchPoint GetFromName(string s)
        {
            foreach (LaunchPoint l in launchPoints)
            {
                if (l.name == s) return l;
            }
            throw new ArgumentException();
        }
        public LaunchPoint GetCorrectLaunchPoint(LaunchPoint l)
        {
            foreach (LaunchPoint r in launchPoints)
            {
                if ((r.position - l.position).Length() <= 5f) return r;
            }
            return l;
        }
        public override void PostDrawTiles()
        {
            if (((StarSailorMod)mod).inLaunchGui)
            {
                Main.spriteBatch.Begin();
                DrawRocketShipGui(Main.spriteBatch);
                Main.spriteBatch.End();
            }
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }
        public void DeactivateLocations()
        {
            foreach (LaunchGuiButton g in ((StarSailorMod)mod).locationButtons)
            {
                g.active = false;
            }
        }

        public void DrawRocketShipGui(SpriteBatch spriteBatch)
        {
          
        }
        


    }
    public class LaunchGuiButton
    {
        string text;
        Vector2 location;
        readonly Function fn;
        float scale = 1f;
        Mod mod;
        int? newLocation;
        public bool active = false;
        Color activeColor = Color.DarkSeaGreen;
        Color inactiveColor = Color.White;

        public LaunchGuiButton(string t, int i, int j, Function f, Mod m, int? l = null)
        {
            text = t;
            location = new Vector2(i, j);
            fn = f;
            mod = m;
            newLocation = l;
            if (fn == Function.Name)
            {
                activeColor = Color.OrangeRed;
                inactiveColor = Color.DarkCyan;
            }
        }
        public void Update()
        {
            if (((StarSailorMod)mod).inLaunchGui)
            {
                Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                Vector2 dims = Main.fontDeathText.MeasureString(text);
                Rectangle textRect = new Rectangle((int)location.X - (int)(dims.X / 2f), (int)location.Y - (int)(dims.Y / 2f), (int)dims.X, (int)dims.Y);
                if (mouseRect.Intersects(textRect))
                {
                    HoverUpdate(true);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        int r = DoPress();
                        switch (r)
                        {
                            case -1:
                                ((StarSailorMod)mod).ExitRocketGui();
                                break;
                            case -2:
                                ((StarSailorMod)mod).inLaunchGui = false;
                                //ModContent.GetInstance<Rocket>().takeOffAnimate = true;
                                LaunchPoint destination = ModContent.GetInstance<LaunchPointManager>().SetDestination();
                                SequenceQueue sq = SequenceBuilder.ConstructSpaceSequence(ModContent.GetInstance<DimensionManager>().currentDimension, destination.dimension, Main.LocalPlayer, 16 * (destination.position + new Vector2(0, -3)), destination.needPlatform);
                                sq.Execute();
                                break;
                            case -3:
                                ((StarSailorMod)mod).nameButton.active = !((StarSailorMod)mod).nameButton.active;
                                break;
                            default:
                                bool temp = active;
                                ModContent.GetInstance<LaunchPointManager>().DeactivateLocations();
                                active = !temp;
                                break;
                        }
                    }
                }
                else HoverUpdate(false);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            Vector2 vector = Main.fontDeathText.MeasureString(text);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text, location, active ? activeColor : inactiveColor, 0f, vector / 2f, new Vector2(scale), -1f, 1.5f);
        }
        private void HoverUpdate(bool hovered)
        {
            if (hovered)
            {
                scale += 0.05f;
                if (scale > 1f)
                {
                    scale = 1f;
                }
            }
            else
            {
                scale -= 0.05f;
                if (scale < 0.75f)
                {
                    scale = 0.75f;
                }
            }
        }
        private int DoPress()
        {
            switch (fn)
            {
                case Function.SelectNewLocation:
                    return (int)newLocation;
                case Function.Exit:
                    return -1;
                case Function.Launch:
                    return -2;
                case Function.Name:
                    return -3;
            }
            throw new InvalidOperationException();
        }

    }

    public enum Function
    {
        SelectNewLocation,
        Exit,
        Launch,
        Name,
    }


    public class LaunchPoint
    {
        public Vector2 position;
        public string name;
        Mod mod;
        public bool needPlatform;
        public Dimensions.Dimensions dimension;
        public LaunchPoint(Vector2 v, string n, Mod m, Dimensions.Dimensions dim, bool np = false)
        {
            position = v;
            name = n;
            mod = m;
            dimension = dim;
            needPlatform = np;
        }
        public LaunchPoint(int i, int j, string n, Mod m, bool originFound, Dimensions.Dimensions dim)
        {

            if (originFound)
            {
                position = new Vector2(i, j);
                name = n;
                mod = m;
                dimension = dim;
                needPlatform = false;
            }
            else
            {

                Tile below = Framing.GetTileSafely(i, j - 1);
                Tile above = Framing.GetTileSafely(i, j + 1);
                try
                {
                    int aboveCheck = above.type == ModContent.GetInstance<LaunchConsole>().Type ? 2 : 0;
                    int belowCheck = below.type == ModContent.GetInstance<LaunchConsole>().Type ? 1 : 0;
                    int newJ = j + aboveCheck + belowCheck;
                    position = new Vector2(i, newJ);
                    name = n;
                    mod = m;
                    dimension = dim;
                    needPlatform = false;
                }
                catch
                {
                    Main.NewText(above.type + " " + below.type + " " + n + " " + m.DisplayName);
                }
            }
        }
        public const int width = 5;
        public string CheckValidity()
        {
            //return "";
            if (dimension != ModContent.GetInstance<DimensionManager>().currentDimension) return "";
            string error = "";
            //int width = 5;
            for (int k = -16; k <= 0; k++)
            {
                for (int l = -width; l <= width; l++)
                {
                    Tile tile = Framing.GetTileSafely((int)position.X + l, (int)position.Y + k);
                    if (k == 0)
                    {

                        if (tile.type != ModContent.GetInstance<LaunchPad>().Type)
                        {
                            error = "Launch pad is not wide enough";
                        }
                    }
                    else if ((k == -1 || k == -2) && l == 0)
                    {
                        if (tile.type != ModContent.GetInstance<LaunchConsole>().Type)
                        {
                            error = "Something is very wrong here...and yet, a little bit right";
                        }
                    }
                    else
                    {
                        if (tile.type != 0)
                        {
                            error = "Launch pad is not uncovered";
                        }
                    }

                }
            }
            return error;
            
        }
        public static bool operator ==(LaunchPoint a, LaunchPoint b)
        {
            return a.name == b.name && a.dimension == b.dimension && a.position == b.position;
        }
        public static bool operator !=(LaunchPoint a, LaunchPoint b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            try
            {
                return this == (LaunchPoint)obj;
            }
            catch
            {
                return base.Equals(obj);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
