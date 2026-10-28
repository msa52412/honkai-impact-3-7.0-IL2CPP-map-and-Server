using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarArtifactLevel : BaseExcelReader<AvatarArtifactLevel, AvatarArtifactLevelExcel>
{
	public override string FileName => "AvatarArtifactLevel.json";

	public AvatarArtifactLevelExcel? FromId(int id)
	{
		return base.All.Where((AvatarArtifactLevelExcel Ava) => Ava.Id == id).FirstOrDefault();
	}
}
