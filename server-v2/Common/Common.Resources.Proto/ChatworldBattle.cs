using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBattle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	private uint? __pbn__KillNum;

	private uint? __pbn__KillStreak;

	private uint? __pbn__LegendaryTimes;

	[ProtoMember(1, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(2, Name = "kill_num")]
	public uint KillNum
	{
		get
		{
			return __pbn__KillNum.GetValueOrDefault();
		}
		set
		{
			__pbn__KillNum = value;
		}
	}

	[ProtoMember(3, Name = "kill_streak")]
	public uint KillStreak
	{
		get
		{
			return __pbn__KillStreak.GetValueOrDefault();
		}
		set
		{
			__pbn__KillStreak = value;
		}
	}

	[ProtoMember(4, Name = "legendary_times")]
	public uint LegendaryTimes
	{
		get
		{
			return __pbn__LegendaryTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LegendaryTimes = value;
		}
	}

	[ProtoMember(5, Name = "collect_battle_item_list")]
	public List<ChatworldCollectBattleItem> CollectBattleItemLists { get; } = new List<ChatworldCollectBattleItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeKillNum()
	{
		return __pbn__KillNum.HasValue;
	}

	public void ResetKillNum()
	{
		__pbn__KillNum = null;
	}

	public bool ShouldSerializeKillStreak()
	{
		return __pbn__KillStreak.HasValue;
	}

	public void ResetKillStreak()
	{
		__pbn__KillStreak = null;
	}

	public bool ShouldSerializeLegendaryTimes()
	{
		return __pbn__LegendaryTimes.HasValue;
	}

	public void ResetLegendaryTimes()
	{
		__pbn__LegendaryTimes = null;
	}
}
