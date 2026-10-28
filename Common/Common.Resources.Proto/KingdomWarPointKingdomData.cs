using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarPointKingdomData : IExtensible
{
	[ProtoContract]
	public class KingdomPlayer : IExtensible
	{
		private IExtension __pbn__extensionData;

		private string __pbn__NickName;

		private uint? __pbn__Score;

		[ProtoMember(1, Name = "nick_name")]
		[DefaultValue("")]
		public string NickName
		{
			get
			{
				return __pbn__NickName ?? "";
			}
			set
			{
				__pbn__NickName = value;
			}
		}

		[ProtoMember(2, Name = "score")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeNickName()
		{
			return __pbn__NickName != null;
		}

		public void ResetNickName()
		{
			__pbn__NickName = null;
		}

		public bool ShouldSerializeScore()
		{
			return __pbn__Score.HasValue;
		}

		public void ResetScore()
		{
			__pbn__Score = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KingdomId;

	private ulong? __pbn__ScoreU64;

	private ulong? __pbn__VoteScoreU64;

	private ulong? __pbn__LastScoreU64;

	private uint? __pbn__PlayerNum;

	private uint? __pbn__LastScoreTime;

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

	[ProtoMember(2, Name = "score_u64")]
	public ulong ScoreU64
	{
		get
		{
			return __pbn__ScoreU64.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreU64 = value;
		}
	}

	[ProtoMember(3, Name = "vote_score_u64")]
	public ulong VoteScoreU64
	{
		get
		{
			return __pbn__VoteScoreU64.GetValueOrDefault();
		}
		set
		{
			__pbn__VoteScoreU64 = value;
		}
	}

	[ProtoMember(4, Name = "last_score_u64")]
	public ulong LastScoreU64
	{
		get
		{
			return __pbn__LastScoreU64.GetValueOrDefault();
		}
		set
		{
			__pbn__LastScoreU64 = value;
		}
	}

	[ProtoMember(5, Name = "player_num")]
	public uint PlayerNum
	{
		get
		{
			return __pbn__PlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerNum = value;
		}
	}

	[ProtoMember(6, Name = "recent_player_list")]
	public List<KingdomPlayer> RecentPlayerLists { get; } = new List<KingdomPlayer>();


	[ProtoMember(7, Name = "last_score_time")]
	public uint LastScoreTime
	{
		get
		{
			return __pbn__LastScoreTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastScoreTime = value;
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

	public bool ShouldSerializeScoreU64()
	{
		return __pbn__ScoreU64.HasValue;
	}

	public void ResetScoreU64()
	{
		__pbn__ScoreU64 = null;
	}

	public bool ShouldSerializeVoteScoreU64()
	{
		return __pbn__VoteScoreU64.HasValue;
	}

	public void ResetVoteScoreU64()
	{
		__pbn__VoteScoreU64 = null;
	}

	public bool ShouldSerializeLastScoreU64()
	{
		return __pbn__LastScoreU64.HasValue;
	}

	public void ResetLastScoreU64()
	{
		__pbn__LastScoreU64 = null;
	}

	public bool ShouldSerializePlayerNum()
	{
		return __pbn__PlayerNum.HasValue;
	}

	public void ResetPlayerNum()
	{
		__pbn__PlayerNum = null;
	}

	public bool ShouldSerializeLastScoreTime()
	{
		return __pbn__LastScoreTime.HasValue;
	}

	public void ResetLastScoreTime()
	{
		__pbn__LastScoreTime = null;
	}
}
