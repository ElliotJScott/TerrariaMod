using Microsoft.Xna.Framework;
using StarSailor.Dusts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor
{
    public class NPCBuffs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public List<BleedLocation> bleedLocations = new List<BleedLocation>();
        public Vector2 oldPosition;
        //public bool bleed;
        public bool slow;
        public int distortion;
        public bool bigSlow;
        public bool freeze;
        public Dictionary<int, int> numShotgunStacks = new Dictionary<int, int>();
        public override void ResetEffects(NPC npc)
        {
            Dictionary<int, int> tempDict = new Dictionary<int, int>();
            foreach (var v in numShotgunStacks)
            {
                tempDict[v.Key] = Math.Max(0, v.Value - 1);
            }
            numShotgunStacks = tempDict;
            
            slow = false;
            bigSlow = false;
            freeze = false;
            distortion = 0;
            
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (freeze)
            {
                npc.velocity.X = 0;
            }
            else if (bigSlow)
            {
                if (Math.Abs(npc.velocity.X) > 0.3f) npc.velocity.X = 0.3f * Math.Sign(npc.velocity.X);
            }
            else if (slow)
            {
                if (Math.Abs(npc.velocity.X) > 1f) npc.velocity.X = 1f * Math.Sign(npc.velocity.X);
            }
            if (distortion != 0)
            {
                int lifeChange = 0;
                int damageNum = 0;
                switch (distortion)
                {
                    case 1:
                        lifeChange = 1;
                        damageNum = 1;
                        break;
                    case 2:
                        lifeChange = 3;
                        damageNum = 3;
                        break;
                    case 3:
                        lifeChange = 5;
                        damageNum = 5;
                        break;
                    case 4:
                        lifeChange = 10;
                        damageNum = 10;
                        break;
                }
                npc.lifeRegen -= lifeChange * 20;
                if (damage < damageNum) damage = damageNum;
            }

            if (bleedLocations.Count > 0)
            {

                npc.lifeRegen -= (int)(Math.Pow(bleedLocations.Count, 1.2) * 20d); //make this not stack like this for bosses
                if (damage < bleedLocations.Count)
                {
                    damage = bleedLocations.Count;
                }
            }

        }
        
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (freeze) drawColor = Color.DarkBlue;
            else if (bigSlow) drawColor = Color.Blue;
            else if (slow) drawColor = Color.SkyBlue;
            for (int i = 0; i < bleedLocations.Count; i++)
            {
                BleedLocation b = bleedLocations[i];
                b.location += npc.position - oldPosition;
                if (Main.rand.NextFloat() < 0.2f)
                {
                    int dust = Dust.NewDust(b.location, 2, 2, ModContent.DustType<Blood>(), 0f, npc.velocity.Y * 0.4f, 100, default, 1.3f);
                }
                b.Update();
                bleedLocations[i] = b;
                if (bleedLocations[i].timer <= 0)
                {
                    bleedLocations.RemoveAt(i);
                    i--;
                }
            }
            oldPosition = npc.position;

        }
    }
    public struct BleedLocation
    {
        public int timer;
        public Vector2 location;
        public BleedLocation(int t, Vector2 l)
        {
            timer = t;
            location = l;
        }
        public void Update()
        {
            timer--;
        }
    }

}
