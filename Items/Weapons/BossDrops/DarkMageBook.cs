using FargowiltasSouls.Projectiles.BossWeapons.DarkMage;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class DarkMageBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old One's Tome");
            Tooltip.SetDefault(
                "Shoots out three sentient page-firing books" +
              "\nRepeated hits summon a short-lived skeleton for extra damage" +
              "\n'The repurposed mount of a defeated foe...'");
        }

        public override void SetDefaults()
        {
            item.damage = 16;
            item.magic = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 45000;
            item.rare = ItemRarityID.Orange;
            item.mana = 14;
            item.UseSound = SoundID.DD2_DarkMageAttack.WithVolume(0.5f);
            item.autoReuse = true;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 6f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            const int bookAmount = 3;
            for (int i = 0; i < bookAmount; i++)
            {
                Vector2 velocity = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.PiOver4) * (1 + Main.rand.NextFloat(0.5f));
                Projectile.NewProjectile(position, velocity, ModContent.ProjectileType<DarkMageBookPro1>(), damage, 0f, player.whoAmI);
            }

            return false;
        }
    }
}
