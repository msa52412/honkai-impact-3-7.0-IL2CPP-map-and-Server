using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AugmentPerformanceLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "finished_entry_perform_id_list")]
	public int[] FinishedEntryPerformIdLists { get; set; }

	[ProtoMember(2, Name = "selected_finish_dialog_id_list")]
	public List<ProtoVector2Int> SelectedFinishDialogIdLists { get; } = new List<ProtoVector2Int>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
