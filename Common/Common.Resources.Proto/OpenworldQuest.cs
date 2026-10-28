using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldQuest : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private OpenworldQuestStatus? __pbn__Status;

	private uint? __pbn__BuffId;

	private uint? __pbn__DebuffId;

	private uint? __pbn__Position;

	private uint? __pbn__Progress;

	private uint? __pbn__RarityRewardId;

	private uint? __pbn__QuestActivityId;

	private uint? __pbn__SpecialTimeoutSec;

	[ProtoMember(2, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(3, Name = "status")]
	[DefaultValue(OpenworldQuestStatus.OpenworldQuestNone)]
	public OpenworldQuestStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(5, Name = "debuff_id")]
	public uint DebuffId
	{
		get
		{
			return __pbn__DebuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__DebuffId = value;
		}
	}

	[ProtoMember(6, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(7, Name = "finish_challenge_list")]
	public uint[] FinishChallengeLists { get; set; }

	[ProtoMember(8, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(9, Name = "rarity_reward_id")]
	public uint RarityRewardId
	{
		get
		{
			return __pbn__RarityRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RarityRewardId = value;
		}
	}

	[ProtoMember(10, Name = "quest_activity_id")]
	public uint QuestActivityId
	{
		get
		{
			return __pbn__QuestActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__QuestActivityId = value;
		}
	}

	[ProtoMember(11, Name = "special_timeout_sec")]
	public uint SpecialTimeoutSec
	{
		get
		{
			return __pbn__SpecialTimeoutSec.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialTimeoutSec = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeDebuffId()
	{
		return __pbn__DebuffId.HasValue;
	}

	public void ResetDebuffId()
	{
		__pbn__DebuffId = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeRarityRewardId()
	{
		return __pbn__RarityRewardId.HasValue;
	}

	public void ResetRarityRewardId()
	{
		__pbn__RarityRewardId = null;
	}

	public bool ShouldSerializeQuestActivityId()
	{
		return __pbn__QuestActivityId.HasValue;
	}

	public void ResetQuestActivityId()
	{
		__pbn__QuestActivityId = null;
	}

	public bool ShouldSerializeSpecialTimeoutSec()
	{
		return __pbn__SpecialTimeoutSec.HasValue;
	}

	public void ResetSpecialTimeoutSec()
	{
		__pbn__SpecialTimeoutSec = null;
	}
}
