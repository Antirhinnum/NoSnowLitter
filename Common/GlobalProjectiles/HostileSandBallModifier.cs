using NoSnowLitter.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class HostileSandBallModifier : GlobalProjectile
	{
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == ProjectileID.SandBallFalling;
		}

		public override bool PreKill(Projectile projectile, int timeLeft)
		{
			// This change can't be done in SetDefaults() because Terraria changes the projectile after SetDefaults() runs.
			// Specifically, after spawning the Sand Ball projectile, Terraria sets projectile.friendly to false and manually syncs the projectile.
			// We have to check projectile.friendly because this projectile is used for both Antlions and falling sand.
			// Beyond that, items are still dropped if projectile.noDropItem is false, much like Snow Ballas' projectiles.

			if (!projectile.friendly && ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter != DropType.Vanilla)
			{
				projectile.noDropItem = true;
			}

			return base.PreKill(projectile, timeLeft);
		}

		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (projectile.friendly)
			{
				return;
			}

			if (ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter != DropType.Item)
			{
				return;
			}

			if (projectile.owner != Main.myPlayer)
			{
				return;
			}

			int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ItemID.SandBlock);
			Main.item[itemIndex].noGrabDelay = 0;

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex, 1f);
			}
		}
	}
}