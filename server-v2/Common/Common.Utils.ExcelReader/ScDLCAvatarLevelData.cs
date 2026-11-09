using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// ScDLCAvatarLevel.json（540 行 = 6 个角色 × 90 级）—— 后崩坏书 2 角色等级表。
///
/// 关键：<c>FeverLevelLimit</c> 是**狂热等级门槛**。30 级要狂热 10，90 级要狂热 30。
/// 只下发 level 而不抬 fever.level，客户端会把等级夹回门槛允许的最大值
/// （狂热 1 时只能显示 1 级），表现就是「角色等级没点」。
/// </summary>
public class ScDLCAvatarLevelData : BaseExcelReader<ScDLCAvatarLevelData, ScDLCAvatarLevelDataExcel>
{
	public override string FileName => "ScDLCAvatarLevel.json";

	public int MaxLevel()
	{
		int max = 0;
		foreach (ScDLCAvatarLevelDataExcel row in base.All)
		{
			if (row.Level > max)
			{
				max = row.Level;
			}
		}
		return max;
	}

	/// <summary>该等级要求的狂热等级；查不到回 0。</summary>
	public int FeverLimit(int level)
	{
		foreach (ScDLCAvatarLevelDataExcel row in base.All)
		{
			if (row.Level == level)
			{
				return row.FeverLevelLimit;
			}
		}
		return 0;
	}
}

public class ScDLCAvatarLevelDataExcel
{
	[JsonProperty("AvatarID")]
	public long AvatarId { get; set; }

	[JsonProperty("Level")]
	public int Level { get; set; }

	/// <summary>解锁这一级需要的狂热等级。</summary>
	[JsonProperty("FeverLevelLimit")]
	public int FeverLevelLimit { get; set; }

	[JsonProperty("HPAdd")]
	public int HpAdd { get; set; }

	[JsonProperty("ATKAdd")]
	public int AtkAdd { get; set; }

	[JsonProperty("DEFAdd")]
	public int DefAdd { get; set; }
}
