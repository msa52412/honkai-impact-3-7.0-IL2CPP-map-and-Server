using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamSyncNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 395
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "team_data")]
	public MpTeamData TeamData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
