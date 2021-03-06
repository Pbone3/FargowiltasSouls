using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class DragonBreathProj : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_687";
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			//projectile.aiStyle = 136;
			projectile.alpha = 255;
			projectile.penetrate = -1;
			projectile.friendly = true;
			//projectile.usesLocalNPCImmunity = true;
			//projectile.localNPCHitCooldown = 8;
			projectile.ranged = true;
			projectile.tileCollide = false;
			projectile.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
		}

		public int timer;
        public float lerp = 0.12f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon's Breath");
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (Main.myPlayer != player.whoAmI)
				return;

			if (player.dead || !player.active)
				projectile.Kill();

			Vector2 center = player.MountedCenter;
			projectile.Center = center;
			projectile.rotation = projectile.velocity.ToRotation();

			if (player.channel)
			{
				projectile.velocity = Vector2.Lerp(Vector2.Normalize(projectile.velocity), 
                    Vector2.Normalize(Main.MouseWorld - player.MountedCenter), lerp); //slowly move towards direction of cursor
				projectile.velocity.Normalize();

				timer++;
				if (timer > 12)
				{
					Main.PlaySound(SoundID.DD2_BetsyFlameBreath, projectile.Center + (projectile.velocity * 600)); //dd2 sound effects are weird so this is temporary(?) fix to sound effect being too loud
					int shoot = 0; //dummy values so i can use pickammo
					float speed = 0;
					bool canshoot = true;
					int damage = 0;
					float knockback = 0;
					player.PickAmmo(player.inventory[player.selectedItem], ref shoot, ref speed, ref canshoot, ref damage, ref knockback, false);
					timer = 0;
				}
				projectile.timeLeft++;
			}

			float extrarotate = (projectile.direction < 0) ? MathHelper.Pi : 0; 
			player.itemRotation = projectile.velocity.ToRotation() + extrarotate;
			player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;

			Vector2 HoldOffset = new Vector2(60, 0).RotatedBy(projectile.velocity.ToRotation());
			projectile.Center += HoldOffset;

			DelegateMethods.v3_1 = new Vector3(1.2f, 1f, 0.3f);
			float num2 = projectile.ai[0] / 40f;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			float num3 = (projectile.ai[0] - 38f) / 40f;
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			Utils.PlotTileLine(projectile.Center + projectile.rotation.ToRotationVector2() * 400f * num3, projectile.Center + projectile.rotation.ToRotationVector2() * 400f * num2, 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));
			Utils.PlotTileLine(projectile.Center + projectile.rotation.ToRotationVector2().RotatedBy(0.19634954631328583, default(Vector2)) * 400f * num3, projectile.Center + projectile.rotation.ToRotationVector2().RotatedBy(0.19634954631328583, default(Vector2)) * 400f * num2, 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));
			Utils.PlotTileLine(projectile.Center + projectile.rotation.ToRotationVector2().RotatedBy(-0.19634954631328583, default(Vector2)) * 400f * num3, projectile.Center + projectile.rotation.ToRotationVector2().RotatedBy(-0.19634954631328583, default(Vector2)) * 400f * num2, 16f, new Utils.PerLinePoint(DelegateMethods.CastLight));

			/*if (Main.rand.Next(4) == 0 && projectile.ai[0] >= 25f)
			{
				Vector2 vector = projectile.Center + projectile.rotation.ToRotationVector2() * 600f;
				vector -= Utils.RandomVector2(Main.rand, -40f, 40f);
				Gore gore = Gore.NewGoreDirect(vector, Vector2.Zero, 61 + Main.rand.Next(3), 1f);
				gore.velocity *= 0.6f;
				gore.velocity += projectile.rotation.ToRotationVector2() * 4f;
			}*/
			//projectile.frameCounter++;
			projectile.ai[0] += 1f;
			if (player.channel && projectile.ai[0] > 50 && player.HasAmmo(player.inventory[player.selectedItem], true))
				projectile.ai[0] = 40;

			if (projectile.ai[0] >= 78f)
			{
				projectile.Kill();
			}
		}

		public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
		{
			float num11 = 0f;
			float num12 = projectile.ai[0] / 25f;
			if (num12 > 1f)
			{
				num12 = 1f;
			}
			float num13 = (projectile.ai[0] - 38f) / 40f;
			if (num13 < 0f)
			{
				num13 = 0f;
			}
			Vector2 lineStart = projectile.Center + projectile.rotation.ToRotationVector2() * 400f * num13;
			Vector2 lineEnd = projectile.Center + projectile.rotation.ToRotationVector2() * 800f * num12;
			if (Collision.CheckAABBvLineCollision(targetRect.TopLeft(), targetRect.Size(), lineStart, lineEnd, 40f * projectile.scale, ref num11))
			{
				return true;
			}
			return false;
		}
		
		public override Color? GetAlpha (Color lightColor)
		{
			return new Color(102, 224, 255);
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 value10 = projectile.Center;
			value10 -= Main.screenPosition;
			float num178 = 40f;
			float num179 = num178 * 2f;
			float num180 = (float)projectile.ai[0] / num178;
			Texture2D texture2D5 = Main.projectileTexture[projectile.type];
			Color color33 = Color.Transparent;
			Color color34 = new Color(255, 255, 255, 0);
			Color color35 = new Color(180, 30, 30, 200);
			Color color36 = new Color(30, 0, 00, 30);
			ulong num181 = 1uL;
			for (float num182 = 0f; num182 < 30f; num182 += 0.66f)
			{
				float num183 = Utils.RandomFloat(ref num181) * 0.25f - 0.125f;
				Vector2 value11 = (projectile.rotation + num183).ToRotationVector2();
				Vector2 value12 = value10 + value11 * 800f;
				float num184 = num180 + num182 * 0.06666667f;
				int num185 = (int)(num184 / 0.06666667f);
				num184 %= 1f;
				if ((num184 <= num180 % 1f || (float)projectile.ai[0] >= num178) && (num184 >= num180 % 1f || (float)projectile.ai[0] < num179 - num178))
				{
					if (num184 < 0.1f)
					{
						color33 = Color.Lerp(Color.Transparent, color34, Utils.InverseLerp(0f, 0.1f, num184, true));
					}
					else if (num184 < 0.35f)
					{
						color33 = color34;
					}
					else if (num184 < 0.7f)
					{
						color33 = Color.Lerp(color34, color35, Utils.InverseLerp(0.35f, 0.7f, num184, true));
					}
					else if (num184 < 0.9f)
					{
						color33 = Color.Lerp(color35, color36, Utils.InverseLerp(0.7f, 0.9f, num184, true));
					}
					else if (num184 < 1f)
					{
						color33 = Color.Lerp(color36, Color.Transparent, Utils.InverseLerp(0.9f, 1f, num184, true));
					}
					else
					{
						color33 = Color.Transparent;
					}
					float num186 = 0.9f + num184 * 0.8f;
					num186 *= num186;
					num186 *= 0.8f;
					Vector2 position = Vector2.SmoothStep(value10, value12, num184);
					Rectangle rectangle2 = texture2D5.Frame(1, 7, 0, (int)(num184 * 7f));
					Main.spriteBatch.Draw(texture2D5, position, new Microsoft.Xna.Framework.Rectangle?(rectangle2), color33, projectile.rotation + 6.28318548f * (num184 + Main.GlobalTime * 1.2f) * 0.2f + (float)num185 * 1.2566371f, rectangle2.Size() / 2f, num186, SpriteEffects.None, 0f);
				}
			}
			return false;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 180, false);
			target.AddBuff(BuffID.Oiled, 180, false);
			target.AddBuff(BuffID.BetsysCurse, 180, false);
		}
	}
}