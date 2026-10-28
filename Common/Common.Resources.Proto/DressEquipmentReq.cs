using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DressEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 39
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private EquipmentSlot? __pbn__Slot;

	private uint? __pbn__UniqueId;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "slot")]
	[DefaultValue(EquipmentSlot.EquipmentSlotWeapon1)]
	public EquipmentSlot Slot
	{
		get
		{
			return __pbn__Slot ?? EquipmentSlot.EquipmentSlotWeapon1;
		}
		set
		{
			__pbn__Slot = value;
		}
	}

	[ProtoMember(3, Name = "unique_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeSlot()
	{
		return __pbn__Slot.HasValue;
	}

	public void ResetSlot()
	{
		__pbn__Slot = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}
}
