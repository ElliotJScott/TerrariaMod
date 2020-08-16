using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using static StarSailor.GUI.GuiHelpers;

namespace StarSailor.GUI
{
    class Shop
    {
        //bullets and health items are not to be included in items;
        //items should not be longer than 3 or 4 items 
        public ShopButton[] mainItems;
        public Shop(ShopItem[] i)
        {
            
            mainItems = new ShopButton[i.Length];
            for (int k = 0; k < i.Length; k++)
            {
                Rectangle rect = new Rectangle(50 + (Main.screenWidth / 4), 50 + (250 * k) + (Main.screenHeight / 4), -100 + (Main.screenWidth / 4), 200);
                mainItems[k] = new ShopButton(i[k], rect, Color.IndianRed, 0.8f, ButtonType.MainItem);
            }
            
        }
        public void Draw(SpriteBatch sb)
        {
            DrawBox(sb, new Rectangle(Main.screenWidth / 4, Main.screenHeight / 4, Main.screenWidth / 4, Main.screenHeight / 2), Color.Green, 0.7f);
            foreach (ShopButton b in mainItems) b.Draw(sb);
        }
        public void Update()
        {

        }

    }
    enum ButtonType
    {
        MainItem,
        Ammo,
        Healing
    }
    class ShopButton
    {
        Rectangle rect;
        ShopItem item;
        float alpha;
        Color color;
        ButtonType type;
        public ShopButton(ShopItem i, Rectangle r, Color c, float a, ButtonType t)
        {
            item = i;
            rect = r;
            color = c;
            alpha = a;
            type = t;
        }
        public void Draw(SpriteBatch sb)
        {
            DrawBox(sb, rect, color, alpha);
        }
        public void Update()
        {

        }
    }
    class MaxAmmoButton
    {
        Rectangle rect;
        float alpha;
        Color color;
        public MaxAmmoButton(Rectangle r, Color c, float a)
        {
            rect = r;
            color = c;
            alpha = a;
        }
        public void Draw(SpriteBatch sb)
        {
            DrawBox(sb, rect, color, alpha);
        }
        public void Update()
        {

        }
    }
    enum Resource
    {
        Money,

    }
    struct ShopItem
    {
        public Item item;
        public int cost;
        public Resource resource;
        public ShopItem(Item i, int c, Resource r)
        {
            item = i;
            cost = c;
            resource = r;
        }
    }
}
