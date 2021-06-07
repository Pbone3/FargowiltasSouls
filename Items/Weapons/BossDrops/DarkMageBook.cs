using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class DarkMageBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old One's Tome");
            Tooltip.SetDefault("'The repurposed mount of a defeated foe...'");
        }

        public override void SetDefaults()
        {
            item.damage = 22;
            item.magic = true;
            item.width = 28;
            item.height = 30;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = 45000;
            item.rare = ItemRarityID.Orange;
            item.mana = 14;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 8f;
        }
    }
}
