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
        public override void ResetEffects(NPC npc)
        {
            slow = false;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (slow)
            {
                if (Math.Abs(npc.velocity.X) > 1f) npc.velocity.X = 1f * Math.Sign(npc.velocity.X);
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
            if (slow) drawColor = Color.SkyBlue;
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
