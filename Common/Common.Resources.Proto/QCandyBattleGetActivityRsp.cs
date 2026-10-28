using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6602
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__TodayCurrencyGotNum;

	private uint? __pbn__RankScore;

	private uint? __pbn__MaxRankScore;

	private uint? __pbn__LastAvatarId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "today_currency_got_num")]
	public uint TodayCurrencyGotNum
	{
		get
		{
			return __pbn__TodayCurrencyGotNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayCurrencyGotNum = value;
		}
	}

	[ProtoMember(4, Name = "rank_score")]
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

	[ProtoMember(5, Name = "max_rank_score")]
	public uint MaxRankScore
	{
		get
		{
			return __pbn__MaxRankScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRankScore = value;
		}
	}

	[ProtoMember(6, Name = "last_avatar_id")]
	public uint LastAvatarId
	{
		get
		{
			return __pbn__LastAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastAvatarId = value;
		}
	}

	[ProtoMember(7, Name = "unlocked_avatar_list")]
	public uint[] UnlockedAvatarLists { get; set; }

	[ProtoMember(8, Name = "settle_record_list")]
	public List<QCandyBattleSettleInfo> SettleRecordLists { get; } = new List<QCandyBattleSettleInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeTodayCurrencyGotNum()
	{
		return __pbn__TodayCurrencyGotNum.HasValue;
	}

	public void ResetTodayCurrencyGotNum()
	{
		__pbn__TodayCurrencyGotNum = null;
	}

	public bool ShouldSerializeRankScore()
	{
		return __pbn__RankScore.HasValue;
	}

	public void ResetRankScore()
	{
		__pbn__RankScore = null;
	}

	public bool ShouldSerializeMaxRankScore()
	{
		return __pbn__MaxRankScore.HasValue;
	}

	public void ResetMaxRankScore()
	{
		__pbn__MaxRankScore = null;
	}

	public bool ShouldSerializeLastAvatarId()
	{
		return __pbn__LastAvatarId.HasValue;
	}

	public void ResetLastAvatarId()
	{
		__pbn__LastAvatarId = null;
	}
}
