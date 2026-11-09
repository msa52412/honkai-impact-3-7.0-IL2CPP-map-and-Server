using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarFragmentData : BaseExcelReader<AvatarFragmentData, AvatarFragmentDataExcel>
{
	public override string FileName => "AvatarFragmentData.json";

	public AvatarFragmentDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarFragmentDataExcel fragment) => fragment.Id == id).FirstOrDefault();
	}
}
