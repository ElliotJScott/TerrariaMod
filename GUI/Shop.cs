using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using static StarSailor.GUI.GuiHelpers;
using Microsoft.Xna.Framework.Input;
using Terraria.UI.Chat;
using Terraria.ModLoader;
using StarSailor.Items.Weapons;
using System;
using Terraria.ID;
using Terraria.Localization;
using StarSailor.Items.Ammo;
using Terraria.UI;

namespace StarSailor.GUI
{
    class Shop
    {
        //bullets and health items are not to be included in items;
        //items should not be longer than 3 or 4 items 
        public const int numAmmoTypes = 5;
        public ShopButton[] mainItems;
        public AmmoButton[] ammoButtons = new AmmoButton[3 * numAmmoTypes];
        public Shop(ShopItem[] i)
        {

            mainItems = new ShopButton[i.Length];
            for (int k = 0; k < i.Length; k++)
            {
                Rectangle rect = new Rectangle(8 + (-460 + Main.screenWidth / 2), 8 + (108 * k) - 300 + (Main.screenHeight / 2), -16 + 450, 100);
                mainItems[k] = new ShopButton(i[k], rect, 0.8f);
            }
            for (int q = 0; q < numAmmoTypes; q++)
            {
                int ind = q * 3;
                int numWidth = (450 / 4);
                Rectangle r1 = new Rectangle(68 + (Main.screenWidth / 2) + numWidth, 8 + (58 * q) + -300 + (Main.screenHeight / 2), -16 + (450 / 4), 50);
                Rectangle r2 = new Rectangle(68 + (Main.screenWidth / 2) + (2 * numWidth), 8 + (58 * q) + -300 + (Main.screenHeight / 2), -16 + (450 / 4), 50);
                Rectangle r3 = new Rectangle(68 + (Main.screenWidth / 2) + (3 * numWidth), 8 + (58 * q) + -300 + (Main.screenHeight / 2), -16 + (450 / 4), 50);
                ammoButtons[ind] = new AmmoButton(r1, 1, q);
                ammoButtons[ind + 1] = new AmmoButton(r2, 10, q);
                ammoButtons[ind + 2] = new AmmoButton(r3, 100, q);
            }

        }
        public void Draw(SpriteBatch sb)
        {
            DrawBox(sb, new Rectangle(-460 + (Main.screenWidth / 2), -300 + (Main.screenHeight / 2), 450, 600), Color.White, 0.7f);
            DrawBox(sb, new Rectangle(10 + (Main.screenWidth / 2), -300 + (Main.screenHeight / 2), 500, 298), Color.White, 0.7f);
            string text = "Ammo";
            Vector2 vector = Main.fontDeathText.MeasureString(text);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text, new Vector2(260 + (Main.screenWidth / 2), -325 + (Main.screenHeight / 2)), Color.White, 0f, vector / 2f, new Vector2(1f), -1f, 1.5f);
            string text2 = "Main Items";
            Vector2 vector2 = Main.fontDeathText.MeasureString(text2);
            string text3 = "Owned";
            Vector2 vector3 = Main.fontDeathText.MeasureString(text3);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text2, new Vector2(-235 + (Main.screenWidth / 2), -325 + (Main.screenHeight / 2)), Color.White, 0f, vector2 / 2f, new Vector2(1f), -1f, 1.5f);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text3, new Vector2(560 + (Main.screenWidth / 2), -310 + (Main.screenHeight / 2)), Color.White, 0f, vector3 / 2f, new Vector2(0.6f), -1f, 1.5f);

            foreach (ShopButton b in mainItems) b.Draw(sb);
            foreach (AmmoButton b in ammoButtons) b.Draw(sb);
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, GetBankString(), new Vector2(10 + (Main.screenWidth / 2), Main.screenHeight / 2), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);

        }
        public void Update()
        {
            foreach (ShopButton b in mainItems) b.Update();
            foreach (AmmoButton b in ammoButtons) b.Update();
        }
        string GetBankString()
        {
            PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
            string coinsText = "In Bank: ";
            int[] coins = Utils.CoinsSplit(pf.GetMoney());
            if (coins[3] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
            }
            if (coins[2] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
            }
            if (coins[1] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
            }
            if (coins[0] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
            }
            return coinsText;

        }

    }
    class ShopButton
    {
        Rectangle rect;
        ShopItem item;
        float alpha;
        bool intersect = false;
        public ShopButton(ShopItem i, Rectangle r, float a)
        {
            item = i;
            rect = r;
            alpha = a;
        }
        string GetCostString()
        {
            switch (item.resource)
            {
                case Resource.Money:
                    string coinsText = "";
                    int[] coins = Utils.CoinsSplit(item.cost);
                    if (coins[3] > 0)
                    {
                        coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
                    }
                    if (coins[2] > 0)
                    {
                        coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
                    }
                    if (coins[1] > 0)
                    {
                        coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
                    }
                    if (coins[0] > 0)
                    {
                        coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
                    }
                    return coinsText;
                //
                //ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costText, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                default:
                    return "";
            }
        }
        string GetCostMeasureString()
        {
            switch (item.resource)
            {
                case Resource.Money:
                    string coinsText = "";
                    int[] coins = Utils.CoinsSplit(item.cost);
                    if (coins[3] > 0)
                    {
                        coinsText = coinsText + " " + coins[3] + " " + Language.GetTextValue("LegacyInterface.15");
                    }
                    if (coins[2] > 0)
                    {
                        coinsText = coinsText + " " + coins[2] + " " + Language.GetTextValue("LegacyInterface.16");
                    }
                    if (coins[1] > 0)
                    {
                        coinsText = coinsText + " " + coins[1] + " " + Language.GetTextValue("LegacyInterface.17");
                    }
                    if (coins[0] > 0)
                    {
                        coinsText = coinsText + " " + coins[0] + " " + Language.GetTextValue("LegacyInterface.18");
                    }
                    return coinsText;
                //ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
                //ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costText, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                default:
                    return "";
            }
        }
        string GetNameString()
        {
            ModItem mit = ModContent.GetModItem(item.itemID);
            switch (mit.item.rare)
            {
                case ItemRarityID.Blue:
                    return "[c/" + Colors.AlphaDarken(Colors.RarityBlue).Hex3() + ":" + mit.DisplayName.GetTranslation(GameCulture.English) + "] ";
                case ItemRarityID.LightPurple:
                    return "[c/" + Colors.AlphaDarken(Colors.RarityPurple).Hex3() + ":" + mit.DisplayName.GetTranslation(GameCulture.English) + "] ";
                case ItemRarityID.LightRed:
                    return "[c/" + Colors.AlphaDarken(Colors.RarityPink).Hex3() + ":" + mit.DisplayName.GetTranslation(GameCulture.English) + "] ";
                case ItemRarityID.Red:
                    return "[c/" + Colors.AlphaDarken(Colors.RarityRed).Hex3() + ":" + mit.DisplayName.GetTranslation(GameCulture.English) + "] ";
            }
            return "";
        }
        public void Draw(SpriteBatch sb)
        {
            PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
            bool canPurchase = pf.CanPurchase(item.cost, item.resource);
            Color color;
            if (canPurchase)
            {
                Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                if (mouseRect.Intersects(rect))
                {
                    color = Color.LightBlue;
                    if (!intersect && Main.hasFocus)
                    {
                        Main.PlaySound(SoundID.MenuTick, Main.LocalPlayer.position);
                    }
                    intersect = true;
                }
                else
                {
                    intersect = false;
                    color = Color.CornflowerBlue;
                }
            }
            else color = Color.DarkRed;
            ModItem mit = ModContent.GetModItem(item.itemID);
            Texture2D boxTex = ModContent.GetInstance<StarSailorMod>().box;
            Texture2D boxInsideTex = ModContent.GetInstance<StarSailorMod>().boxInside;
            Texture2D itemTex = Main.itemTexture[item.itemID];
            DrawBox(sb, rect, color, alpha);
            sb.Draw(boxTex, new Rectangle(rect.X + 10, rect.Y + (rect.Height / 2) - (boxTex.Height / 2), boxTex.Width, boxTex.Height), Color.White);
            sb.Draw(boxInsideTex, new Rectangle(rect.X + 10, rect.Y + (rect.Height / 2) - (boxTex.Height / 2), boxTex.Width, boxTex.Height), Color.White * 0.9f);
            sb.Draw(itemTex, new Rectangle(rect.X + 10 + (boxTex.Width / 2) - (itemTex.Width / 2), rect.Y + (rect.Height / 2) - (itemTex.Height / 2), itemTex.Width, itemTex.Height), Color.White);
            float scale = 0.3f;
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontMouseText, GetNameString(), new Vector2(rect.X + 20 + boxTex.Width, rect.Y + (rect.Height / 2) - (boxTex.Height / 2)), Color.White, 0f, new Vector2(0), new Vector2(1f), -1f, 1.5f);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, item.description, new Vector2(rect.X + 20 + boxTex.Width, 30 + rect.Y + (rect.Height / 2) - (boxTex.Height / 2)), Color.White, 0f, new Vector2(0), new Vector2(scale), -1f, 1.5f);
            string costString = GetCostString();
            Vector2 dims = Main.fontMouseText.MeasureString(GetCostMeasureString());
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costString, new Vector2(rect.X + rect.Width - dims.X, rect.Y + 5), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);

        }
        public void Update()
        {
            PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
            bool canPurchase = pf.CanPurchase(item.cost, item.resource);
            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            
            if (intersect && Main.hasFocus && sm.newMouseState.LeftButton == ButtonState.Pressed && sm.oldMouseState.LeftButton == ButtonState.Released && canPurchase)
            {
               
                Item.NewItem(Main.LocalPlayer.position, item.itemID);
                if (item.resource == Resource.Money)
                    pf.ChargeCoins(item.cost);
            }
        }
    }
    class AmmoButton
    {
        static int[] ammoPrices = { 10, 20, 200, 500, 50 };
        int index;
        Rectangle rect;
        float alpha;
        int quantity;
        int pricePerAmmo;
        bool intersect = false;

        public AmmoButton(Rectangle r, int q, int i)
        {
            pricePerAmmo = ammoPrices[i];
            quantity = q;
            rect = r;
            alpha = 0.8f;
            index = i;
        }
        int GetQuantity()
        {
            int count = 0;
            int[] ammoIDS = { ModContent.ItemType<NinemmRound>(), ModContent.ItemType<FiveFivemmRound>(), ModContent.ItemType<RocketItem>(), ModContent.ItemType<MeteorBombItem>(), ModContent.ItemType<ShotgunShell>() };
            int id = ammoIDS[index];
            foreach (Item i in Main.LocalPlayer.inventory)
            {
                if (i.type == id) count += i.stack;
            }
            return count;
        }
        public void Draw(SpriteBatch sb)
        {
            PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
            bool canPurchase = pf.CanPurchase(quantity * pricePerAmmo, Resource.Money);
            Color color;
            if (canPurchase)
            {
                Rectangle mouseRect = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1);
                if (mouseRect.Intersects(rect))
                {
                    color = Color.LightBlue;
                    if (!intersect && Main.hasFocus)
                    {
                        Main.PlaySound(SoundID.MenuTick, Main.LocalPlayer.position);
                    }
                    intersect = true;
                }
                else
                {
                    intersect = false;
                    color = Color.CornflowerBlue;
                }
            }
            else color = Color.DarkRed;
            string text = "Buy " + quantity;
            float scale = 0.40f;
            Vector2 vector = Main.fontDeathText.MeasureString(text);
            Vector2 location = new Vector2(rect.X, rect.Y + 4) + (0.5f * new Vector2(rect.Width, rect.Height));
            DrawBox(sb, rect, color, alpha);
            ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, text, new Vector2(0, -10) + location, Color.White, 0f, vector / 2f, new Vector2(scale), -1f, 1.5f);
            int[] ammoIDS = { ModContent.ItemType<NinemmRound>(), ModContent.ItemType<FiveFivemmRound>(), ModContent.ItemType<RocketItem>(), ModContent.ItemType<MeteorBombItem>(), ModContent.ItemType<ShotgunShell>() };
            Vector2 dims = Main.fontMouseText.MeasureString(GetCostMeasureString());
            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, GetCostString(), new Vector2(0, 10) + location, Color.White, 0f, dims/2, Vector2.One, -1f, 2f);

            if (quantity == 1)
            {
                int type1;
                //int typeMax;
                switch (index)
                {
                    case 0:
                        type1 = ModContent.ItemType<PistolV1>();
                        //typeMax = ModContent.ItemType<PistolVMax>();
                        break;
                    case 1:
                        type1 = ModContent.ItemType<AssaultRifleV1>();
                        //typeMax = ModContent.ItemType<AssaultRifleVMax>();
                        break;
                    case 2:
                        type1 = ModContent.ItemType<RocketLauncherV1>();
                        //typeMax = ModContent.ItemType<RocketLauncherVMax>();
                        break;
                    case 3:
                        type1 = ModContent.ItemType<MeteorBombV1>();
                        //typeMax = ModContent.ItemType<MeteorBombVMax>();
                        break;
                    case 4:
                        type1 = ModContent.ItemType<ShotgunV1>();
                        //typeMax = ModContent.ItemType<ShotgunVMax>();
                        break;
                    default:
                        throw new ArgumentException();
                }
                Texture2D tex1 = Main.itemTexture[type1];
                Texture2D texMax = Main.itemTexture[ammoIDS[index]];
                sb.Draw(tex1, new Rectangle(50 + (Main.screenWidth / 2) - (tex1.Width / 2), rect.Y + (int)(0.5f * (rect.Height - tex1.Height)), tex1.Width, tex1.Height), Color.White);
                sb.Draw(texMax, new Rectangle(130 + (Main.screenWidth / 2) - (texMax.Width / 2), rect.Y + (int)(0.5f * (rect.Height - tex1.Height)), texMax.Width, texMax.Height), Color.White);
            }
            else if (quantity == 100)
            {
                ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontDeathText, "x" + GetQuantity(), location + new Vector2(30 + rect.Width, 0), Color.White, 0f, vector / 2, new Vector2(scale), -1f, 1.5f);
            }
        }
        public void Update()
        {
            PlayerFixer pf = Main.LocalPlayer.GetModPlayer<PlayerFixer>();
            StarSailorMod sm = ModContent.GetInstance<StarSailorMod>();
            bool canPurchase = pf.CanPurchase(quantity * pricePerAmmo, Resource.Money);

            if (intersect && Main.hasFocus && sm.newMouseState.LeftButton == ButtonState.Pressed && sm.oldMouseState.LeftButton == ButtonState.Released && canPurchase)
            {
              
                int[] ammoIDS = { ModContent.ItemType<NinemmRound>(), ModContent.ItemType<FiveFivemmRound>(), ModContent.ItemType<RocketItem>(), ModContent.ItemType<MeteorBombItem>(), ModContent.ItemType<ShotgunShell>() };
                Item.NewItem(Main.LocalPlayer.position, ammoIDS[index], quantity);
                pf.ChargeCoins(quantity * pricePerAmmo);
            }
        }
        string GetCostString()
        {

            string coinsText = "";
            int[] coins = Utils.CoinsSplit(quantity * pricePerAmmo);
            if (coins[3] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
            }
            if (coins[2] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
            }
            if (coins[1] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
            }
            if (coins[0] > 0)
            {
                coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
            }
            return coinsText;

        }
        string GetCostMeasureString()
        {

            string coinsText = "";
            int[] coins = Utils.CoinsSplit(quantity * pricePerAmmo);
            if (coins[3] > 0)
            {
                coinsText = coinsText + " " + coins[3] + " " + Language.GetTextValue("LegacyInterface.15");
            }
            if (coins[2] > 0)
            {
                coinsText = coinsText + " " + coins[2] + " " + Language.GetTextValue("LegacyInterface.16");
            }
            if (coins[1] > 0)
            {
                coinsText = coinsText + " " + coins[1] + " " + Language.GetTextValue("LegacyInterface.17");
            }
            if (coins[0] > 0)
            {
                coinsText = coinsText + " " + coins[0] + " " + Language.GetTextValue("LegacyInterface.18");
            }
            return coinsText;


        }
    }
    public enum Resource
    {
        Money,
        Cum
    }
    public struct ShopItem
    {
        public int itemID;
        public int cost;
        public Resource resource;
        public string description;
        public ShopItem(int i, int c, Resource r, string d)
        {
            itemID = i;
            cost = c;
            resource = r;
            description = d;
        }
    }
}
