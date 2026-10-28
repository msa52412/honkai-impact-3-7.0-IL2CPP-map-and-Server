using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptFriendInviteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 228
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
		InviteInvalidRegion
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__InviteUid;

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
}
