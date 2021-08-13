using NoSnowLitter.Common.Configs;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class SandGunProjectileModifier : GlobalProjectile
	{
		public override void SetDefaults(Projectile projectile)
		{
			if (!ModContent.GetInstance<BlockLitterConfig>().StopAntlionSandLitter)
			{
				return;
			}

			// The Sandgun has four different projectiles:
			// SandBallGun (42), EbonsandBallGun (65), PearlSandBallGun (68), and CrimsandBallGun (354).
			// Like all projectiles that place blocks, these will only place blocks if projectile.noDropItem is false.

			int[] sandGunProjectiles = { ProjectileID.SandBallGun, ProjectileID.EbonsandBallGun, ProjectileID.PearlSandBallGun, ProjectileID.CrimsandBallGun };

			if (sandGunProjectiles.Contains(projectile.type))
			{
				projectile.noDropItem = true;
			}
		}
	}
}