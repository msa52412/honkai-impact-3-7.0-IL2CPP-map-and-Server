using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTeamBriefInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 352
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private bool? __pbn__IsFriendInvitation;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "is_friend_invitation")]
	public bool IsFriendInvitation
	{
		get
		{
			return __pbn__IsFriendInvitation.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFriendInvitation = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeIsFriendInvitation()
	{
		return __pbn__IsFriendInvitation.HasValue;
	}

	public void ResetIsFriendInvitation()
	{
		__pbn__IsFriendInvitation = null;
	}
}
