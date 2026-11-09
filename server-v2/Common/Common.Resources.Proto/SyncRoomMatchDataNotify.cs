using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncRoomMatchDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3169
	}

	[ProtoContract]
	public enum Reason
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "CANCEL")]
		Cancel,
		[ProtoEnum(Name = "TIMEOUT")]
		Timeout,
		[ProtoEnum(Name = "MATCH_SUCC")]
		MatchSucc
	}

	private IExtension __pbn__extensionData;

	private RoomMode? __pbn__RoomMode;

	private bool? __pbn__IsInMatching;

	private uint? __pbn__MatchStartTime;

	private uint? __pbn__EstimateMatchCostTime;

	private Reason? __pbn__reason;

	[ProtoMember(1, Name = "room_mode")]
	[DefaultValue(RoomMode.RoomModeInvalid)]
	public RoomMode RoomMode
	{
		get
		{
			return __pbn__RoomMode.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomMode = value;
		}
	}

	[ProtoMember(2, Name = "is_in_matching")]
	public bool IsInMatching
	{
		get
		{
			return __pbn__IsInMatching.GetValueOrDefault();
		}
		set
		{
			__pbn__IsInMatching = value;
		}
	}

	[ProtoMember(3, Name = "match_start_time")]
	public uint MatchStartTime
	{
		get
		{
			return __pbn__MatchStartTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MatchStartTime = value;
		}
	}

	[ProtoMember(4, Name = "estimate_match_cost_time")]
	public uint EstimateMatchCostTime
	{
		get
		{
			return __pbn__EstimateMatchCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EstimateMatchCostTime = value;
		}
	}

	[ProtoMember(5)]
	[DefaultValue(Reason.Succ)]
	public Reason reason
	{
		get
		{
			return __pbn__reason.GetValueOrDefault();
		}
		set
		{
			__pbn__reason = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoomMode()
	{
		return __pbn__RoomMode.HasValue;
	}

	public void ResetRoomMode()
	{
		__pbn__RoomMode = null;
	}

	public bool ShouldSerializeIsInMatching()
	{
		return __pbn__IsInMatching.HasValue;
	}

	public void ResetIsInMatching()
	{
		__pbn__IsInMatching = null;
	}

	public bool ShouldSerializeMatchStartTime()
	{
		return __pbn__MatchStartTime.HasValue;
	}

	public void ResetMatchStartTime()
	{
		__pbn__MatchStartTime = null;
	}

	public bool ShouldSerializeEstimateMatchCostTime()
	{
		return __pbn__EstimateMatchCostTime.HasValue;
	}

	public void ResetEstimateMatchCostTime()
	{
		__pbn__EstimateMatchCostTime = null;
	}

	public bool ShouldSerializereason()
	{
		return __pbn__reason.HasValue;
	}

	public void Resetreason()
	{
		__pbn__reason = null;
	}
}
