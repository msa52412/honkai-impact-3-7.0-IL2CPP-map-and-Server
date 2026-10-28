using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class OpenWorldCycleDataExcel
{
	[JsonProperty("level")]
	public int Level { get; set; }

	[JsonProperty("needstory")]
	public int Needstory { get; set; }

	[JsonProperty("finishreward")]
	public int Finishreward { get; set; }

	[JsonProperty("cycleMap")]
	public int CycleMap { get; set; }

	[JsonProperty("hardLvKey")]
	public string HardLvKey { get; set; }

	[JsonProperty("CycleName")]
	public HashName CycleName { get; set; }

	[JsonProperty("EntranceScene")]
	public int EntranceScene { get; set; }

	[JsonProperty("EntranceImg1")]
	public string EntranceImg1 { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("cycle")]
	public int Cycle { get; set; }
}
