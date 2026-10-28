using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBoxTakeRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2470
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "DAILY_LIMIT")]
		DailyLimit,
		[ProtoEnum(Name = "BOX_NOT_EXIST")]
		BoxNotExist,
		[ProtoEnum(Name = "FINISH")]
		Finish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BoxSeriesId;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__StageId;

	private uint? __pbn__BoxUniqueId;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__SceneId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "box_series_id")]
	public uint BoxSeriesId
	{
		get
		{
			return __pbn__BoxSeriesId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxSeriesId = value;
		}
	}

	[ProtoMember(5, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(6, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(7, Name = "box_unique_id")]
	public uint BoxUniqueId
	{
		get
		{
			return __pbn__BoxUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BoxUniqueId = value;
		}
	}

	[ProtoMember(8, Name = "open_box_unique_id_u64_list")]
	public ulong[] OpenBoxUniqueIdU64Lists { get; set; }

	[ProtoMember(9, Name = "chatworld_id")]
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

	[ProtoMember(10, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBoxSeriesId()
	{
		return __pbn__BoxSeriesId.HasValue;
	}

	public void ResetBoxSeriesId()
	{
		__pbn__BoxSeriesId = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBoxUniqueId()
	{
		return __pbn__BoxUniqueId.HasValue;
	}

	public void ResetBoxUniqueId()
	{
		__pbn__BoxUniqueId = null;
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}
}
