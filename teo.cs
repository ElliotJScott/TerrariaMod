using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using teo.Buffs;
using teo.GUI;
using teo.Mounts;
using teo.Skies;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace teo
{
    class TEO : Mod
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
        public List<SpeechBubble> speechBubbles = new List<SpeechBubble>();
        public int rocketGuiPageNum = 0;
        public LaunchGuiButton exitButton;
        public LaunchGuiButton launchButton;
        public LaunchGuiButton nameButton;
        public LaunchGuiButton[] locationButtons = new LaunchGuiButton[11];
        string oldText = "wew";
        MouseState currentState;
        MouseState oldState;
        public bool updateButtonsFlag = false;
        public bool inLaunchGui;
        public TEO()
        {
            currentState = Mouse.GetState();
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                
                Filters.Scene["TEO:SkySpace"] = new Filter(new ScreenShaderData("FilterMoonLord"), EffectPriority.Medium);
                SkyManager.Instance["TEO:SkySpace"] = new SpaceSky();
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
        public string inputText(string oldString)
        {
            Main.inputTextEnter = false;
            Main.inputTextEscape = false;
            string text = oldString;
            string text2 = "";
            if (text == null)
            {
                text = "";
            }
            bool flag = false;
            if (Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl) || Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightControl))
            {
                Main.NewText("Test");
                if (Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z) && !Main.oldInputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z))
                {
                    text = "";
                }
                else if (Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X) && !Main.oldInputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X))
                {
                    Platform.Current.Clipboard = oldString;

                    text = "";
                }
                else if ((Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C) && !Main.oldInputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.C)) || (Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Insert) && !Main.oldInputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Insert)))
                {
                    Platform.Current.Clipboard = oldString;
                }
                else if (Main.inputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.V) && !Main.oldInputText.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.V))
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
                    Main.NewText(str);
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
            //Main.NewText(pressedKeys.Length + " " + pressedKeys2.Length);
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
                    TextSnippet[] array = ChatManager.ParseMessage(text, Microsoft.Xna.Framework.Color.White).ToArray();
                    text = ((!array[array.Length - 1].DeleteWhole) ? text.Substring(0, text.Length - 1) : text.Substring(0, text.Length - array[array.Length - 1].TextOriginal.Length));
                }
            }
            return text;
        }
        public override void UpdateUI(GameTime gameTime)
        {
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
            base.UpdateUI(gameTime);
        }
        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            foreach (SpeechBubble sp in speechBubbles)
                sp.Draw(spriteBatch);
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
                oldText = inputText(oldText);
                //spriteBatch.End();
                //Main.NewText(oldText);
            }
            base.PostDrawInterface(spriteBatch);
        }
        public string GenerateName()
        {
            Random rnd = new Random();
            string s = words[rnd.Next(words.Length)] +  " " + words[rnd.Next(words.Length)] + " " + rnd.Next(1000);
            foreach (LaunchPoint l in ModContent.GetInstance<LaunchPointManager>().GetLaunchPoints())
            {
                if (l.name == s) return GenerateName();
            }
            return s;
        }
    }
}
