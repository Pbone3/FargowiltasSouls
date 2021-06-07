using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

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

        public override void AI()
        {
            projectile.alpha -= 30;
            if (projectile.alpha < 0)
                projectile.alpha = 0;

            if (projectile.damage > 0)
            {
                // Safe damage it was spawned with
                projectile.ai[1] = projectile.damage;
                projectile.damage = 0;
            }

            // Slow down
            projectile.velocity *= 0.95f;

            // Twice, at 32 ticks and right before death
            if (Counter++ % 16 == 0 && Counter > 17)
            {
                // Shoot
                Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(Main.MouseWorld) * 8, ProjectileID.BookStaffShot, (int)projectile.ai[1], 1f, projectile.owner);
            }

            if (Counter >= 50)
            {
                // Die
                projectile.Kill();

                // Dust
                Gore.NewGore(projectile.Center, default, GoreID.PageScrap);

                if (Main.rand.NextBool(4))
                    Gore.NewGore(projectile.Center, default, GoreID.Pages);
                if (Main.rand.NextBool(5))
                    Gore.NewGore(projectile.Center, default, GoreID.PageScrap);

                for (int i = 0; i < 7; i++)
                {
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.width + 4, projectile.height + 4, 86); // DustID.AmethystBolt
                    dust.noGravity = true;
                }
            }

            if (projectile.owner == Main.myPlayer)
            {
                // Rotate to cursor pos
                projectile.rotation = Utils.AngleLerp(projectile.rotation, projectile.DirectionTo(Main.MouseWorld).ToRotation(), 0.3f);
            }
        }

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
