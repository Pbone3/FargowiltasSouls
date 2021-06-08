using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons.DarkMage
{
    public class DarkMagePage : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.BookStaffShot;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = Main.projFrames[ProjectileID.BookStaffShot];
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BookStaffShot);
            aiType = ProjectileID.BookStaffShot;
            projectile.alpha = 255;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextBool(2))
            {
                Gore g = Gore.NewGoreDirect(projectile.Center, Vector2.Zero, GoreID.Pages);
                g.timeLeft -= 60;
            }
            else
            {
                Gore g = Gore.NewGoreDirect(projectile.Center, Vector2.Zero, GoreID.PageScrap);
                g.timeLeft -= 60;

                g = Gore.NewGoreDirect(projectile.Center, Vector2.Zero, GoreID.PageScrap);
                g.timeLeft -= 60;
            }
            /*FargoPlayer mPlayer = Main.player[projectile.owner].GetModPlayer<FargoPlayer>();

            if (projectile.owner == Main.myPlayer)
            {
                mPlayer.DarkMageBookCounter++;
                if (mPlayer.DarkMageBookCounter >= DarkMageSkeletonFriend.HitsToTrigger)
                {
                    // Summon skeleton friend
                    Vector2 position = target.Center + new Vector2(160 * (target.direction == 0 ? 1 : -1), Main.screenHeight / 2f);
                    int d = (int)(projectile.damage * 1.33);
                    mPlayer.DarkMageBookCounter = 0;

                    Projectile pro = Projectile.NewProjectileDirect(position, new Vector2(0, -24), ModContent.ProjectileType<DarkMageSkeletonFriend>(), d, 0f, projectile.owner);
                    pro.localAI[0] = target.whoAmI;
                }
            }*/
        }
    }
}
