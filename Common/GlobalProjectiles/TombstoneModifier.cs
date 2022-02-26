using NoSnowLitter.Common.Configs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class TombstoneModifier : GlobalProjectile
	{
		private static readonly Dictionary<int, int> _graveMarkerProjectileTypeToItemType = new()
		{
			{ ProjectileID.Tombstone, ItemID.Tombstone },
			{ ProjectileID.GraveMarker, ItemID.GraveMarker },
			{ ProjectileID.CrossGraveMarker, ItemID.CrossGraveMarker },
			{ ProjectileID.Headstone, ItemID.Headstone },
			{ ProjectileID.Gravestone, ItemID.Gravestone },
			{ ProjectileID.Obelisk, ItemID.Obelisk },
			{ ProjectileID.RichGravestone1, ItemID.RichGravestone1 },
			{ ProjectileID.RichGravestone2, ItemID.RichGravestone2 },
			{ ProjectileID.RichGravestone3, ItemID.RichGravestone3 },
			{ ProjectileID.RichGravestone4, ItemID.RichGravestone4 },
			{ ProjectileID.RichGravestone5, ItemID.RichGravestone5 }
		};

		public override bool PreAI(Projectile projectile)
		{
			// It's not worth it to prevent tombstones from dropping entirely, so instead,
			// on the very first frame of a Tombstone existing, it is killed and the associated
			// item is dropped.

			if (projectile.aiStyle != ProjAIStyleID.GraveMarker || projectile.owner != Main.myPlayer)
			{
				return base.PreAI(projectile);
			}

			DropType stopTombstoneLitterOption = ModContent.GetInstance<BlockLitterConfig>().TombstoneLitter;

			if (stopTombstoneLitterOption == DropType.Vanilla)
			{
				return base.PreAI(projectile);
			}

			if (stopTombstoneLitterOption == DropType.Item)
			{
				int itemIndex = Item.NewItem(projectile.Hitbox, _graveMarkerProjectileTypeToItemType[projectile.type]);
				Main.item[itemIndex].noGrabDelay = 0;

				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex, 1f);
				}
			}

			projectile.Kill();
			return false;
		}
	}
}