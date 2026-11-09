using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UniqueGachaUpdateItemPoolReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4722
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "select_item_list")]
	public uint[] SelectItemLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
