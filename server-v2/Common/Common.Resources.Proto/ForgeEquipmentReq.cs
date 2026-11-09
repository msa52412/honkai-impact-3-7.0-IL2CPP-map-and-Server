using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ForgeEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3652
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ForgeId;

	private uint? __pbn__KeepRuneEquipUniqueId;

	[ProtoMember(1, Name = "forge_id")]
	public uint ForgeId
	{
		get
		{
			return __pbn__ForgeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ForgeId = value;
		}
	}

	[ProtoMember(2, Name = "consume_item_list")]
	public EquipmentItemList ConsumeItemList { get; set; }

	[ProtoMember(4, Name = "keep_rune_equip_unique_id")]
	public uint KeepRuneEquipUniqueId
	{
		get
		{
			return __pbn__KeepRuneEquipUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__KeepRuneEquipUniqueId = value;
		}
	}

	[ProtoMember(5, Name = "keep_rune_cost_item_list")]
	public EquipmentItemList KeepRuneCostItemList { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeForgeId()
	{
		return __pbn__ForgeId.HasValue;
	}

	public void ResetForgeId()
	{
		__pbn__ForgeId = null;
	}

	public bool ShouldSerializeKeepRuneEquipUniqueId()
	{
		return __pbn__KeepRuneEquipUniqueId.HasValue;
	}

	public void ResetKeepRuneEquipUniqueId()
	{
		__pbn__KeepRuneEquipUniqueId = null;
	}
}
