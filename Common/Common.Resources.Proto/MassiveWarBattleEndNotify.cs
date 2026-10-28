using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarBattleEndNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4810
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleUniqId;

	private string __pbn__TransactionStr;

	private uint? __pbn__TotalDamage;

	private uint? __pbn__DamageLevel;

	private uint? __pbn__Rank;

	private uint? __pbn__TotalDamageScore;

	private uint? __pbn__RankScore;

	private uint? __pbn__BasicDamageScore;

	private uint? __pbn__TotalMemberNum;

	private uint? __pbn__BattleCostTime;

	[ProtoMember(1, Name = "battle_uniq_id")]
	public uint BattleUniqId
	{
		get
		{
			return __pbn__BattleUniqId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleUniqId = value;
		}
	}

	[ProtoMember(2, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr
	{
		get
		{
			return __pbn__TransactionStr ?? "";
		}
		set
		{
			__pbn__TransactionStr = value;
		}
	}

	[ProtoMember(3, Name = "total_damage")]
	public uint TotalDamage
	{
		get
		{
			return __pbn__TotalDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDamage = value;
		}
	}

	[ProtoMember(4, Name = "damage_level")]
	public uint DamageLevel
	{
		get
		{
			return __pbn__DamageLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__DamageLevel = value;
		}
	}

	[ProtoMember(5, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(6, Name = "total_damage_score")]
	public uint TotalDamageScore
	{
		get
		{
			return __pbn__TotalDamageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDamageScore = value;
		}
	}

	[ProtoMember(7, Name = "rank_score")]
	public uint RankScore
	{
		get
		{
			return __pbn__RankScore.GetValueOrDefault();
		}
		set
		{
			__pbn__RankScore = value;
		}
	}

	[ProtoMember(8, Name = "basic_damage_score")]
	public uint BasicDamageScore
	{
		get
		{
			return __pbn__BasicDamageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BasicDamageScore = value;
		}
	}

	[ProtoMember(9, Name = "total_member_num")]
	public uint TotalMemberNum
	{
		get
		{
			return __pbn__TotalMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalMemberNum = value;
		}
	}

	[ProtoMember(10, Name = "battle_cost_time")]
	public uint BattleCostTime
	{
		get
		{
			return __pbn__BattleCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleCostTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleUniqId()
	{
		return __pbn__BattleUniqId.HasValue;
	}

	public void ResetBattleUniqId()
	{
		__pbn__BattleUniqId = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeTotalDamage()
	{
		return __pbn__TotalDamage.HasValue;
	}

	public void ResetTotalDamage()
	{
		__pbn__TotalDamage = null;
	}

	public bool ShouldSerializeDamageLevel()
	{
		return __pbn__DamageLevel.HasValue;
	}

	public void ResetDamageLevel()
	{
		__pbn__DamageLevel = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeTotalDamageScore()
	{
		return __pbn__TotalDamageScore.HasValue;
	}

	public void ResetTotalDamageScore()
	{
		__pbn__TotalDamageScore = null;
	}

	public bool ShouldSerializeRankScore()
	{
		return __pbn__RankScore.HasValue;
	}

	public void ResetRankScore()
	{
		__pbn__RankScore = null;
	}

	public bool ShouldSerializeBasicDamageScore()
	{
		return __pbn__BasicDamageScore.HasValue;
	}

	public void ResetBasicDamageScore()
	{
		__pbn__BasicDamageScore = null;
	}

	public bool ShouldSerializeTotalMemberNum()
	{
		return __pbn__TotalMemberNum.HasValue;
	}

	public void ResetTotalMemberNum()
	{
		__pbn__TotalMemberNum = null;
	}

	public bool ShouldSerializeBattleCostTime()
	{
		return __pbn__BattleCostTime.HasValue;
	}

	public void ResetBattleCostTime()
	{
		__pbn__BattleCostTime = null;
	}
}
