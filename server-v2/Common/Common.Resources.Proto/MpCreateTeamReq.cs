using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpCreateTeamReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 348
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "member")]
	public MpTeamMember Member { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
