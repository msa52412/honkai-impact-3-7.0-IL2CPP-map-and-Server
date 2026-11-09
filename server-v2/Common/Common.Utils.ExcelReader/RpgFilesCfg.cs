using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// RpgFilesDataV2.json（112 行）—— RPG 故事的「档案/文件」，按 TaleID 分组。
/// `GetRpgTaleFileRsp.file_id_list` 就是这张表里该 tale 的 FileID 列表。
/// </summary>
public class RpgFilesCfg : BaseExcelReader<RpgFilesCfg, RpgFilesCfgExcel>
{
	public override string FileName => "RpgFilesDataV2.json";

	public List<uint> FileIdsOf(uint taleId)
	{
		List<uint> ids = new List<uint>();
		if (base.All == null)
		{
			return ids;
		}
		foreach (RpgFilesCfgExcel row in base.All)
		{
			if (row.TaleId == taleId && row.FileId != 0u)
			{
				ids.Add(row.FileId);
			}
		}
		return ids;
	}
}

public class RpgFilesCfgExcel
{
	[JsonProperty("TaleID")]
	public uint TaleId { get; set; }

	[JsonProperty("FileID")]
	public uint FileId { get; set; }

	[JsonProperty("AreaType")]
	public string AreaType { get; set; }
}
