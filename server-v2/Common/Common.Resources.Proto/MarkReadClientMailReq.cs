using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MarkReadClientMailReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3804
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "mail_key")]
	public ClientMailKey MailKey { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
