using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFisher : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__SeatIdx;

	private ChatworldFisherStatus? __pbn__FisherStatus;

	private ulong? __pbn__StatusStartTimeMsU64;

	private uint? __pbn__WaitAssistFishHardLevel;

	private uint? __pbn__LastGetFishId;

	private ulong? __pbn__LastGetFishTimeMsU64;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "seat_idx")]
	public uint SeatIdx
	{
		get
		{
			return __pbn__SeatIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SeatIdx = value;
		}
	}

	[ProtoMember(3, Name = "fisher_status")]
	[DefaultValue(ChatworldFisherStatus.ChatworldFisherStatusNone)]
	public ChatworldFisherStatus FisherStatus
	{
		get
		{
			return __pbn__FisherStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__FisherStatus = value;
		}
	}

	[ProtoMember(4, Name = "status_start_time_ms_u64")]
	public ulong StatusStartTimeMsU64
	{
		get
		{
			return __pbn__StatusStartTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__StatusStartTimeMsU64 = value;
		}
	}

	[ProtoMember(5, Name = "wait_assist_fish_hard_level")]
	public uint WaitAssistFishHardLevel
	{
		get
		{
			return __pbn__WaitAssistFishHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitAssistFishHardLevel = value;
		}
	}

	[ProtoMember(6, Name = "assist_uid_list")]
	public uint[] AssistUidLists { get; set; }

	[ProtoMember(7, Name = "like_uid_list")]
	public uint[] LikeUidLists { get; set; }

	[ProtoMember(8, Name = "last_get_fish_id")]
	public uint LastGetFishId
	{
		get
		{
			return __pbn__LastGetFishId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastGetFishId = value;
		}
	}

	[ProtoMember(9, Name = "last_get_fish_time_ms_u64")]
	public ulong LastGetFishTimeMsU64
	{
		get
		{
			return __pbn__LastGetFishTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__LastGetFishTimeMsU64 = value;
		}
	}

	[ProtoMember(10, Name = "fish_info")]
	public ChatworldFishInfo FishInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeSeatIdx()
	{
		return __pbn__SeatIdx.HasValue;
	}

	public void ResetSeatIdx()
	{
		__pbn__SeatIdx = null;
	}

	public bool ShouldSerializeFisherStatus()
	{
		return __pbn__FisherStatus.HasValue;
	}

	public void ResetFisherStatus()
	{
		__pbn__FisherStatus = null;
	}

	public bool ShouldSerializeStatusStartTimeMsU64()
	{
		return __pbn__StatusStartTimeMsU64.HasValue;
	}

	public void ResetStatusStartTimeMsU64()
	{
		__pbn__StatusStartTimeMsU64 = null;
	}

	public bool ShouldSerializeWaitAssistFishHardLevel()
	{
		return __pbn__WaitAssistFishHardLevel.HasValue;
	}

	public void ResetWaitAssistFishHardLevel()
	{
		__pbn__WaitAssistFishHardLevel = null;
	}

	public bool ShouldSerializeLastGetFishId()
	{
		return __pbn__LastGetFishId.HasValue;
	}

	public void ResetLastGetFishId()
	{
		__pbn__LastGetFishId = null;
	}

	public bool ShouldSerializeLastGetFishTimeMsU64()
	{
		return __pbn__LastGetFishTimeMsU64.HasValue;
	}

	public void ResetLastGetFishTimeMsU64()
	{
		__pbn__LastGetFishTimeMsU64 = null;
	}
}
