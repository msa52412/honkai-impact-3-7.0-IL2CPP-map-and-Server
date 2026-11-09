using System.Linq;

namespace Common.Utils.ExcelReader;

public class ElfData : BaseExcelReader<ElfData, ElfDataExcel>
{
	public override string FileName => "ElfData.json";

	public ElfDataExcel? FromId(int id)
	{
		return base.All.Where((ElfDataExcel elf) => elf.ElfId == id).FirstOrDefault();
	}
}
