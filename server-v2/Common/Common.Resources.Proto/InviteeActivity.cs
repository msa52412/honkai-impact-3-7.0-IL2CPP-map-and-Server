using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteeActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private InviteeActivityType? __pbn__ActivityType;

	private uint? __pbn__TakenInviteeRewardId;

	private uint? __pbn__EndTime;

	[ProtoMember(1, Name = "schedule_id")]
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

	[ProtoMember(2, Name = "activity_type")]
	[DefaultValue(InviteeActivityType.InviteeActivityTypeNone)]
	public InviteeActivityType ActivityType
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

	[ProtoMember(3, Name = "taken_invitee_reward_id")]
	public uint TakenInviteeRewardId
	{
		get
		{
			return __pbn__TakenInviteeRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__TakenInviteeRewardId = value;
		}
	}

	[ProtoMember(4, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}

	public bool ShouldSerializeTakenInviteeRewardId()
	{
		return __pbn__TakenInviteeRewardId.HasValue;
	}

	public void ResetTakenInviteeRewardId()
	{
		__pbn__TakenInviteeRewardId = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}
}
