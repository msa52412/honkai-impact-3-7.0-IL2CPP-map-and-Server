using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpKickFromTeamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 354
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MemberUid;

	[ProtoMember(1, Name = "member_uid")]
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
