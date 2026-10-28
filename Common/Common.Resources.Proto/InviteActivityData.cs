using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteActivityData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__HasInviteNum;

	[ProtoMember(1, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(2, Name = "end_time")]
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

	[ProtoMember(3, Name = "has_invite_num")]
	public uint HasInviteNum
	{
		get
		{
			return __pbn__HasInviteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasInviteNum = value;
		}
	}

	[ProtoMember(4, Name = "my_invite_reward_list")]
	public List<InviteFriendRewardData> MyInviteRewardLists { get; } = new List<InviteFriendRewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeHasInviteNum()
	{
		return __pbn__HasInviteNum.HasValue;
	}

	public void ResetHasInviteNum()
	{
		__pbn__HasInviteNum = null;
	}
}
