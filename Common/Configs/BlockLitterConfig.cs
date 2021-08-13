using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NoSnowLitter.Common.Configs
{
	public class BlockLitterConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[DefaultValue(true)]
		[DisplayName("Stop Snow Balla Litter")]
		[Tooltip("Stops Snow Ballas from creating Snow Blocks.")]
		public bool StopSnowballLitter { get; set; }

		[DefaultValue(true)]
		[DisplayName("Stop Antlion Litter")]
		[Tooltip("Stops Antlions from creating Sand Blocks.")]
		public bool StopAntlionSandLitter { get; set; }

		[DefaultValue(true)]
		[DisplayName("Stop Sandgun Litter")]
		[Tooltip("Stops the Sandgun from placing Sand blocks.")]
		public bool StopSandgunLitter { get; set; }

		//[DefaultValue(true)]
		//[DisplayName("Stop Tombstone Litter")]
		//[Tooltip("Stops dropped Tombstones from placing into the world.")]
		//public bool StopTombstoneLitter { get; set; }
	}
}