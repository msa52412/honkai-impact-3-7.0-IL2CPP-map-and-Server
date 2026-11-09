using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalExploreInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__KingdomId;

	private uint? __pbn__ExploreLevel;

	private uint? __pbn__ExploreExp;

	private uint? __pbn__ExploreStamina;

	private uint? __pbn__ExploreStaminaLimit;

	private bool? __pbn__IsTakeKingdomReward;

	[ProtoMember(1, Name = "kingdom_id")]
	public uint KingdomId
	{
		get
		{
			return __pbn__KingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__KingdomId = value;
		}
	}

	[ProtoMember(2, Name = "cur_pos")]
	public GlobalExplorePos CurPos { get; set; }

	[ProtoMember(3, Name = "cur_stage")]
	public GlobalExploreStage CurStage { get; set; }

	[ProtoMember(4, Name = "explore_level")]
	public uint ExploreLevel
	{
		get
		{
			return __pbn__ExploreLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ExploreLevel = value;
		}
	}

	[ProtoMember(5, Name = "explore_exp")]
	public uint ExploreExp
	{
		get
		{
			return __pbn__ExploreExp.GetValueOrDefault();
		}
		set
		{
			__pbn__ExploreExp = value;
		}
	}

	[ProtoMember(6, Name = "explore_stamina")]
	public uint ExploreStamina
	{
		get
		{
			return __pbn__ExploreStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__ExploreStamina = value;
		}
	}

	[ProtoMember(7, Name = "entity_list")]
	public List<GlobalExploreEntity> EntityLists { get; } = new List<GlobalExploreEntity>();


	[ProtoMember(8, Name = "grid_list")]
	public List<GlobalExploreGrid> GridLists { get; } = new List<GlobalExploreGrid>();


	[ProtoMember(9, Name = "quest_list")]
	public List<GlobalExploreQuest> QuestLists { get; } = new List<GlobalExploreQuest>();


	[ProtoMember(10, Name = "explore_stamina_limit")]
	public uint ExploreStaminaLimit
	{
		get
		{
			return __pbn__ExploreStaminaLimit.GetValueOrDefault();
		}
		set
		{
			__pbn__ExploreStaminaLimit = value;
		}
	}

	[ProtoMember(11, Name = "is_take_kingdom_reward")]
	public bool IsTakeKingdomReward
	{
		get
		{
			return __pbn__IsTakeKingdomReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeKingdomReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKingdomId()
	{
		return __pbn__KingdomId.HasValue;
	}

	public void ResetKingdomId()
	{
		__pbn__KingdomId = null;
	}

	public bool ShouldSerializeExploreLevel()
	{
		return __pbn__ExploreLevel.HasValue;
	}

	public void ResetExploreLevel()
	{
		__pbn__ExploreLevel = null;
	}

	public bool ShouldSerializeExploreExp()
	{
		return __pbn__ExploreExp.HasValue;
	}

	public void ResetExploreExp()
	{
		__pbn__ExploreExp = null;
	}

	public bool ShouldSerializeExploreStamina()
	{
		return __pbn__ExploreStamina.HasValue;
	}

	public void ResetExploreStamina()
	{
		__pbn__ExploreStamina = null;
	}

	public bool ShouldSerializeExploreStaminaLimit()
	{
		return __pbn__ExploreStaminaLimit.HasValue;
	}

	public void ResetExploreStaminaLimit()
	{
		__pbn__ExploreStaminaLimit = null;
	}

	public bool ShouldSerializeIsTakeKingdomReward()
	{
		return __pbn__IsTakeKingdomReward.HasValue;
	}

	public void ResetIsTakeKingdomReward()
	{
		__pbn__IsTakeKingdomReward = null;
	}
}
