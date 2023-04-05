using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NoSnowLitter.Common.Configs;

public enum DropType
{
	Vanilla,
	Item,
	None
}

[Label("$Mods.NoSnowLitter.Config.DisplayName")]
public sealed class BlockLitterConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[DefaultValue(DropType.Item)]
	[DrawTicks]
	[Label("$Mods.NoSnowLitter.Config.SnowBallaLitter.Label")]
	[Tooltip("$Mods.NoSnowLitter.Config.SnowBallaLitter.Tooltip")]
	public DropType SnowballLitter { get; set; }

	[DefaultValue(DropType.Item)]
	[DrawTicks]
	[Label("$Mods.NoSnowLitter.Config.AntlionLitter.Label")]
	[Tooltip("$Mods.NoSnowLitter.Config.AntlionLitter.Tooltip")]
	public DropType AntlionSandLitter { get; set; }

	[DefaultValue(DropType.Vanilla)]
	[DrawTicks]
	[Label("$Mods.NoSnowLitter.Config.SandgunLitter.Label")]
	[Tooltip("$Mods.NoSnowLitter.Config.SandgunLitter.Tooltip")]
	public DropType SandgunLitter { get; set; }

	[DefaultValue(DropType.Item)]
	[DrawTicks]
	[Label("$Mods.NoSnowLitter.Config.TombstoneLitter.Label")]
	[Tooltip("$Mods.NoSnowLitter.Config.TombstoneLitter.Tooltip")]
	public DropType TombstoneLitter { get; set; }
}