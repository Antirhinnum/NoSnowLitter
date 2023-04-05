using NoSnowLitter.Common.Configs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles;

public sealed class TombstoneModifier : GlobalProjectile
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

	public override bool PreAI(Projectile projectile)
	{
		// It's not worth it to prevent tombstones from dropping entirely, so instead,
		// on the very first frame of a Tombstone existing, it is killed and the associated
		// item is dropped.

		DropType stopTombstoneLitterOption = ModContent.GetInstance<BlockLitterConfig>().TombstoneLitter;

		if (stopTombstoneLitterOption == DropType.Vanilla)
		{
			return base.PreAI(projectile);
		}

		if (stopTombstoneLitterOption == DropType.Item && projectile.owner == Main.myPlayer)
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

		return false;
	}
}