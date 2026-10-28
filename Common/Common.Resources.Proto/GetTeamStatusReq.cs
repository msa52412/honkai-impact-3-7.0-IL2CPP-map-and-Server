using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTeamStatusReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1553
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "team_id_list")]
	public uint[] TeamIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
