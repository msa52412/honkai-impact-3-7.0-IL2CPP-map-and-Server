using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessItemEffect.json 读取器 —— 按道具 type 取「背包上限 / 效果上限」。
///
/// 注意：这张表的主键是 typeID（1..6），不是道具 ID。
/// 用错成 OWEndlessItem.ID 会一条都查不到，然后所有上限判定都退化成 0。
/// </summary>
public class OWEndlessItemEffect : BaseExcelReader<OWEndlessItemEffect, OWEndlessItemEffectExcel>
{
	public override string FileName => "OWEndlessItemEffect.json";

	/// <summary>按 typeID 查一行；查不到返回 null。</summary>
	public OWEndlessItemEffectExcel? ByType(int typeId)
	{
		return All.FirstOrDefault((OWEndlessItemEffectExcel row) => row.TypeID == typeId);
	}

	/// <summary>背包上限；表里没有该 type 时回 99（宁松不紧，避免私服直接玩不了）。</summary>
	public int BagAmount(int typeId)
	{
		return ByType(typeId)?.BagAmount ?? 99;
	}

	/// <summary>效果上限；表里没有该 type 时回 1。</summary>
	public int EffectAmount(int typeId)
	{
		return ByType(typeId)?.EffectAmount ?? 1;
	}
}
