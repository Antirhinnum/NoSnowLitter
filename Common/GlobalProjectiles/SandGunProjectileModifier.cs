using NoSnowLitter.Common.Configs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class SandGunProjectileModifier : GlobalProjectile
	{
		private static readonly IReadOnlyDictionary<int, int> _sandGunProjectileToItem = new Dictionary<int, int>()
		{
			{ ProjectileID.SandBallGun, ItemID.SandBlock },
			{ ProjectileID.EbonsandBallGun, ItemID.EbonsandBlock },
			{ ProjectileID.PearlSandBallGun, ItemID.PearlsandBlock },
			{ ProjectileID.CrimsandBallGun, ItemID.CrimsandBlock }
		};

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return _sandGunProjectileToItem.ContainsKey(entity.type);
		}

		public override bool PreKill(Projectile projectile, int timeLeft)
		{
			// The Sandgun has four different projectiles:
			// SandBallGun (42), EbonsandBallGun (65), PearlSandBallGun (68), and CrimsandBallGun (354).
			// Like most projectiles that place blocks, these will only place blocks if projectile.noDropItem is false.

			if (ModContent.GetInstance<BlockLitterConfig>().SandgunLitter == DropType.Vanilla)
			{
				return base.PreKill(projectile, timeLeft);
			}

			projectile.noDropItem = true;

			if (ModContent.GetInstance<BlockLitterConfig>().SandgunLitter != DropType.Item)
			{
				return base.PreKill(projectile, timeLeft);
			}

			// Removed -- For some reason, Sandgun projectiles never spawn items when this check is in place.
			//if (projectile.owner != Main.myPlayer)
			//{
			//	return base.PreKill(projectile, timeLeft);
			//}

			int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, _sandGunProjectileToItem[projectile.type]);
			Main.item[itemIndex].noGrabDelay = 0;

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex, 1f);
			}

			return base.PreKill(projectile, timeLeft);
		}
	}
}