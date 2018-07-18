using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DanikAPI.Models;
using DanikAPI.Models.Uniforms.Enums;

namespace DanikAPI.Helpers
{
	public class ClothingUpdateHelper
	{
		public static bool CheckIfLeoNeeded(LevelsEnum previousLevel, LevelsEnum newLevel)
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

		public static bool ChekIfJacketNeeded(Gymnast gymnast, Gymnast updatedGymnast)
		{
			if (gymnast.Level >= LevelsEnum.Level3 && updatedGymnast.Level >= LevelsEnum.Level3)
			{
				var updatedJacketSize = GetJacketSizeFromMeasurements(updatedGymnast.ChestMeasurement, updatedGymnast.WaistMeasurement);
				var updatedTorsoSize = GetTorsoSizeFromMeasurements(updatedJacketSize, updatedGymnast.TorsoMeasurement);

				return (updatedJacketSize != gymnast.JacketSize || updatedTorsoSize != gymnast.TorsoSize);
			}
			return false;
		}

		private static GkJacketSizeEnum GetJacketSizeFromMeasurements(int chestMeasurement, int waistMeasurement)
		{

			if ((chestMeasurement >= 17 && chestMeasurement <= 19) && (waistMeasurement >= 18 && waistMeasurement <=19))
			{
				return GkJacketSizeEnum.ChildExtraExtraSmall;
			}
			if ((chestMeasurement >= 20 && chestMeasurement <= 22) && (waistMeasurement >= 19 && waistMeasurement <= 21))
			{
				return GkJacketSizeEnum.ChildExtraSmall;
			}
			if ((chestMeasurement >= 23 && chestMeasurement <= 26) && (waistMeasurement >= 21 && waistMeasurement <= 22))
			{
				return GkJacketSizeEnum.ChildSmall;
			}
			if ((chestMeasurement >= 26 && chestMeasurement <= 29) && (waistMeasurement >= 22 && waistMeasurement <= 23))
			{
				return GkJacketSizeEnum.ChildMedium;
			}
			if ((chestMeasurement >= 29 && chestMeasurement <= 31) && (waistMeasurement >= 23 && waistMeasurement <= 25))
			{
				return GkJacketSizeEnum.ChildLarge;
			}
			if ((chestMeasurement >= 32 && chestMeasurement <= 34) && (waistMeasurement >= 23 && waistMeasurement <= 25))
			{
				return GkJacketSizeEnum.AdultExtraSmall;
			}
			if ((chestMeasurement >= 33 && chestMeasurement <= 35) && (waistMeasurement >= 25 && waistMeasurement <= 26))
			{
				return GkJacketSizeEnum.AdultSmall;
			}
			if ((chestMeasurement >= 35 && chestMeasurement <= 36) && (waistMeasurement >= 26 && waistMeasurement <= 27))
			{
				return GkJacketSizeEnum.AdultMedium;
			}
			if ((chestMeasurement >= 36 && chestMeasurement <= 37) && (waistMeasurement >= 28 && waistMeasurement <= 29))
			{
				return GkJacketSizeEnum.AdultLarge;
			}
			if ((chestMeasurement >= 37 && chestMeasurement <= 39) && (waistMeasurement >= 29 && waistMeasurement <= 30))
			{
				return GkJacketSizeEnum.AdultExtraLarge;
			}
			if ((chestMeasurement >= 39 && chestMeasurement <= 42) && (waistMeasurement >= 30 && waistMeasurement <= 33))
			{
				return GkJacketSizeEnum.Adult2Xl;
			}
			if ((chestMeasurement >= 41 && chestMeasurement <= 44) && (waistMeasurement >= 32 && waistMeasurement <= 35))
			{
				return GkJacketSizeEnum.Adult3Xl;
			}
			if ((chestMeasurement >= 43 && chestMeasurement <= 46) && (waistMeasurement >= 34 && waistMeasurement <= 37))
			{
				return GkJacketSizeEnum.Adult4Xl;
			}
			return GkJacketSizeEnum.Unmatched;
		}

		private static GkTorsoSizeEnum GetTorsoSizeFromMeasurements(GkJacketSizeEnum jacketSize, int torsoMeasurement)
		{
			if (jacketSize == GkJacketSizeEnum.ChildExtraExtraSmall)
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
			if (jacketSize == GkJacketSizeEnum.ChildExtraSmall)
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
			if (jacketSize == GkJacketSizeEnum.ChildSmall)
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
			if (jacketSize == GkJacketSizeEnum.ChildMedium)
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
			if (jacketSize == GkJacketSizeEnum.ChildLarge)
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
			if (jacketSize == GkJacketSizeEnum.AdultExtraSmall)
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
			if (jacketSize == GkJacketSizeEnum.AdultSmall)
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
			if (jacketSize == GkJacketSizeEnum.AdultMedium)
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
			if (jacketSize == GkJacketSizeEnum.AdultLarge)
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
			if (jacketSize == GkJacketSizeEnum.AdultExtraLarge)
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
			if (jacketSize == GkJacketSizeEnum.Adult2Xl)
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
			if (jacketSize == GkJacketSizeEnum.Adult3Xl)
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
			if (jacketSize == GkJacketSizeEnum.Adult4Xl)
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
