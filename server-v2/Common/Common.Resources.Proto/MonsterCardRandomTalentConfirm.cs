using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonsterCardRandomTalentConfirmData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__CostMaterialId;

	private uint? __pbn__TalentIndex;

	private uint? __pbn__OldTalentId;

	private uint? __pbn__NewTalentId;

	[ProtoMember(1, Name = "unique_id")]
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

	[ProtoMember(2, Name = "cost_material_id")]
	public uint CostMaterialId
	{
		get
		{
			return __pbn__CostMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__CostMaterialId = value;
		}
	}

	[ProtoMember(3, Name = "talent_index")]
	public uint TalentIndex
	{
		get
		{
			return __pbn__TalentIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__TalentIndex = value;
		}
	}

	[ProtoMember(4, Name = "old_talent_id")]
	public uint OldTalentId
	{
		get
		{
			return __pbn__OldTalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldTalentId = value;
		}
	}

	[ProtoMember(5, Name = "new_talent_id")]
	public uint NewTalentId
	{
		get
		{
			return __pbn__NewTalentId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewTalentId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeCostMaterialId()
	{
		return __pbn__CostMaterialId.HasValue;
	}

	public void ResetCostMaterialId()
	{
		__pbn__CostMaterialId = null;
	}

	public bool ShouldSerializeTalentIndex()
	{
		return __pbn__TalentIndex.HasValue;
	}

	public void ResetTalentIndex()
	{
		__pbn__TalentIndex = null;
	}

	public bool ShouldSerializeOldTalentId()
	{
		return __pbn__OldTalentId.HasValue;
	}

	public void ResetOldTalentId()
	{
		__pbn__OldTalentId = null;
	}

	public bool ShouldSerializeNewTalentId()
	{
		return __pbn__NewTalentId.HasValue;
	}

	public void ResetNewTalentId()
	{
		__pbn__NewTalentId = null;
	}
}
[ProtoContract]
public enum MonsterCardRandomTalentConfirmType
{
	[ProtoEnum(Name = "MONSTER_CARD_RANDOM_TALENT_CONFIRM_TYPE_NONE")]
	MonsterCardRandomTalentConfirmTypeNone,
	[ProtoEnum(Name = "MONSTER_CARD_RANDOM_TALENT_CONFIRM_TYPE_CONFIRM")]
	MonsterCardRandomTalentConfirmTypeConfirm,
	[ProtoEnum(Name = "MONSTER_CARD_RANDOM_TALENT_CONFIRM_TYPE_ABANDON")]
	MonsterCardRandomTalentConfirmTypeAbandon
}
