//using Terraria;
//using MonoMod.Cil;
//using Terraria.ModLoader;
//using System;
//using NoSnowLitter.Common.Configs;
//using Mono.Cecil.Cil;

//namespace NoSnowLitter.Common.GlobalProjectiles
//{
//	public class TombstoneModifier : GlobalProjectile
//	{
//		public override bool Autoload(ref string name)
//		{
//			IL.Terraria.Projectile.VanillaAI += PreventPlacingTombstones;
//			return base.Autoload(ref name);
//		}

//		/// <summary>
//		/// Changes the following check in Projectile.VanillaAI:
//		///
//		///		if (owner != Main.myPlayer)
//		///			return;
//		///		
//		///		int num261 = (int)((base.position.X + (float)(width / 2)) / 16f);
//		///
//		/// to:
//		///
//		///		if (owner != Main.myPlayer)
//		///			return;
//		///		
//		///		if (ModContent.GetInstance<BlockLitterConfig>().StopTombstoneLitter)
//		///			return;
//		///		
//		///		int num261 = (int)((base.position.X + (float)(width / 2)) / 16f);
//		///
//		/// </summary>
//		private void PreventPlacingTombstones(ILContext il)
//		{
//			ILCursor cursor = new ILCursor(il);

//			/// Match the following IL:
//			///		IL_BCFA: ldarg.0
//			///		IL_BCFB: ldfld     int32 Terraria.Projectile::aiStyle
//			///		IL_BD00: ldc.i4.s  17
//			///		IL_BD02: bne.un    IL_BEF8
//			///	This places the cursor onto ldarg.0.
			
//			if (!cursor.TryGotoNext(MoveType.Before,
//				i => i.MatchLdarg(0),
//				i => i.MatchLdfld<Projectile>(nameof(Projectile.aiStyle)),
//				i => i.MatchLdcI4(17),
//				i => i.MatchBneUn(out _)
//			))
//			{
//				throw new Exception("Unable to patch Terraria.Projectile.VanillaAI: Could not match IL (aiStyle check).");
//			}

//			/// Match the following IL:
//			/// 	IL_BD63: ldarg.0
//			///		IL_BD64: ldfld     int32 Terraria.Projectile::owner
//			///		IL_BD69: ldsfld    int32 Terraria.Main::myPlayer
//			///		IL_BD6E: beq.s     IL_BD71
//			///	This places the cursor onto ldarg.0. 

//			if (!cursor.TryGotoNext(MoveType.Before,
//				i => i.MatchLdarg(0),
//				i => i.MatchLdfld<Projectile>(nameof(Projectile.owner)),
//				i => i.MatchLdsfld<Main>(nameof(Main.myPlayer)),
//				i => i.MatchBeq(out _)
//			))
//			{
//				throw new Exception("Unable to patch Terraria.Projectile.VanillaAI: Could not match IL (owner check).");
//			}

//			cursor.Index += 4;

//			// Add a return after the owner check.

//			ILLabel label = il.DefineLabel();

//			cursor.EmitDelegate<Func<bool>>(() => ModContent.GetInstance<BlockLitterConfig>().StopTombstoneLitter);
//			cursor.Emit(OpCodes.Brfalse, label);
//			cursor.Emit(OpCodes.Ret);
//			cursor.MarkLabel(label);
//		}
//	}
//}