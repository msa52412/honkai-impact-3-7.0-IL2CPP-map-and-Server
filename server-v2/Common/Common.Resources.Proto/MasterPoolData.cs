using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPoolData : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "material_list")]
	public List<Material> MaterialLists { get; } = new List<Material>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
