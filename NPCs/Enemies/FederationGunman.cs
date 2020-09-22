using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarSailor.NPCs.Enemies
{
    class FederationGunman : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Federation Gunman");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.SkeletonSniper];
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.SkeletonSniper);
            npc.aiStyle = 0;
            animationType = NPCID.SkeletonSniper;
        }





        public override void AI()
        {

			if (npc.type == 466)
			{
				int num = 200;
				if (npc.ai[2] == 0f)
				{
					npc.alpha = num;
					npc.TargetClosest();
					if (!Main.player[npc.target].dead && (Main.player[npc.target].Center - npc.Center).Length() < 170f)
					{
						npc.ai[2] = -16f;
					}
					if (npc.velocity.X != 0f || npc.velocity.Y < 0f || npc.velocity.Y > 2f || npc.justHit)
					{
						npc.ai[2] = -16f;
					}
					return;
				}
				if (npc.ai[2] < 0f)
				{
					if (npc.alpha > 0)
					{
						npc.alpha -= num / 16;
						if (npc.alpha < 0)
						{
							npc.alpha = 0;
						}
					}
					ref float reference = ref npc.ai[2];
					reference += 1f;
					if (npc.ai[2] == 0f)
					{
						npc.ai[2] = 1f;
						npc.velocity.X = npc.direction * 2;
					}
					return;
				}
				npc.alpha = 0;
			}
			if (npc.type == 166)
			{
				if (Main.netMode != 1 && Main.rand.Next(240) == 0)
				{
					npc.ai[2] = Main.rand.Next(-480, -60);
					npc.netUpdate = true;
				}
				if (npc.ai[2] < 0f)
				{
					npc.TargetClosest();
					if (npc.justHit)
					{
						npc.ai[2] = 0f;
					}
					if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						npc.ai[2] = 0f;
					}
				}
				if (npc.ai[2] < 0f)
				{
					npc.velocity.X *= 0.9f;
					if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
					{
						npc.velocity.X = 0f;
					}
					ref float reference = ref npc.ai[2];
					reference += 1f;
					if (npc.ai[2] == 0f)
					{
						npc.velocity.X = (float)npc.direction * 0.1f;
					}
					return;
				}
			}
			if (npc.type == 461)
			{
				if (npc.wet)
				{
					npc.knockBackResist = 0f;
					npc.ai[3] = -0.10101f;
					npc.noGravity = true;
					Vector2 center = npc.Center;
					npc.width = 34;
					npc.height = 24;
					npc.position.X = center.X - (float)(npc.width / 2);
					npc.position.Y = center.Y - (float)(npc.height / 2);
					npc.TargetClosest();
					if (npc.collideX)
					{
						npc.velocity.X = 0f - npc.oldVelocity.X;
					}
					if (npc.velocity.X < 0f)
					{
						npc.direction = -1;
					}
					if (npc.velocity.X > 0f)
					{
						npc.direction = 1;
					}
					if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].Center, 1, 1))
					{
						Vector2 vector = Main.player[npc.target].Center - npc.Center;
						vector.Normalize();
						vector *= 5f;
						npc.velocity = (npc.velocity * 19f + vector) / 20f;
						return;
					}
					float num2 = 5f;
					if (npc.velocity.Y > 0f)
					{
						num2 = 3f;
					}
					if (npc.velocity.Y < 0f)
					{
						num2 = 8f;
					}
					Vector2 vector2 = new Vector2(npc.direction, -1f);
					vector2.Normalize();
					vector2 *= num2;
					if (num2 < 5f)
					{
						npc.velocity = (npc.velocity * 24f + vector2) / 25f;
					}
					else
					{
						npc.velocity = (npc.velocity * 9f + vector2) / 10f;
					}
					return;
				}
				npc.knockBackResist = 0.4f * Main.knockBackMultiplier;
				npc.noGravity = false;
				Vector2 center2 = npc.Center;
				npc.width = 18;
				npc.height = 40;
				npc.position.X = center2.X - (float)(npc.width / 2);
				npc.position.Y = center2.Y - (float)(npc.height / 2);
				if (npc.ai[3] == -0.10101f)
				{
					npc.ai[3] = 0f;
					float num3 = npc.velocity.Length();
					num3 *= 2f;
					if (num3 > 10f)
					{
						num3 = 10f;
					}
					npc.velocity.Normalize();
					npc.velocity *= num3;
					if (npc.velocity.X < 0f)
					{
						npc.direction = -1;
					}
					if (npc.velocity.X > 0f)
					{
						npc.direction = 1;
					}
					npc.spriteDirection = npc.direction;
				}
			}
			if (npc.type == 379 || npc.type == 380)
			{
				if (npc.ai[3] < 0f)
				{
					npc.damage = 0;
					npc.velocity.X *= 0.93f;
					if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
					{
						npc.velocity.X = 0f;
					}
					int num4 = (int)(0f - npc.ai[3] - 1f);
					int num5 = Math.Sign(Main.npc[num4].Center.X - npc.Center.X);
					if (num5 != npc.direction)
					{
						npc.velocity.X = 0f;
						npc.direction = num5;
						npc.netUpdate = true;
					}
					if (npc.justHit && Main.netMode != 1 && Main.npc[num4].localAI[0] == 0f)
					{
						Main.npc[num4].localAI[0] = 1f;
					}
					if (npc.ai[0] < 1000f)
					{
						npc.ai[0] = 1000f;
					}
					ref float reference = ref npc.ai[0];
					if ((reference += 1f) >= 1300f)
					{
						npc.ai[0] = 1000f;
						npc.netUpdate = true;
					}
					return;
				}
				if (npc.ai[0] >= 1000f)
				{
					npc.ai[0] = 0f;
				}
				npc.damage = npc.defDamage;
			}
			if (npc.type == 383 && npc.ai[2] == 0f && npc.localAI[0] == 0f && Main.netMode != 1)
			{
				int num6 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 384, npc.whoAmI);
				npc.ai[2] = num6 + 1;
				npc.localAI[0] = -1f;
				npc.netUpdate = true;
				Main.npc[num6].ai[0] = npc.whoAmI;
				Main.npc[num6].netUpdate = true;
			}
			if (npc.type == 383)
			{
				int num7 = (int)npc.ai[2] - 1;
				if (num7 != -1 && Main.npc[num7].active && Main.npc[num7].type == 384)
				{
					npc.dontTakeDamage = true;
				}
				else
				{
					npc.dontTakeDamage = false;
					npc.ai[2] = 0f;
					if (npc.localAI[0] == -1f)
					{
						npc.localAI[0] = 180f;
					}
					if (npc.localAI[0] > 0f)
					{
						ref float reference = ref npc.localAI[0];
						reference -= 1f;
					}
				}
			}
			if (npc.type == 482)
			{
				int num8 = 300;
				int num9 = 120;
				npc.dontTakeDamage = false;
				if (npc.ai[2] < 0f)
				{
					npc.dontTakeDamage = true;
					ref float reference = ref npc.ai[2];
					reference += 1f;
					npc.velocity.X *= 0.9f;
					if ((double)Math.Abs(npc.velocity.X) < 0.001)
					{
						npc.velocity.X = 0.001f * (float)npc.direction;
					}
					if (Math.Abs(npc.velocity.Y) > 1f)
					{
						reference = ref npc.ai[2];
						reference += 10f;
					}
					if (npc.ai[2] >= 0f)
					{
						npc.netUpdate = true;
						npc.velocity.X += (float)npc.direction * 0.3f;
					}
					return;
				}
				if (npc.ai[2] < (float)num8)
				{
					if (npc.justHit)
					{
                        npc.ai[2] += 15f;
					}
                    npc.ai[2] += 1f;
				}
				else if (npc.velocity.Y == 0f)
				{
					npc.ai[2] = 0f - (float)num9;
					npc.netUpdate = true;
				}
			}
			if (npc.type == 480)
			{
				int num10 = 180;
				int num11 = 300;
				int num12 = 180;
				int num13 = 60;
				int num14 = 20;
				if (npc.life < npc.lifeMax / 3)
				{
					num10 = 120;
					num11 = 240;
					num12 = 240;
					num13 = 90;
				}
				if (npc.ai[2] > 0f)
				{
					ref float reference = ref npc.ai[2];
					reference -= 1f;
				}
				else if (npc.ai[2] == 0f)
				{
					if (((Main.player[npc.target].Center.X < npc.Center.X && npc.direction < 0) || (Main.player[npc.target].Center.X > npc.Center.X && npc.direction > 0)) && npc.velocity.Y == 0f && npc.Distance(Main.player[npc.target].Center) < 900f && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						npc.ai[2] = 0f - (float)num12 - (float)num14;
						npc.netUpdate = true;
					}
				}
				else
				{
					if (npc.ai[2] < 0f && npc.ai[2] < 0f - (float)num12)
					{
						npc.velocity.X *= 0.9f;
						if (npc.velocity.Y < -2f || npc.velocity.Y > 4f || npc.justHit)
						{
							npc.ai[2] = num10;
						}
						else
						{
							ref float reference = ref npc.ai[2];
							reference += 1f;
							if (npc.ai[2] == 0f)
							{
								npc.ai[2] = num11;
							}
						}
						float num15 = npc.ai[2] + (float)num12 + (float)num14;
						if (num15 == 1f)
						{
							Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 17);
						}
						if (num15 < (float)num14)
						{
							Vector2 vector3 = npc.Top + new Vector2(npc.spriteDirection * 6, 6f);
							float scaleFactor = MathHelper.Lerp(20f, 30f, (num15 * 3f + 50f) / 182f);
							Main.rand.NextFloat();
							for (float num16 = 0f; num16 < 2f; num16 += 1f)
							{
								Vector2 vector4 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (Main.rand.NextFloat() * 0.5f + 0.5f);
								Dust dust = Main.dust[Dust.NewDust(vector3, 0, 0, 228)];
								dust.position = vector3 + vector4 * scaleFactor;
								dust.noGravity = true;
								dust.velocity = vector4 * 2f;
								dust.scale = 0.5f + Main.rand.NextFloat() * 0.5f;
							}
						}
						Lighting.AddLight(npc.Center, 0.9f, 0.75f, 0.1f);
						return;
					}
					if (npc.ai[2] < 0f && npc.ai[2] >= 0f - (float)num12)
					{
						Lighting.AddLight(npc.Center, 0.9f, 0.75f, 0.1f);
						npc.velocity.X *= 0.9f;
						if (npc.velocity.Y < -2f || npc.velocity.Y > 4f || npc.justHit)
						{
							npc.ai[2] = num10;
						}
						else
						{
							ref float reference = ref npc.ai[2];
							reference += 1f;
							if (npc.ai[2] == 0f)
							{
								npc.ai[2] = num11;
							}
						}
						float num17 = npc.ai[2] + (float)num12;
						if (num17 < 180f && (Main.rand.Next(3) == 0 || npc.ai[2] % 3f == 0f))
						{
							Vector2 vector5 = npc.Top + new Vector2(npc.spriteDirection * 10, 10f);
							float scaleFactor2 = MathHelper.Lerp(20f, 30f, (num17 * 3f + 50f) / 182f);
							Main.rand.NextFloat();
							for (float num18 = 0f; num18 < 1f; num18 += 1f)
							{
								Vector2 vector6 = Vector2.UnitY.RotatedByRandom(6.2831854820251465) * (Main.rand.NextFloat() * 0.5f + 0.5f);
								Dust dust2 = Main.dust[Dust.NewDust(vector5, 0, 0, 228)];
								dust2.position = vector5 + vector6 * scaleFactor2;
								dust2.noGravity = true;
								dust2.velocity = vector6 * 4f;
								dust2.scale = 0.5f + Main.rand.NextFloat();
							}
						}
						if (Main.netMode == 2)
						{
							return;
						}
						Player player = Main.player[Main.myPlayer];
						int myPlayer = Main.myPlayer;
						if (player.dead || !player.active || player.FindBuffIndex(156) != -1)
						{
							return;
						}
						Vector2 vector7 = player.Center - npc.Center;
						if (!(vector7.Length() < 700f))
						{
							return;
						}
						bool flag = vector7.Length() < 30f;
						if (!flag)
						{
							float x = ((float)Math.PI / 4f).ToRotationVector2().X;
							Vector2 vector8 = Vector2.Normalize(vector7);
							if (vector8.X > x || vector8.X < 0f - x)
							{
								flag = true;
							}
						}
						if ((((player.Center.X < npc.Center.X && npc.direction < 0 && player.direction > 0) || (player.Center.X > npc.Center.X && npc.direction > 0 && player.direction < 0)) & flag) && (Collision.CanHitLine(npc.Center, 1, 1, player.Center, 1, 1) || Collision.CanHitLine(npc.Center - Vector2.UnitY * 16f, 1, 1, player.Center, 1, 1) || Collision.CanHitLine(npc.Center + Vector2.UnitY * 8f, 1, 1, player.Center, 1, 1)))
						{
							player.AddBuff(156, num13 + (int)npc.ai[2] * -1);
						}
						return;
					}
				}
			}
			if (npc.type == 471)
			{
				if (npc.ai[3] < 0f)
				{
					npc.knockBackResist = 0f;
					npc.defense = (int)((double)npc.defDefense * 1.1);
					npc.noGravity = true;
					npc.noTileCollide = true;
					if (npc.velocity.X < 0f)
					{
						npc.direction = -1;
					}
					else if (npc.velocity.X > 0f)
					{
						npc.direction = 1;
					}
					npc.rotation = npc.velocity.X * 0.1f;
					if (Main.netMode != 1)
					{
						ref float reference = ref npc.localAI[3];
						reference += 1f;
						if (npc.localAI[3] > (float)Main.rand.Next(20, 180))
						{
							npc.localAI[3] = 0f;
							Vector2 center3 = npc.Center;
							center3 += npc.velocity;
							NPC.NewNPC((int)center3.X, (int)center3.Y, 30);
						}
					}
				}
				else
				{
					npc.localAI[3] = 0f;
					npc.knockBackResist = 0.35f * Main.knockBackMultiplier;
					npc.rotation *= 0.9f;
					npc.defense = npc.defDefense;
					npc.noGravity = false;
					npc.noTileCollide = false;
				}
				if (npc.ai[3] == 1f)
				{
					npc.knockBackResist = 0f;
					npc.defense += 10;
				}
				if (npc.ai[3] == -1f)
				{
					npc.TargetClosest();
					float num19 = 8f;
					float num20 = 40f;
					Vector2 vector9 = Main.player[npc.target].Center - npc.Center;
					float num21 = vector9.Length();
					num19 += num21 / 200f;
					vector9.Normalize();
					vector9 *= num19;
					npc.velocity = (npc.velocity * (num20 - 1f) + vector9) / num20;
					if (num21 < 500f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
					{
						npc.ai[3] = 0f;
						npc.ai[2] = 0f;
					}
					return;
				}
				if (npc.ai[3] == -2f)
				{
					npc.velocity.Y -= 0.2f;
					if (npc.velocity.Y < -10f)
					{
						npc.velocity.Y = -10f;
					}
					if (Main.player[npc.target].Center.Y - npc.Center.Y > 200f)
					{
						npc.TargetClosest();
						npc.ai[3] = -3f;
						if (Main.player[npc.target].Center.X > npc.Center.X)
						{
							npc.ai[2] = 1f;
						}
						else
						{
							npc.ai[2] = -1f;
						}
					}
					npc.velocity.X *= 0.99f;
					return;
				}
				if (npc.ai[3] == -3f)
				{
					if (npc.direction == 0)
					{
						npc.TargetClosest();
					}
					if (npc.ai[2] == 0f)
					{
						npc.ai[2] = npc.direction;
					}
					npc.velocity.Y *= 0.9f;
					npc.velocity.X += npc.ai[2] * 0.3f;
					if (npc.velocity.X > 10f)
					{
						npc.velocity.X = 10f;
					}
					if (npc.velocity.X < -10f)
					{
						npc.velocity.X = -10f;
					}
					float num22 = Main.player[npc.target].Center.X - npc.Center.X;
					if ((npc.ai[2] < 0f && num22 > 300f) || (npc.ai[2] > 0f && num22 < -300f))
					{
						npc.ai[3] = -4f;
						npc.ai[2] = 0f;
					}
					else if (Math.Abs(num22) > 800f)
					{
						npc.ai[3] = -1f;
						npc.ai[2] = 0f;
					}
					return;
				}
				if (npc.ai[3] == -4f)
				{
					ref float reference = ref npc.ai[2];
					reference += 1f;
					npc.velocity.Y += 0.1f;
					if (npc.velocity.Length() > 4f)
					{
						npc.velocity *= 0.9f;
					}
					int num23 = (int)npc.Center.X / 16;
					int num24 = (int)(npc.position.Y + (float)npc.height + 12f) / 16;
					bool flag2 = false;
					int num26;
					for (int num25 = num23 - 1; num25 <= num23 + 1; num25 = num26 + 1)
					{
						if (Main.tile[num25, num24] == null)
						{
							Main.tile[num23, num24] = new Tile();
						}
						if (Main.tile[num25, num24].active() && Main.tileSolid[Main.tile[num25, num24].type])
						{
							flag2 = true;
						}
						num26 = num25;
					}
					if (flag2 && !Collision.SolidCollision(npc.position, npc.width, npc.height))
					{
						npc.ai[3] = 0f;
						npc.ai[2] = 0f;
					}
					else if (npc.ai[2] > 300f || npc.Center.Y > Main.player[npc.target].Center.Y + 200f)
					{
						npc.ai[3] = -1f;
						npc.ai[2] = 0f;
					}
				}
				else
				{
					if (npc.ai[3] == 1f)
					{
						Vector2 center4 = npc.Center;
                        center4.Y -= 70f;
						npc.velocity.X *= 0.8f;
                        npc.ai[2] += 1f;
						if (npc.ai[2] == 60f)
						{
							if (Main.netMode != 1)
							{
								NPC.NewNPC((int)center4.X, (int)center4.Y + 18, 472);
							}
						}
						else if (npc.ai[2] >= 90f)
						{
							npc.ai[3] = -2f;
							npc.ai[2] = 0f;
						}
						int num26;
						for (int num27 = 0; num27 < 2; num27 = num26 + 1)
						{
							Vector2 value = center4;
							Vector2 vector10 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
							vector10.Normalize();
							vector10 *= (float)Main.rand.Next(0, 100) * 0.1f;
							value += vector10;
							vector10.Normalize();
							vector10 *= (float)Main.rand.Next(50, 90) * 0.1f;
							int num28 = Dust.NewDust(value, 1, 1, 27);
							Main.dust[num28].velocity = -vector10 * 0.3f;
							Main.dust[num28].alpha = 100;
							if (Main.rand.Next(2) == 0)
							{
								Main.dust[num28].noGravity = true;
								Dust dust3 = Main.dust[num28];
								dust3.scale += 0.3f;
							}
							num26 = num27;
						}
						return;
					}
                    npc.ai[2] += 1f;
					int num29 = 10;
					if (npc.velocity.Y == 0f && NPC.CountNPCS(472) < num29)
					{
						if (npc.ai[2] >= 180f)
						{
							npc.ai[2] = 0f;
							npc.ai[3] = 1f;
						}
					}
					else
					{
						if (NPC.CountNPCS(472) >= num29)
						{
                            npc.ai[2] += 1f;
						}
						if (npc.ai[2] >= 360f)
						{
							npc.ai[2] = 0f;
							npc.ai[3] = -2f;
							npc.velocity.Y -= 3f;
						}
					}
					if (npc.target >= 0 && !Main.player[npc.target].dead && (Main.player[npc.target].Center - npc.Center).Length() > 800f)
					{
						npc.ai[3] = -1f;
						npc.ai[2] = 0f;
					}
				}
				if (Main.player[npc.target].dead)
				{
					npc.TargetClosest();
					if (Main.player[npc.target].dead && npc.timeLeft > 1)
					{
						npc.timeLeft = 1;
					}
				}
			}
			if (npc.type == 419)
			{
				npc.reflectingProjectiles = false;
				npc.takenDamageMultiplier = 1f;
				int num30 = 6;
				int num31 = 10;
				float scaleFactor3 = 16f;
				if (npc.ai[2] > 0f)
				{
					ref float reference = ref npc.ai[2];
					reference -= 1f;
				}
				if (npc.ai[2] == 0f)
				{
					if (((Main.player[npc.target].Center.X < npc.Center.X && npc.direction < 0) || (Main.player[npc.target].Center.X > npc.Center.X && npc.direction > 0)) && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						npc.ai[2] = -1f;
						npc.netUpdate = true;
						npc.TargetClosest();
					}
				}
				else
				{
					if (npc.ai[2] < 0f && npc.ai[2] > 0f - (float)num30)
					{
						ref float reference = ref npc.ai[2];
						reference -= 1f;
						npc.velocity.X *= 0.9f;
						return;
					}
					if (npc.ai[2] == 0f - (float)num30)
					{
						ref float reference = ref npc.ai[2];
						reference -= 1f;
						npc.TargetClosest();
						Vector2 vec = npc.DirectionTo(Main.player[npc.target].Top + new Vector2(0f, -30f));
						if (vec.HasNaNs())
						{
							vec = Vector2.Normalize(new Vector2(npc.spriteDirection, -1f));
						}
						npc.velocity = vec * scaleFactor3;
						npc.netUpdate = true;
						return;
					}
					if (npc.ai[2] < 0f - (float)num30)
					{
						ref float reference = ref npc.ai[2];
						reference -= 1f;
						if (npc.velocity.Y == 0f)
						{
							npc.ai[2] = 60f;
						}
						else if (npc.ai[2] < 0f - (float)num30 - (float)num31)
						{
							npc.velocity.Y += 0.15f;
							if (npc.velocity.Y > 24f)
							{
								npc.velocity.Y = 24f;
							}
						}
						npc.reflectingProjectiles = true;
						npc.takenDamageMultiplier = 3f;
						if (npc.justHit)
						{
							npc.ai[2] = 60f;
							npc.netUpdate = true;
						}
						return;
					}
				}
			}
			if (npc.type == 415)
			{
				int num32 = 42;
				int num33 = 18;
				if (npc.justHit)
				{
					npc.ai[2] = 120f;
					npc.netUpdate = true;
				}
				if (npc.ai[2] > 0f)
				{
					ref float reference = ref npc.ai[2];
					reference -= 1f;
				}
				if (npc.ai[2] == 0f)
				{
					int num34 = 0;
					int num26;
					for (int num35 = 0; num35 < 200; num35 = num26 + 1)
					{
						if (Main.npc[num35].active && Main.npc[num35].type == 516)
						{
							num26 = num34;
							num34 = num26 + 1;
						}
						num26 = num35;
					}
					if (num34 > 6)
					{
						npc.ai[2] = 90f;
					}
					else if (((Main.player[npc.target].Center.X < npc.Center.X && npc.direction < 0) || (Main.player[npc.target].Center.X > npc.Center.X && npc.direction > 0)) && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
					{
						npc.ai[2] = -1f;
						npc.netUpdate = true;
						npc.TargetClosest();
					}
				}
				else if (npc.ai[2] < 0f && npc.ai[2] > 0f - (float)num32)
				{
					ref float reference = ref npc.ai[2];
					reference -= 1f;
					if (npc.ai[2] == 0f - (float)num32)
					{
						npc.ai[2] = 180 + 30 * Main.rand.Next(10);
					}
					npc.velocity.X *= 0.8f;
					if (npc.ai[2] == 0f - (float)num33 || npc.ai[2] == 0f - (float)num33 - 8f || npc.ai[2] == 0f - (float)num33 - 16f)
					{
						int num26;
						for (int num36 = 0; num36 < 20; num36 = num26 + 1)
						{
							Vector2 vector11 = npc.Center + Vector2.UnitX * npc.spriteDirection * 40f;
							Dust dust4 = Main.dust[Dust.NewDust(vector11, 0, 0, 259)];
							Vector2 vector12 = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
							dust4.position = vector11 + vector12 * 4f;
							dust4.velocity = vector12 * 2f + Vector2.UnitX * Main.rand.NextFloat() * npc.spriteDirection * 3f;
							dust4.scale = 0.3f + vector12.X * (0f - (float)npc.spriteDirection);
							dust4.fadeIn = 0.7f;
							dust4.noGravity = true;
							num26 = num36;
						}
						if (npc.velocity.X > -0.5f && npc.velocity.X < 0.5f)
						{
							npc.velocity.X = 0f;
						}
						if (Main.netMode != 1)
						{
							NPC.NewNPC((int)npc.Center.X + npc.spriteDirection * 45, (int)npc.Center.Y + 8, 516, 0, 0f, 0f, 0f, 0f, npc.target);
						}
					}
					return;
				}
			}
			if (npc.type == 428)
			{
				ref float reference = ref npc.localAI[0];
				reference += 1f;
				if (npc.localAI[0] >= 300f)
				{
					int num37 = (int)npc.Center.X / 16 - 1;
					int num38 = (int)npc.Center.Y / 16 - 1;
					if (!Collision.SolidTiles(num37, num37 + 2, num38, num38 + 1) && Main.netMode != 1)
					{
						npc.Transform(427);
						npc.life = npc.lifeMax;
						npc.localAI[0] = 0f;
						return;
					}
				}
				int maxValue = (npc.localAI[0] < 60f) ? 16 : ((npc.localAI[0] < 120f) ? 8 : ((npc.localAI[0] < 180f) ? 4 : ((npc.localAI[0] < 240f) ? 2 : ((!(npc.localAI[0] < 300f)) ? 1 : 1))));
				if (Main.rand.Next(maxValue) == 0)
				{
					Dust dust5 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 229)];
					dust5.noGravity = true;
					dust5.scale = 1f;
					dust5.noLight = true;
					dust5.velocity = npc.DirectionFrom(dust5.position) * dust5.velocity.Length();
					Dust dust3 = dust5;
					dust3.position -= dust5.velocity * 5f;
					Dust dust6 = dust5;
					dust6.position.X = dust6.position.X + (float)(npc.direction * 6);
					Dust dust7 = dust5;
					dust7.position.Y = dust7.position.Y + 4f;
				}
			}
			if (npc.type == 427)
			{
				ref float reference = ref npc.localAI[0];
				reference += 1f;
				reference = ref npc.localAI[0];
				reference += Math.Abs(npc.velocity.X) / 2f;
				if (npc.localAI[0] >= 1200f && Main.netMode != 1)
				{
					int num39 = (int)npc.Center.X / 16 - 2;
					int num40 = (int)npc.Center.Y / 16 - 3;
					if (!Collision.SolidTiles(num39, num39 + 4, num40, num40 + 4))
					{
						npc.Transform(426);
						npc.life = npc.lifeMax;
						npc.localAI[0] = 0f;
						return;
					}
				}
				int maxValue2 = (npc.localAI[0] < 360f) ? 32 : ((npc.localAI[0] < 720f) ? 16 : ((npc.localAI[0] < 1080f) ? 6 : ((npc.localAI[0] < 1440f) ? 2 : ((!(npc.localAI[0] < 1800f)) ? 1 : 1))));
				if (Main.rand.Next(maxValue2) == 0)
				{
					Dust dust8 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 229)];
					dust8.noGravity = true;
					dust8.scale = 1f;
					dust8.noLight = true;
				}
			}
			bool flag3 = false;
			if (npc.velocity.X == 0f)
			{
				flag3 = true;
			}
			if (npc.justHit)
			{
				flag3 = false;
			}
			if (Main.netMode != 1 && npc.type == 198 && (double)npc.life <= (double)npc.lifeMax * 0.55)
			{
				npc.Transform(199);
			}
			if (Main.netMode != 1 && npc.type == 348 && (double)npc.life <= (double)npc.lifeMax * 0.55)
			{
				npc.Transform(349);
			}
			int num41 = 60;
			if (npc.type == 120)
			{
				num41 = 180;
				if (npc.ai[3] == -120f)
				{
					npc.velocity *= 0f;
					npc.ai[3] = 0f;
					Main.PlaySound(SoundID.Item8, npc.position);
					Vector2 vector13 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num42 = npc.oldPos[2].X + (float)npc.width * 0.5f - vector13.X;
					float num43 = npc.oldPos[2].Y + (float)npc.height * 0.5f - vector13.Y;
					float num44 = (float)Math.Sqrt(num42 * num42 + num43 * num43);
					num44 = 2f / num44;
					num42 *= num44;
					num43 *= num44;
					int num26;
					for (int num45 = 0; num45 < 20; num45 = num26 + 1)
					{
						int num46 = Dust.NewDust(npc.position, npc.width, npc.height, 71, num42, num43, 200, default(Color), 2f);
						Main.dust[num46].noGravity = true;
						Dust dust9 = Main.dust[num46];
						dust9.velocity.X = dust9.velocity.X * 2f;
						num26 = num45;
					}
					for (int num47 = 0; num47 < 20; num47 = num26 + 1)
					{
						int num48 = Dust.NewDust(npc.oldPos[2], npc.width, npc.height, 71, 0f - num42, 0f - num43, 200, default(Color), 2f);
						Main.dust[num48].noGravity = true;
						Dust dust10 = Main.dust[num48];
						dust10.velocity.X = dust10.velocity.X * 2f;
						num26 = num47;
					}
				}
			}
			bool flag4 = false;
			bool flag5 = true;
			if (npc.type == 343 || npc.type == 47 || npc.type == 67 || npc.type == 109 || npc.type == 110 || npc.type == 111 || npc.type == 120 || npc.type == 163 || npc.type == 164 || npc.type == 239 || npc.type == 168 || npc.type == 199 || npc.type == 206 || npc.type == 214 || npc.type == 215 || npc.type == 216 || npc.type == 217 || npc.type == 218 || npc.type == 219 || npc.type == 220 || npc.type == 226 || npc.type == 243 || npc.type == 251 || npc.type == 257 || npc.type == 258 || npc.type == 290 || (npc.type == 291 || true) || npc.type == 292 || npc.type == 293 || npc.type == 305 || npc.type == 306 || npc.type == 307 || npc.type == 308 || npc.type == 309 || npc.type == 348 || npc.type == 349 || npc.type == 350 || npc.type == 351 || npc.type == 379 || (npc.type >= 430 && npc.type <= 436) || npc.type == 380 || npc.type == 381 || npc.type == 382 || npc.type == 383 || npc.type == 386 || npc.type == 391 || (npc.type >= 449 && npc.type <= 452) || npc.type == 466 || npc.type == 464 || npc.type == 166 || npc.type == 469 || npc.type == 468 || npc.type == 471 || npc.type == 470 || npc.type == 480 || npc.type == 481 || npc.type == 482 || npc.type == 411 || npc.type == 424 || npc.type == 409 || (npc.type >= 494 && npc.type <= 506) || npc.type == 425 || npc.type == 427 || npc.type == 426 || npc.type == 428 || npc.type == 508 || npc.type == 415 || npc.type == 419 || npc.type == 520 || (npc.type >= 524 && npc.type <= 527) || npc.type == 528 || npc.type == 529 || npc.type == 530 || npc.type == 532)
			{
				flag5 = false;
			}
			bool flag6 = false;
			int num49 = npc.type;
			if (num49 == 425 || num49 == 471)
			{
				flag6 = true;
			}
			bool flag7 = true;
			switch (npc.type)
			{
			case 110:
			case 111:
			case 206:
			case 214:
			case 215:
			case 216:
			case 291:
			case 292:
			case 293:
			case 350:
			case 379:
			case 380:
			case 381:
			case 382:
			case 409:
			case 411:
			case 424:
			case 426:
			case 466:
			case 498:
			case 499:
			case 500:
			case 501:
			case 502:
			case 503:
			case 504:
			case 505:
			case 506:
			case 520:
				if (npc.ai[2] > 0f)
				{
					flag7 = false;
				}
				break;
			}
			if (!flag6 && flag7)
			{
				if (npc.velocity.Y == 0f && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
				{
					flag4 = true;
				}
				if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= (float)num41) | flag4)
				{
					ref float reference = ref npc.ai[3];
					reference += 1f;
				}
				else if ((double)Math.Abs(npc.velocity.X) > 0.9 && npc.ai[3] > 0f)
				{
					ref float reference = ref npc.ai[3];
					reference -= 1f;
				}
				if (npc.ai[3] > (float)(num41 * 10))
				{
					npc.ai[3] = 0f;
				}
				if (npc.justHit)
				{
					npc.ai[3] = 0f;
				}
				if (npc.ai[3] == (float)num41)
				{
					npc.netUpdate = true;
				}
			}
			if (npc.type == 463 && Main.netMode != 1)
			{
				if (npc.localAI[3] > 0f)
				{
					ref float reference = ref npc.localAI[3];
					reference -= 1f;
				}
				if (npc.justHit && npc.localAI[3] <= 0f && Main.rand.Next(3) == 0)
				{
					npc.localAI[3] = 30f;
					int num50 = Main.rand.Next(3, 6);
					int[] array = new int[num50];
					int num51 = 0;
					int num26;
					for (int num52 = 0; num52 < 255; num52 = num26 + 1)
					{
						if (Main.player[num52].active && !Main.player[num52].dead && Collision.CanHitLine(npc.position, npc.width, npc.height, Main.player[num52].position, Main.player[num52].width, Main.player[num52].height))
						{
							array[num51] = num52;
							num26 = num51;
							num51 = num26 + 1;
							if (num51 == num50)
							{
								break;
							}
						}
						num26 = num52;
					}
					if (num51 > 1)
					{
						for (int num53 = 0; num53 < 100; num53 = num26 + 1)
						{
							int num54 = Main.rand.Next(num51);
							int num55;
							for (num55 = num54; num55 == num54; num55 = Main.rand.Next(num51))
							{
							}
							int num56 = array[num54];
							array[num54] = array[num55];
							array[num55] = num56;
							num26 = num53;
						}
					}
					Vector2 vector14 = new Vector2(-1f, -1f);
					for (int num57 = 0; num57 < num51; num57 = num26 + 1)
					{
						Vector2 value2 = Main.npc[array[num57]].Center - npc.Center;
						value2.Normalize();
						vector14 += value2;
						num26 = num57;
					}
					vector14.Normalize();
					for (int num58 = 0; num58 < num50; num58 = num26 + 1)
					{
						float scaleFactor4 = Main.rand.Next(8, 13);
						Vector2 value3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
						value3.Normalize();
						if (num51 > 0)
						{
							value3 += vector14;
							value3.Normalize();
						}
						value3 *= scaleFactor4;
						if (num51 > 0)
						{
							num26 = num51;
							num51 = num26 - 1;
							value3 = Main.player[array[num51]].Center - npc.Center;
							value3.Normalize();
							value3 *= scaleFactor4;
						}
						Projectile.NewProjectile(npc.Center.X, npc.position.Y + (float)(npc.width / 4), value3.X, value3.Y, 498, (int)((double)npc.damage * 0.15), 1f);
						num26 = num58;
					}
				}
			}
			if (npc.type == 460)
			{
				if (npc.velocity.Y < 0f - 0.3f || npc.velocity.Y > 0.3f)
				{
					npc.knockBackResist = 0f;
				}
				else
				{
					npc.knockBackResist = 0.25f * Main.knockBackMultiplier;
				}
			}
			if (npc.type == 469)
			{
				npc.knockBackResist = 0.45f * Main.knockBackMultiplier;
				if (npc.ai[2] == 1f)
				{
					npc.knockBackResist = 0f;
				}
				bool flag8 = false;
				int num59 = (int)npc.Center.X / 16;
				int num60 = (int)npc.Center.Y / 16;
				int num26;
				for (int num61 = num59 - 1; num61 <= num59 + 1; num61 = num26 + 1)
				{
					for (int num62 = num60 - 1; num62 <= num60 + 1; num62 = num26 + 1)
					{
						if (Main.tile[num61, num62] != null && Main.tile[num61, num62].wall > 0)
						{
							flag8 = true;
							break;
						}
						num26 = num62;
					}
					if (flag8)
					{
						break;
					}
					num26 = num61;
				}
				if (npc.ai[2] == 0f && flag8)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.Y = -4.6f;
						npc.velocity.X *= 1.3f;
					}
					else if (npc.velocity.Y > 0f)
					{
						npc.ai[2] = 1f;
					}
				}
				if (flag8 && npc.ai[2] == 1f && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
				{
					Vector2 vector15 = Main.player[npc.target].Center - npc.Center;
					float num63 = vector15.Length();
					vector15.Normalize();
					vector15 *= 4.5f + num63 / 300f;
					npc.velocity = (npc.velocity * 29f + vector15) / 30f;
					npc.noGravity = true;
					npc.ai[2] = 1f;
					return;
				}
				npc.noGravity = false;
				npc.ai[2] = 0f;
			}
			if (npc.type == 462 && npc.velocity.Y == 0f && (Main.player[npc.target].Center - npc.Center).Length() < 150f && Math.Abs(npc.velocity.X) > 3f && ((npc.velocity.X < 0f && npc.Center.X > Main.player[npc.target].Center.X) || (npc.velocity.X > 0f && npc.Center.X < Main.player[npc.target].Center.X)))
			{
				npc.velocity.X *= 1.75f;
				npc.velocity.Y -= 4.5f;
				if (npc.Center.Y - Main.player[npc.target].Center.Y > 20f)
				{
					npc.velocity.Y -= 0.5f;
				}
				if (npc.Center.Y - Main.player[npc.target].Center.Y > 40f)
				{
					npc.velocity.Y -= 1f;
				}
				if (npc.Center.Y - Main.player[npc.target].Center.Y > 80f)
				{
					npc.velocity.Y -= 1.5f;
				}
				if (npc.Center.Y - Main.player[npc.target].Center.Y > 100f)
				{
					npc.velocity.Y -= 1.5f;
				}
				if (Math.Abs(npc.velocity.X) > 7f)
				{
					if (npc.velocity.X < 0f)
					{
						npc.velocity.X = -7f;
					}
					else
					{
						npc.velocity.X = 7f;
					}
				}
			}
			if (npc.ai[3] < (float)num41 && (Main.eclipse || !Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0 || (Main.invasionType == 1 && (npc.type == 343 || npc.type == 350)) || (Main.invasionType == 1 && (npc.type == 26 || npc.type == 27 || npc.type == 28 || npc.type == 111 || npc.type == 471)) || npc.type == 73 || (Main.invasionType == 3 && npc.type >= 212 && npc.type <= 216) || (Main.invasionType == 4 && (npc.type == 381 || npc.type == 382 || npc.type == 383 || npc.type == 385 || npc.type == 386 || npc.type == 389 || npc.type == 391 || npc.type == 520)) || npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296 || npc.type == 47 || npc.type == 67 || npc.type == 77 || npc.type == 78 || npc.type == 79 || npc.type == 80 || npc.type == 110 || npc.type == 120 || npc.type == 168 || npc.type == 181 || npc.type == 185 || npc.type == 198 || npc.type == 199 || npc.type == 206 || npc.type == 217 || npc.type == 218 || npc.type == 219 || npc.type == 220 || npc.type == 239 || npc.type == 243 || npc.type == 254 || npc.type == 255 || npc.type == 257 || npc.type == 258 || (npc.type == 291 || true) || npc.type == 292 || npc.type == 293 || npc.type == 379 || npc.type == 380 || npc.type == 464 || npc.type == 470 || npc.type == 424 || (npc.type == 411 && (npc.ai[1] >= 180f || npc.ai[1] < 90f)) || npc.type == 409 || npc.type == 425 || npc.type == 429 || npc.type == 427 || npc.type == 428 || npc.type == 508 || npc.type == 415 || npc.type == 419 || (npc.type >= 524 && npc.type <= 527) || npc.type == 528 || npc.type == 529 || npc.type == 530 || npc.type == 532))
			{
				if ((npc.type == 3 || npc.type == 331 || npc.type == 332 || npc.type == 21 || (npc.type >= 449 && npc.type <= 452) || npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296 || npc.type == 77 || npc.type == 110 || npc.type == 132 || npc.type == 167 || npc.type == 161 || npc.type == 162 || npc.type == 186 || npc.type == 187 || npc.type == 188 || npc.type == 189 || npc.type == 197 || npc.type == 200 || npc.type == 201 || npc.type == 202 || npc.type == 203 || npc.type == 223 || (npc.type == 291 || true) || npc.type == 292 || npc.type == 293 || npc.type == 320 || npc.type == 321 || npc.type == 319 || npc.type == 481) && Main.rand.Next(1000) == 0)
				{
					Main.PlaySound(14, (int)npc.position.X, (int)npc.position.Y);
				}
				if (npc.type == 489 && Main.rand.Next(800) == 0)
				{
					Main.PlaySound(14, (int)npc.position.X, (int)npc.position.Y, npc.type);
				}
				if ((npc.type == 78 || npc.type == 79 || npc.type == 80) && Main.rand.Next(500) == 0)
				{
					Main.PlaySound(26, (int)npc.position.X, (int)npc.position.Y);
				}
				if (npc.type == 159 && Main.rand.Next(500) == 0)
				{
					Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 7);
				}
				if (npc.type == 162 && Main.rand.Next(500) == 0)
				{
					Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 6);
				}
				if (npc.type == 181 && Main.rand.Next(500) == 0)
				{
					Main.PlaySound(29, (int)npc.position.X, (int)npc.position.Y, 8);
				}
				if (npc.type >= 269 && npc.type <= 280 && Main.rand.Next(1000) == 0)
				{
					Main.PlaySound(14, (int)npc.position.X, (int)npc.position.Y);
				}
				npc.TargetClosest();
			}
			else if (npc.ai[2] <= 0f || (npc.type != 110 && npc.type != 111 && npc.type != 206 && npc.type != 216 && npc.type != 214 && npc.type != 215 && (npc.type != 291 && false) && npc.type != 292 && npc.type != 293 && npc.type != 350 && npc.type != 381 && npc.type != 382 && npc.type != 383 && npc.type != 385 && npc.type != 386 && npc.type != 389 && npc.type != 391 && npc.type != 469 && npc.type != 166 && npc.type != 466 && npc.type != 471 && npc.type != 411 && npc.type != 409 && npc.type != 424 && npc.type != 425 && npc.type != 426 && npc.type != 415 && npc.type != 419 && npc.type != 520))
			{
				if (Main.dayTime && (double)(npc.position.Y / 16f) < Main.worldSurface && npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
				if (npc.velocity.X == 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						ref float reference = ref npc.ai[0];
						reference += 1f;
						if (npc.ai[0] >= 2f)
						{
							npc.direction *= -1;
							npc.spriteDirection = npc.direction;
							npc.ai[0] = 0f;
						}
					}
				}
				else
				{
					npc.ai[0] = 0f;
				}
				if (npc.direction == 0)
				{
					npc.direction = 1;
				}
			}
			if (npc.type == 159 || npc.type == 349)
			{
				if (npc.type == 159 && ((npc.velocity.X > 0f && npc.direction < 0) || (npc.velocity.X < 0f && npc.direction > 0)))
				{
					npc.velocity.X *= 0.95f;
				}
				if (npc.velocity.X < -6f || npc.velocity.X > 6f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < 6f && npc.direction == 1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X < 0f)
					{
						npc.velocity.X *= 0.99f;
					}
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > 6f)
					{
						npc.velocity.X = 6f;
					}
				}
				else if (npc.velocity.X > -6f && npc.direction == -1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X > 0f)
					{
						npc.velocity.X *= 0.99f;
					}
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < -6f)
					{
						npc.velocity.X = -6f;
					}
				}
			}
			else if (npc.type == 199)
			{
				if (npc.velocity.X < -4f || npc.velocity.X > 4f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < 4f && npc.direction == 1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X < 0f)
					{
						npc.velocity.X *= 0.8f;
					}
					npc.velocity.X += 0.1f;
					if (npc.velocity.X > 4f)
					{
						npc.velocity.X = 4f;
					}
				}
				else if (npc.velocity.X > -4f && npc.direction == -1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X > 0f)
					{
						npc.velocity.X *= 0.8f;
					}
					npc.velocity.X -= 0.1f;
					if (npc.velocity.X < -4f)
					{
						npc.velocity.X = -4f;
					}
				}
			}
			else if (npc.type == 120 || npc.type == 166 || npc.type == 213 || npc.type == 258 || npc.type == 528 || npc.type == 529)
			{
				if (npc.velocity.X < -3f || npc.velocity.X > 3f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < 3f && npc.direction == 1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X < 0f)
					{
						npc.velocity.X *= 0.99f;
					}
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > 3f)
					{
						npc.velocity.X = 3f;
					}
				}
				else if (npc.velocity.X > -3f && npc.direction == -1)
				{
					if (npc.velocity.Y == 0f && npc.velocity.X > 0f)
					{
						npc.velocity.X *= 0.99f;
					}
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < -3f)
					{
						npc.velocity.X = -3f;
					}
				}
			}
			else if (npc.type == 461 || npc.type == 27 || npc.type == 77 || npc.type == 104 || npc.type == 163 || npc.type == 162 || npc.type == 196 || npc.type == 197 || npc.type == 212 || npc.type == 257 || npc.type == 326 || npc.type == 343 || npc.type == 348 || npc.type == 351 || (npc.type >= 524 && npc.type <= 527) || npc.type == 530)
			{
				if (npc.velocity.X < -2f || npc.velocity.X > 2f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < 2f && npc.direction == 1)
				{
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > 2f)
					{
						npc.velocity.X = 2f;
					}
				}
				else if (npc.velocity.X > -2f && npc.direction == -1)
				{
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < -2f)
					{
						npc.velocity.X = -2f;
					}
				}
			}
			else if (npc.type == 109)
			{
				if (npc.velocity.X < -2f || npc.velocity.X > 2f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < 2f && npc.direction == 1)
				{
					npc.velocity.X += 0.04f;
					if (npc.velocity.X > 2f)
					{
						npc.velocity.X = 2f;
					}
				}
				else if (npc.velocity.X > -2f && npc.direction == -1)
				{
					npc.velocity.X -= 0.04f;
					if (npc.velocity.X < -2f)
					{
						npc.velocity.X = -2f;
					}
				}
			}
			else if (npc.type == 21 || npc.type == 26 || npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296 || npc.type == 47 || npc.type == 73 || npc.type == 140 || npc.type == 164 || npc.type == 239 || npc.type == 167 || npc.type == 168 || npc.type == 185 || npc.type == 198 || npc.type == 201 || npc.type == 202 || npc.type == 203 || npc.type == 217 || npc.type == 218 || npc.type == 219 || npc.type == 226 || npc.type == 181 || npc.type == 254 || npc.type == 338 || npc.type == 339 || npc.type == 340 || npc.type == 342 || npc.type == 385 || npc.type == 389 || npc.type == 462 || npc.type == 463 || npc.type == 466 || npc.type == 464 || npc.type == 469 || npc.type == 470 || npc.type == 480 || npc.type == 482 || npc.type == 425 || npc.type == 429)
			{
				float num64 = 1.5f;
				if (npc.type == 294)
				{
					num64 = 2f;
				}
				else if (npc.type == 295)
				{
					num64 = 1.75f;
				}
				else if (npc.type == 296)
				{
					num64 = 1.25f;
				}
				else if (npc.type == 201)
				{
					num64 = 1.1f;
				}
				else if (npc.type == 202)
				{
					num64 = 0.9f;
				}
				else if (npc.type == 203)
				{
					num64 = 1.2f;
				}
				else if (npc.type == 338)
				{
					num64 = 1.75f;
				}
				else if (npc.type == 339)
				{
					num64 = 1.25f;
				}
				else if (npc.type == 340)
				{
					num64 = 2f;
				}
				else if (npc.type == 385)
				{
					num64 = 1.8f;
				}
				else if (npc.type == 389)
				{
					num64 = 2.25f;
				}
				else if (npc.type == 462)
				{
					num64 = 4f;
				}
				else if (npc.type == 463)
				{
					num64 = 0.75f;
				}
				else if (npc.type == 466)
				{
					num64 = 3.75f;
				}
				else if (npc.type == 469)
				{
					num64 = 3.25f;
				}
				else if (npc.type == 480)
				{
					num64 = 1.5f + (1f - (float)npc.life / (float)npc.lifeMax) * 2f;
				}
				else if (npc.type == 425)
				{
					num64 = 6f;
				}
				else if (npc.type == 429)
				{
					num64 = 4f;
				}
				if (npc.type == 21 || npc.type == 201 || npc.type == 202 || npc.type == 203 || npc.type == 342)
				{
					num64 *= 1f + (1f - npc.scale);
				}
				if (npc.velocity.X < 0f - num64 || npc.velocity.X > num64)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < num64 && npc.direction == 1)
				{
					if (npc.type == 466 && npc.velocity.X < -2f)
					{
						npc.velocity.X *= 0.9f;
					}
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > num64)
					{
						npc.velocity.X = num64;
					}
				}
				else if (npc.velocity.X > 0f - num64 && npc.direction == -1)
				{
					if (npc.type == 466 && npc.velocity.X > 2f)
					{
						npc.velocity.X *= 0.9f;
					}
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < 0f - num64)
					{
						npc.velocity.X = 0f - num64;
					}
				}
				if (npc.velocity.Y == 0f && npc.type == 462 && ((npc.direction > 0 && npc.velocity.X < 0f) || (npc.direction < 0 && npc.velocity.X > 0f)))
				{
					npc.velocity.X *= 0.9f;
				}
			}
			else if (npc.type >= 269 && npc.type <= 280)
			{
				float num65 = 1.5f;
				if (npc.type == 269)
				{
					num65 = 2f;
				}
				if (npc.type == 270)
				{
					num65 = 1f;
				}
				if (npc.type == 271)
				{
					num65 = 1.5f;
				}
				if (npc.type == 272)
				{
					num65 = 3f;
				}
				if (npc.type == 273)
				{
					num65 = 1.25f;
				}
				if (npc.type == 274)
				{
					num65 = 3f;
				}
				if (npc.type == 275)
				{
					num65 = 3.25f;
				}
				if (npc.type == 276)
				{
					num65 = 2f;
				}
				if (npc.type == 277)
				{
					num65 = 2.75f;
				}
				if (npc.type == 278)
				{
					num65 = 1.8f;
				}
				if (npc.type == 279)
				{
					num65 = 1.3f;
				}
				if (npc.type == 280)
				{
					num65 = 2.5f;
				}
				num65 *= 1f + (1f - npc.scale);
				if (npc.velocity.X < 0f - num65 || npc.velocity.X > num65)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < num65 && npc.direction == 1)
				{
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > num65)
					{
						npc.velocity.X = num65;
					}
				}
				else if (npc.velocity.X > 0f - num65 && npc.direction == -1)
				{
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < 0f - num65)
					{
						npc.velocity.X = 0f - num65;
					}
				}
			}
			else if (npc.type >= 305 && npc.type <= 314)
			{
				float num66 = 1.5f;
				if (npc.type == 305 || npc.type == 310)
				{
					num66 = 2f;
				}
				if (npc.type == 306 || npc.type == 311)
				{
					num66 = 1.25f;
				}
				if (npc.type == 307 || npc.type == 312)
				{
					num66 = 2.25f;
				}
				if (npc.type == 308 || npc.type == 313)
				{
					num66 = 1.5f;
				}
				if (npc.type == 309 || npc.type == 314)
				{
					num66 = 1f;
				}
				if (npc.type < 310)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.X *= 0.85f;
						if ((double)npc.velocity.X > -0.3 && (double)npc.velocity.X < 0.3)
						{
							npc.velocity.Y = -7f;
							npc.velocity.X = num66 * (float)npc.direction;
						}
					}
					else if (npc.spriteDirection == npc.direction)
					{
						npc.velocity.X = (npc.velocity.X * 10f + num66 * (float)npc.direction) / 11f;
					}
				}
				else if (npc.velocity.X < 0f - num66 || npc.velocity.X > num66)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < num66 && npc.direction == 1)
				{
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > num66)
					{
						npc.velocity.X = num66;
					}
				}
				else if (npc.velocity.X > 0f - num66 && npc.direction == -1)
				{
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < 0f - num66)
					{
						npc.velocity.X = 0f - num66;
					}
				}
			}
			else if (npc.type == 67 || npc.type == 220 || npc.type == 428)
			{
				if (npc.velocity.X < -0.5f || npc.velocity.X > 0.5f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < 0.5f && npc.direction == 1)
				{
					npc.velocity.X += 0.03f;
					if (npc.velocity.X > 0.5f)
					{
						npc.velocity.X = 0.5f;
					}
				}
				else if (npc.velocity.X > -0.5f && npc.direction == -1)
				{
					npc.velocity.X -= 0.03f;
					if (npc.velocity.X < -0.5f)
					{
						npc.velocity.X = -0.5f;
					}
				}
			}
			else if (npc.type == 78 || npc.type == 79 || npc.type == 80)
			{
				float num67 = 1f;
				float num68 = 0.05f;
				if (npc.life < npc.lifeMax / 2)
				{
					num67 = 2f;
					num68 = 0.1f;
				}
				if (npc.type == 79)
				{
					num67 *= 1.5f;
				}
				if (npc.velocity.X < 0f - num67 || npc.velocity.X > num67)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < num67 && npc.direction == 1)
				{
					npc.velocity.X += num68;
					if (npc.velocity.X > num67)
					{
						npc.velocity.X = num67;
					}
				}
				else if (npc.velocity.X > 0f - num67 && npc.direction == -1)
				{
					npc.velocity.X -= num68;
					if (npc.velocity.X < 0f - num67)
					{
						npc.velocity.X = 0f - num67;
					}
				}
			}
			else if (npc.type == 287)
			{
				float num69 = 5f;
				float num70 = 0.2f;
				if (npc.velocity.X < 0f - num69 || npc.velocity.X > num69)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < num69 && npc.direction == 1)
				{
					npc.velocity.X += num70;
					if (npc.velocity.X > num69)
					{
						npc.velocity.X = num69;
					}
				}
				else if (npc.velocity.X > 0f - num69 && npc.direction == -1)
				{
					npc.velocity.X -= num70;
					if (npc.velocity.X < 0f - num69)
					{
						npc.velocity.X = 0f - num69;
					}
				}
			}
			else if (npc.type == 243)
			{
				float num71 = 1f;
				float num72 = 0.07f;
				num71 += (1f - (float)npc.life / (float)npc.lifeMax) * 1.5f;
				num72 += (1f - (float)npc.life / (float)npc.lifeMax) * 0.15f;
				if (npc.velocity.X < 0f - num71 || npc.velocity.X > num71)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < num71 && npc.direction == 1)
				{
					npc.velocity.X += num72;
					if (npc.velocity.X > num71)
					{
						npc.velocity.X = num71;
					}
				}
				else if (npc.velocity.X > 0f - num71 && npc.direction == -1)
				{
					npc.velocity.X -= num72;
					if (npc.velocity.X < 0f - num71)
					{
						npc.velocity.X = 0f - num71;
					}
				}
			}
			else if (npc.type == 251)
			{
				float num73 = 1f;
				float num74 = 0.08f;
				num73 += (1f - (float)npc.life / (float)npc.lifeMax) * 2f;
				num74 += (1f - (float)npc.life / (float)npc.lifeMax) * 0.2f;
				if (npc.velocity.X < 0f - num73 || npc.velocity.X > num73)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < num73 && npc.direction == 1)
				{
					npc.velocity.X += num74;
					if (npc.velocity.X > num73)
					{
						npc.velocity.X = num73;
					}
				}
				else if (npc.velocity.X > 0f - num73 && npc.direction == -1)
				{
					npc.velocity.X -= num74;
					if (npc.velocity.X < 0f - num73)
					{
						npc.velocity.X = 0f - num73;
					}
				}
			}
			else if (npc.type == 386)
			{
				if (npc.ai[2] > 0f)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.X *= 0.8f;
					}
				}
				else
				{
					float num75 = 0.15f;
					float num76 = 1.5f;
					if (npc.velocity.X < 0f - num76 || npc.velocity.X > num76)
					{
						if (npc.velocity.Y == 0f)
						{
							npc.velocity *= 0.7f;
						}
					}
					else if (npc.velocity.X < num76 && npc.direction == 1)
					{
						npc.velocity.X += num75;
						if (npc.velocity.X > num76)
						{
							npc.velocity.X = num76;
						}
					}
					else if (npc.velocity.X > 0f - num76 && npc.direction == -1)
					{
						npc.velocity.X -= num75;
						if (npc.velocity.X < 0f - num76)
						{
							npc.velocity.X = 0f - num76;
						}
					}
				}
			}
			else if (npc.type == 460)
			{
				float num77 = 3f;
				float num78 = 0.1f;
				if (Math.Abs(npc.velocity.X) > 2f)
				{
					num78 *= 0.8f;
				}
				if ((double)Math.Abs(npc.velocity.X) > 2.5)
				{
					num78 *= 0.8f;
				}
				if (Math.Abs(npc.velocity.X) > 3f)
				{
					num78 *= 0.8f;
				}
				if ((double)Math.Abs(npc.velocity.X) > 3.5)
				{
					num78 *= 0.8f;
				}
				if (Math.Abs(npc.velocity.X) > 4f)
				{
					num78 *= 0.8f;
				}
				if ((double)Math.Abs(npc.velocity.X) > 4.5)
				{
					num78 *= 0.8f;
				}
				if (Math.Abs(npc.velocity.X) > 5f)
				{
					num78 *= 0.8f;
				}
				if ((double)Math.Abs(npc.velocity.X) > 5.5)
				{
					num78 *= 0.8f;
				}
				num77 += (1f - (float)npc.life / (float)npc.lifeMax) * 3f;
				if (npc.velocity.X < 0f - num77 || npc.velocity.X > num77)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.7f;
					}
				}
				else if (npc.velocity.X < num77 && npc.direction == 1)
				{
					if (npc.velocity.X < 0f)
					{
						npc.velocity.X *= 0.93f;
					}
					npc.velocity.X += num78;
					if (npc.velocity.X > num77)
					{
						npc.velocity.X = num77;
					}
				}
				else if (npc.velocity.X > 0f - num77 && npc.direction == -1)
				{
					if (npc.velocity.X > 0f)
					{
						npc.velocity.X *= 0.93f;
					}
					npc.velocity.X -= num78;
					if (npc.velocity.X < 0f - num77)
					{
						npc.velocity.X = 0f - num77;
					}
				}
			}
			else if (npc.type == 508)
			{
				float num79 = 2.5f;
				float num80 = 40f;
				float num81 = Math.Abs(npc.velocity.X);
				if (num81 > 2.75f)
				{
					num79 = 3.5f;
					num80 += 80f;
				}
				else if ((double)num81 > 2.25)
				{
					num79 = 3f;
					num80 += 60f;
				}
				if ((double)Math.Abs(npc.velocity.Y) < 0.5)
				{
					if (npc.velocity.X > 0f && npc.direction < 0)
					{
						npc.velocity *= 0.9f;
					}
					if (npc.velocity.X < 0f && npc.direction > 0)
					{
						npc.velocity *= 0.9f;
					}
				}
				if (Math.Abs(npc.velocity.Y) > 0.3f)
				{
					num80 *= 3f;
				}
				if (npc.velocity.X <= 0f && npc.direction < 0)
				{
					npc.velocity.X = (npc.velocity.X * num80 - num79) / (num80 + 1f);
				}
				else if (npc.velocity.X >= 0f && npc.direction > 0)
				{
					npc.velocity.X = (npc.velocity.X * num80 + num79) / (num80 + 1f);
				}
				else if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) > 20f && Math.Abs(npc.velocity.Y) <= 0.3f)
				{
					npc.velocity.X *= 0.99f;
					npc.velocity.X += (float)npc.direction * 0.025f;
				}
			}
			else if (npc.type == 391 || npc.type == 427 || npc.type == 415 || npc.type == 419 || npc.type == 518 || npc.type == 532)
			{
				float num82 = 5f;
				float num83 = 0.25f;
				float num84 = 0.7f;
				if (npc.type == 427)
				{
					num82 = 6f;
					num83 = 0.2f;
					num84 = 0.8f;
				}
				else if (npc.type == 415)
				{
					num82 = 4f;
					num83 = 0.1f;
					num84 = 0.95f;
				}
				else if (npc.type == 419)
				{
					num82 = 6f;
					num83 = 0.15f;
					num84 = 0.85f;
				}
				else if (npc.type == 518)
				{
					num82 = 5f;
					num83 = 0.1f;
					num84 = 0.95f;
				}
				else if (npc.type == 532)
				{
					num82 = 5f;
					num83 = 0.15f;
					num84 = 0.98f;
				}
				if (npc.velocity.X < 0f - num82 || npc.velocity.X > num82)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= num84;
					}
				}
				else if (npc.velocity.X < num82 && npc.direction == 1)
				{
					npc.velocity.X += num83;
					if (npc.velocity.X > num82)
					{
						npc.velocity.X = num82;
					}
				}
				else if (npc.velocity.X > 0f - num82 && npc.direction == -1)
				{
					npc.velocity.X -= num83;
					if (npc.velocity.X < 0f - num82)
					{
						npc.velocity.X = 0f - num82;
					}
				}
			}
			else if ((npc.type >= 430 && npc.type <= 436) || npc.type == 494 || npc.type == 495)
			{
				if (npc.ai[2] == 0f)
				{
					npc.damage = npc.defDamage;
					float num85 = 1f;
					num85 *= 1f + (1f - npc.scale);
					if (npc.velocity.X < 0f - num85 || npc.velocity.X > num85)
					{
						if (npc.velocity.Y == 0f)
						{
							npc.velocity *= 0.8f;
						}
					}
					else if (npc.velocity.X < num85 && npc.direction == 1)
					{
						npc.velocity.X += 0.07f;
						if (npc.velocity.X > num85)
						{
							npc.velocity.X = num85;
						}
					}
					else if (npc.velocity.X > 0f - num85 && npc.direction == -1)
					{
						npc.velocity.X -= 0.07f;
						if (npc.velocity.X < 0f - num85)
						{
							npc.velocity.X = 0f - num85;
						}
					}
					if (npc.velocity.Y == 0f && (!Main.dayTime || (double)npc.position.Y > Main.worldSurface * 16.0) && !Main.player[npc.target].dead)
					{
						Vector2 vector16 = npc.Center - Main.player[npc.target].Center;
						int num86 = 50;
						if (npc.type >= 494 && npc.type <= 495)
						{
							num86 = 42;
						}
						if (vector16.Length() < (float)num86 && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
						{
							npc.velocity.X *= 0.7f;
							npc.ai[2] = 1f;
						}
					}
				}
				else
				{
					npc.damage = (int)((double)npc.defDamage * 1.5);
					npc.ai[3] = 1f;
					npc.velocity.X *= 0.9f;
					if ((double)Math.Abs(npc.velocity.X) < 0.1)
					{
						npc.velocity.X = 0f;
					}
					ref float reference = ref npc.ai[2];
					reference += 1f;
					if (npc.ai[2] >= 20f || npc.velocity.Y != 0f || (Main.dayTime && (double)npc.position.Y < Main.worldSurface * 16.0))
					{
						npc.ai[2] = 0f;
					}
				}
			}
			else if (npc.type != 110 && npc.type != 111 && npc.type != 206 && npc.type != 214 && npc.type != 215 && npc.type != 216 && npc.type != 290 && (npc.type != 291 && false) && npc.type != 292 && npc.type != 293 && npc.type != 350 && npc.type != 379 && npc.type != 380 && npc.type != 381 && npc.type != 382 && (npc.type < 449 || npc.type > 452) && npc.type != 468 && npc.type != 481 && npc.type != 411 && npc.type != 409 && (npc.type < 498 || npc.type > 506) && npc.type != 424 && npc.type != 426 && npc.type != 520)
			{
				float num87 = 1f;
				if (npc.type == 186)
				{
					num87 = 1.1f;
				}
				if (npc.type == 187)
				{
					num87 = 0.9f;
				}
				if (npc.type == 188)
				{
					num87 = 1.2f;
				}
				if (npc.type == 189)
				{
					num87 = 0.8f;
				}
				if (npc.type == 132)
				{
					num87 = 0.95f;
				}
				if (npc.type == 200)
				{
					num87 = 0.87f;
				}
				if (npc.type == 223)
				{
					num87 = 1.05f;
				}
				if (npc.type == 489)
				{
					float num88 = (Main.player[npc.target].Center - npc.Center).Length();
					num88 *= 0.0025f;
					if ((double)num88 > 1.5)
					{
						num88 = 1.5f;
					}
					num87 = ((!Main.expertMode) ? (2.5f - num88) : (3f - num88));
					num87 *= 0.8f;
				}
				if (npc.type == 489 || npc.type == 3 || npc.type == 132 || npc.type == 186 || npc.type == 187 || npc.type == 188 || npc.type == 189 || npc.type == 200 || npc.type == 223 || npc.type == 331 || npc.type == 332)
				{
					num87 *= 1f + (1f - npc.scale);
				}
				if (npc.velocity.X < 0f - num87 || npc.velocity.X > num87)
				{
					if (npc.velocity.Y == 0f)
					{
						npc.velocity *= 0.8f;
					}
				}
				else if (npc.velocity.X < num87 && npc.direction == 1)
				{
					npc.velocity.X += 0.07f;
					if (npc.velocity.X > num87)
					{
						npc.velocity.X = num87;
					}
				}
				else if (npc.velocity.X > 0f - num87 && npc.direction == -1)
				{
					npc.velocity.X -= 0.07f;
					if (npc.velocity.X < 0f - num87)
					{
						npc.velocity.X = 0f - num87;
					}
				}
			}
			if (npc.type >= 277 && npc.type <= 280)
			{
				Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.2f, 0.1f, 0f);
			}
			else if (npc.type == 520)
			{
				Lighting.AddLight(npc.Top + new Vector2(0f, 20f), 0.3f, 0.3f, 0.7f);
			}
			else if (npc.type == 525)
			{
				Vector3 rgb = new Vector3(0.7f, 1f, 0.2f) * 0.5f;
				Lighting.AddLight(npc.Top + new Vector2(0f, 15f), rgb);
			}
			else if (npc.type == 526)
			{
				Vector3 rgb2 = new Vector3(1f, 1f, 0.5f) * 0.4f;
				Lighting.AddLight(npc.Top + new Vector2(0f, 15f), rgb2);
			}
			else if (npc.type == 527)
			{
				Vector3 rgb3 = new Vector3(0.6f, 0.3f, 1f) * 0.4f;
				Lighting.AddLight(npc.Top + new Vector2(0f, 15f), rgb3);
			}
			else if (npc.type == 415)
			{
				npc.hide = false;
				int num26;
				for (int num89 = 0; num89 < 200; num89 = num26 + 1)
				{
					if (Main.npc[num89].active && Main.npc[num89].type == 416 && Main.npc[num89].ai[0] == (float)npc.whoAmI)
					{
						npc.hide = true;
						break;
					}
					num26 = num89;
				}
			}
			else if (npc.type == 258)
			{
				if (npc.velocity.Y != 0f)
				{
					npc.TargetClosest();
					npc.spriteDirection = npc.direction;
					if (Main.player[npc.target].Center.X < npc.position.X && npc.velocity.X > 0f)
					{
						npc.velocity.X *= 0.95f;
					}
					else if (Main.player[npc.target].Center.X > npc.position.X + (float)npc.width && npc.velocity.X < 0f)
					{
						npc.velocity.X *= 0.95f;
					}
					if (Main.player[npc.target].Center.X < npc.position.X && npc.velocity.X > -5f)
					{
						npc.velocity.X -= 0.1f;
					}
					else if (Main.player[npc.target].Center.X > npc.position.X + (float)npc.width && npc.velocity.X < 5f)
					{
						npc.velocity.X += 0.1f;
					}
				}
				else if (Main.player[npc.target].Center.Y + 50f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					npc.velocity.Y = -7f;
				}
			}
			else if (npc.type == 425)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.ai[2] = 0f;
				}
				if (npc.velocity.Y != 0f && npc.ai[2] == 1f)
				{
					npc.TargetClosest();
					npc.spriteDirection = -npc.direction;
					if (Collision.CanHit(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
					{
						float num90 = Main.player[npc.target].Center.X - (float)(npc.direction * 400) - npc.Center.X;
						float num91 = Main.player[npc.target].Bottom.Y - npc.Bottom.Y;
						if (num90 < 0f && npc.velocity.X > 0f)
						{
							npc.velocity.X *= 0.9f;
						}
						else if (num90 > 0f && npc.velocity.X < 0f)
						{
							npc.velocity.X *= 0.9f;
						}
						if (num90 < 0f && npc.velocity.X > -5f)
						{
							npc.velocity.X -= 0.1f;
						}
						else if (num90 > 0f && npc.velocity.X < 5f)
						{
							npc.velocity.X += 0.1f;
						}
						if (npc.velocity.X > 6f)
						{
							npc.velocity.X = 6f;
						}
						if (npc.velocity.X < -6f)
						{
							npc.velocity.X = -6f;
						}
						if (num91 < -20f && npc.velocity.Y > 0f)
						{
							npc.velocity.Y *= 0.8f;
						}
						else if (num91 > 20f && npc.velocity.Y < 0f)
						{
							npc.velocity.Y *= 0.8f;
						}
						if (num91 < -20f && npc.velocity.Y > -5f)
						{
							npc.velocity.Y -= 0.3f;
						}
						else if (num91 > 20f && npc.velocity.Y < 5f)
						{
							npc.velocity.Y += 0.3f;
						}
					}
					if (Main.rand.Next(3) == 0)
					{
						Vector2 position = npc.Center + new Vector2(npc.direction * -14, -8f) - Vector2.One * 4f;
						Vector2 vector17 = new Vector2(npc.direction * -6, 12f) * 0.2f + Utils.RandomVector2(Main.rand, -1f, 1f) * 0.1f;
						Dust dust11 = Main.dust[Dust.NewDust(position, 8, 8, 229, vector17.X, vector17.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 0.5f)];
						dust11.noGravity = true;
						dust11.velocity = vector17;
						dust11.customData = this;
					}
					int num26;
					for (int num92 = 0; num92 < 200; num92 = num26 + 1)
					{
						if (num92 != npc.whoAmI && Main.npc[num92].active && Main.npc[num92].type == npc.type && Math.Abs(npc.position.X - Main.npc[num92].position.X) + Math.Abs(npc.position.Y - Main.npc[num92].position.Y) < (float)npc.width)
						{
							if (npc.position.X < Main.npc[num92].position.X)
							{
								npc.velocity.X -= 0.05f;
							}
							else
							{
								npc.velocity.X += 0.05f;
							}
							if (npc.position.Y < Main.npc[num92].position.Y)
							{
								npc.velocity.Y -= 0.05f;
							}
							else
							{
								npc.velocity.Y += 0.05f;
							}
						}
						num26 = num92;
					}
				}
				else if (Main.player[npc.target].Center.Y + 100f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					npc.velocity.Y = -5f;
					npc.ai[2] = 1f;
				}
				if (Main.netMode != 1)
				{
					ref float reference = ref npc.localAI[2];
					reference += 1f;
					if (npc.localAI[2] >= (float)(360 + Main.rand.Next(360)) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
					{
						npc.localAI[2] = 0f;
						Vector2 vector18 = npc.Center + new Vector2(npc.direction * 30, 2f);
						Vector2 vector19 = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
						if (vector19.HasNaNs())
						{
							vector19 = new Vector2(npc.direction * 8, 0f);
						}
						int num93 = Main.expertMode ? 50 : 75;
						int num26;
						for (int num94 = 0; num94 < 4; num94 = num26 + 1)
						{
							Vector2 vector20 = vector19 + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
							Projectile.NewProjectile(vector18.X, vector18.Y, vector20.X, vector20.Y, 577, num93, 1f, Main.myPlayer);
							num26 = num94;
						}
					}
				}
			}
			else if (npc.type == 427)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.ai[2] = 0f;
					npc.rotation = 0f;
				}
				else
				{
					npc.rotation = npc.velocity.X * 0.1f;
				}
				if (npc.velocity.Y != 0f && npc.ai[2] == 1f)
				{
					npc.TargetClosest();
					npc.spriteDirection = -npc.direction;
					if (Collision.CanHit(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
					{
						float num95 = Main.player[npc.target].Center.X - npc.Center.X;
						float num96 = Main.player[npc.target].Center.Y - npc.Center.Y;
						if (num95 < 0f && npc.velocity.X > 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						else if (num95 > 0f && npc.velocity.X < 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						if (num95 < -20f && npc.velocity.X > -6f)
						{
							npc.velocity.X -= 0.015f;
						}
						else if (num95 > 20f && npc.velocity.X < 6f)
						{
							npc.velocity.X += 0.015f;
						}
						if (npc.velocity.X > 6f)
						{
							npc.velocity.X = 6f;
						}
						if (npc.velocity.X < -6f)
						{
							npc.velocity.X = -6f;
						}
						if (num96 < -20f && npc.velocity.Y > 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						else if (num96 > 20f && npc.velocity.Y < 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						if (num96 < -20f && npc.velocity.Y > -6f)
						{
							npc.velocity.Y -= 0.15f;
						}
						else if (num96 > 20f && npc.velocity.Y < 6f)
						{
							npc.velocity.Y += 0.15f;
						}
					}
					int num26;
					for (int num97 = 0; num97 < 200; num97 = num26 + 1)
					{
						if (num97 != npc.whoAmI && Main.npc[num97].active && Main.npc[num97].type == npc.type && Math.Abs(npc.position.X - Main.npc[num97].position.X) + Math.Abs(npc.position.Y - Main.npc[num97].position.Y) < (float)npc.width)
						{
							if (npc.position.X < Main.npc[num97].position.X)
							{
								npc.velocity.X -= 0.05f;
							}
							else
							{
								npc.velocity.X += 0.05f;
							}
							if (npc.position.Y < Main.npc[num97].position.Y)
							{
								npc.velocity.Y -= 0.05f;
							}
							else
							{
								npc.velocity.Y += 0.05f;
							}
						}
						num26 = num97;
					}
				}
				else if (Main.player[npc.target].Center.Y + 100f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					npc.velocity.Y = -5f;
					npc.ai[2] = 1f;
				}
			}
			else if (npc.type == 426)
			{
				if (npc.ai[1] > 0f && npc.velocity.Y > 0f)
				{
					npc.velocity.Y *= 0.85f;
					if (npc.velocity.Y == 0f)
					{
						npc.velocity.Y = -0.4f;
					}
				}
				if (npc.velocity.Y != 0f)
				{
					npc.TargetClosest();
					npc.spriteDirection = npc.direction;
					if (Collision.CanHit(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
					{
						float num98 = Main.player[npc.target].Center.X - (float)(npc.direction * 300) - npc.Center.X;
						if (num98 < 40f && npc.velocity.X > 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						else if (num98 > 40f && npc.velocity.X < 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						if (num98 < 40f && npc.velocity.X > -5f)
						{
							npc.velocity.X -= 0.2f;
						}
						else if (num98 > 40f && npc.velocity.X < 5f)
						{
							npc.velocity.X += 0.2f;
						}
						if (npc.velocity.X > 6f)
						{
							npc.velocity.X = 6f;
						}
						if (npc.velocity.X < -6f)
						{
							npc.velocity.X = -6f;
						}
					}
				}
				else if (Main.player[npc.target].Center.Y + 100f < npc.position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					npc.velocity.Y = -6f;
				}
				int num26;
				for (int num99 = 0; num99 < 200; num99 = num26 + 1)
				{
					if (num99 != npc.whoAmI && Main.npc[num99].active && Main.npc[num99].type == npc.type && Math.Abs(npc.position.X - Main.npc[num99].position.X) + Math.Abs(npc.position.Y - Main.npc[num99].position.Y) < (float)npc.width)
					{
						if (npc.position.X < Main.npc[num99].position.X)
						{
							npc.velocity.X -= 0.1f;
						}
						else
						{
							npc.velocity.X += 0.1f;
						}
						if (npc.position.Y < Main.npc[num99].position.Y)
						{
							npc.velocity.Y -= 0.1f;
						}
						else
						{
							npc.velocity.Y += 0.1f;
						}
					}
					num26 = num99;
				}
				if (Main.rand.Next(6) == 0 && npc.ai[1] <= 20f)
				{
					Dust dust12 = Main.dust[Dust.NewDust(npc.Center + new Vector2((npc.spriteDirection == 1) ? 8 : (-20), -20f), 8, 8, 229, npc.velocity.X, npc.velocity.Y, 100)];
					dust12.velocity = dust12.velocity / 4f + npc.velocity / 2f;
					dust12.scale = 0.6f;
					dust12.noLight = true;
				}
				if (npc.ai[1] >= 57f)
				{
					int num100 = Utils.SelectRandom<int>(Main.rand, 161, 229);
					Dust dust13 = Main.dust[Dust.NewDust(npc.Center + new Vector2((npc.spriteDirection == 1) ? 8 : (-20), -20f), 8, 8, num100, npc.velocity.X, npc.velocity.Y, 100)];
					dust13.velocity = dust13.velocity / 4f + npc.DirectionTo(Main.player[npc.target].Top);
					dust13.scale = 1.2f;
					dust13.noLight = true;
				}
				if (Main.rand.Next(6) == 0)
				{
					Dust dust14 = Main.dust[Dust.NewDust(npc.Center, 2, 2, 229)];
					dust14.position = npc.Center + new Vector2((npc.spriteDirection == 1) ? 26 : (-26), 24f);
					dust14.velocity.X = 0f;
					if (dust14.velocity.Y < 0f)
					{
						dust14.velocity.Y = 0f;
					}
					dust14.noGravity = true;
					dust14.scale = 1f;
					dust14.noLight = true;
				}
			}
			else if (npc.type == 185)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.rotation = 0f;
					npc.localAI[0] = 0f;
				}
				else if (npc.localAI[0] == 1f)
				{
					npc.rotation += npc.velocity.X * 0.05f;
				}
			}
			else if (npc.type == 428)
			{
				if (npc.velocity.Y == 0f)
				{
					npc.rotation = 0f;
				}
				else
				{
					npc.rotation += npc.velocity.X * 0.08f;
				}
			}
			if (npc.type == 159 && Main.netMode != 1)
			{
				Vector2 vector21 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float num101 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector21.X;
				float num102 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector21.Y;
				float num103 = (float)Math.Sqrt(num101 * num101 + num102 * num102);
				if (num103 > 300f)
				{
					npc.Transform(158);
				}
			}
			if (npc.type == 164 && Main.netMode != 1 && npc.velocity.Y == 0f)
			{
				int num104 = (int)npc.Center.X / 16;
				int num105 = (int)npc.Center.Y / 16;
				bool flag9 = false;
				int num26;
				for (int num106 = num104 - 1; num106 <= num104 + 1; num106 = num26 + 1)
				{
					for (int num107 = num105 - 1; num107 <= num105 + 1; num107 = num26 + 1)
					{
						if (Main.tile[num106, num107].wall > 0)
						{
							flag9 = true;
						}
						num26 = num107;
					}
					num26 = num106;
				}
				if (flag9)
				{
					npc.Transform(165);
				}
			}
			if (npc.type == 239 && Main.netMode != 1 && npc.velocity.Y == 0f)
			{
				int num108 = (int)npc.Center.X / 16;
				int num109 = (int)npc.Center.Y / 16;
				bool flag10 = false;
				int num26;
				for (int num110 = num108 - 1; num110 <= num108 + 1; num110 = num26 + 1)
				{
					for (int num111 = num109 - 1; num111 <= num109 + 1; num111 = num26 + 1)
					{
						if (Main.tile[num110, num111].wall > 0)
						{
							flag10 = true;
						}
						num26 = num111;
					}
					num26 = num110;
				}
				if (flag10)
				{
					npc.Transform(240);
				}
			}
			if (npc.type == 530 && Main.netMode != 1 && npc.velocity.Y == 0f)
			{
				int num112 = (int)npc.Center.X / 16;
				int num113 = (int)npc.Center.Y / 16;
				bool flag11 = false;
				int num26;
				for (int num114 = num112 - 1; num114 <= num112 + 1; num114 = num26 + 1)
				{
					for (int num115 = num113 - 1; num115 <= num113 + 1; num115 = num26 + 1)
					{
						if (Main.tile[num114, num115].wall > 0)
						{
							flag11 = true;
						}
						num26 = num115;
					}
					num26 = num114;
				}
				if (flag11)
				{
					npc.Transform(531);
				}
			}
			if (Main.netMode != 1 && Main.expertMode && npc.target >= 0 && (npc.type == 163 || npc.type == 238) && Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
			{
				ref float reference = ref npc.localAI[0];
				reference += 1f;
				if (npc.justHit)
				{
					reference = ref npc.localAI[0];
					reference -= Main.rand.Next(20, 60);
					if (npc.localAI[0] < 0f)
					{
						npc.localAI[0] = 0f;
					}
				}
				if (npc.localAI[0] > (float)Main.rand.Next(180, 900))
				{
					npc.localAI[0] = 0f;
					Vector2 vector22 = Main.player[npc.target].Center - npc.Center;
					vector22.Normalize();
					vector22 *= 8f;
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector22.X, vector22.Y, 472, 18, 0f, Main.myPlayer);
				}
			}
			if (npc.type == 163 && Main.netMode != 1 && npc.velocity.Y == 0f)
			{
				int num116 = (int)npc.Center.X / 16;
				int num117 = (int)npc.Center.Y / 16;
				bool flag12 = false;
				int num26;
				for (int num118 = num116 - 1; num118 <= num116 + 1; num118 = num26 + 1)
				{
					for (int num119 = num117 - 1; num119 <= num117 + 1; num119 = num26 + 1)
					{
						if (Main.tile[num118, num119].wall > 0)
						{
							flag12 = true;
						}
						num26 = num119;
					}
					num26 = num118;
				}
				if (flag12)
				{
					npc.Transform(238);
				}
			}
			if (npc.type == 236 && Main.netMode != 1 && npc.velocity.Y == 0f)
			{
				int num120 = (int)npc.Center.X / 16;
				int num121 = (int)npc.Center.Y / 16;
				bool flag13 = false;
				int num26;
				for (int num122 = num120 - 1; num122 <= num120 + 1; num122 = num26 + 1)
				{
					for (int num123 = num121 - 1; num123 <= num121 + 1; num123 = num26 + 1)
					{
						if (Main.tile[num122, num123].wall > 0)
						{
							flag13 = true;
						}
						num26 = num123;
					}
					num26 = num122;
				}
				if (flag13)
				{
					npc.Transform(237);
				}
			}
			if (npc.type == 243)
			{
				if (npc.justHit && Main.rand.Next(3) == 0)
				{
                    npc.ai[2] -= Main.rand.Next(30);
				}
				if (npc.ai[2] < 0f)
				{
					npc.ai[2] = 0f;
				}
				if (npc.confused)
				{
					npc.ai[2] = 0f;
				}
                npc.ai[2] += 1f;
				float num124 = Main.rand.Next(30, 900);
				num124 *= (float)npc.life / (float)npc.lifeMax;
				num124 += 30f;
				if (Main.netMode != 1 && npc.ai[2] >= num124 && npc.velocity.Y == 0f && !Main.player[npc.target].dead && !Main.player[npc.target].frozen && ((npc.direction > 0 && npc.Center.X < Main.player[npc.target].Center.X) || (npc.direction < 0 && npc.Center.X > Main.player[npc.target].Center.X)) && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					float num125 = 15f;
					Vector2 vector23 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + 20f);
                    vector23.X += 10 * npc.direction;
					float num126 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
					float num127 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
					num126 += (float)Main.rand.Next(-40, 41);
					num127 += (float)Main.rand.Next(-40, 41);
					float num128 = (float)Math.Sqrt(num126 * num126 + num127 * num127);
					npc.netUpdate = true;
					num128 = num125 / num128;
					num126 *= num128;
					num127 *= num128;
					int num129 = 32;
					int num130 = 257;
                    vector23.X += num126 * 3f;
                    vector23.Y += num127 * 3f;
					Projectile.NewProjectile(vector23.X, vector23.Y, num126, num127, num130, num129, 0f, Main.myPlayer);
					npc.ai[2] = 0f;
				}
			}
			if (npc.type == 251)
			{
				if (npc.justHit)
				{
                    npc.ai[2] -= Main.rand.Next(30);
				}
				if (npc.ai[2] < 0f)
				{
					npc.ai[2] = 0f;
				}
				if (npc.confused)
				{
					npc.ai[2] = 0f;
				}
                npc.ai[2] += 1f;
				float num131 = Main.rand.Next(60, 1800);
				num131 *= (float)npc.life / (float)npc.lifeMax;
				num131 += 15f;
				if (Main.netMode != 1 && npc.ai[2] >= num131 && npc.velocity.Y == 0f && !Main.player[npc.target].dead && !Main.player[npc.target].frozen && ((npc.direction > 0 && npc.Center.X < Main.player[npc.target].Center.X) || (npc.direction < 0 && npc.Center.X > Main.player[npc.target].Center.X)) && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					float num132 = 15f;
					Vector2 vector24 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + 12f);
                    vector24.X += 6 * npc.direction;
					float num133 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector24.X;
					float num134 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector24.Y;
					num133 += (float)Main.rand.Next(-40, 41);
					num134 += (float)Main.rand.Next(-30, 0);
					float num135 = (float)Math.Sqrt(num133 * num133 + num134 * num134);
					npc.netUpdate = true;
					num135 = num132 / num135;
					num133 *= num135;
					num134 *= num135;
					int num136 = 30;
					int num137 = 83;
                    vector24.X += num133 * 3f;
                    vector24.Y += num134 * 3f;
					Projectile.NewProjectile(vector24.X, vector24.Y, num133, num134, num137, num136, 0f, Main.myPlayer);
					npc.ai[2] = 0f;
				}
			}
			if (npc.type == 386)
			{
				if (npc.confused)
				{
					npc.ai[2] = -60f;
				}
				else
				{
					if (npc.ai[2] < 60f)
					{
						ref float reference = ref npc.ai[2];
						reference += 1f;
					}
					if (npc.ai[2] > 0f && NPC.CountNPCS(387) >= 4 * NPC.CountNPCS(386))
					{
						npc.ai[2] = 0f;
					}
					if (npc.justHit)
					{
						npc.ai[2] = -30f;
					}
					if (npc.ai[2] == 30f)
					{
						int num138 = (int)npc.position.X / 16;
						int num139 = (int)npc.position.Y / 16;
						int num140 = (int)npc.position.X / 16;
						int num141 = (int)npc.position.Y / 16;
						int num142 = 5;
						int num143 = 0;
						bool flag14 = false;
						int num144 = 2;
						int num145 = 0;
						while (!flag14 && num143 < 100)
						{
							int num26 = num143;
							num143 = num26 + 1;
							int num146 = Main.rand.Next(num138 - num142, num138 + num142);
							int num147 = Main.rand.Next(num139 - num142, num139 + num142);
							for (int num148 = num147; num148 < num139 + num142; num148 = num26 + 1)
							{
								if ((num148 < num139 - num144 || num148 > num139 + num144 || num146 < num138 - num144 || num146 > num138 + num144) && (num148 < num141 - num145 || num148 > num141 + num145 || num146 < num140 - num145 || num146 > num140 + num145) && Main.tile[num146, num148].nactive())
								{
									bool flag15 = true;
									if (Main.tile[num146, num148 - 1].lava())
									{
										flag15 = false;
									}
									if (flag15 && Main.tileSolid[Main.tile[num146, num148].type] && !Collision.SolidTiles(num146 - 1, num146 + 1, num148 - 4, num148 - 1))
									{
										int num149 = NPC.NewNPC(num146 * 16 - npc.width / 2, num148 * 16, 387);
										Main.npc[num149].position.Y = num148 * 16 - Main.npc[num149].height;
										flag14 = true;
										npc.netUpdate = true;
										break;
									}
								}
								num26 = num148;
							}
						}
					}
					if (npc.ai[2] == 60f)
					{
						npc.ai[2] = -120f;
					}
				}
			}
			if (npc.type == 389)
			{
				if (npc.confused)
				{
					npc.ai[2] = -60f;
				}
				else
				{
					if (npc.ai[2] < 20f)
					{
						ref float reference = ref npc.ai[2];
						reference += 1f;
					}
					if (npc.justHit)
					{
						npc.ai[2] = -30f;
					}
					if (npc.ai[2] == 20f && Main.netMode != 1)
					{
						npc.ai[2] = -10 + Main.rand.Next(3) * -10;
						Projectile.NewProjectile(npc.Center.X, npc.Center.Y + 8f, npc.direction * 6, 0f, 437, 25, 1f, Main.myPlayer);
					}
				}
			}
			if (npc.type == 110 || npc.type == 111 || npc.type == 206 || npc.type == 214 || npc.type == 215 || npc.type == 216 || npc.type == 290 || (npc.type == 291 || true) || npc.type == 292 || npc.type == 293 || npc.type == 350 || npc.type == 379 || npc.type == 380 || npc.type == 381 || npc.type == 382 || (npc.type >= 449 && npc.type <= 452) || npc.type == 468 || npc.type == 481 || npc.type == 411 || npc.type == 409 || (npc.type >= 498 && npc.type <= 506) || npc.type == 424 || npc.type == 426 || npc.type == 520)
			{
				bool flag16 = npc.type == 381 || npc.type == 382 || npc.type == 520;
				bool flag17 = npc.type == 426;
				bool flag18 = true;
				int num150 = -1;
				int num151 = -1;
				if (npc.type == 411)
				{
					flag16 = true;
					num150 = 90;
					num151 = 90;
					if (npc.ai[1] <= 150f)
					{
						flag18 = false;
					}
				}
				if (npc.confused)
				{
					npc.ai[2] = 0f;
				}
				else
				{
					if (npc.ai[1] > 0f)
					{
						ref float reference = ref npc.ai[1];
						reference -= 1f;
					}
					if (npc.justHit)
					{
						npc.ai[1] = 30f;
						npc.ai[2] = 0f;
					}
					int num152 = 70;
					if (npc.type == 379 || npc.type == 380)
					{
						num152 = 80;
					}
					if (npc.type == 381 || npc.type == 382)
					{
						num152 = 80;
					}
					if (npc.type == 520)
					{
						num152 = 15;
					}
					if (npc.type == 350)
					{
						num152 = 110;
					}
					if ((npc.type == 291 || true))
					{
						num152 = 200;
					}
					if (npc.type == 292)
					{
						num152 = 120;
					}
					if (npc.type == 293)
					{
						num152 = 90;
					}
					if (npc.type == 111)
					{
						num152 = 180;
					}
					if (npc.type == 206)
					{
						num152 = 50;
					}
					if (npc.type == 481)
					{
						num152 = 100;
					}
					if (npc.type == 214)
					{
						num152 = 40;
					}
					if (npc.type == 215)
					{
						num152 = 80;
					}
					if (npc.type == 290)
					{
						num152 = 30;
					}
					if (npc.type == 411)
					{
						num152 = 300;
					}
					if (npc.type == 409)
					{
						num152 = 60;
					}
					if (npc.type == 424)
					{
						num152 = 180;
					}
					if (npc.type == 426)
					{
						num152 = 60;
					}
					bool flag19 = false;
					if (npc.type == 216)
					{
						if (npc.localAI[2] >= 20f)
						{
							flag19 = true;
						}
						num152 = ((!flag19) ? 8 : 60);
					}
					int num153 = num152 / 2;
					if (npc.type == 424)
					{
						num153 = num152 - 1;
					}
					if (npc.type == 426)
					{
						num153 = num152 - 1;
					}
					if (npc.ai[2] > 0f)
					{
						if (flag18)
						{
							npc.TargetClosest();
						}
						if (npc.ai[1] == (float)num153)
						{

							if (npc.type == 216)
							{
                                npc.localAI[2] += 1f;
							}
							float num154 = 11f;
							if (npc.type == 111)
							{
								num154 = 9f;
							}
							if (npc.type == 206)
							{
								num154 = 7f;
							}
							if (npc.type == 290)
							{
								num154 = 9f;
							}
							if (npc.type == 293)
							{
								num154 = 4f;
							}
							if (npc.type == 214)
							{
								num154 = 14f;
							}
							if (npc.type == 215)
							{
								num154 = 16f;
							}
							if (npc.type == 382)
							{
								num154 = 7f;
							}
							if (npc.type == 520)
							{
								num154 = 8f;
							}
							if (npc.type == 409)
							{
								num154 = 4f;
							}
							if (npc.type >= 449 && npc.type <= 452)
							{
								num154 = 7f;
							}
							if (npc.type == 481)
							{
								num154 = 8f;
							}
							if (npc.type == 468)
							{
								num154 = 7.5f;
							}
							if (npc.type == 411)
							{
								num154 = 1f;
							}
							if (npc.type >= 498 && npc.type <= 506)
							{
								num154 = 7f;
							}
							Vector2 vector25 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							if (npc.type == 481)
							{
                                vector25.Y -= 14f;
							}
							if (npc.type == 206)
							{
                                vector25.Y -= 10f;
							}
							if (npc.type == 290)
							{
                                vector25.Y -= 10f;
							}
							if (npc.type == 381 || npc.type == 382)
							{
                                vector25.Y += 6f;
							}
							if (npc.type == 520)
							{
								vector25.Y = npc.position.Y + 20f;
							}
							if (npc.type >= 498 && npc.type <= 506)
							{
                                vector25.Y -= 8f;
							}
							if (npc.type == 426)
							{
								vector25 += new Vector2(npc.spriteDirection * 2, -12f);
							}
							float num155 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector25.X;
							float num156 = Math.Abs(num155) * 0.1f;
							if ((npc.type == 291 || true) || npc.type == 292)
							{
								num156 = 0f;
							}
							if (npc.type == 215)
							{
								num156 = Math.Abs(num155) * 0.08f;
							}
							if (npc.type == 214 || (npc.type == 216 && !flag19))
							{
								num156 = 0f;
							}
							if (npc.type == 381 || npc.type == 382 || npc.type == 520)
							{
								num156 = 0f;
							}
							if (npc.type >= 449 && npc.type <= 452)
							{
								num156 = Math.Abs(num155) * (float)Main.rand.Next(10, 50) * 0.01f;
							}
							if (npc.type == 468)
							{
								num156 = Math.Abs(num155) * (float)Main.rand.Next(10, 50) * 0.01f;
							}
							if (npc.type == 481)
							{
								num156 = Math.Abs(num155) * (float)Main.rand.Next(-10, 11) * 0.0035f;
							}
							if (npc.type >= 498 && npc.type <= 506)
							{
								num156 = Math.Abs(num155) * (float)Main.rand.Next(1, 11) * 0.0025f;
							}
							float num157 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector25.Y - num156;
							if ((npc.type == 291 || true))
							{
								num155 += (float)Main.rand.Next(-40, 41) * 0.2f;
								num157 += (float)Main.rand.Next(-40, 41) * 0.2f;
							}
							else if (npc.type == 381 || npc.type == 382 || npc.type == 520)
							{
								num155 += (float)Main.rand.Next(-100, 101) * 0.4f;
								num157 += (float)Main.rand.Next(-100, 101) * 0.4f;
								num155 *= (float)Main.rand.Next(85, 116) * 0.01f;
								num157 *= (float)Main.rand.Next(85, 116) * 0.01f;
								if (npc.type == 520)
								{
									num155 += (float)Main.rand.Next(-100, 101) * 0.6f;
									num157 += (float)Main.rand.Next(-100, 101) * 0.6f;
									num155 *= (float)Main.rand.Next(85, 116) * 0.015f;
									num157 *= (float)Main.rand.Next(85, 116) * 0.015f;
								}
							}
							else if (npc.type == 481)
							{
								num155 += (float)Main.rand.Next(-40, 41) * 0.4f;
								num157 += (float)Main.rand.Next(-40, 41) * 0.4f;
							}
							else if (npc.type >= 498 && npc.type <= 506)
							{
								num155 += (float)Main.rand.Next(-40, 41) * 0.3f;
								num157 += (float)Main.rand.Next(-40, 41) * 0.3f;
							}
							else if (npc.type != 292)
							{
								num155 += (float)Main.rand.Next(-40, 41);
								num157 += (float)Main.rand.Next(-40, 41);
							}
							float num158 = (float)Math.Sqrt(num155 * num155 + num157 * num157);
							npc.netUpdate = true;
							num158 = num154 / num158;
							num155 *= num158;
							num157 *= num158;
							int num159 = 35;
							int num160 = 82;
							if (npc.type == 111)
							{
								num159 = 11;
							}
							if (npc.type == 206)
							{
								num159 = 37;
							}
							if (npc.type == 379 || npc.type == 380)
							{
								num159 = 40;
							}
							if (npc.type == 350)
							{
								num159 = 45;
							}
							if (npc.type == 468)
							{
								num159 = 50;
							}
							if (npc.type == 111)
							{
								num160 = 81;
							}
							if (npc.type == 379 || npc.type == 380)
							{
								num160 = 81;
							}
							if (npc.type == 381)
							{
								num160 = 436;
								num159 = 24;
							}
							if (npc.type == 382)
							{
								num160 = 438;
								num159 = 30;
							}
							if (npc.type == 520)
							{
								num160 = 592;
								num159 = 35;
							}
							if (npc.type >= 449 && npc.type <= 452)
							{
								num160 = 471;
								num159 = 20;
							}
							if (npc.type >= 498 && npc.type <= 506)
							{
								num160 = 572;
								num159 = 14;
							}
							if (npc.type == 481)
							{
								num160 = 508;
								num159 = 18;
							}
							if (npc.type == 206)
							{
								num160 = 177;
							}
							if (npc.type == 468)
							{
								num160 = 501;
							}
							if (npc.type == 411)
							{
								num160 = 537;
								num159 = (Main.expertMode ? 45 : 60);
							}
							if (npc.type == 424)
							{
								num160 = 573;
								num159 = (Main.expertMode ? 45 : 60);
							}
							if (npc.type == 426)
							{
								num160 = 581;
								num159 = (Main.expertMode ? 45 : 60);
							}
							if ((npc.type == 291 || true))
							{
								num160 = 302;
								num159 = 100;
							}
							if (npc.type == 290)
							{
								num160 = 300;
								num159 = 60;
							}
							if (npc.type == 293)
							{
								num160 = 303;
								num159 = 60;
							}
							if (npc.type == 214)
							{
								num160 = 180;
								num159 = 25;
							}
							if (npc.type == 215)
							{
								num160 = 82;
								num159 = 40;
							}
							if (npc.type == 292)
							{
								num159 = 50;
								num160 = 180;
							}
							if (npc.type == 216)
							{
								num160 = 180;
								num159 = 30;
								if (flag19)
								{
									num159 = 100;
									num160 = 240;
									npc.localAI[2] = 0f;
								}
							}
		
                            vector25.X += num155;
							
                            vector25.Y += num157;
							if (Main.expertMode && npc.type == 290)
							{
								num159 = (int)((double)num159 * 0.75);
							}
							if (Main.expertMode && npc.type >= 381 && npc.type <= 392)
							{
								num159 = (int)((double)num159 * 0.8);
							}
							if (Main.netMode != 1)
							{
								if (npc.type == 292)
								{
									int num26;
									for (int num161 = 0; num161 < 4; num161 = num26 + 1)
									{
										num155 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector25.X;
										num157 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector25.Y;
										num158 = (float)Math.Sqrt(num155 * num155 + num157 * num157);
										num158 = 12f / num158;
										num155 += (float)Main.rand.Next(-40, 41);
										num157 += (float)Main.rand.Next(-40, 41);
										num155 *= num158;
										num157 *= num158;
										Projectile.NewProjectile(vector25.X, vector25.Y, num155, num157, num160, num159, 0f, Main.myPlayer);
										num26 = num161;
									}
								}
								else if (npc.type == 411)
								{
									Projectile.NewProjectile(vector25.X, vector25.Y, num155, num157, num160, num159, 0f, Main.myPlayer, 0f, npc.whoAmI);
								}
								else if (npc.type == 424)
								{
									int num26;
									for (int num162 = 0; num162 < 4; num162 = num26 + 1)
									{
										Projectile.NewProjectile(npc.Center.X - (float)(npc.spriteDirection * 4), npc.Center.Y + 6f, (float)(-3 + 2 * num162) * 0.15f, (0f - (float)Main.rand.Next(0, 3)) * 0.2f - 0.1f, num160, num159, 0f, Main.myPlayer, 0f, npc.whoAmI);
										num26 = num162;
									}
								}
								else if (npc.type == 409)
								{
									int num163 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, 410, npc.whoAmI);
									Main.npc[num163].velocity = new Vector2(num155, -6f + num157);
								}
								else
								{
									Projectile.NewProjectile(vector25.X, vector25.Y, num155, num157, num160, num159, 0f, Main.myPlayer);
								}
							}
							if (Math.Abs(num157) > Math.Abs(num155) * 2f)
							{
								if (num157 > 0f)
								{
									npc.ai[2] = 1f;
								}
								else
								{
									npc.ai[2] = 5f;
								}
							}
							else if (Math.Abs(num155) > Math.Abs(num157) * 2f)
							{
								npc.ai[2] = 3f;
							}
							else if (num157 > 0f)
							{
								npc.ai[2] = 2f;
							}
							else
							{
								npc.ai[2] = 4f;
							}
						}
						if ((npc.velocity.Y != 0f && !flag17) || npc.ai[1] <= 0f)
						{
							npc.ai[2] = 0f;
							npc.ai[1] = 0f;
						}
						else if (!flag16 || (num150 != -1 && npc.ai[1] >= (float)num150 && npc.ai[1] < (float)(num150 + num151) && (!flag17 || npc.velocity.Y == 0f)))
						{
							npc.velocity.X *= 0.9f;
							npc.spriteDirection = npc.direction;
						}
					}
					if (npc.type == 468 && !Main.eclipse)
					{
						flag16 = true;
					}
					else if (((npc.ai[2] <= 0f) | flag16) && ((npc.velocity.Y == 0f) | flag17) && npc.ai[1] <= 0f && !Main.player[npc.target].dead)
					{
						bool flag20 = Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height);
						if (npc.type == 520)
						{
							flag20 = Collision.CanHitLine(npc.Top + new Vector2(0f, 20f), 0, 0, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height);
						}
						if (Main.player[npc.target].stealth == 0f && Main.player[npc.target].itemAnimation == 0)
						{
							flag20 = false;
						}
						if (flag20)
						{
							float num164 = 10f;
							Vector2 vector26 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num165 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector26.X;
							float num166 = Math.Abs(num165) * 0.1f;
							float num167 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector26.Y - num166;
							num165 += (float)Main.rand.Next(-40, 41);
							num167 += (float)Main.rand.Next(-40, 41);
							float num168 = (float)Math.Sqrt(num165 * num165 + num167 * num167);
							float num169 = 700f;
							if (npc.type == 214)
							{
								num169 = 550f;
							}
							if (npc.type == 215)
							{
								num169 = 800f;
							}
							if (npc.type >= 498 && npc.type <= 506)
							{
								num169 = 190f;
							}
							if (npc.type >= 449 && npc.type <= 452)
							{
								num169 = 200f;
							}
							if (npc.type == 481)
							{
								num169 = 400f;
							}
							if (npc.type == 468)
							{
								num169 = 400f;
							}
							if (num168 < num169)
							{
								npc.netUpdate = true;
								npc.velocity.X *= 0.5f;
								num168 = num164 / num168;
								num165 *= num168;
								num167 *= num168;
								npc.ai[2] = 3f;
								npc.ai[1] = num152;
								if (Math.Abs(num167) > Math.Abs(num165) * 2f)
								{
									if (num167 > 0f)
									{
										npc.ai[2] = 1f;
									}
									else
									{
										npc.ai[2] = 5f;
									}
								}
								else if (Math.Abs(num165) > Math.Abs(num167) * 2f)
								{
									npc.ai[2] = 3f;
								}
								else if (num167 > 0f)
								{
									npc.ai[2] = 2f;
								}
								else
								{
									npc.ai[2] = 4f;
								}
							}
						}
					}
					if (npc.ai[2] <= 0f || (flag16 && (num150 == -1 || npc.ai[1] < (float)num150 || npc.ai[1] >= (float)(num150 + num151))))
					{
						float num170 = 1f;
						float num171 = 0.07f;
						float num172 = 0.8f;
						if (npc.type == 214)
						{
							num170 = 2f;
							num171 = 0.09f;
						}
						else if (npc.type == 215)
						{
							num170 = 1.5f;
							num171 = 0.08f;
						}
						else if (npc.type == 381 || npc.type == 382)
						{
							num170 = 2f;
							num171 = 0.5f;
						}
						else if (npc.type == 520)
						{
							num170 = 4f;
							num171 = 1f;
							num172 = 0.7f;
						}
						else if (npc.type == 411)
						{
							num170 = 2f;
							num171 = 0.5f;
						}
						else if (npc.type == 409)
						{
							num170 = 2f;
							num171 = 0.5f;
						}
						bool flag21 = false;
						if ((npc.type == 381 || npc.type == 382) && Vector2.Distance(npc.Center, Main.player[npc.target].Center) < 300f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
						{
							flag21 = true;
							npc.ai[3] = 0f;
						}
						if (npc.type == 520 && Vector2.Distance(npc.Center, Main.player[npc.target].Center) < 400f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
						{
							flag21 = true;
							npc.ai[3] = 0f;
						}
						if ((npc.velocity.X < 0f - num170 || npc.velocity.X > num170) | flag21)
						{
							if (npc.velocity.Y == 0f)
							{
								npc.velocity *= num172;
							}
						}
						else if (npc.velocity.X < num170 && npc.direction == 1)
						{
							npc.velocity.X += num171;
							if (npc.velocity.X > num170)
							{
								npc.velocity.X = num170;
							}
						}
						else if (npc.velocity.X > 0f - num170 && npc.direction == -1)
						{
							npc.velocity.X -= num171;
							if (npc.velocity.X < 0f - num170)
							{
								npc.velocity.X = 0f - num170;
							}
						}
					}
					if (npc.type == 520)
					{
						ref float reference = ref npc.localAI[2];
						reference += 1f;
						if (npc.localAI[2] >= 6f)
						{
							npc.localAI[2] = 0f;
							npc.localAI[3] = Main.player[npc.target].DirectionFrom(npc.Top + new Vector2(0f, 20f)).ToRotation();
						}
					}
				}
			}
			if (npc.type == 109 && Main.netMode != 1 && !Main.player[npc.target].dead)
			{
				if (npc.justHit)
				{
					npc.ai[2] = 0f;
				}
				ref float reference = ref npc.ai[2];
				reference += 1f;
				if (npc.ai[2] > 450f)
				{
					Vector2 vector27 = new Vector2(npc.position.X + (float)npc.width * 0.5f - (float)(npc.direction * 24), npc.position.Y + 4f);
					int num173 = 3 * npc.direction;
					int num174 = -5;
					int num175 = Projectile.NewProjectile(vector27.X, vector27.Y, num173, num174, 75, 0, 0f, Main.myPlayer);
					Main.projectile[num175].timeLeft = 300;
					npc.ai[2] = 0f;
				}
			}
			bool flag22 = false;
			if (npc.velocity.Y == 0f)
			{
				int num176 = (int)(npc.position.Y + (float)npc.height + 7f) / 16;
				int num177 = (int)npc.position.X / 16;
				int num178 = (int)(npc.position.X + (float)npc.width) / 16;
				int num26;
				for (int num179 = num177; num179 <= num178; num179 = num26 + 1)
				{
					if (Main.tile[num179, num176] == null)
					{
						return;
					}
					if (Main.tile[num179, num176].nactive() && Main.tileSolid[Main.tile[num179, num176].type])
					{
						flag22 = true;
						break;
					}
					num26 = num179;
				}
			}
			if (npc.type == 428)
			{
				flag22 = false;
			}
			if (npc.velocity.Y >= 0f)
			{
				int num180 = 0;
				if (npc.velocity.X < 0f)
				{
					num180 = -1;
				}
				if (npc.velocity.X > 0f)
				{
					num180 = 1;
				}
				Vector2 position2 = npc.position;
				ref float reference = ref position2.X;
				reference += npc.velocity.X;
				int num181 = (int)((position2.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 1) * num180)) / 16f);
				int num182 = (int)((position2.Y + (float)npc.height - 1f) / 16f);
				if (Main.tile[num181, num182] == null)
				{
					Main.tile[num181, num182] = new Tile();
				}
				if (Main.tile[num181, num182 - 1] == null)
				{
					Main.tile[num181, num182 - 1] = new Tile();
				}
				if (Main.tile[num181, num182 - 2] == null)
				{
					Main.tile[num181, num182 - 2] = new Tile();
				}
				if (Main.tile[num181, num182 - 3] == null)
				{
					Main.tile[num181, num182 - 3] = new Tile();
				}
				if (Main.tile[num181, num182 + 1] == null)
				{
					Main.tile[num181, num182 + 1] = new Tile();
				}
				if (Main.tile[num181 - num180, num182 - 3] == null)
				{
					Main.tile[num181 - num180, num182 - 3] = new Tile();
				}
				if ((float)(num181 * 16) < position2.X + (float)npc.width && (float)(num181 * 16 + 16) > position2.X && ((Main.tile[num181, num182].nactive() && !Main.tile[num181, num182].topSlope() && !Main.tile[num181, num182 - 1].topSlope() && Main.tileSolid[Main.tile[num181, num182].type] && !Main.tileSolidTop[Main.tile[num181, num182].type]) || (Main.tile[num181, num182 - 1].halfBrick() && Main.tile[num181, num182 - 1].nactive())) && (!Main.tile[num181, num182 - 1].nactive() || !Main.tileSolid[Main.tile[num181, num182 - 1].type] || Main.tileSolidTop[Main.tile[num181, num182 - 1].type] || (Main.tile[num181, num182 - 1].halfBrick() && (!Main.tile[num181, num182 - 4].nactive() || !Main.tileSolid[Main.tile[num181, num182 - 4].type] || Main.tileSolidTop[Main.tile[num181, num182 - 4].type]))) && (!Main.tile[num181, num182 - 2].nactive() || !Main.tileSolid[Main.tile[num181, num182 - 2].type] || Main.tileSolidTop[Main.tile[num181, num182 - 2].type]) && (!Main.tile[num181, num182 - 3].nactive() || !Main.tileSolid[Main.tile[num181, num182 - 3].type] || Main.tileSolidTop[Main.tile[num181, num182 - 3].type]) && (!Main.tile[num181 - num180, num182 - 3].nactive() || !Main.tileSolid[Main.tile[num181 - num180, num182 - 3].type]))
				{
					float num183 = num182 * 16;
					if (Main.tile[num181, num182].halfBrick())
					{
						num183 += 8f;
					}
					if (Main.tile[num181, num182 - 1].halfBrick())
					{
						num183 -= 8f;
					}
					if (num183 < position2.Y + (float)npc.height)
					{
						float num184 = position2.Y + (float)npc.height - num183;
						float num185 = 16.1f;
						if (npc.type == 163 || npc.type == 164 || npc.type == 236 || npc.type == 239 || npc.type == 530)
						{
							num185 += 8f;
						}
						if (num184 <= num185)
						{
							npc.gfxOffY += npc.position.Y + (float)npc.height - num183;
							npc.position.Y = num183 - (float)npc.height;
							if (num184 < 9f)
							{
                                npc.stepSpeed = 1f;
							}
							else
							{
                                npc.stepSpeed = 2f;
							}
						}
					}
				}
			}
			if (flag22)
			{
				int num186 = (int)((npc.position.X + (float)(npc.width / 2) + (float)(15 * npc.direction)) / 16f);
				int num187 = (int)((npc.position.Y + (float)npc.height - 15f) / 16f);
				if (npc.type == 109 || npc.type == 163 || npc.type == 164 || npc.type == 199 || npc.type == 236 || npc.type == 239 || npc.type == 257 || npc.type == 258 || npc.type == 290 || npc.type == 391 || npc.type == 425 || npc.type == 427 || npc.type == 426 || npc.type == 508 || npc.type == 415 || npc.type == 530 || npc.type == 532)
				{
					num186 = (int)((npc.position.X + (float)(npc.width / 2) + (float)((npc.width / 2 + 16) * npc.direction)) / 16f);
				}
				if (Main.tile[num186, num187] == null)
				{
					Main.tile[num186, num187] = new Tile();
				}
				if (Main.tile[num186, num187 - 1] == null)
				{
					Main.tile[num186, num187 - 1] = new Tile();
				}
				if (Main.tile[num186, num187 - 2] == null)
				{
					Main.tile[num186, num187 - 2] = new Tile();
				}
				if (Main.tile[num186, num187 - 3] == null)
				{
					Main.tile[num186, num187 - 3] = new Tile();
				}
				if (Main.tile[num186, num187 + 1] == null)
				{
					Main.tile[num186, num187 + 1] = new Tile();
				}
				if (Main.tile[num186 + npc.direction, num187 - 1] == null)
				{
					Main.tile[num186 + npc.direction, num187 - 1] = new Tile();
				}
				if (Main.tile[num186 + npc.direction, num187 + 1] == null)
				{
					Main.tile[num186 + npc.direction, num187 + 1] = new Tile();
				}
				if (Main.tile[num186 - npc.direction, num187 + 1] == null)
				{
					Main.tile[num186 - npc.direction, num187 + 1] = new Tile();
				}
				Main.tile[num186, num187 + 1].halfBrick();
				if ((Main.tile[num186, num187 - 1].nactive() && (TileLoader.IsClosedDoor(Main.tile[num186, num187 - 1]) || Main.tile[num186, num187 - 1].type == 388)) & flag5)
				{
					ref float reference = ref npc.ai[2];
					reference += 1f;
					npc.ai[3] = 0f;
					if (npc.ai[2] >= 60f)
					{
						if (!Main.bloodMoon && (npc.type == 3 || npc.type == 331 || npc.type == 332 || npc.type == 132 || npc.type == 161 || npc.type == 186 || npc.type == 187 || npc.type == 188 || npc.type == 189 || npc.type == 200 || npc.type == 223 || npc.type == 320 || npc.type == 321 || npc.type == 319))
						{
							npc.ai[1] = 0f;
						}
						npc.velocity.X = 0.5f * (0f - (float)npc.direction);
						int num188 = 5;
						if (Main.tile[num186, num187 - 1].type == 388)
						{
							num188 = 2;
						}
						reference = ref npc.ai[1];
						reference += num188;
						if (npc.type == 27)
						{
							reference = ref npc.ai[1];
							reference += 1f;
						}
						if (npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296)
						{
							reference = ref npc.ai[1];
							reference += 6f;
						}
						npc.ai[2] = 0f;
						bool flag23 = false;
						if (npc.ai[1] >= 10f)
						{
							flag23 = true;
							npc.ai[1] = 10f;
						}
						if (npc.type == 460)
						{
							flag23 = true;
						}
						WorldGen.KillTile(num186, num187 - 1, fail: true);
						if (((Main.netMode != 1 || !flag23) & flag23) && Main.netMode != 1)
						{
							if (npc.type == 26)
							{
								WorldGen.KillTile(num186, num187 - 1);
								if (Main.netMode == 2)
								{
									NetMessage.SendData(17, -1, -1, null, 0, num186, num187 - 1);
								}
							}
							else
							{
								if (TileLoader.OpenDoorID(Main.tile[num186, num187 - 1]) >= 0)
								{
									bool flag24 = WorldGen.OpenDoor(num186, num187 - 1, npc.direction);
									if (!flag24)
									{
										npc.ai[3] = num41;
										npc.netUpdate = true;
									}
									if (Main.netMode == 2 && flag24)
									{
										NetMessage.SendData(19, -1, -1, null, 0, num186, num187 - 1, npc.direction);
									}
								}
								if (Main.tile[num186, num187 - 1].type == 388)
								{
									bool flag25 = WorldGen.ShiftTallGate(num186, num187 - 1, closing: false);
									if (!flag25)
									{
										npc.ai[3] = num41;
										npc.netUpdate = true;
									}
									if (Main.netMode == 2 && flag25)
									{
										NetMessage.SendData(19, -1, -1, null, 4, num186, num187 - 1);
									}
								}
							}
						}
					}
				}
				else
				{
					int num189 = npc.spriteDirection;
					if (npc.type == 425)
					{
						num189 *= -1;
					}
					if ((npc.velocity.X < 0f && num189 == -1) || (npc.velocity.X > 0f && num189 == 1))
					{
						if (npc.height >= 32 && Main.tile[num186, num187 - 2].nactive() && Main.tileSolid[Main.tile[num186, num187 - 2].type])
						{
							if (Main.tile[num186, num187 - 3].nactive() && Main.tileSolid[Main.tile[num186, num187 - 3].type])
							{
								npc.velocity.Y = -8f;
								npc.netUpdate = true;
							}
							else
							{
								npc.velocity.Y = -7f;
								npc.netUpdate = true;
							}
						}
						else if (Main.tile[num186, num187 - 1].nactive() && Main.tileSolid[Main.tile[num186, num187 - 1].type])
						{
							npc.velocity.Y = -6f;
							npc.netUpdate = true;
						}
						else if (npc.position.Y + (float)npc.height - (float)(num187 * 16) > 20f && Main.tile[num186, num187].nactive() && !Main.tile[num186, num187].topSlope() && Main.tileSolid[Main.tile[num186, num187].type])
						{
							npc.velocity.Y = -5f;
							npc.netUpdate = true;
						}
						else if (npc.directionY < 0 && npc.type != 67 && (!Main.tile[num186, num187 + 1].nactive() || !Main.tileSolid[Main.tile[num186, num187 + 1].type]) && (!Main.tile[num186 + npc.direction, num187 + 1].nactive() || !Main.tileSolid[Main.tile[num186 + npc.direction, num187 + 1].type]))
						{
							npc.velocity.Y = -8f;
							npc.velocity.X *= 1.5f;
							npc.netUpdate = true;
						}
						else if (flag5)
						{
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
						}
						if (npc.velocity.Y == 0f && flag3 && npc.ai[3] == 1f)
						{
							npc.velocity.Y = -5f;
						}
					}
					if ((npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296 || npc.type == 47 || npc.type == 77 || npc.type == 104 || npc.type == 168 || npc.type == 196 || npc.type == 385 || npc.type == 389 || npc.type == 464 || npc.type == 470 || (npc.type >= 524 && npc.type <= 527)) && npc.velocity.Y == 0f && Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) < 100f && Math.Abs(npc.position.Y + (float)(npc.height / 2) - (Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))) < 50f && ((npc.direction > 0 && npc.velocity.X >= 1f) || (npc.direction < 0 && npc.velocity.X <= -1f)))
					{
						npc.velocity.X *= 2f;
						if (npc.velocity.X > 3f)
						{
							npc.velocity.X = 3f;
						}
						if (npc.velocity.X < -3f)
						{
							npc.velocity.X = -3f;
						}
						npc.velocity.Y = -4f;
						npc.netUpdate = true;
					}
					if (npc.type == 120 && npc.velocity.Y < 0f)
					{
						npc.velocity.Y *= 1.1f;
					}
					if (npc.type == 287 && npc.velocity.Y == 0f && Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) < 150f && Math.Abs(npc.position.Y + (float)(npc.height / 2) - (Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))) < 50f && ((npc.direction > 0 && npc.velocity.X >= 1f) || (npc.direction < 0 && npc.velocity.X <= -1f)))
					{
						npc.velocity.X = 8 * npc.direction;
						npc.velocity.Y = -4f;
						npc.netUpdate = true;
					}
					if (npc.type == 287 && npc.velocity.Y < 0f)
					{
						npc.velocity.X *= 1.2f;
						npc.velocity.Y *= 1.1f;
					}
					if (npc.type == 460 && npc.velocity.Y < 0f)
					{
						npc.velocity.X *= 1.3f;
						npc.velocity.Y *= 1.1f;
					}
				}
			}
			else if (flag5)
			{
				npc.ai[1] = 0f;
				npc.ai[2] = 0f;
			}
			if (Main.netMode == 1 || npc.type != 120 || !(npc.ai[3] >= (float)num41))
			{
				return;
			}
			int num190 = (int)Main.player[npc.target].position.X / 16;
			int num191 = (int)Main.player[npc.target].position.Y / 16;
			int num192 = (int)npc.position.X / 16;
			int num193 = (int)npc.position.Y / 16;
			int num194 = 20;
			int num195 = 0;
			bool flag26 = false;
			if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) + Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
			{
				num195 = 100;
				flag26 = true;
			}
			while (!flag26 && num195 < 100)
			{
				int num26 = num195;
				num195 = num26 + 1;
				int num196 = Main.rand.Next(num190 - num194, num190 + num194);
				int num197 = Main.rand.Next(num191 - num194, num191 + num194);
				for (int num198 = num197; num198 < num191 + num194; num198 = num26 + 1)
				{
					if ((num198 < num191 - 4 || num198 > num191 + 4 || num196 < num190 - 4 || num196 > num190 + 4) && (num198 < num193 - 1 || num198 > num193 + 1 || num196 < num192 - 1 || num196 > num192 + 1) && Main.tile[num196, num198].nactive())
					{
						bool flag27 = true;
						if (npc.type == 32 && Main.tile[num196, num198 - 1].wall == 0)
						{
							flag27 = false;
						}
						else if (Main.tile[num196, num198 - 1].lava())
						{
							flag27 = false;
						}
						if (flag27 && Main.tileSolid[Main.tile[num196, num198].type] && !Collision.SolidTiles(num196 - 1, num196 + 1, num198 - 4, num198 - 1))
						{
							npc.position.X = num196 * 16 - npc.width / 2;
							npc.position.Y = num198 * 16 - npc.height;
							npc.netUpdate = true;
							npc.ai[3] = -120f;
						}
					}
					num26 = num198;
				}
			}
		}

        
    }
}
