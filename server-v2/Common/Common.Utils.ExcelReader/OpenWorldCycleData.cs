using System.Linq;

namespace Common.Utils.ExcelReader;

public class OpenWorldCycleData : BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>
{
	public override string FileName => "OpenWorldCycleData.json";

	public uint GetInitCycle(uint mapId)
	{
		return ((uint?)(from x in base.All
			where x.CycleMap == mapId
			orderby x.Cycle
			select x).FirstOrDefault()?.Cycle).GetValueOrDefault();
	}

	public uint GetNextCycle(uint mapId, uint cycle)
	{
		return (uint)((from x in base.All
			where x.CycleMap == mapId && x.Cycle > cycle
			orderby x.Cycle
			select x).FirstOrDefault()?.Cycle ?? ((int)cycle));
	}

	/// <summary>
	/// 该地图配置里最大的 cycle。
	///
	/// OpenWorldCycleData.json 是按 cycleMap + cycle 分档的：
	///   map 1 -> cycle 1/2/3/4（needstory 0/111/207/308）
	///   map 2 -> cycle 11/12/13/14
	///   map 401/501/601/701/801/1001 -> 各只有一档
	/// 服务端 all_story_done=true 时客户端认为剧情全通、周期已到顶，
	/// 所以这里也返回最大档，两边口径才一致。
	/// </summary>
	public uint GetMaxCycle(uint mapId)
	{
		OpenWorldCycleDataExcel? last = (from x in base.All
			where x.CycleMap == mapId
			orderby x.Cycle descending
			select x).FirstOrDefault();
		return (uint)(last?.Cycle ?? 0);
	}
}
