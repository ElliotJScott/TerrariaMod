using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.Projectiles
{
    class Tentacle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tentacle");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            flippy = Main.rand.NextFloat() < 0.5f ? SpriteEffects.FlipVertically : SpriteEffects.None;
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 90;
            projectile.alpha = 255;
            projectile.light = 0f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            aiType = -1;
        }
        List<Vector2> pointsPassedThrough = new List<Vector2>();
        SpriteEffects flippy;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ((StarSailorMod)mod).friendlyTentacle;
            List<Vector2> pointsToConsider = GetPointsOnScreen();
            Vector2 currentPivot = pointsToConsider.Last();
            pointsToConsider.RemoveAt(pointsToConsider.Count - 1);
            int mode = 0;
            while (pointsToConsider.Count > 0)
            {
                int len = mode < 2 ? 16 : 20;
                Vector2 disp = currentPivot - pointsToConsider.Last();
                if (disp.Length() >= len)
                {
                    int srcX = 0;
                    switch (mode)
                    {
                        case 0:
                            srcX = 96;
                            break;
                        case 1:
                            srcX = 80;
                            break;
                        default:
                            srcX = 20 * (5 - mode);
                            break;
                    }
                    float angle = (float)Math.Atan(disp.Y / disp.X);
                    //if (Math.Sign(disp.Y) == -1) angle = (2 * (float)Math.PI) - angle;
                    //else angle = -angle;
                    spriteBatch.Draw(tex, new Rectangle((int)Math.Min(currentPivot.X, pointsToConsider.Last().X), (int)Math.Max(currentPivot.Y, pointsToConsider.Last().Y), (int)disp.Length(), tex.Height), new Rectangle(srcX, 0, len, tex.Height), Color.White, angle, new Vector2(len / 2, tex.Height / 2), flippy, 0);
                    if (mode < 5) mode++;
                    else mode = 5;
                    currentPivot = pointsToConsider.Last();
                }
                pointsToConsider.RemoveAt(pointsToConsider.Count - 1);

            }
            return false;
        }
        private List<Vector2> GetAmendedList()
        {
            Player owner = Main.player[projectile.owner];
            List<Vector2> results = new List<Vector2>();
            Vector2 endPoint = pointsPassedThrough.Last();
            Vector2 startPoint = pointsPassedThrough.First();
            Vector2 initDisp = endPoint - startPoint;
            Vector2 corrDisp = endPoint - owner.Center;
            for (int i = 0; i < pointsPassedThrough.Count; i++)
            {
                Vector2 disp = pointsPassedThrough[i] - endPoint;
                if ((int)initDisp.X == 0)
                    initDisp.X = 1;
                if ((int)initDisp.Y == 0)
                    initDisp.Y = 1;

                float xScale = corrDisp.X / initDisp.X;
                float yScale = corrDisp.Y / initDisp.Y;
                if (Math.Abs(xScale) > 10) Main.NewText("x " + xScale + " " + corrDisp.X + " " + initDisp.X);
                if (Math.Abs(yScale) > 10) Main.NewText("y " + yScale + " " + corrDisp.Y + " " + initDisp.Y);
                Vector2 newDisp = new Vector2(disp.X * xScale, disp.Y * yScale);
                results.Add(newDisp + endPoint);
            }
            return results;
        }
        private List<Vector2> GetPointsOnScreen()
        {
            List<Vector2> outp = new List<Vector2>();
            foreach (Vector2 v in GetAmendedList()) outp.Add(v - Main.screenPosition);
            return outp;
        }
        public override void AI()
        {
            if (pointsPassedThrough.Count == 0)
                pointsPassedThrough.Add(Main.player[projectile.owner].Center);
            else
                pointsPassedThrough.Add(projectile.position);
            Vector2 closee = new Vector2(100000);
            bool haveFound = false;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].townNPC && !Main.npc[i].friendly)
                {
                    Vector2 trial = Main.npc[i].position - projectile.position;
                    if (trial.Length() < closee.Length()) closee = trial;
                    haveFound = true;
                }
            }
            if (haveFound)
            {
                //Main.NewText(90 - projectile.timeLeft);
                Vector2 newVel = projectile.velocity + (closee * 0.00003f * (90 - projectile.timeLeft));
                projectile.velocity = newVel * (projectile.velocity.Length() / newVel.Length());

            }
        }
    }
}
