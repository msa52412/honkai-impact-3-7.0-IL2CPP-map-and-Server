using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetReclaimAvatarTeamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1171
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "reclaim_avatar_team_list")]
	public uint[] ReclaimAvatarTeamLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
