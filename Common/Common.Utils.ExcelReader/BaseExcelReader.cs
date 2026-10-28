using System;
using System.IO;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public abstract class BaseExcelReader<Self, Scheme>
{
	private readonly Logger c = new Logger("Factory", ConsoleColor.Yellow);

	private static Self Instance;

	public Scheme[] All { get; set; }

	public abstract string FileName { get; }

	public static Self GetInstance()
	{
		Self instance = Instance;
		if (instance == null)
		{
			Instance = Activator.CreateInstance<Self>();
		}
		if ((Instance as BaseExcelReader<Self, Scheme>).All == null)
		{
			(Instance as BaseExcelReader<Self, Scheme>).Load();
			if (Global.config.VerboseLevel > VerboseLevel.Normal)
			{
				(Instance as BaseExcelReader<Self, Scheme>).c.Log(typeof(Self).Name + " Excel Loaded From " + (Instance as BaseExcelReader<Self, Scheme>).FileName);
			}
		}
		return Instance;
	}

	public void Load()
	{
		All = JsonConvert.DeserializeObject<Scheme[]>(File.ReadAllText("Resources\\ExcelOutputAsset\\" + FileName)) ?? Array.Empty<Scheme>();
	}
}
