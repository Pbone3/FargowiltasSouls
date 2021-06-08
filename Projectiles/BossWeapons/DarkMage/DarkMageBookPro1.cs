using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using System;

namespace FargowiltasSouls.Projectiles.BossWeapons.DarkMage
{
    public class DarkMageBookPro1 : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
        }

        public float Counter { get => projectile.ai[0]; set => projectile.ai[0] = value; }
        public float RotationFlag { get => projectile.localAI[1]; set => projectile.localAI[1] = value; }

        public override void AI()
        {
            projectile.alpha -= 30;
            if (projectile.alpha < 0)
                projectile.alpha = 0;

            if (projectile.damage > 0)
            {
                // Save damage it was spawned with
                projectile.ai[1] = projectile.damage;
                projectile.damage = 0;
            }

            // Slow down
            projectile.velocity *= 0.95f;

            // Twice, at 40 ticks and once at 60
            if (Counter++ % 20 == 0 && Counter > 21)
            {
                // Shoot
                Vector2 velocity = projectile.DirectionTo(Main.MouseWorld) * 8;
                Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<DarkMagePage>(), (int)projectile.ai[1], 1f, projectile.owner);
            }

            if (Counter >= 61)
            {
                // Die
                projectile.Kill();

                // Dust
                Gore g = Gore.NewGoreDirect(projectile.Center, default, GoreID.PageScrap);
                g.timeLeft -= 60;

                if (Main.rand.NextBool(4))
                {
                    g = Gore.NewGoreDirect(projectile.Center, default, GoreID.Pages);
                    g.timeLeft -= 60;
                }
                if (Main.rand.NextBool(5))
                {
                    g = Gore.NewGoreDirect(projectile.Center, default, GoreID.PageScrap);
                    g.timeLeft -= 60;
                }

                for (int i = 0; i < 7; i++)
                {
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.width + 4, projectile.height + 4, 86); // DustID.AmethystBolt
                    dust.noGravity = true;
                }
            }

            // Only run on owner client
            if (projectile.owner == Main.myPlayer)
            {
                if (RotationFlag == 0)
                {
                    // Spain without the A
                    projectile.rotation += MathHelper.Pi / 12;
                    if (projectile.rotation > MathHelper.TwoPi)
                        projectile.rotation -= MathHelper.TwoPi;

                    Vector2 directionTo = projectile.DirectionTo(Main.MouseWorld);
                    directionTo.Normalize();
                    float compare = directionTo.ToRotation();

                    // Spin for a bit, then wait until you're close to facing cursor
                    if (Counter > 24 && AngDiff(projectile.rotation, compare) < 0.25f)
                    {
                        RotationFlag = 1;
                    }
                }
                else
                {
                    projectile.rotation = Utils.AngleLerp(projectile.rotation, projectile.DirectionTo(Main.MouseWorld).ToRotation(), 0.3f);
                }
            }
        }

        private float AngDiff(float a, float b) => (float)Math.Atan2(Math.Sin(a - b), Math.Cos(a - b));

        public override bool OnTileCollide(Vector2 oldVelocity) => false;

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 position = projectile.position + projectile.Size * 0.5f;
            spriteBatch.Draw(texture, position - Main.screenPosition, null, Color.White, projectile.rotation, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            return false;
        }
    }
}
