using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryRoomData : BaseExcelReader<DormitoryRoomData, DormitoryRoomDataExcel>
{
	public override string FileName => "DormitoryRoomData.json";

	public DormitoryRoomDataExcel? FromId(int id)
	{
		return base.All.Where((DormitoryRoomDataExcel DormRoom) => DormRoom.RoomId == id).FirstOrDefault();
	}
}
