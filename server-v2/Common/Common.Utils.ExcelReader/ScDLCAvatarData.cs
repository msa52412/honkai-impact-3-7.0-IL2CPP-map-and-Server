using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>后崩坏书 2 角色表（ScDLCAvatar.json）读取器。</summary>
public class ScDLCAvatarData : BaseExcelReader<ScDLCAvatarData, ScDLCAvatarDataExcel>
{
	public override string FileName => "ScDLCAvatar.json";

	public ScDLCAvatarDataExcel? FromId(int avatarId)
	{
		return base.All.Where((ScDLCAvatarDataExcel x) => x.AvatarId == avatarId).FirstOrDefault();
	}
}
