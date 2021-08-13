using NoSnowLitter.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class HostileSnowballModifier : GlobalProjectile
	{
		public override void SetDefaults(Projectile projectile)
		{
			if (!ModContent.GetInstance<BlockLitterConfig>().StopSnowballLitter)
			{
				return;
			}

			// Snow Ballas create one projectile: SnowBallHostile (109).
			// This will only create a block if projectile.noDropItem is false, so we set it to true.

			if (projectile.type == ProjectileID.SnowBallHostile)
			{
				projectile.noDropItem = true;
			}
		}
	}
}