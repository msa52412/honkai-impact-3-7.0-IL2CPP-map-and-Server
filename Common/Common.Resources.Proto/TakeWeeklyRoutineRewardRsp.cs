using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeWeeklyRoutineRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4232
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ROUTINE_CONTRIBUTION_LACK")]
		RoutineContributionLack,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WeeklyRoutineRewardId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "weekly_routine_reward_id")]
	public uint WeeklyRoutineRewardId
	{
		get
		{
			return __pbn__WeeklyRoutineRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__WeeklyRoutineRewardId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWeeklyRoutineRewardId()
	{
		return __pbn__WeeklyRoutineRewardId.HasValue;
	}

	public void ResetWeeklyRoutineRewardId()
	{
		__pbn__WeeklyRoutineRewardId = null;
	}
}
