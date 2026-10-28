using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeWeeklyRoutineRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4231
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WeeklyRoutineRewardId;

	[ProtoMember(1, Name = "weekly_routine_reward_id")]
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
