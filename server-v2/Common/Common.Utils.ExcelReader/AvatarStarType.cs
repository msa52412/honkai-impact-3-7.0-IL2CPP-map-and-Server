using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarStarType : BaseExcelReader<AvatarStarType, AvatarStarTypeExcel>
{
	public struct StarInfo
	{
		public int Star { get; set; }

		public int SubStar { get; set; }

		public int Cost { get; set; }

		public StarInfo(int star, int subStar)
		{
			Cost = 0;
			Star = star;
			SubStar = subStar;
		}
	}

	public override string FileName => "AvatarStarType.json";

	public StarInfo GetNextStar(int avatarType, int starUpType, StarInfo currentStarInfo)
	{
		AvatarStarTypeExcel avatarStarTypeExcel = base.All.FirstOrDefault((AvatarStarTypeExcel x) => x.AvatarType == avatarType && x.AvatarStarUpType == starUpType && x.Star == currentStarInfo.Star && x.SubStar == currentStarInfo.SubStar);
		if (avatarStarTypeExcel != null)
		{
			currentStarInfo.Cost = avatarStarTypeExcel.Upgrade;
		}
		AvatarStarTypeExcel avatarStarTypeExcel2 = base.All.FirstOrDefault((AvatarStarTypeExcel x) => x.AvatarType == avatarType && x.AvatarStarUpType == starUpType && x.Star == ((currentStarInfo.SubStar == 3 || currentStarInfo.Star < 3) ? (currentStarInfo.Star + 1) : currentStarInfo.Star) && x.SubStar == ((currentStarInfo.SubStar < 3 && currentStarInfo.Star >= 3) ? (currentStarInfo.SubStar + 1) : 0));
		if (avatarStarTypeExcel2 != null)
		{
			currentStarInfo.SubStar = avatarStarTypeExcel2.SubStar;
			currentStarInfo.Star = avatarStarTypeExcel2.Star;
		}
		return currentStarInfo;
	}
}
