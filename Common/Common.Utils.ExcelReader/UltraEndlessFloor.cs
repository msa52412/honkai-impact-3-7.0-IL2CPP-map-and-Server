using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

public class UltraEndlessFloor : BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>
{
	public override string FileName => "UltraEndlessFloor.json";

	public List<UltraEndlessFloorExcel> GetFloorDatasFromStageId(int id)
	{
		return base.All.Where((UltraEndlessFloorExcel floor) => floor.StageId == id).ToList();
	}

	public UltraEndlessFloorExcel? FromStageAndFloorId(int stageId, int floorId)
	{
		return base.All.FirstOrDefault((UltraEndlessFloorExcel x) => x.FloorId == floorId && x.StageId == stageId);
	}
}
