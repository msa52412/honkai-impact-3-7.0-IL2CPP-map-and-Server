using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainRole : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__VirtualRoleId;

	[ProtoMember(1, Name = "virtual_role_id")]
	public uint VirtualRoleId
	{
		get
		{
			return __pbn__VirtualRoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualRoleId = value;
		}
	}

	[ProtoMember(2, Name = "custom_list")]
	public List<VirtualTrainCustom> CustomLists { get; } = new List<VirtualTrainCustom>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVirtualRoleId()
	{
		return __pbn__VirtualRoleId.HasValue;
	}

	public void ResetVirtualRoleId()
	{
		__pbn__VirtualRoleId = null;
	}
}
