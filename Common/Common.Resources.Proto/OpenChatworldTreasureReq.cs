using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenChatworldTreasureReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2417
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__TreasureId;

	private uint? __pbn__CurTreasureScheduleId;

	private bool? __pbn__IsGsCheck;

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

	[ProtoMember(2, Name = "treasure_id")]
	public uint TreasureId
	{
		get
		{
			return __pbn__TreasureId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureId = value;
		}
	}

	[ProtoMember(3, Name = "cur_treasure_schedule_id")]
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

	[ProtoMember(4, Name = "is_gs_check")]
	public bool IsGsCheck
	{
		get
		{
			return __pbn__IsGsCheck.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGsCheck = value;
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

	public bool ShouldSerializeTreasureId()
	{
		return __pbn__TreasureId.HasValue;
	}

	public void ResetTreasureId()
	{
		__pbn__TreasureId = null;
	}

	public bool ShouldSerializeCurTreasureScheduleId()
	{
		return __pbn__CurTreasureScheduleId.HasValue;
	}

	public void ResetCurTreasureScheduleId()
	{
		__pbn__CurTreasureScheduleId = null;
	}

	public bool ShouldSerializeIsGsCheck()
	{
		return __pbn__IsGsCheck.HasValue;
	}

	public void ResetIsGsCheck()
	{
		__pbn__IsGsCheck = null;
	}
}
