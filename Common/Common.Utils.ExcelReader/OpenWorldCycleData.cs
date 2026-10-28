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
}
