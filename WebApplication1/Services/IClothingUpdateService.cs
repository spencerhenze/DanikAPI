using DanikAPI.Models;

namespace DanikAPI.Services
{
	public interface IClothingUpdateService
	{
		void SetGymnastClothingNeedsFlags(Gymnast gymnast, Gymnast updatedGymnast);

	}
}