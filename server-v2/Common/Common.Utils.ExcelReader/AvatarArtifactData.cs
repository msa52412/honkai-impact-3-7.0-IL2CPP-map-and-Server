using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarArtifactData : BaseExcelReader<AvatarArtifactData, AvatarArtifactDataExcel>
{
	public override string FileName => "AvatarArtifactData.json";

	public AvatarArtifactDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarArtifactDataExcel Ava) => Ava.ArtifactId == id).FirstOrDefault();
	}
}
