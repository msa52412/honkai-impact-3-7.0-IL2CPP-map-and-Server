using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeInviteActivityInviterProgressRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1692
	}

	private IExtension __pbn__extensionData;

	private InviterActivityType? __pbn__ActivityType;

	[ProtoMember(1, Name = "activity_type")]
	[DefaultValue(InviterActivityType.InviterActivityTypeNone)]
	public InviterActivityType ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}
}
[ProtoContract]
public class TakeInviteActivityInviterProgressRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1693
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private InviterActivityType? __pbn__ActivityType;

	private uint? __pbn__TakenRewardProgress;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_type")]
	[DefaultValue(InviterActivityType.InviterActivityTypeNone)]
	public InviterActivityType ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	[ProtoMember(3, Name = "taken_reward_progress")]
	public uint TakenRewardProgress
	{
		get
		{
			return __pbn__TakenRewardProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__TakenRewardProgress = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}

	public bool ShouldSerializeTakenRewardProgress()
	{
		return __pbn__TakenRewardProgress.HasValue;
	}

	public void ResetTakenRewardProgress()
	{
		__pbn__TakenRewardProgress = null;
	}
}
