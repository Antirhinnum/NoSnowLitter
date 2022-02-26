using NoSnowLitter.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class HostileSandBallModifier : GlobalProjectile
	{
		public override bool PreKill(Projectile projectile, int timeLeft)
		{
			if (ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter == DropType.Vanilla)
			{
				return base.PreKill(projectile, timeLeft);
			}

			// This change can't be done in SetDefaults() because Terraria changes the projectile after SetDefaults() runs.
			// Specifically, after spawning the Sand Ball projectile, Terraria sets projectile.friendly to false and manually syncs the projectile.
			// We have to check projectile.friendly because this projectile is used for both Antlions and falling sand.
			// Beyond that, items are still dropped if projectile.noDropItem is false, much like Snow Ballas' projectiles.

			if (projectile.type == ProjectileID.SandBallFalling && !projectile.friendly)
			{
				projectile.noDropItem = true;
			}

			return base.PreKill(projectile, timeLeft);
		}

		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter != DropType.Item)
			{
				return;
			}

			if (projectile.type != ProjectileID.SandBallFalling || projectile.friendly)
			{
				return;
			}

			Item.NewItem(projectile.Hitbox, ItemID.SandBlock);
		}
	}
}