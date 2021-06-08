using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace FargowiltasSouls.Projectiles.BossWeapons.DarkMage
{
	public class DarkMageSkeletonFriend : ModProjectile
	{
		public const int HitsToTrigger = 20;

		public const int JumpTime = 80;

		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.tileCollide = false;
		}

		public float Counter { get => projectile.ai[0]; set => projectile.ai[0] = value; }
		public float StartYVel { get => projectile.ai[1]; set => projectile.ai[1] = value; }

		public override void AI()
		{
			projectile.alpha -= 30;
			if (projectile.alpha < 0)
				projectile.alpha = 0;

			if (StartYVel == 0)
            {
				StartYVel = projectile.velocity.Y;
            }

			projectile.velocity.Y -= StartYVel / JumpTime;

			if (Counter++ == JumpTime && projectile.owner == Main.myPlayer)
            {
				Projectile.NewProjectileDirect(projectile.Center, new Vector2(16, 16), ModContent.ProjectileType<DarkMageSkeletonX>(), projectile.damage, 0f, projectile.owner, projectile.localAI[0]);
            }

			if (Counter >= JumpTime * 2)
            {
				projectile.Kill();
            }
		}
	}
}
