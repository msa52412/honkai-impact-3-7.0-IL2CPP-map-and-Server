using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReforgeEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3657
	}

	private IExtension __pbn__extensionData;

	private EquipmentType? __pbn__Type;

	private uint? __pbn__UniqueId;

	private uint? __pbn__TargetEquipId;

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

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "target_equip_id")]
	public uint TargetEquipId
	{
		get
		{
			return __pbn__TargetEquipId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetEquipId = value;
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

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeTargetEquipId()
	{
		return __pbn__TargetEquipId.HasValue;
	}

	public void ResetTargetEquipId()
	{
		__pbn__TargetEquipId = null;
	}
}
