using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetChatworldTreasureDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2415
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__CurTreasureScheduleId;

	[ProtoMember(1, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(2, Name = "cur_treasure_schedule_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
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
