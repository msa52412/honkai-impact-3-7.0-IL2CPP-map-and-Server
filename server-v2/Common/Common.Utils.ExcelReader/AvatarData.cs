using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarData : BaseExcelReader<AvatarData, AvatarDataExcel>
{
	public override string FileName => "AvatarData.json";

	public AvatarDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarDataExcel avatar) => avatar.AvatarId == id).FirstOrDefault();
	}
}
