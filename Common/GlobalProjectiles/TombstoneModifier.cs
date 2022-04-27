using NoSnowLitter.Common.Configs;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles
{
	public class TombstoneModifier : GlobalProjectile
	{
		private static readonly IReadOnlyDictionary<int, int> _graveMarkerProjectileTypeToItemType = new Dictionary<int, int>()
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

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return _graveMarkerProjectileTypeToItemType.ContainsKey(entity.type);
		}

		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			// It's not worth it to prevent tombstones from dropping entirely, so instead,
			// on the very first frame of a Tombstone existing, it is killed and the associated
			// item is dropped.

			if (projectile.owner != Main.myPlayer)
			{
				return;
			}

			DropType stopTombstoneLitterOption = ModContent.GetInstance<BlockLitterConfig>().TombstoneLitter;

			if (stopTombstoneLitterOption == DropType.Vanilla)
			{
				return;
			}

			if (stopTombstoneLitterOption == DropType.Item)
			{
				int itemType = _graveMarkerProjectileTypeToItemType[projectile.type];
				int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, itemType);
				Main.item[itemIndex].noGrabDelay = 0;

				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex, 1f);
				}
			}

			projectile.Kill();
		}
	}
}