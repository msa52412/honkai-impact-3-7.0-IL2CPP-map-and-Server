using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyFurnitureReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 621
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "furniture_list")]
	public List<DepotFurniture> FurnitureLists { get; } = new List<DepotFurniture>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
