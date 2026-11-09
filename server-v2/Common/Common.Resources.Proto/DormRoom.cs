using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DormRoom : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private string __pbn__Name;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(3, Name = "furniture_list")]
	public List<Furniture> FurnitureLists { get; } = new List<Furniture>();


	[ProtoMember(4, Name = "avatar_list")]
	public uint[] AvatarLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}
}
