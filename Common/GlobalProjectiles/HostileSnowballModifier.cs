using Terraria;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class HostileSnowballModifier : GlobalProjectile
	{
		public override void SetDefaults(Projectile projectile)
		{
			// Projectile.Kill() checks for noDropItem being false before going through with the tile-placing logic, so this stops the code there without removing the dust that's spawned.
			if (projectile.type == Terraria.ID.ProjectileID.SnowBallHostile)
			{
				projectile.noDropItem = true;
			}
		}
	}
}