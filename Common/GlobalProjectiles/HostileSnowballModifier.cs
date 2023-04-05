using NoSnowLitter.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoSnowLitter.Common.GlobalProjectiles;

public sealed class HostileSnowballModifier : GlobalProjectile
{
	public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
	{
		return entity.type == ProjectileID.SnowBallHostile;
	}

	public override void SetDefaults(Projectile projectile)
	{
		if (ModContent.GetInstance<BlockLitterConfig>().SnowballLitter == DropType.Vanilla)
		{
			return;
		}

		// Snow Ballas create one projectile: SnowBallHostile (109).
		// This will only create a block if projectile.noDropItem is false, so we set it to true.

		projectile.noDropItem = true;
	}

	public override void Kill(Projectile projectile, int timeLeft)
	{
		if (ModContent.GetInstance<BlockLitterConfig>().SnowballLitter != DropType.Item)
		{
			return;
		}

		if (projectile.owner != Main.myPlayer)
		{
			return;
		}

		int itemIndex = Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ItemID.SnowBlock);
		Main.item[itemIndex].noGrabDelay = 0;

		if (Main.netMode == NetmodeID.Server)
		{
			NetMessage.SendData(MessageID.SyncItem, -1, -1, null, itemIndex, 1f);
		}
	}
}