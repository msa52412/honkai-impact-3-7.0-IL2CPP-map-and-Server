using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessItem.json 读取器 —— 迪拉克之海 / 量子奇点的游戏内道具表。
///
/// 服务端用途：
///   3420 GetOpenworldEndlessDataRsp.item_list        背包（item_id + num）
///   3420 GetOpenworldEndlessDataRsp.effect_item_list 生效中的增益
///   3422 UseOpenworldEndlessItemReq                  校验 item_id 是否存在 / 取 type
///   3427 GetOpenworldEndlessStageInnerDataRsp        关卡内生效效果
///
/// 表里没有「背包上限」，上限在 OWEndlessItemEffect.bagAmount（按 type 分组）。
/// </summary>
public class OWEndlessItem : BaseExcelReader<OWEndlessItem, OWEndlessItemExcel>
{
	public override string FileName => "OWEndlessItem.json";

	/// <summary>按 item_id 查一行；查不到返回 null（服务端必须按 null 回 FAIL）。</summary>
	public OWEndlessItemExcel? ById(uint itemId)
	{
		return All.FirstOrDefault((OWEndlessItemExcel row) => (uint)row.ID == itemId);
	}

	/// <summary>按语义类型取全部道具（type 1..6）。</summary>
	public List<OWEndlessItemExcel> ByType(int type)
	{
		return All.Where((OWEndlessItemExcel row) => row.Type == type).ToList();
	}
}
