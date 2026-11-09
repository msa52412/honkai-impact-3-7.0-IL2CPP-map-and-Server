using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetDepotFurnitureReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 607
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "furniture_id_list")]
	public uint[] FurnitureIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
