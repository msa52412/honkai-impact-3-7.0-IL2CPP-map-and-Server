using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryAvatarConfig : BaseExcelReader<DormitoryAvatarConfig, DormitoryAvatarConfigExcel>
{
	public override string FileName => "DormitoryAvatarConfig.json";

	public DormitoryAvatarConfigExcel? FromAvatarId(uint avatarId)
	{
		return base.All.Where((DormitoryAvatarConfigExcel x) => x.AvatarId == (int)avatarId).FirstOrDefault();
	}
}
