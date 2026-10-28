using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarMemoirData : BaseExcelReader<AvatarMemoirData, AvatarMemoirDataExcel>
{
	public override string FileName => "AvatarMemoirData.json";

	public AvatarMemoirDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarMemoirDataExcel amd) => amd.AvatarId == id).FirstOrDefault();
	}
}
