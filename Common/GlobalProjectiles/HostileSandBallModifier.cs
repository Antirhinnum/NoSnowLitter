using NoSnowLitter.Common.Configs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class HostileSandBallModifier : GlobalProjectile
	{
		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == ProjectileID.SandBallFalling && !entity.friendly;
		}

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			// This change can't be done in SetDefaults() because Terraria changes the projectile after SetDefaults() runs.
			// Specifically, after spawning the Sand Ball projectile, Terraria sets projectile.friendly to false and manually syncs the projectile.
			// We have to check projectile.friendly because this projectile is used for both Antlions and falling sand.
			// Beyond that, items are still dropped if projectile.noDropItem is false, much like Snow Ballas' projectiles.

			if (ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter != DropType.Vanilla)
			{
				projectile.noDropItem = true;
			}
		}

		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (ModContent.GetInstance<BlockLitterConfig>().AntlionSandLitter != DropType.Item)
			{
				return;
			}

			Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ItemID.SandBlock);
		}
	}
}