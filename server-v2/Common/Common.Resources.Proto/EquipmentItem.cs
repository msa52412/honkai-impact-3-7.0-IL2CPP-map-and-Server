using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipmentItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private EquipmentType? __pbn__Type;

	private uint? __pbn__IdOrUniqueId;

	private uint? __pbn__Num;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(EquipmentType.EquipmentMaterial)]
	public EquipmentType Type
	{
		get
		{
			return __pbn__Type ?? EquipmentType.EquipmentMaterial;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "id_or_unique_id")]
	public uint IdOrUniqueId
	{
		get
		{
			return __pbn__IdOrUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__IdOrUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeIdOrUniqueId()
	{
		return __pbn__IdOrUniqueId.HasValue;
	}

	public void ResetIdOrUniqueId()
	{
		__pbn__IdOrUniqueId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
