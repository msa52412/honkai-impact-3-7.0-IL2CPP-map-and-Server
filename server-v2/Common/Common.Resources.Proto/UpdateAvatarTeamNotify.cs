using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateAvatarTeamNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 49
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "team")]
	public AvatarTeam Team { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
