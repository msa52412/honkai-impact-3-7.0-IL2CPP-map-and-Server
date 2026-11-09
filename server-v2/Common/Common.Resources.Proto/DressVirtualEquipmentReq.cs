using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DressVirtualEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3506
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__VirtualAvatarId;

	private uint? __pbn__EquipType;

	private uint? __pbn__Slot;

	private uint? __pbn__VirtualEquipId;

	[ProtoMember(1, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(2, Name = "virtual_avatar_id")]
	public uint VirtualAvatarId
	{
		get
		{
			return __pbn__VirtualAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualAvatarId = value;
		}
	}

	[ProtoMember(3, Name = "equip_type")]
	public uint EquipType
	{
		get
		{
			return __pbn__EquipType.GetValueOrDefault();
		}
		set
		{
			__pbn__EquipType = value;
		}
	}

	[ProtoMember(4, Name = "slot")]
	public uint Slot
	{
		get
		{
			return __pbn__Slot.GetValueOrDefault();
		}
		set
		{
			__pbn__Slot = value;
		}
	}

	[ProtoMember(5, Name = "virtual_equip_id")]
	public uint VirtualEquipId
	{
		get
		{
			return __pbn__VirtualEquipId.GetValueOrDefault();
		}
		set
		{
			__pbn__VirtualEquipId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}

	public bool ShouldSerializeVirtualAvatarId()
	{
		return __pbn__VirtualAvatarId.HasValue;
	}

	public void ResetVirtualAvatarId()
	{
		__pbn__VirtualAvatarId = null;
	}

	public bool ShouldSerializeEquipType()
	{
		return __pbn__EquipType.HasValue;
	}

	public void ResetEquipType()
	{
		__pbn__EquipType = null;
	}

	public bool ShouldSerializeSlot()
	{
		return __pbn__Slot.HasValue;
	}

	public void ResetSlot()
	{
		__pbn__Slot = null;
	}

	public bool ShouldSerializeVirtualEquipId()
	{
		return __pbn__VirtualEquipId.HasValue;
	}

	public void ResetVirtualEquipId()
	{
		__pbn__VirtualEquipId = null;
	}
}
