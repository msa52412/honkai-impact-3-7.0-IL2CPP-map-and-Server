using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpKickFromTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 355
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MemberUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "member_uid")]
	public uint MemberUid
	{
		get
		{
			return __pbn__MemberUid.GetValueOrDefault();
		}
		set
		{
			__pbn__MemberUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMemberUid()
	{
		return __pbn__MemberUid.HasValue;
	}

	public void ResetMemberUid()
	{
		__pbn__MemberUid = null;
	}
}
