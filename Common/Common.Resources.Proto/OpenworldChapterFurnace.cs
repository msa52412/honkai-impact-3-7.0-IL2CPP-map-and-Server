using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterFurnace : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "formula_use_history_list")]
	public List<OpenworldChapterFurnaceFormulaUseHistory> FormulaUseHistoryLists { get; } = new List<OpenworldChapterFurnaceFormulaUseHistory>();


	[ProtoMember(2, Name = "furnace_work_list")]
	public List<OpenworldChapterFurnaceWork> FurnaceWorkLists { get; } = new List<OpenworldChapterFurnaceWork>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
