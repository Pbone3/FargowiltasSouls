using System;
using Terraria;
using Terraria.ID;

namespace FargowiltasSouls.Projectiles.BossWeapons.DarkMage
{
    public class DarkMageSkeletonX : Bonez
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Mage Bonez");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
        }

        public float Target { get => projectile.ai[0]; set => projectile.ai[0] = value; }
        public float Counter { get => projectile.ai[1]; set => projectile.ai[1] = value; }

        public override void AI()
        {
            projectile.rotation += 0.3f * Math.Sign(projectile.velocity.X);

            projectile.velocity = projectile.DirectionTo(Main.npc[(int)Target].Center) * 8f;
        }

        // Do NOT remove
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        }

        // Do NOT remove
        public override void Kill(int timeLeft)
        {
        }
    }
}
