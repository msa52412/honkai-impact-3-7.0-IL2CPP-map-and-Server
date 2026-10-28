using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageDropDisplayInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private bool? __pbn__DoubleDrop;

	private uint? __pbn__BattleDisplayId;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "drop_item_id_list")]
	public uint[] DropItemIdLists { get; set; }

	[ProtoMember(3, Name = "first_drop_item_id_list")]
	public uint[] FirstDropItemIdLists { get; set; }

	[ProtoMember(4, Name = "double_drop")]
	public bool DoubleDrop
	{
		get
		{
			return __pbn__DoubleDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__DoubleDrop = value;
		}
	}

	[ProtoMember(5, Name = "bonus_drop_item_id_list")]
	public uint[] BonusDropItemIdLists { get; set; }

	[ProtoMember(6, Name = "battle_buff_list")]
	public List<string> BattleBuffLists { get; } = new List<string>();


	[ProtoMember(7, Name = "bonus_equip_need_list")]
	public uint[] BonusEquipNeedLists { get; set; }

	[ProtoMember(8, Name = "battle_display_id")]
	public uint BattleDisplayId
	{
		get
		{
			return __pbn__BattleDisplayId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleDisplayId = value;
		}
	}

	[ProtoMember(9, Name = "battle_attr_list")]
	public List<string> BattleAttrLists { get; } = new List<string>();


	[ProtoMember(10, Name = "first_drop_item_data_list")]
	public List<RewardItemData> FirstDropItemDataLists { get; } = new List<RewardItemData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeDoubleDrop()
	{
		return __pbn__DoubleDrop.HasValue;
	}

	public void ResetDoubleDrop()
	{
		__pbn__DoubleDrop = null;
	}

	public bool ShouldSerializeBattleDisplayId()
	{
		return __pbn__BattleDisplayId.HasValue;
	}

	public void ResetBattleDisplayId()
	{
		__pbn__BattleDisplayId = null;
	}
}
