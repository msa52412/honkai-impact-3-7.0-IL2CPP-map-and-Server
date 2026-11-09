using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldTreasureMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurTreasureScheduleId;

	[ProtoMember(1, Name = "cur_treasure_schedule_id")]
	public uint CurTreasureScheduleId
	{
		get
		{
			return __pbn__CurTreasureScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTreasureScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "new_treasure_list")]
	public List<ChatworldTreasure> NewTreasureLists { get; } = new List<ChatworldTreasure>();


	[ProtoMember(3, Name = "update_treasure_list")]
	public List<ChatworldTreasure> UpdateTreasureLists { get; } = new List<ChatworldTreasure>();


	[ProtoMember(4, Name = "delete_treasure_list")]
	public List<ChatworldTreasure> DeleteTreasureLists { get; } = new List<ChatworldTreasure>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurTreasureScheduleId()
	{
		return __pbn__CurTreasureScheduleId.HasValue;
	}

	public void ResetCurTreasureScheduleId()
	{
		__pbn__CurTreasureScheduleId = null;
	}
}
