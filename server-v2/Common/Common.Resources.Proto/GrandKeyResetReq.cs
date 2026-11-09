using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyResetReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 755
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "key_id_list")]
	public uint[] KeyIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
