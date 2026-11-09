using System.Linq;

namespace Common.Utils.ExcelReader;

public class GodWarMainAvatar : BaseExcelReader<GodWarMainAvatar, GodWarMainAvatarExcel>
{
	public override string FileName => "GodWarMainAvatar.json";

	public GodWarMainAvatarExcel? FromId(int id)
	{
		return base.All.Where((GodWarMainAvatarExcel gwmae) => gwmae.MainAvatarId == id).FirstOrDefault();
	}
}
