using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteActivityAcceptInviteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1697
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "INVITE_CODE_ERROR")]
		InviteCodeError,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "INVITE_FULL")]
		InviteFull,
		[ProtoEnum(Name = "INVITE_INVALID_REGION")]
		InviteInvalidRegion,
		[ProtoEnum(Name = "INVITE_CONDITION_ERROR")]
		InviteConditionError,
		[ProtoEnum(Name = "NO_ACTIVITY")]
		NoActivity,
		[ProtoEnum(Name = "ALREADY_INVITED")]
		AlreadyInvited,
		[ProtoEnum(Name = "INVITE_DAILY_LIMIT")]
		InviteDailyLimit,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__InviteUid;

	private InviteeActivityType? __pbn__InviteType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "invite_uid")]
	public uint InviteUid
	{
		get
		{
			return __pbn__InviteUid.GetValueOrDefault();
		}
		set
		{
			__pbn__InviteUid = value;
		}
	}

	[ProtoMember(3, Name = "invite_type")]
	[DefaultValue(InviteeActivityType.InviteeActivityTypeNone)]
	public InviteeActivityType InviteType
	{
		get
		{
			return __pbn__InviteType.GetValueOrDefault();
		}
		set
		{
			__pbn__InviteType = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInviteUid()
	{
		return __pbn__InviteUid.HasValue;
	}

	public void ResetInviteUid()
	{
		__pbn__InviteUid = null;
	}

	public bool ShouldSerializeInviteType()
	{
		return __pbn__InviteType.HasValue;
	}

	public void ResetInviteType()
	{
		__pbn__InviteType = null;
	}
}
