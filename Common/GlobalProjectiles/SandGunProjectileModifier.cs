using NoSnowLitter.Common.Configs;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class SandGunProjectileModifier : GlobalProjectile
	{
		private static readonly int[] _sandGunProjectiles = new int[]
		{
			ProjectileID.SandBallGun,
			ProjectileID.EbonsandBallGun,
			ProjectileID.PearlSandBallGun,
			ProjectileID.CrimsandBallGun
		};

		private static readonly IReadOnlyDictionary<int, int> _sandGunProjectileToItem = new Dictionary<int, int>()
		{
			{ ProjectileID.SandBallGun, ItemID.SandBlock },
			{ ProjectileID.EbonsandBallGun, ItemID.EbonsandBlock },
			{ ProjectileID.PearlSandBallGun, ItemID.PearlsandBlock },
			{ ProjectileID.CrimsandBallGun, ItemID.CrimsandBlock }
		};

		public override void SetDefaults(Projectile projectile)
		{
			if (ModContent.GetInstance<BlockLitterConfig>().SandgunLitter == DropType.Vanilla)
			{
				return;
			}

			// The Sandgun has four different projectiles:
			// SandBallGun (42), EbonsandBallGun (65), PearlSandBallGun (68), and CrimsandBallGun (354).
			// Like most projectiles that place blocks, these will only place blocks if projectile.noDropItem is false.

			if (_sandGunProjectiles.Contains(projectile.type))
			{
				projectile.noDropItem = true;
			}
		}

		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (ModContent.GetInstance<BlockLitterConfig>().SandgunLitter != DropType.Item)
			{
				return;
			}

			if (projectile.owner != Main.myPlayer || !_sandGunProjectiles.Contains(projectile.type))
			{
				return;
			}

			Item.NewItem(projectile.Hitbox, _sandGunProjectileToItem[projectile.type]);
		}
	}
}