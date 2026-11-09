using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DressReclaimEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1173
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReclaimAvatarId;

	private EquipmentSlot? __pbn__Slot;

	private uint? __pbn__ReclaimEquipId;

	[ProtoMember(1, Name = "reclaim_avatar_id")]
	public uint ReclaimAvatarId
	{
		get
		{
			return __pbn__ReclaimAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimAvatarId = value;
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

	[ProtoMember(3, Name = "reclaim_equip_id")]
	public uint ReclaimEquipId
	{
		get
		{
			return __pbn__ReclaimEquipId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReclaimEquipId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReclaimAvatarId()
	{
		return __pbn__ReclaimAvatarId.HasValue;
	}

	public void ResetReclaimAvatarId()
	{
		__pbn__ReclaimAvatarId = null;
	}

	public bool ShouldSerializeSlot()
	{
		return __pbn__Slot.HasValue;
	}

	public void ResetSlot()
	{
		__pbn__Slot = null;
	}

	public bool ShouldSerializeReclaimEquipId()
	{
		return __pbn__ReclaimEquipId.HasValue;
	}

	public void ResetReclaimEquipId()
	{
		__pbn__ReclaimEquipId = null;
	}
}
