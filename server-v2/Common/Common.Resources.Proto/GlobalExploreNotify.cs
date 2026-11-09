using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6902
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "event_list")]
	public List<GlobalExploreEvent> EventLists { get; } = new List<GlobalExploreEvent>();


	[ProtoMember(2, Name = "move_pos_list")]
	public List<GlobalExplorePos> MovePosLists { get; } = new List<GlobalExplorePos>();


	[ProtoMember(3, Name = "update_info")]
	public GlobalExploreInfo UpdateInfo { get; set; }

	[ProtoMember(4, Name = "remove_quest_list")]
	public uint[] RemoveQuestLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
