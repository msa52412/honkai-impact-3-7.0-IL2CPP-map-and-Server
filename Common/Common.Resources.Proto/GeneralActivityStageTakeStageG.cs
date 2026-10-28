using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityStageTakeStageGroupRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4036
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityStageGroupId;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "activity_stage_group_id")]
	public uint ActivityStageGroupId
	{
		get
		{
			return __pbn__ActivityStageGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityStageGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeActivityStageGroupId()
	{
		return __pbn__ActivityStageGroupId.HasValue;
	}

	public void ResetActivityStageGroupId()
	{
		__pbn__ActivityStageGroupId = null;
	}
}
[ProtoContract]
public class GeneralActivityStageTakeStageGroupRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4037
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken,
		[ProtoEnum(Name = "NOT_REACH")]
		NotReach
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityStageGroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(3, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(4, Name = "activity_stage_group_id")]
	public uint ActivityStageGroupId
	{
		get
		{
			return __pbn__ActivityStageGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityStageGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeActivityStageGroupId()
	{
		return __pbn__ActivityStageGroupId.HasValue;
	}

	public void ResetActivityStageGroupId()
	{
		__pbn__ActivityStageGroupId = null;
	}
}
