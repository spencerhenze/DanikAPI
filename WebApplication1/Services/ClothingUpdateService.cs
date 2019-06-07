using DanikAPI.Models;
using DanikAPI.Models.Uniforms.Enums;
using DanikAPI.Services;

namespace DanikAPI.Helpers
{
	public class ClothingUpdateService : IClothingUpdateService
	{
		public void SetGymnastClothingNeedsFlags(Gymnast gymnast, Gymnast updatedGymnast)
		{
			if (gymnast.Level != updatedGymnast.Level)
			{
				gymnast.NeedsLeo = CheckIfLeoNeeded(gymnast, updatedGymnast);
				gymnast.NeedsJacket = ChekIfJacketNeeded(gymnast, updatedGymnast);
			}
		}

		public bool CheckIfLeoNeeded(Gymnast gymnast, Gymnast updatedGymnast)
		{
			var needsLeoBecauseOfLevelChange = CheckIfLeoNeededBecauseOfLevelChange(gymnast.Level, updatedGymnast.Level);

			if (needsLeoBecauseOfLevelChange)
			{
				return true;
			}

			var needsLeoBecauseOfSizeChange = CheckIfLeoNeededBecauseOfSizeChange(gymnast, updatedGymnast);

			return false;
		}

		private bool CheckIfLeoNeededBecauseOfSizeChange(Gymnast gymnast, Gymnast updatedGymnast)
		{
			// need to create this method
			//			var updatedLeoSize = GetLeoSizeFromMeasurements(updatedGymnast.ChestMeasurement, updatedGymnast.WaistMeasurement, updatedGymnast.HipsMeasurement);
			//			var updatedTorsoSize = GetTorsoSizeFromMeasurements(updatedLeoSize, updatedGymnast.TorsoMeasurement);
			return true;
		}

		private bool CheckIfLeoNeededBecauseOfLevelChange(LevelsEnum previousLevel, LevelsEnum newLevel)
		{
			if (previousLevel < LevelsEnum.Level5 && newLevel == LevelsEnum.Level5)
			{
				return false;
			}
			if (previousLevel >= LevelsEnum.Level6 && newLevel <= LevelsEnum.Level10)
			{
				return false;
			}
			if (previousLevel == LevelsEnum.XcelBronze && newLevel == LevelsEnum.XcelSilver || previousLevel == LevelsEnum.XcelSilver && newLevel == LevelsEnum.XcelBronze)
			{
				return false;
			}
			if (previousLevel >= LevelsEnum.XcelGold && newLevel <= LevelsEnum.XcelDiamond)
			{
				return false;
			}
			return true;
		}

		public bool ChekIfJacketNeeded(Gymnast gymnast, Gymnast updatedGymnast)
		{
			if (gymnast.Level >= LevelsEnum.Level3 && updatedGymnast.Level >= LevelsEnum.Level3)
			{
				var updatedJacketSize = GetJacketSizeFromMeasurements(updatedGymnast.ChestMeasurement, updatedGymnast.WaistMeasurement);
				var updatedTorsoSize = GetTorsoSizeFromMeasurements(updatedJacketSize, updatedGymnast.TorsoMeasurement);

				return (updatedJacketSize != gymnast.LeoAndJacketSize || updatedTorsoSize != gymnast.TorsoSize);
			}
			return false;
		}


		private GkLeoAndJacketSizeEnum GetJacketSizeFromMeasurements(int chestMeasurement, int waistMeasurement)
		{

			if ((chestMeasurement >= 17 && chestMeasurement <= 19) && (waistMeasurement >= 18 && waistMeasurement <= 19))
			{
				return GkLeoAndJacketSizeEnum.ChildExtraExtraSmall;
			}
			if ((chestMeasurement >= 20 && chestMeasurement <= 22) && (waistMeasurement >= 19 && waistMeasurement <= 21))
			{
				return GkLeoAndJacketSizeEnum.ChildExtraSmall;
			}
			if ((chestMeasurement >= 23 && chestMeasurement <= 26) && (waistMeasurement >= 21 && waistMeasurement <= 22))
			{
				return GkLeoAndJacketSizeEnum.ChildSmall;
			}
			if ((chestMeasurement >= 26 && chestMeasurement <= 29) && (waistMeasurement >= 22 && waistMeasurement <= 23))
			{
				return GkLeoAndJacketSizeEnum.ChildMedium;
			}
			if ((chestMeasurement >= 29 && chestMeasurement <= 31) && (waistMeasurement >= 23 && waistMeasurement <= 25))
			{
				return GkLeoAndJacketSizeEnum.ChildLarge;
			}
			if ((chestMeasurement >= 32 && chestMeasurement <= 34) && (waistMeasurement >= 23 && waistMeasurement <= 25))
			{
				return GkLeoAndJacketSizeEnum.AdultExtraSmall;
			}
			if ((chestMeasurement >= 33 && chestMeasurement <= 35) && (waistMeasurement >= 25 && waistMeasurement <= 26))
			{
				return GkLeoAndJacketSizeEnum.AdultSmall;
			}
			if ((chestMeasurement >= 35 && chestMeasurement <= 36) && (waistMeasurement >= 26 && waistMeasurement <= 27))
			{
				return GkLeoAndJacketSizeEnum.AdultMedium;
			}
			if ((chestMeasurement >= 36 && chestMeasurement <= 37) && (waistMeasurement >= 28 && waistMeasurement <= 29))
			{
				return GkLeoAndJacketSizeEnum.AdultLarge;
			}
			if ((chestMeasurement >= 37 && chestMeasurement <= 39) && (waistMeasurement >= 29 && waistMeasurement <= 30))
			{
				return GkLeoAndJacketSizeEnum.AdultExtraLarge;
			}
			if ((chestMeasurement >= 39 && chestMeasurement <= 42) && (waistMeasurement >= 30 && waistMeasurement <= 33))
			{
				return GkLeoAndJacketSizeEnum.Adult2Xl;
			}
			if ((chestMeasurement >= 41 && chestMeasurement <= 44) && (waistMeasurement >= 32 && waistMeasurement <= 35))
			{
				return GkLeoAndJacketSizeEnum.Adult3Xl;
			}
			if ((chestMeasurement >= 43 && chestMeasurement <= 46) && (waistMeasurement >= 34 && waistMeasurement <= 37))
			{
				return GkLeoAndJacketSizeEnum.Adult4Xl;
			}
			return GkLeoAndJacketSizeEnum.Unmatched;
		}

		private GkTorsoSizeEnum GetTorsoSizeFromMeasurements(GkLeoAndJacketSizeEnum leoAndJacketSize, int torsoMeasurement)
		{
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.ChildExtraExtraSmall)
			{
				if (torsoMeasurement < 34 || (torsoMeasurement >= 34 && torsoMeasurement <= 36))
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 37 && torsoMeasurement <= 39)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.ChildExtraSmall)
			{
				if (torsoMeasurement >= 37 && torsoMeasurement <= 39)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 40 && torsoMeasurement <= 42)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.ChildSmall)
			{
				if (torsoMeasurement >= 40 && torsoMeasurement <= 42)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 43 && torsoMeasurement <= 45)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.ChildMedium)
			{
				if (torsoMeasurement >= 43 && torsoMeasurement <= 45)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 46 && torsoMeasurement <= 48)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.ChildLarge)
			{
				if (torsoMeasurement >= 46 && torsoMeasurement <= 48)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 49 && torsoMeasurement <= 51)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.AdultExtraSmall)
			{
				if (torsoMeasurement >= 49 && torsoMeasurement <= 51)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 52 && torsoMeasurement <= 54)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.AdultSmall)
			{
				if (torsoMeasurement >= 52 && torsoMeasurement <= 54)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 55 && torsoMeasurement <= 56)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.AdultMedium)
			{
				if (torsoMeasurement >= 55 && torsoMeasurement <= 56)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 57 && torsoMeasurement <= 59)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.AdultLarge)
			{
				if (torsoMeasurement >= 57 && torsoMeasurement <= 59)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 60 && torsoMeasurement <= 62)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.AdultExtraLarge)
			{
				if (torsoMeasurement >= 60 && torsoMeasurement <= 62)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 63 && torsoMeasurement <= 65)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.Adult2Xl)
			{
				if (torsoMeasurement >= 63 && torsoMeasurement <= 65)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 65 && torsoMeasurement <= 67)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.Adult3Xl)
			{
				if (torsoMeasurement >= 65 && torsoMeasurement <= 67)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 68 && torsoMeasurement <= 69)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}
			if (leoAndJacketSize == GkLeoAndJacketSizeEnum.Adult4Xl)
			{
				if (torsoMeasurement >= 67 && torsoMeasurement <= 69)
				{
					return GkTorsoSizeEnum.ShortTorso;
				}
				if (torsoMeasurement >= 70 && torsoMeasurement <= 71)
				{
					return GkTorsoSizeEnum.RegularTorso;
				}
			}

			return GkTorsoSizeEnum.LongTorso;
		}
	}
}
