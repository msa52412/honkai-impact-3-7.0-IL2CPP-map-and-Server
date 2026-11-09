using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviterActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__InviteNum;

	private uint? __pbn__TakenRewardProgress;

	private uint? __pbn__AcceptInviteNum;

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

	[ProtoMember(2, Name = "invite_num")]
	public uint InviteNum
	{
		get
		{
			return __pbn__InviteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__InviteNum = value;
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

	[ProtoMember(4, Name = "accept_invite_num")]
	public uint AcceptInviteNum
	{
		get
		{
			return __pbn__AcceptInviteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptInviteNum = value;
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

	public bool ShouldSerializeInviteNum()
	{
		return __pbn__InviteNum.HasValue;
	}

	public void ResetInviteNum()
	{
		__pbn__InviteNum = null;
	}

	public bool ShouldSerializeTakenRewardProgress()
	{
		return __pbn__TakenRewardProgress.HasValue;
	}

	public void ResetTakenRewardProgress()
	{
		__pbn__TakenRewardProgress = null;
	}

	public bool ShouldSerializeAcceptInviteNum()
	{
		return __pbn__AcceptInviteNum.HasValue;
	}

	public void ResetAcceptInviteNum()
	{
		__pbn__AcceptInviteNum = null;
	}
}
