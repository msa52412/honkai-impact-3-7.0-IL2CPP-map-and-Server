using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessBattleArea.json 读取器 —— 区域 → 战斗列表。
///
/// 用途：GetOpenworldEndlessStageInnerDataReq (3426) 的响应。
/// 客户端点「开始战斗」后先发 3426（area_id = 当前选中区域），
/// 页面 OWEndlessPreparePageContext 收到 Succ 才会调 DoBeginLevel 切场景。
/// </summary>
public class OWEndlessBattleArea : BaseExcelReader<OWEndlessBattleArea, OWEndlessBattleAreaExcel>
{
	public override string FileName => "OWEndlessBattleArea.json";

	/// <summary>某个区域下的全部战斗，按 battleID 升序（= AreaA..AreaH 的顺序）。</summary>
	public List<OWEndlessBattleAreaExcel> ByArea(int areaId)
	{
		return All.Where((OWEndlessBattleAreaExcel row) => row.AreaId == areaId)
			.OrderBy((OWEndlessBattleAreaExcel row) => row.BattleID)
			.ToList();
	}

	/// <summary>按 (areaId, 区域名) 取一行，和客户端 GetBattleIDByAreaNameAreaID 同键。</summary>
	public OWEndlessBattleAreaExcel? ByAreaName(int areaId, string battleArea)
	{
		return All.FirstOrDefault((OWEndlessBattleAreaExcel row) => row.AreaId == areaId && row.BattleArea == battleArea);
	}
}
