using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExtractEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3654
	}

	private IExtension __pbn__extensionData;

	private EquipmentType? __pbn__Type;

	private uint? __pbn__UniqueId;

	private uint? __pbn__CapsuleId;

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

	[ProtoMember(3, Name = "capsule_id")]
	public uint CapsuleId
	{
		get
		{
			return __pbn__CapsuleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CapsuleId = value;
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

	public bool ShouldSerializeCapsuleId()
	{
		return __pbn__CapsuleId.HasValue;
	}

	public void ResetCapsuleId()
	{
		__pbn__CapsuleId = null;
	}
}
